<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMainMenu
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
        Me.picTitle = New System.Windows.Forms.PictureBox()
        Me.picPlay = New System.Windows.Forms.PictureBox()
        Me.picExit = New System.Windows.Forms.PictureBox()
        Me.btnHelp = New System.Windows.Forms.PictureBox()
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPlay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picExit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picTitle
        '
        Me.picTitle.BackgroundImage = Global.StreetFighter.My.Resources.Resources.titlescreen
        Me.picTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picTitle.Location = New System.Drawing.Point(167, 114)
        Me.picTitle.Name = "picTitle"
        Me.picTitle.Size = New System.Drawing.Size(449, 130)
        Me.picTitle.TabIndex = 3
        Me.picTitle.TabStop = False
        '
        'picPlay
        '
        Me.picPlay.BackColor = System.Drawing.Color.Transparent
        Me.picPlay.BackgroundImage = Global.StreetFighter.My.Resources.Resources.start2
        Me.picPlay.Location = New System.Drawing.Point(269, 292)
        Me.picPlay.Name = "picPlay"
        Me.picPlay.Size = New System.Drawing.Size(248, 94)
        Me.picPlay.TabIndex = 4
        Me.picPlay.TabStop = False
        '
        'picExit
        '
        Me.picExit.BackColor = System.Drawing.Color.Transparent
        Me.picExit.BackgroundImage = Global.StreetFighter.My.Resources.Resources.exit2
        Me.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picExit.Location = New System.Drawing.Point(606, 12)
        Me.picExit.Name = "picExit"
        Me.picExit.Size = New System.Drawing.Size(182, 70)
        Me.picExit.TabIndex = 5
        Me.picExit.TabStop = False
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.Color.Transparent
        Me.btnHelp.BackgroundImage = Global.StreetFighter.My.Resources.Resources.help
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHelp.Location = New System.Drawing.Point(12, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(167, 58)
        Me.btnHelp.TabIndex = 6
        Me.btnHelp.TabStop = False
        '
        'frmMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.StreetFighter.My.Resources.Resources.mainmenu
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.picExit)
        Me.Controls.Add(Me.picPlay)
        Me.Controls.Add(Me.picTitle)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main Menu"
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPlay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picExit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picTitle As PictureBox
    Friend WithEvents picPlay As PictureBox
    Friend WithEvents picExit As PictureBox
    Friend WithEvents btnHelp As PictureBox
End Class
