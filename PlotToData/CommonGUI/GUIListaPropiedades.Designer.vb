Namespace Extras
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class GUIListaPropiedades
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GUIListaPropiedades))
			Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.ListBox1 = New System.Windows.Forms.ListBox()
			Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
			Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
			Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
			CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SplitContainer1.Panel1.SuspendLayout()
			Me.SplitContainer1.Panel2.SuspendLayout()
			Me.SplitContainer1.SuspendLayout()
			Me.ToolStrip1.SuspendLayout()
			Me.SuspendLayout()
			'
			'SplitContainer1
			'
			Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.SplitContainer1.Name = "SplitContainer1"
			Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
			'
			'SplitContainer1.Panel1
			'
			Me.SplitContainer1.Panel1.Controls.Add(Me.ListBox1)
			Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
			'
			'SplitContainer1.Panel2
			'
			Me.SplitContainer1.Panel2.Controls.Add(Me.PropertyGrid1)
			Me.SplitContainer1.Size = New System.Drawing.Size(439, 460)
			Me.SplitContainer1.SplitterDistance = 113
			Me.SplitContainer1.TabIndex = 0
			'
			'ListBox1
			'
			Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.ListBox1.FormattingEnabled = True
			Me.ListBox1.Location = New System.Drawing.Point(0, 25)
			Me.ListBox1.Name = "ListBox1"
			Me.ListBox1.Size = New System.Drawing.Size(439, 88)
			Me.ListBox1.TabIndex = 0
			'
			'ToolStrip1
			'
			Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
			Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
			Me.ToolStrip1.Name = "ToolStrip1"
			Me.ToolStrip1.Size = New System.Drawing.Size(439, 25)
			Me.ToolStrip1.TabIndex = 1
			Me.ToolStrip1.Text = "ToolStrip1"
			'
			'ToolStripButton1
			'
			Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
			Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.ToolStripButton1.Name = "ToolStripButton1"
			Me.ToolStripButton1.Size = New System.Drawing.Size(60, 22)
			Me.ToolStripButton1.Text = "Delete"
			'
			'PropertyGrid1
			'
			Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
			Me.PropertyGrid1.Name = "PropertyGrid1"
			Me.PropertyGrid1.Size = New System.Drawing.Size(439, 343)
			Me.PropertyGrid1.TabIndex = 0
			'
			'GUIListaPropiedades
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(439, 460)
			Me.Controls.Add(Me.SplitContainer1)
			Me.Name = "GUIListaPropiedades"
			Me.Text = "ListaPropiedades"
			Me.SplitContainer1.Panel1.ResumeLayout(False)
			Me.SplitContainer1.Panel1.PerformLayout()
			Me.SplitContainer1.Panel2.ResumeLayout(False)
			CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.SplitContainer1.ResumeLayout(False)
			Me.ToolStrip1.ResumeLayout(False)
			Me.ToolStrip1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		Friend WithEvents SplitContainer1 As SplitContainer
        Friend WithEvents ListBox1 As ListBox
        Friend WithEvents PropertyGrid1 As PropertyGrid
        Friend WithEvents ToolStrip1 As ToolStrip
        Friend WithEvents ToolStripButton1 As ToolStripButton
    End Class
End Namespace
