Public Class Form1

    Private _originalSize As Size = Nothing
    Private _scale As Single = 1
    Private _scaleDelta As Single = 0.0005


    Dim pntStart As Point
    Dim PuntoAMover As Point

    Dim PlotC As Plot2DComponent
    Dim DrawCo As New DrawData
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
            Dim X As Integer = (pntStart.X - e.X)
            Dim Y As Integer = (pntStart.Y - e.Y)
            PuntoAMover = New Point((X - Panel1.AutoScrollPosition.X), (Y - Panel1.AutoScrollPosition.Y))

            Panel1.AutoScrollPosition = PuntoAMover
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Middle Then
            PictureBox1.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel1.Refresh()

    End Sub
End Class
