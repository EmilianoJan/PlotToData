
''' <summary>
''' Corresponde a un grafico bidimensional
''' </summary>
Public Class Plot2DComponent

	Public Property X_axis As ComponentAxis

	Public Property Y_axis As ComponentAxis

	Public Property Title As String

	Dim WithEvents conten As DrawData

	''' <summary>
	''' Inicialización del componente (requiere disponer de un panel sobre el
	''' cual se van cargando los puntos)
	''' </summary>
	''' <param name="Contenedor"></param>
	Sub New(Contenedor As DrawData)
		conten = Contenedor
		X_axis = New ComponentAxis(Contenedor)
		Y_axis = New ComponentAxis(Contenedor)

	End Sub

End Class
