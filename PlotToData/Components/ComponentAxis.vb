
''' <summary>
''' Corresponde a un eje de un grafico bidimensional
''' </summary>
Public Class ComponentAxis
	Implements NameInterface
	Public Enum TypesOfScales
		Lineal
		Logaritmic
	End Enum

	Public Property Scale As TypesOfScales = TypesOfScales.Lineal

	Public Property Name As String Implements NameInterface.Name

	Public Property Start_Value As Double

	Public Property End_Value As Double

	Public WithEvents Start_Point As PointComponent

	Public WithEvents End_Pont As PointComponent

	Dim WithEvents Conten As DrawData

	Public Event EdicionEjeTerminada(sender As ComponentAxis)
	Public Event Click(sender As ComponentAxis)


	Sub New(Contenedor As DrawData)
		Conten = Contenedor
		Start_Point = New PointComponent(Conten)
		End_Pont = New PointComponent(Conten)
		End_Pont.Node_Color = Color.FromArgb(0, 0, 250)
		Start_Point.Node_Color = Color.FromArgb(0, 0, 50)
		AddHandler Conten.Contenedor.Paint, AddressOf MyPanel_Paint
	End Sub

	Private Sub Conten_CambioEscala() Handles Conten.CambioEscala

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

	Private Sub Start_Point_EdicionPuntoTerminada(e As MouseEventArgs) Handles Start_Point.EdicionPuntoTerminada
		RaiseEvent EdicionEjeTerminada(Me)
	End Sub

	Private Sub End_Pont_EdicionPuntoTerminada(e As MouseEventArgs) Handles End_Pont.EdicionPuntoTerminada
		RaiseEvent EdicionEjeTerminada(Me)
	End Sub

	Private Sub Start_Point_Click() Handles Start_Point.Click
		RaiseEvent Click(Me)
	End Sub

	Private Sub End_Pont_Click() Handles End_Pont.Click
		RaiseEvent Click(Me)
	End Sub
End Class
