
''' <summary>
''' Represents a point on a two-dimensional plane.
''' </summary>
<Serializable> Public Class PointComponent

	Public Property X As Double
	Public Property Y As Double

	Public Property Node_Color As Color
		Get
			Return BackNodeColor
		End Get
		Set(value As Color)
			BackNodeColor = value
			Lab.BackColor = BackNodeColor
		End Set
	End Property
	Dim BackNodeColor As Color = Color.Black

	Public Event Click()
	Public Event PosChange()
	Public Event PointEditionFinished(e As MouseEventArgs)
	Public Event PointEditionFinished_Sender(e As MouseEventArgs, Sender As PointComponent)
	Public Event KeyPress(e As KeyEventArgs, Sender As PointComponent)

	Dim WithEvents Contenedor As DrawData

	Dim WithEvents Lab As Label

	Dim WithEvents Pane As PictureBox

	Dim Moviendo As Boolean = False

	Public SerieIndex As Integer

	Sub New(Conten As DrawData)
		Contenedor = Conten
		AddHandler Conten.Conteiner.Paint, AddressOf MyPanel_Paint
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
		Conten.Conteiner.Controls.Add(Lab)
		Lab.BringToFront()
		Moviendo = False
		Pane = Conten.Conteiner
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

	Private Sub Contenedor_CambioEscala() Handles Contenedor.ScaleChange
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

			If Moviendo = True Then
				With Lab
					X = (e.X + .Left) / Contenedor.Escala
					Y = (e.Y + .Top) / Contenedor.Escala
					.Top = e.Y + .Top  '+ .Height / 2
					.Left = e.X + .Left   '+ .Width / 2
				End With
				Pane.Refresh()
			End If
		ElseIf e.Button = MouseButtons.Middle Then
			If Moviendo = True Then
				Contenedor.MoveDragAcction(New PointF(e.X, e.Y))
			End If

		End If
	End Sub

	Private Sub Lab_MouseLeave(sender As Object, e As EventArgs) Handles Lab.MouseLeave
		'If  = MouseButtons.None Then
		Lab.BackColor = BackNodeColor
		'End If
	End Sub

	Private Sub Lab_MouseDown(sender As Object, e As MouseEventArgs) Handles Lab.MouseDown
		'If e.Button = MouseButtons.Left Then
		'	Moviendo = True
		'End If
		If (e.Button = MouseButtons.Middle) And (Moviendo = True) Then
			Contenedor.NodeStartEdition(New PointF(e.X, e.Y))
		End If
	End Sub

	Private Sub Lab_MouseUp(sender As Object, e As MouseEventArgs) Handles Lab.MouseUp
		'If e.Button = MouseButtons.Left Then
		'	Moviendo = False
		'End If
		If Moviendo = True Then
			If e.Button = MouseButtons.Middle Then
				Lab.Cursor = Cursors.Cross
			Else
				If omitirUpEnClick = True Then
					omitirUpEnClick = False
				Else
					Moviendo = False
					RaiseEvent PointEditionFinished(e)
					RaiseEvent PointEditionFinished_Sender(e, Me)
				End If

			End If
		End If
	End Sub

	Private Sub Pane_MouseMove(sender As Object, e As MouseEventArgs) Handles Pane.MouseMove

		If e.Button = MouseButtons.Middle Then
			If Moviendo = True Then
				Contenedor.MoveDragAcction(New PointF(e.X, e.Y))
			End If
		Else
			If Moviendo = True Then
				X = e.X / Contenedor.Escala
				Y = e.Y / Contenedor.Escala
				With Lab
					.Top = e.Y '+ .Height / 2
					.Left = e.X '+ .Width / 2
				End With
				Pane.Refresh()
			End If
		End If

	End Sub

	Private Sub Pane_MouseUp(sender As Object, e As MouseEventArgs) Handles Pane.MouseUp
		If e.Button = MouseButtons.Middle Then

		Else
			If Moviendo = True Then

				Moviendo = False
				RaiseEvent PointEditionFinished(e)
				RaiseEvent PointEditionFinished_Sender(e, Me)
			End If
		End If
	End Sub

	Dim omitirUpEnClick As Boolean
	Private Sub Lab_Click(sender As Object, e As EventArgs) Handles Lab.Click

		Moviendo = Not Moviendo

		If Moviendo = True Then
			omitirUpEnClick = True
			StartEdition()
		Else
			Contenedor.ChangeNodeVisibility(True)
			'Lab.Cursor = Cursors.Cross
			RaiseEvent PointEditionFinished(e)
			RaiseEvent PointEditionFinished_Sender(e, Me)
		End If
	End Sub

	''' <summary>
	''' Routine that starts the edition of the point
	''' </summary>
	Public Sub StartEdition()
		Moviendo = True
		Contenedor.ChangeNodeVisibility(False)
		Lab.Cursor = Cursors.Cross
		RaiseEvent Click()
	End Sub



	Private Sub Contenedor_VisibilidadNodos(Visible As Boolean) Handles Contenedor.NodeVisible
		If Moviendo = False Then
			Lab.Visible = Visible
		Else
			Lab.Visible = True
		End If
	End Sub

	''' <summary>
	''' Routine that stops the edition of the point
	''' </summary>
	Public Sub StopEdition()
		Moviendo = False
		Contenedor.ChangeNodeVisibility(True)
	End Sub
	''' <summary>
	''' Routine that eliminates the point
	''' </summary>
	Public Sub DeletePoint()
		RemoveHandler Contenedor.Conteiner.Paint, AddressOf MyPanel_Paint
		Pane = Nothing
		Lab.Dispose()
		Lab = Nothing
		Contenedor = Nothing
	End Sub


	Private Sub ResolverKeyDown(e As KeyEventArgs)
		If Moviendo = True Then
			Select Case e.KeyCode
				Case Keys.Escape
					StopEdition() ' detengo el movimiento
				Case Else
					RaiseEvent KeyPress(e, Me)
					'hay que eliminar el punto de la serie. 
			End Select
		End If
	End Sub

	Private Sub Contenedor_KeyPressEvent(e As KeyEventArgs) Handles Contenedor.KeyPressEvent
		ResolverKeyDown(e)
	End Sub
End Class
