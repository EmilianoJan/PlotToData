
''' <summary>
''' Representa una serie de datos
''' </summary>
Public Class SerieComponet

	Public Property Serie_Name As String

	Public Property Points As New List(Of PointComponent)


	Dim WithEvents Conten As DrawData

	Dim WithEvents UltimoPunto As PointComponent
	Dim UltimoPuntoIndi As Integer = 0
	Dim AgregarAlFinal As Boolean = True
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
		'If IsNothing(UltimoPunto) = False Then
		'	UltimoPunto.DetenerMovimiento()
		'End If
		Dim punto As New PointComponent(Conten)
		'AddHandler punto.EdicionPuntoTerminada, AddressOf EventosPuntos
		punto.IniciadoMOvimiento()
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
		AddHandler punto.EdicionPuntoTerminada_Completo, AddressOf TodosLosPuntos
		AddHandler punto.KeyPress, AddressOf PuntoKeyPress
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
			RemoveHandler UltimoPunto.EdicionPuntoTerminada_Completo, AddressOf TodosLosPuntos
			RemoveHandler UltimoPunto.KeyPress, AddressOf PuntoKeyPress
			UltimoPunto.DetenerMovimiento()
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

	Private Sub UltimoPunto_EdicionPuntoTerminada(e As MouseEventArgs) Handles UltimoPunto.EdicionPuntoTerminada
		If e.Button = MouseButtons.Left Then
			agregarPunto()
		ElseIf e.Button = MouseButtons.Right Then
			DetenerAgregado()
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

End Class
