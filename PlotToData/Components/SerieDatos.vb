Imports System.ComponentModel

''' <summary>
''' Objeto que representa una serie de datos X,Y
''' </summary>
<Serializable()> Public Class DataSeries

    ''' <summary>
    ''' Estructura que modela una dupla de valores 
    ''' </summary>
    <Serializable()> Public Structure DuplaDeValores
        ''' <summary>
        ''' Valor X
        ''' </summary>
        Dim X As Single

        ''' <summary>
        ''' Valor Y
        ''' </summary>
        Dim Y As Single
    End Structure

    ''' <summary>
    ''' Contiene un listado de valores (X,Y)
    ''' </summary>
    Public Datos As New List(Of DuplaDeValores)

    ''' <summary>
    ''' Indica el nombre del eje X
    ''' </summary>
    Public NombreX As String

    ''' <summary>
    ''' Indica el nombre del eje Y
    ''' </summary>
    Public NombreY As String



    ''' <summary>
    ''' Esta función convierte la serie de datos en una tabla double con dos columnas y n filas (la cantidad de filas
    ''' corresponde con la cantidad de elementos almacenados en la serie de datos.
    ''' </summary>
    ''' <param name="Tabla"> Tabla que será redimensionalizada para contener todos los elementos de la serie. Los datos anteriores se eliminan. El formato es [x, y] </param>
    Public Sub ConvertirDatosAMatrizDouble(ByRef Tabla(,) As Double)
        Dim max As Integer
        max = Datos.Count - 1
        ReDim Tabla(max, 1)


        Dim Valor As DuplaDeValores
        Dim indice As Integer
        indice = 0
        For Each Valor In Datos
            Tabla(indice, 0) = Valor.X
            Tabla(indice, 1) = Valor.Y
            indice = indice + 1
        Next
    End Sub



    ''' <summary>
    ''' Agrega al final de la lista de DATOS un elemento (permite agilizar la carga de datos)
    ''' </summary>
    ''' <param name="X">Componente X</param>
    ''' <param name="Y">Componente Y</param>
    ''' <remarks></remarks>
    Public Sub AgregarPunto(X As Double, Y As Double)
        Dim ACargar As DuplaDeValores
        With ACargar
            .X = X
            .Y = Y
        End With
        Datos.Add(ACargar)
    End Sub

    ''' <summary>
    ''' Agrega al final de la lista de DATOS los vectores pasados como parámetro
    ''' </summary>
    ''' <param name="X">Vector de elementos X</param>
    ''' <param name="Y">Vector de elementos Y</param>
    ''' <remarks></remarks>
    Public Sub AgregarPuntos(X() As Double, Y() As Double)
        Dim TamTabla As Integer = UBound(X)
        For e = 0 To TamTabla
            Dim ACargar As DuplaDeValores
            With ACargar
                .X = X(e)
                .Y = Y(e)
            End With
            Datos.Add(ACargar)
        Next
    End Sub

    ''' <summary>
    ''' Representa los datos de la serie en un objeto DataGridView borrando el contenido previo.
    ''' </summary>
    ''' <param name="Tabla"></param>
    Public Sub RepresentarEn(Tabla As DataGridView)
        Tabla.Columns.Clear()
        With Tabla
            .Columns.Add("p1", NombreX)
            .Columns.Add("p2", NombreY)

            Dim e As Integer
            Dim fila(1) As String
            For e = 0 To Datos.Count - 1
                fila(0) = Datos(e).X.ToString
                fila(1) = Datos(e).Y.ToString
                .Rows.Add(fila)
            Next
        End With
    End Sub

    ''' <summary>
    ''' rutina que reordena los elementos de la serie de forma x ascendente.
    ''' </summary>
    Public Sub OrdenarPorXCreciente()
        Datos.Sort(Function(x, y) x.X.CompareTo(y.X))
    End Sub

End Class


''' <summary>
''' Objeto que representa una serie de datos complejos orientado a las mediciones realizadas con el lockin
''' </summary>
<Serializable()> Public Class ComplexDataSeries

    ''' <summary>
    ''' Estructura de datos que representa señales principalmente fototérmicas
    ''' </summary>
    <Serializable()> Public Structure MedicionAdquiridaComplejaEstructura

        ''' <summary>
        ''' Frecuencia a la cual se obtuvo la amplitud y retardo de fase
        ''' </summary>
        Dim Frecuencia As Double

        ''' <summary>
        ''' Componente de amplitud de la medición adquirida
        ''' </summary>
        Dim Amplitud As Double

        ''' <summary>
        ''' Componente de retardo de fase de la medición adquirida.
        ''' </summary>
        Dim RetardoDeFase As Double
    End Structure
    Property Datos As New List(Of MedicionAdquiridaComplejaEstructura)

    ''' <summary>
    ''' Nombre de la serie.
    ''' </summary>
    Public NombreSerie As String



    ''' <summary>
    ''' Convierte las series cargadas a codigo Octave
    ''' </summary>
    ''' <param name="NombreDeSeries"> Nombre de serie de datos</param>
    ''' <returns>Devuelve código m con los valores de la serie.</returns>
    Public Function aTexto(Optional NombreDeSeries As String = "") As String
        Dim nomb As String
        If NombreDeSeries = "" Then
            nomb = NombreSerie
        Else
            nomb = NombreDeSeries
        End If

        Dim Frec As String
        Dim Amp As String
        Dim Fas As String
        Dim avar As String

        Dim i As Integer
        Frec = nomb & "_Frecuencia = [ "
        Amp = nomb & "_Amplitud = [ "
        Fas = nomb & "_RetardoFase = [ "
        For i = 0 To Datos.Count - 1
            Frec = Frec & Datos(i).Frecuencia.ToString & " "
            Amp = Amp & Datos(i).Amplitud.ToString & " "
            Fas = Fas & Datos(i).RetardoDeFase.ToString & " "
        Next

        Frec = Frec & "];"
        Amp = Amp & "];"
        Fas = Fas & "];"
        avar = vbCrLf & Frec & vbCrLf & Amp & vbCrLf & Fas & vbCrLf
        aTexto = avar
    End Function

    ''' <summary>
    ''' Agrega al final de la lista de DATOS un elemento (permite agilizar la carga de datos)
    ''' </summary>
    ''' <param name="Frecuencia">Frecuencia a cargar</param>
    ''' <param name="Amplitud">Amplitud a cargar</param>
    ''' <param name="RetardoDeFase">Retardo de fase a cargar</param>
    ''' <remarks></remarks>
    Public Sub AgregarPunto(Frecuencia As Double, Amplitud As Double, RetardoDeFase As Double)
        Dim ACargar As MedicionAdquiridaComplejaEstructura
        With ACargar
            .Frecuencia = Frecuencia
            .Amplitud = Amplitud
            .RetardoDeFase = RetardoDeFase
        End With
        Datos.Add(ACargar)
    End Sub

    Public Enum TiposDeSeries
        Amplitud
        Retardo_de_fase
    End Enum



    ''' <summary>
    ''' Esta función convierte los datos en dos arrays tipo single. Los datos contenidos en los parámetros
    '''  "Amplitud" y "RetardoFase" son borrados por esta función.
    ''' </summary>
    ''' <param name="Amplitud"> La estructura final de los datos es con filas de [Frecuencia Amplitud] </param>
    ''' <param name="RetardoFase"> La estructura final de los datos es con filas de [Frecuencia Retardo de fase]</param>
    Public Sub ConvertirDatosAMatricesDouble(ByRef Amplitud(,) As Double, ByRef RetardoFase(,) As Double)
        Dim max As Integer
        max = Datos.Count - 1
        ReDim Amplitud(max, 1)
        ReDim RetardoFase(max, 1)

        Dim Valor As MedicionAdquiridaComplejaEstructura
        Dim indice As Integer
        indice = 0
        For Each Valor In Datos
            Amplitud(indice, 0) = Valor.Frecuencia
            RetardoFase(indice, 0) = Valor.Frecuencia
            Amplitud(indice, 1) = Valor.Amplitud
            RetardoFase(indice, 1) = Valor.RetardoDeFase
            indice = indice + 1
        Next


    End Sub

End Class