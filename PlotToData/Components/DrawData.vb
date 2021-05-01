Namespace Plot
	''' <summary>
	''' Clase que contiene toda la información para el renderizado de los ejes 
	''' y demás coponentes
	''' </summary>
	Public Class DrawData

		Public WithEvents Conteiner As PictureBox

		Public Escala As Double

		Public Event ScaleChange()

		Public Event NodeVisible(Visible As Boolean)

		Public Event MoveEditionStart(pos As PointF)

		Public Event MoveEditionStart_Displacement(pos As PointF)

		Public Event KeyPressEvent(e As KeyEventArgs)

		Public Sub Refresh()
			RaiseEvent ScaleChange()
		End Sub

		Public Sub ChangeNodeVisibility(Visible As Boolean)
			RaiseEvent NodeVisible(Visible)
		End Sub

		Public Sub NodeStartEdition(Pos As PointF)
			RaiseEvent MoveEditionStart(Pos)
		End Sub

		Public Sub MoveDragAcction(Pos As PointF)
			RaiseEvent MoveEditionStart_Displacement(Pos)
		End Sub

		Public Sub KeyPressEve(e As KeyEventArgs)
			RaiseEvent KeyPressEvent(e)
		End Sub

	End Class
End Namespace
