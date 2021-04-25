
''' <summary>
''' Representa una serie de datos
''' </summary>
Public Class SerieComponet

	Public Property Serie_Name As String

	Public Property Points As List(Of PointComponent)


	Dim WithEvents Contenedor As Panel
	Sub New(Component As Panel)
		Contenedor = Component
	End Sub
End Class
