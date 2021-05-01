Public Class Form1

    Private _originalSize As Size = Nothing
    Private _scale As Single = 1
    Private _scaleDelta As Single = 0.0005


    Dim pntStart As Point
    Dim PuntoAMover As Point

    Dim WithEvents PlotC As Plot2DComponent
    Dim WithEvents DrawCo As New DrawData
    Dim original As Bitmap
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VerificarPictureBox()
        DrawCo.Contenedor = PictureBox1
        DrawCo.Escala = _scale
        PlotC = New Plot2DComponent(DrawCo)
        ' original = New Bitmap(Panel1.BackgroundImage)

    End Sub

    Private Sub Form1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        'ActualizarZoom(e)
    End Sub

    Private Sub VerificarPictureBox()
        'init this from here or a method depending on your needs
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

            '_originalSize = PictureBox1.Size
            'PictureBox1.Size = Panel1.Size
            '_originalSize = PictureBox1.Size

            _originalSize.Height = PictureBox1.Image.Height
            _originalSize.Width = PictureBox1.Image.Width

        End If

        'If Panel1.BackgroundImage IsNot Nothing Then
        '    'PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        '    'If Panel1.BackgroundImage

        '    '_originalSize = PictureBox1.Size
        '    'PictureBox1.Size = Panel1.Size
        '    '_originalSize = PictureBox1.Size

        '    _originalSize.Height = Panel1.BackgroundImage.Height  'PictureBox1.Image.Height
        '    _originalSize.Width = Panel1.BackgroundImage.Width ' PictureBox1.Image.Width

        'End If

    End Sub


    Dim prevalor As Single = 0

    Private Sub ActualizarZoomPicture(e As MouseEventArgs)
        'if very sensitive mouse, change 0.00005 to something even smaller   
        _scaleDelta = Math.Sqrt(PictureBox1.Width * PictureBox1.Height) * 0.00005

        If e.Delta < 0 Then
            _scale -= _scaleDelta
        ElseIf e.Delta > 0 Then
            _scale += _scaleDelta
        End If

        If e.Delta <> 0 Then




            'PictureBox1.Size = New Size(CInt(Math.Round((_originalSize.Width) * _scale)), CInt(Math.Round((_originalSize.Height) * _scale)))

            'Dim previo As Integer = PictureBox1.Size.Width

            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

            'PictureBox1.Size = New Size((_originalSize.Width * _scale), ((_originalSize.Height) * _scale))
            'PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            'DrawCo.Escala = DrawCo.Escala + (PictureBox1.Size.Width / previo - 1)   '(PictureBox1.Width - 5) / _originalSize.Width

            PictureBox1.Height = Math.Round(_originalSize.Height * _scale)
            PictureBox1.Width = Math.Round(_originalSize.Width * _scale)

            'DrawCo.Escala = (PictureBox1.Width - 5) / _originalSize.Width
            DrawCo.Escala = _scale

            DrawCo.Actualizar()

            'Actualizo la posición del panel
            'Panel1.HorizontalScroll.Value = prevalor * (1 + _scaleDelta)
            'Panel1.VerticalScroll.Value = Panel1.VerticalScroll.Value * (1 + _scaleDelta)

            Dim valor As Integer
            valor = Panel1.HorizontalScroll.Value * (1 + _scaleDelta)

            If valor < Panel1.HorizontalScroll.Minimum Then
                valor = Panel1.HorizontalScroll.Minimum
            End If
            If valor > Panel1.HorizontalScroll.Maximum Then
                valor = Panel1.HorizontalScroll.Maximum
            End If
            Panel1.HorizontalScroll.Value = valor

            valor = prevalor * (1 + _scaleDelta)
            If valor < Panel1.VerticalScroll.Minimum Then
                valor = Panel1.VerticalScroll.Minimum
            End If
            If valor > Panel1.VerticalScroll.Maximum Then
                valor = Panel1.VerticalScroll.Maximum
            End If
            Panel1.VerticalScroll.Value = valor


        End If


    End Sub



    Private Sub ActualizarZoom(e As MouseEventArgs)
        'if very sensitive mouse, change 0.00005 to something even smaller   
        _scaleDelta = Math.Sqrt(Panel1.BackgroundImage.Width * Panel1.BackgroundImage.Height) * 0.00005

        If e.Delta < 0 Then
            _scale -= _scaleDelta
        ElseIf e.Delta > 0 Then
            _scale += _scaleDelta
        End If

        If e.Delta <> 0 Then

            DrawCo.Escala = _scale

            'PictureBox1.Size = New Size(CInt(Math.Round((_originalSize.Width * _ratWidth) * _scale)),
            '                                    CInt(Math.Round((_originalSize.Height * _ratHeight) * _scale)))
            'PictureBox2.Size = New Size(CInt(Math.Round((_originalSize.Width * _ratWidth) * _scale)),
            '                        CInt(Math.Round((_originalSize.Height * _ratHeight) * _scale)))

            ' Panel1.BackgroundImage.Size = New Size(CInt(Math.Round((_originalSize.Width) * _scale)),
            'CInt(Math.Round((_originalSize.Height) * _scale)))

            Panel1.BackgroundImage = New Bitmap(original, New Size(CInt(Math.Round((_originalSize.Width) * _scale)),
                                                CInt(Math.Round((_originalSize.Height) * _scale))))
            'PictureBox2.Size = New Size(CInt(Math.Round((_originalSize.Width) * _scale)),
            '                        CInt(Math.Round((_originalSize.Height) * _scale)))
            DrawCo.Actualizar()
        End If


    End Sub



    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        OpenImage()
    End Sub


    ''' <summary>
    ''' Rutina que abre una imagen
    ''' </summary>
    Private Sub OpenImage()
        Dim ofd As New OpenFileDialog

        ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"

        If ofd.ShowDialog() = DialogResult.OK Then
            original = New Bitmap(ofd.FileName)
            Panel1.BackgroundImage = original
            VerificarPictureBox()
        End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel1_Scroll(sender As Object, e As ScrollEventArgs) Handles Panel1.Scroll

    End Sub

    Private Sub Panel1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Panel1.MouseWheel
        'ActualizarZoom(e)
        ActualizarZoomPicture(e)
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Middle Then
            pntStart = New Point(e.X, e.Y)

            PictureBox1.Cursor = Cursors.SizeAll
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Arrastrar(New PointF(e.X, e.Y))
        End If
    End Sub

    Private Sub Arrastrar(Pos As PointF)
        Dim X As Integer = (pntStart.X - Pos.X)
        Dim Y As Integer = (pntStart.Y - Pos.Y)
        PuntoAMover = New Point((X - Panel1.AutoScrollPosition.X), (Y - Panel1.AutoScrollPosition.Y))

        Panel1.AutoScrollPosition = PuntoAMover
        prevalor = Panel1.VerticalScroll.Value
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Middle Then
            PictureBox1.Cursor = Cursors.Cross
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel1.Refresh()

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub DrawCo_MoverArrastrar(pos As PointF) Handles DrawCo.MoverArrastrar_Inicio
        pntStart = New Point(pos.X, pos.Y)
        PictureBox1.Cursor = Cursors.SizeAll
    End Sub

    Private Sub DrawCo_MoverArrastrar_Desplazamiento(pos As PointF) Handles DrawCo.MoverArrastrar_Desplazamiento
        Arrastrar(pos)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        DrawCo.KeyPressEve(e)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        PlotC.AgregarSerie_ConPuntos()
    End Sub

    Private Sub PlotC_Click_on_Serie(sender As SerieComponet) Handles PlotC.Click_on_Serie
        'GroupBox1.Text = sender.
        PropObjetos(sender)
    End Sub

    Private Sub PlotC_Click_on_axis(sender As ComponentAxis) Handles PlotC.Click_on_axis
        PropObjetos(sender)
    End Sub
    Private Sub PropObjetos(objeto As Object)

        If GetType(NameInterface).IsAssignableFrom(objeto.GetType) Then

            Dim refe As NameInterface = objeto
            GroupBox1.Text = refe.Name
        End If
        PropertyGrid1.SelectedObject = objeto

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        TextBox1.Text = PlotC.GenerateCode(Plot2DComponent.TypesCode.Matlab)
    End Sub
End Class
