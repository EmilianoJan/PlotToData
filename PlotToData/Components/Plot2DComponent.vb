Namespace Plot
	''' <summary>
	''' Corresponde a un grafico bidimensional
	''' </summary>
	<System.ComponentModel.Category("Figure")>
	<System.ComponentModel.Description("Configurations of all the elements that make up the chart")>
	<Serializable> Public Class Plot2DComponent
		Implements NameInterface

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("Contains the X-axis configurations of the figure")>
		<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
		Public Property X_axis As ComponentAxis

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("Contains the Y-axis configurations of the figure")>
		<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
		Public Property Y_axis As ComponentAxis

		<System.ComponentModel.Category("Plot")>
		<System.ComponentModel.Description("Contains the name of the figure")>
		Public Property Title As String = "Plot" Implements NameInterface.Name

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("It has a list of data series.")>
		Public Property Series As New List(Of SerieComponet)

		Public Corected_Series As New List(Of DataSeries)

		Dim WithEvents conten As DrawData

		Public Event Click_on_Serie(sender As SerieComponet)

		Public Event Click_on_axis(sender As ComponentAxis)

		Dim AutoIncrementalSerialIndex As Integer = 0

		Public Enum TypesCode
			Matlab
			Python
		End Enum

		Public Enum SaveFormatOptions
			mat
			csv
			NIST
		End Enum

		''' <summary>
		''' Inicialización del componente (requiere disponer de un panel sobre el
		''' cual se van cargando los puntos)
		''' </summary>
		''' <param name="Contenedor"></param>
		Sub New(Contenedor As DrawData)
			conten = Contenedor
			X_axis = New ComponentAxis(Contenedor)
			Y_axis = New ComponentAxis(Contenedor)
			AddHandler X_axis.Click, AddressOf click_on_AxisFun
			AddHandler Y_axis.Click, AddressOf click_on_AxisFun
			X_axis.Name = "X Axis"
			Y_axis.Name = "Y Axis"
		End Sub

		''' <summary>
		''' Add a new data series, automatically name it, and set it to start point-add edit mode
		''' </summary>
		Public Sub AddSeriesAndEdit()
			Dim seri As New SerieComponet(conten, AutoIncrementalSerialIndex)
			AutoIncrementalSerialIndex = AutoIncrementalSerialIndex + 1
			Series.Add(seri)
			seri.StrartPointAggregation()
			AddHandler seri.Click, AddressOf Click_on_SerieFun
			seri.Serie_Name = "Serie " & Series.Count.ToString
		End Sub

		Private Sub Click_on_SerieFun(sender As SerieComponet)
			RaiseEvent Click_on_Serie(sender)
		End Sub

		Private Sub click_on_AxisFun(sender As ComponentAxis)
			RaiseEvent Click_on_axis(sender)
		End Sub

		''' <summary>
		''' Function that returns the code that generates a figure in different languages
		''' </summary>
		''' <param name="Code"></param>
		''' <returns></returns>
		Public Function GenerateCode(Code As TypesCode) As String
			RefreshValues()
			Dim salida As String = ""
			Select Case Code
				Case TypesCode.Matlab
					salida = MatlabCode()
				Case TypesCode.Python

			End Select
			Return salida
		End Function

		''' <summary>
		''' Function that saves the data of the figure. Autodetects the format to save from the file suffix
		''' </summary>
		''' <param name="Dir"></param>
		Public Sub Save(Dir As String)
			RefreshValues()
			Dim Format As SaveFormatOptions
			Select Case Strings.LCase(Strings.Right(Dir, 3))
				Case "mat"
					Format = SaveFormatOptions.mat
				Case "csv"
					Format = SaveFormatOptions.csv
				Case "mtx"
					Format = SaveFormatOptions.NIST
			End Select

			Select Case Format
				Case SaveFormatOptions.mat
					Dim ListaMatrices As New Dictionary(Of String, MathNet.Numerics.LinearAlgebra.Matrix(Of Double))
					For Each seri In Corected_Series
						Dim tabla(,) As Double
						seri.ConvertirDatosAMatrizDouble(tabla)
						Dim B = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(tabla)
						ListaMatrices.Add(MatlabSeriesName(seri.NombreY), B)
					Next
					MathNet.Numerics.Data.Matlab.MatlabWriter.Write(Dir, ListaMatrices)
				Case SaveFormatOptions.csv
					'generamos un archivo por serie.
					Dim dirb As String = Strings.Mid(Dir, 1, Strings.Len(Dir) - 3)
					For Each seri In Corected_Series
						Dim tabla(,) As Double
						seri.ConvertirDatosAMatrizDouble(tabla)
						Dim B = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(tabla)
						MathNet.Numerics.Data.Text.DelimitedWriter.Write(dirb & " " & seri.NombreY & ".csv", B, ",")
					Next
				Case SaveFormatOptions.NIST
					'generamos un archivo por serie.
					Dim dirb As String = Strings.Mid(Dir, 1, Strings.Len(Dir) - 3)
					For Each seri In Corected_Series
						Dim tabla(,) As Double
						seri.ConvertirDatosAMatrizDouble(tabla)
						Dim B = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(tabla)
						MathNet.Numerics.Data.Text.MatrixMarketWriter.WriteMatrix(dirb & " " & seri.NombreY & ".mtx", B)
					Next
			End Select


		End Sub

		Private Sub RefreshValues()
			Corected_Series.Clear()
			For Each seri In Series
				Dim ser As New DataSeries
				ser.NombreX = X_axis.Name
				ser.NombreY = seri.Serie_Name
				Corected_Series.Add(ser)
				Dim xby, xbx, xax, xay As Double 'para proyectar en el eje X
				Dim yby, ybx, yax, yay As Double 'para proyecar en el eje y

				Dim Xpos As Double
				Dim Ypos As Double

				xbx = X_axis.End_Pont.X - X_axis.Start_Point.X
				xby = X_axis.End_Pont.Y - X_axis.Start_Point.Y

				ybx = Y_axis.End_Pont.X - Y_axis.Start_Point.X
				yby = Y_axis.End_Pont.Y - Y_axis.Start_Point.Y

				For Each punto In seri.Points
					'calculo la proyección
					xax = punto.X - X_axis.Start_Point.X
					xay = punto.Y - X_axis.Start_Point.Y

					yax = punto.X - Y_axis.Start_Point.X
					yay = punto.Y - Y_axis.Start_Point.Y

					Xpos = (xax * xbx + xay * xby) / (Math.Sqrt(xbx ^ 2 + xby ^ 2))
					Ypos = (yax * ybx + yay * yby) / (Math.Sqrt(ybx ^ 2 + yby ^ 2))

					'calculamos las escalas que deben tener
					If X_axis.Scale = ComponentAxis.TypesOfScales.Lineal Then
						Xpos = (Xpos * (X_axis.End_Value - X_axis.Start_Value)) / Math.Sqrt(xbx ^ 2 + xby ^ 2) + X_axis.Start_Value
					Else
						Xpos = (Xpos * (Math.Log(X_axis.End_Value) - Math.Log(X_axis.Start_Value))) / Math.Sqrt(xbx ^ 2 + xby ^ 2) + Math.Log(X_axis.Start_Value)
						Xpos = Math.Exp(Xpos)
					End If

					If Y_axis.Scale = ComponentAxis.TypesOfScales.Lineal Then
						Ypos = (Ypos * (Y_axis.End_Value - Y_axis.Start_Value)) / Math.Sqrt(ybx ^ 2 + yby ^ 2) + Y_axis.Start_Value
					Else
						Ypos = (Ypos * (Math.Log(Y_axis.End_Value) - Math.Log(Y_axis.Start_Value))) / Math.Sqrt(ybx ^ 2 + yby ^ 2) + Math.Log(Y_axis.Start_Value)
						Ypos = Math.Exp(Ypos)
					End If
					ser.AgregarPunto(Xpos, Ypos)
				Next
			Next



		End Sub


		Private Function MatlabCode() As String
			Dim sal As String = "% Loading series" & vbCrLf

			For Each serie In Corected_Series
				Dim sname As String = MatlabSeriesName(serie.NombreY)
				sal = sal & sname & " = [ "
				For Each ele In serie.Datos
					sal = sal & ele.X.ToString & " " & ele.Y.ToString & "; "
				Next
				sal = Strings.Mid(sal, 1, sal.Length - 1)
				sal = sal & "]; " & vbCrLf
			Next

			sal = sal & "figure % Generate plot" & vbCrLf
			Dim primera As Boolean = True
			For Each serie In Corected_Series

				Dim sname As String = MatlabSeriesName(serie.NombreY)

				If X_axis.Scale = ComponentAxis.TypesOfScales.Lineal Then
					If Y_axis.Scale = ComponentAxis.TypesOfScales.Lineal Then
						sal = sal & "plot( " & sname & "(:, 1), " & sname & "(:, 2))" & vbCrLf
					Else
						sal = sal & "semilogy( " & sname & "(:, 1), " & sname & "(:, 2))" & vbCrLf
					End If
				Else
					If Y_axis.Scale = ComponentAxis.TypesOfScales.Lineal Then
						sal = sal & "semilogx( " & sname & "(:, 1), " & sname & "(:, 2))" & vbCrLf
					Else
						sal = sal & "loglog( " & sname & "(:, 1), " & sname & "(:, 2))" & vbCrLf
					End If
				End If
				If primera = True Then
					primera = False
					sal = sal & "hold on" & vbCrLf
				End If
			Next
			sal = sal & "xlabel('" & X_axis.Name & "')" & vbCrLf
			sal = sal & "title('" & Title & "')" & vbCrLf
			sal = sal & "grid on" & vbCrLf
			sal = sal & "grid minor" & vbCrLf

			sal = sal & "legend("
			'', '
			For Each serie In Corected_Series
				sal = sal & "'" & serie.NombreY & "' ,"
			Next
			sal = Strings.Mid(sal, 1, sal.Length - 1)
			sal = sal & ") " & vbCrLf

			Return sal
		End Function

		Private Function MatlabSeriesName(Name As String) As String
			Dim sal As String
			sal = Strings.Replace(Name, " ", "_")
			Return sal
		End Function

	End Class
End Namespace
