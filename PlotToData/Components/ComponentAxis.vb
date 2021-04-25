
''' <summary>
''' Corresponde a un eje de un grafico bidimensional
''' </summary>
Public Class ComponentAxis

	Public Enum TypesOfScales
		Lineal
		Logaritmic
	End Enum

	Public Property Scale As TypesOfScales = TypesOfScales.Lineal

	Public Property Name As String

	Public Property Start_Value As Double

	Public Property End_Value As Double

	Public Property Start_Point As PointComponent

	Public Property End_Pont As PointComponent


	Dim WithEvents Conten As DrawData


	Sub New(Contenedor As DrawData)
		Conten = Contenedor
		Start_Point = New PointComponent(Conten)
		End_Pont = New PointComponent(Conten)
		AddHandler Conten.Contenedor.Paint, AddressOf MyPanel_Paint
	End Sub

	Private Sub Conten_CambioEscala() Handles Conten.CambioEscala

	End Sub

	Private Sub MyPanel_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
		'e.Graphics.DrawLine(Pens.Black, 20, 65, 20, 65)

		Dim x1, x2, y1, y2 As Single
		x1 = Start_Point.X
		y1 = Start_Point.Y
		x2 = End_Pont.X
		y2 = End_Pont.Y

		e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2)
	End Sub


End Class
