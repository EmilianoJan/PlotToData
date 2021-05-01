Namespace Plot
	''' <summary>
	''' Represents a data series 
	''' </summary>
	<System.ComponentModel.Category("Series")>
	<System.ComponentModel.Description("Data series. Manage and configure the properties of all points.")>
	<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
	<Serializable> Public Class SerieComponet
		Implements NameInterface
		Implements IDisposable

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("Contains the name of the data series. It is used to generate the graphics, legends.")>
		Public Property Serie_Name As String Implements NameInterface.Name

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("data series points")>
		<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>
		Public Property Points As New List(Of PointComponent)

		Public Serie_Index As Integer

		Dim WithEvents Conten As DrawData

		Dim WithEvents UltimoPunto As PointComponent
		Dim UltimoPuntoIndi As Integer = 0
		Dim AgregarAlFinal As Boolean = True
		Private disposedValue As Boolean

		Public Event Click(sender As SerieComponet)
		Public Event Deleting_Series(sender As SerieComponet)
		Sub New(Component As DrawData, SerieIndex As Integer)
			Conten = Component
			Serie_Index = SerieIndex
			AddHandler Conten.Conteiner.Paint, AddressOf MyPanel_Paint
		End Sub

		Public Sub StrartPointAggregation()
			agregarPunto()
		End Sub

		Public Sub StopPointAggregation()

		End Sub

		Private Sub agregarPunto()
			'If IsNothing(UltimoPunto) = False Then
			'	UltimoPunto.DetenerMovimiento()
			'End If
			Dim punto As New PointComponent(Conten)
			'AddHandler punto.EdicionPuntoTerminada, AddressOf EventosPuntos
			punto.StartEdition()
			If AgregarAlFinal = True Then
				Points.Add(punto)
				UltimoPuntoIndi = Points.Count - 1
			Else
				Points.Insert(0, punto)
				UltimoPuntoIndi = 0
				ActualizarIndicesPuntos()
			End If

			punto.SerieIndex = UltimoPuntoIndi
			UltimoPunto = punto
			AddHandler punto.PointEditionFinished_Sender, AddressOf TodosLosPuntos
			AddHandler punto.KeyPress, AddressOf PuntoKeyPress
			AddHandler punto.Click, AddressOf ClickInNodo
		End Sub

		Private Sub ActualizarIndicesPuntos()
			Dim indi As Integer = 0
			For Each punto In Points
				punto.SerieIndex = indi
				indi = indi + 1
			Next
		End Sub

		Private Sub DetenerAgregado()
			If IsNothing(UltimoPunto) = False Then
				RemoveHandler UltimoPunto.PointEditionFinished_Sender, AddressOf TodosLosPuntos
				RemoveHandler UltimoPunto.KeyPress, AddressOf PuntoKeyPress
				RemoveHandler UltimoPunto.Click, AddressOf ClickInNodo
				UltimoPunto.StopEdition()
				Points.RemoveAt(UltimoPuntoIndi)
				UltimoPunto.DeletePoint()
				UltimoPuntoIndi = 0
				UltimoPunto = Nothing
			End If
		End Sub

		Private Sub MyPanel_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
			'e.Graphics.DrawLine(Pens.Black, 20, 65, 20, 65)

			If Points.Count > 1 Then
				Dim x1, x2, y1, y2 As Single
				Dim i As Integer
				x1 = Points(0).X * Conten.Escala
				y1 = Points(0).Y * Conten.Escala
				For i = 1 To Points.Count - 1
					x2 = Points(i).X * Conten.Escala
					y2 = Points(i).Y * Conten.Escala
					'x2 = End_Pont.X * Conten.Escala
					'y2 = End_Pont.Y * Conten.Escala
					e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2)
					x1 = x2
					y1 = y2
				Next
			End If
		End Sub

		Private Sub UltimoPunto_EdicionPuntoTerminada(e As MouseEventArgs) Handles UltimoPunto.PointEditionFinished
			If e.Button = MouseButtons.Left Then
				agregarPunto()
			ElseIf e.Button = MouseButtons.Right Then
				DetenerAgregado()
				ActualizarIndicesPuntos()
			End If
		End Sub

		Private Sub TodosLosPuntos(e As MouseEventArgs, Punto As PointComponent)
			If IsNothing(UltimoPunto) = True Then
				'significa que podríamos agregar elementos.
				If Punto.SerieIndex = Points.Count - 1 Then ' verificamos que sea el último
					AgregarAlFinal = True
					agregarPunto()
				End If
				If Punto.SerieIndex = 0 Then
					AgregarAlFinal = False
					agregarPunto()
				End If
			End If
		End Sub


		Private Sub PuntoKeyPress(e As KeyEventArgs, Sender As PointComponent)
			Select Case e.KeyCode
				Case Keys.Delete
					UltimoPunto = Sender
					UltimoPuntoIndi = Sender.SerieIndex
					DetenerAgregado()
					ActualizarIndicesPuntos()
			End Select
		End Sub

		Private Sub ClickInNodo()
			RaiseEvent Click(Me)
		End Sub

		Protected Overridable Sub Dispose(disposing As Boolean)
			If Not disposedValue Then
				If disposing Then
					RaiseEvent Deleting_Series(Me)
					' TODO: dispose managed state (managed objects)
					For Each punto In Points
						RemoveHandler punto.PointEditionFinished_Sender, AddressOf TodosLosPuntos
						RemoveHandler punto.KeyPress, AddressOf PuntoKeyPress
						RemoveHandler punto.Click, AddressOf ClickInNodo
						punto.DeletePoint()
					Next
					Points.Clear()
				End If

				' TODO: free unmanaged resources (unmanaged objects) and override finalizer
				' TODO: set large fields to null
				disposedValue = True
			End If
		End Sub

		' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
		' Protected Overrides Sub Finalize()
		'     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
		'     Dispose(disposing:=False)
		'     MyBase.Finalize()
		' End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
			Dispose(disposing:=True)
			GC.SuppressFinalize(Me)
		End Sub
	End Class
End Namespace
