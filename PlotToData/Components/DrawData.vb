
''' <summary>
''' Clase que contiene toda la información para el renderizado de los ejes 
''' y demás coponentes
''' </summary>
Public Class DrawData

	Public WithEvents Contenedor As PictureBox

	Public Escala As Double

	Public Event CambioEscala()

	Public Sub Actualizar()
		RaiseEvent CambioEscala()
	End Sub

End Class
