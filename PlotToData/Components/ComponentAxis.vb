
Namespace Plot


	''' <summary>
	''' Corresponds to an axis of a two-dimensional graph
	''' </summary>
	Public Class ComponentAxis
		Implements NameInterface
		Public Enum TypesOfScales
			Lineal
			Logaritmic
		End Enum

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("Sets the type of scale that will be applied to the axis.")>
		Public Property Scale As TypesOfScales = TypesOfScales.Lineal

		<System.ComponentModel.Category("Series")>
		<System.ComponentModel.Description("Sets the series name")>
		Public Property Name As String Implements NameInterface.Name

		<System.ComponentModel.Category("Values")>
		<System.ComponentModel.Description("Initial value of the axis. The point must be located in such a way that it is clear to establish the value from the image.")>
		Public Property Start_Value As Double

		<System.ComponentModel.Category("Values")>
		<System.ComponentModel.Description("Final value of the axis. The point must be located in such a way that it is clear to establish the value from the image.")>
		Public Property End_Value As Double

		Public WithEvents Start_Point As PointComponent
		Public WithEvents End_Pont As PointComponent

		Dim WithEvents Conten As DrawData

		Public Event AxisEditionFinished(sender As ComponentAxis)
		Public Event Click(sender As ComponentAxis)


		Sub New(Contenedor As DrawData)
			Conten = Contenedor
			Start_Point = New PointComponent(Conten)
			End_Pont = New PointComponent(Conten)
			End_Pont.Node_Color = Color.FromArgb(0, 0, 250)
			Start_Point.Node_Color = Color.FromArgb(0, 0, 50)
			AddHandler Conten.Conteiner.Paint, AddressOf MyPanel_Paint
		End Sub

		Private Sub Conten_CambioEscala() Handles Conten.ScaleChange

		End Sub

		Private Sub MyPanel_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
			'e.Graphics.DrawLine(Pens.Black, 20, 65, 20, 65)

			Dim x1, x2, y1, y2 As Single
			x1 = Start_Point.X * Conten.Escala
			y1 = Start_Point.Y * Conten.Escala
			x2 = End_Pont.X * Conten.Escala
			y2 = End_Pont.Y * Conten.Escala

			e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2)

		End Sub

		Private Sub Start_Point_EdicionPuntoTerminada(e As MouseEventArgs) Handles Start_Point.PointEditionFinished
			RaiseEvent AxisEditionFinished(Me)
		End Sub

		Private Sub End_Pont_EdicionPuntoTerminada(e As MouseEventArgs) Handles End_Pont.PointEditionFinished
			RaiseEvent AxisEditionFinished(Me)
		End Sub

		Private Sub Start_Point_Click() Handles Start_Point.Click
			RaiseEvent Click(Me)
		End Sub

		Private Sub End_Pont_Click() Handles End_Pont.Click
			RaiseEvent Click(Me)
		End Sub
	End Class
End Namespace
