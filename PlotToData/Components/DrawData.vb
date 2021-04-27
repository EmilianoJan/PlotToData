
''' <summary>
''' Clase que contiene toda la información para el renderizado de los ejes 
''' y demás coponentes
''' </summary>
Public Class DrawData

	Public WithEvents Contenedor As PictureBox

	Public Escala As Double

	Public Event CambioEscala()

	Public Event VisibilidadNodos(Visible As Boolean)

	Public Event MoverArrastrar_Inicio(pos As PointF)

	Public Event MoverArrastrar_Desplazamiento(pos As PointF)

	Public Event KeyPressEvent(e As KeyEventArgs)

	Public Sub Actualizar()
		RaiseEvent CambioEscala()
	End Sub

	Public Sub CambiarVisibilidadNodos(Visible As Boolean)
		RaiseEvent VisibilidadNodos(Visible)
	End Sub

	Public Sub MoveArrastrar(Pos As PointF)
		RaiseEvent MoverArrastrar_Inicio(Pos)
	End Sub

	Public Sub MoverArrastrarAccion(Pos As PointF)
		RaiseEvent MoverArrastrar_Desplazamiento(Pos)
	End Sub

	Public Sub KeyPressEve(e As KeyEventArgs)
		RaiseEvent KeyPressEvent(e)
	End Sub

End Class
