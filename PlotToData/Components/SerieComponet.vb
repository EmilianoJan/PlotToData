
''' <summary>
''' Representa una serie de datos
''' </summary>
Public Class SerieComponet

	Public Property Serie_Name As String

	Public Property Points As New List(Of PointComponent)


	Dim WithEvents Conten As DrawData

	Dim WithEvents UltimoPunto As PointComponent
	Sub New(Component As DrawData)
		Conten = Component
		AddHandler Conten.Contenedor.Paint, AddressOf MyPanel_Paint
	End Sub

	Public Sub IniciarAgregadoPuntos()
		agregarPunto()
	End Sub

	Public Sub DetenerAgregadoPuntos()

	End Sub

	Private Sub agregarPunto()
		If IsNothing(UltimoPunto) = False Then
			UltimoPunto.DetenerMovimiento()
		End If
		Dim punto As New PointComponent(Conten)
		'AddHandler punto.EdicionPuntoTerminada, AddressOf EventosPuntos
		punto.IniciadoMOvimiento()
		Points.Add(punto)
		UltimoPunto = punto
	End Sub

	Private Sub DetenerAgregado()
		If IsNothing(UltimoPunto) = False Then
			UltimoPunto.DetenerMovimiento()
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

	Private Sub UltimoPunto_EdicionPuntoTerminada(e As MouseEventArgs) Handles UltimoPunto.EdicionPuntoTerminada
		If e.Button = MouseButtons.Left Then
			agregarPunto()
		ElseIf e.Button = MouseButtons.Right Then
			DetenerAgregado()
		End If
	End Sub
End Class
