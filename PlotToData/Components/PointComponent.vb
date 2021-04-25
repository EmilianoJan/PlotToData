
''' <summary>
''' Representa un punto en un plano bidimensional.
''' </summary>
Public Class PointComponent

	Public Property X As Double
	Public Property Y As Double


	Public Event Click()
	Public Event PosChange()

	Dim WithEvents Contenedor As DrawData

	Dim WithEvents Lab As Label

	Dim WithEvents Pane As PictureBox

	Dim Moviendo As Boolean
	Sub New(Conten As DrawData)
		Contenedor = Conten
		AddHandler Conten.Contenedor.Paint, AddressOf MyPanel_Paint
		Lab = New Label
		With Lab
			.Height = 10
			.Width = 10
			.Top = X + .Height / 2
			.Left = Y + .Width / 2
			.Text = ""
			.Visible = True
			.BackColor = Color.Black

		End With
		Conten.Contenedor.Controls.Add(Lab)
		Lab.BringToFront()
		Moviendo = False
		Pane = Conten.Contenedor
	End Sub



	Private Sub MyPanel_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
		'e.Graphics.DrawLine(Pens.Black, 20, 65, 20, 65)
		With Lab
			'.Top = CInt(Math.Round(Y * Contenedor.Escala + .Height / 2))
			'.Left = CInt(Math.Round(X * Contenedor.Escala + .Width / 2))

			.Top = Math.Round(Y * Contenedor.Escala) '+ .Height / 2
			.Left = Math.Round(X * Contenedor.Escala) '+ .Width / 2
		End With

	End Sub

	Private Sub Contenedor_CambioEscala() Handles Contenedor.CambioEscala
		'X = X * Contenedor.Escala
		'Y = Y * Contenedor.Escala
		With Lab
			'.Top = CInt(Math.Round(Y * Contenedor.Escala + .Height / 2))
			'.Left = CInt(Math.Round(X * Contenedor.Escala + .Width / 2))
			'.Top = Y * Contenedor.Escala + .Height / 2
			'.Left = X * Contenedor.Escala + .Width / 2
		End With
	End Sub

	Private Sub Lab_MouseMove(sender As Object, e As MouseEventArgs) Handles Lab.MouseMove
		If e.Button = MouseButtons.None Then
			Lab.BackColor = Color.Red
		End If
		If Moviendo = True Then
			With Lab
				X = (e.X + .Left) / Contenedor.Escala
				Y = (e.Y + .Top) / Contenedor.Escala
				.Top = e.Y + .Top  '+ .Height / 2
				.Left = e.X + .Left   '+ .Width / 2
			End With
		End If
	End Sub

	Private Sub Lab_MouseLeave(sender As Object, e As EventArgs) Handles Lab.MouseLeave
		'If  = MouseButtons.None Then
		Lab.BackColor = Color.Black
		'End If
	End Sub

	Private Sub Lab_MouseDown(sender As Object, e As MouseEventArgs) Handles Lab.MouseDown
		'If e.Button = MouseButtons.Left Then
		'	Moviendo = True
		'End If
	End Sub

	Private Sub Lab_MouseUp(sender As Object, e As MouseEventArgs) Handles Lab.MouseUp
		'If e.Button = MouseButtons.Left Then
		'	Moviendo = False
		'End If
	End Sub

	Private Sub Pane_MouseMove(sender As Object, e As MouseEventArgs) Handles Pane.MouseMove
		If Moviendo = True Then
			X = e.X / Contenedor.Escala
			Y = e.Y / Contenedor.Escala
			With Lab
				.Top = e.Y '+ .Height / 2
				.Left = e.X '+ .Width / 2
			End With
		End If
	End Sub

	Private Sub Pane_MouseUp(sender As Object, e As MouseEventArgs) Handles Pane.MouseUp
		Moviendo = False
	End Sub

	Private Sub Lab_Click(sender As Object, e As EventArgs) Handles Lab.Click
		Moviendo = Not Moviendo
	End Sub
End Class
