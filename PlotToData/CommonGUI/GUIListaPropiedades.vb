Namespace Extras
    Public Class GUIListaPropiedades
        Dim listado As List(Of SerieComponet)
        Private Sub GUIListaPropiedades_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        End Sub

        Public Sub CargarLista(lista As List(Of SerieComponet))
            listado = lista
        End Sub

        Private Sub RegenerarLista()

            ListBox1.Items.Clear()
            For Each filtro In listado
                ListBox1.Items.Add(filtro.Serie_Name)
            Next
        End Sub

        Public Sub ActualizarLista()
            RegenerarLista()
        End Sub

        Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
            PropertyGrid1.SelectedObject = listado(ListBox1.SelectedIndex)

        End Sub

        Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
            Dim serie As SerieComponet = listado(ListBox1.SelectedIndex)
            serie.Dispose()
            listado.RemoveAt(ListBox1.SelectedIndex)
            RegenerarLista()
        End Sub
    End Class

    ''' <summary>
    ''' Permite condigurar un listado de objetos pariendo de un diccionario con key texto y Valor, objeto
    ''' </summary>
    Public Class GUIListadoSeleccionableDescripcion

        ''' <summary>
        ''' contiene todos los objetos de la interfaz grafica, junto con variables y clases declaradas.
        ''' </summary>
        Public InterfazGrafica As New GUIListaPropiedades

        Public Lista As List(Of SerieComponet)




        Public Sub InsertarSeEn(Contenedor As Control.ControlCollection)
            With InterfazGrafica
                .TopLevel = False
                .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
                .Dock = DockStyle.Fill
                Contenedor.Add(InterfazGrafica)
                .Show()
            End With
        End Sub

        Sub New()

        End Sub

        Sub New(Contenedor As Control.ControlCollection)
            InsertarSeEn(Contenedor)
        End Sub

        Sub New(lista As List(Of SerieComponet))
            Me.Lista = lista
        End Sub
        Public Sub CargarListado(lista As List(Of SerieComponet))
            Me.Lista = lista
            InterfazGrafica.CargarLista(lista)
            ActualizarListado()
        End Sub

        Sub New(Contenedor As Control.ControlCollection, lista As List(Of SerieComponet))
            InsertarSeEn(Contenedor)
            Me.Lista = lista
            InterfazGrafica.CargarLista(lista)
        End Sub

        Public Sub ActualizarListado()
            InterfazGrafica.CargarLista(Lista)
            InterfazGrafica.ActualizarLista()
        End Sub
    End Class
End Namespace
