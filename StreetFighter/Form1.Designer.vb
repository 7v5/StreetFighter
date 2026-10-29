<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGame
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGame))
        Me.tmrPhysics = New System.Windows.Forms.Timer(Me.components)
        Me.lblPlr1Name = New System.Windows.Forms.Label()
        Me.lblPlr2Name = New System.Windows.Forms.Label()
        Me.lblHealthPlr2Red = New System.Windows.Forms.Label()
        Me.lblHealthPlr1Red = New System.Windows.Forms.Label()
        Me.lblHealthPlr2 = New System.Windows.Forms.Label()
        Me.lblHealthPlr1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblWinner = New System.Windows.Forms.Label()
        Me.btnReplay = New System.Windows.Forms.Label()
        Me.btnMainMenu = New System.Windows.Forms.Label()
        Me.picGuard6 = New System.Windows.Forms.PictureBox()
        Me.picGuard5 = New System.Windows.Forms.PictureBox()
        Me.picGuard4 = New System.Windows.Forms.PictureBox()
        Me.picGuard3 = New System.Windows.Forms.PictureBox()
        Me.picGuard2 = New System.Windows.Forms.PictureBox()
        Me.picGuard1 = New System.Windows.Forms.PictureBox()
        Me.picAmmo4 = New System.Windows.Forms.PictureBox()
        Me.picAmmo3 = New System.Windows.Forms.PictureBox()
        Me.picAmmo2 = New System.Windows.Forms.PictureBox()
        Me.picAmmo1 = New System.Windows.Forms.PictureBox()
        Me.picEdgeL = New System.Windows.Forms.PictureBox()
        Me.picEdgeR = New System.Windows.Forms.PictureBox()
        Me.picFloor = New System.Windows.Forms.PictureBox()
        Me.picPlr2 = New System.Windows.Forms.PictureBox()
        Me.picPlr1 = New System.Windows.Forms.PictureBox()
        CType(Me.picGuard6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuard5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuard4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuard3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuard2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuard1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmmo4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmmo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmmo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmmo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEdgeL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEdgeR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFloor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPlr2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPlr1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrPhysics
        '
        Me.tmrPhysics.Enabled = True
        Me.tmrPhysics.Interval = 20
        '
        'lblPlr1Name
        '
        Me.lblPlr1Name.BackColor = System.Drawing.Color.SkyBlue
        Me.lblPlr1Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlr1Name.ForeColor = System.Drawing.Color.Black
        Me.lblPlr1Name.Location = New System.Drawing.Point(12, 32)
        Me.lblPlr1Name.Name = "lblPlr1Name"
        Me.lblPlr1Name.Size = New System.Drawing.Size(72, 25)
        Me.lblPlr1Name.TabIndex = 6
        Me.lblPlr1Name.Text = "Player 1"
        Me.lblPlr1Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPlr2Name
        '
        Me.lblPlr2Name.BackColor = System.Drawing.Color.SkyBlue
        Me.lblPlr2Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPlr2Name.ForeColor = System.Drawing.Color.Black
        Me.lblPlr2Name.Location = New System.Drawing.Point(716, 32)
        Me.lblPlr2Name.Name = "lblPlr2Name"
        Me.lblPlr2Name.Size = New System.Drawing.Size(72, 25)
        Me.lblPlr2Name.TabIndex = 7
        Me.lblPlr2Name.Text = "Player 2"
        Me.lblPlr2Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHealthPlr2Red
        '
        Me.lblHealthPlr2Red.BackColor = System.Drawing.Color.Red
        Me.lblHealthPlr2Red.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHealthPlr2Red.Location = New System.Drawing.Point(403, 9)
        Me.lblHealthPlr2Red.Name = "lblHealthPlr2Red"
        Me.lblHealthPlr2Red.Size = New System.Drawing.Size(385, 23)
        Me.lblHealthPlr2Red.TabIndex = 9
        '
        'lblHealthPlr1Red
        '
        Me.lblHealthPlr1Red.BackColor = System.Drawing.Color.Red
        Me.lblHealthPlr1Red.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHealthPlr1Red.Location = New System.Drawing.Point(12, 9)
        Me.lblHealthPlr1Red.Name = "lblHealthPlr1Red"
        Me.lblHealthPlr1Red.Size = New System.Drawing.Size(385, 23)
        Me.lblHealthPlr1Red.TabIndex = 8
        '
        'lblHealthPlr2
        '
        Me.lblHealthPlr2.BackColor = System.Drawing.Color.Lime
        Me.lblHealthPlr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHealthPlr2.Location = New System.Drawing.Point(403, 9)
        Me.lblHealthPlr2.Name = "lblHealthPlr2"
        Me.lblHealthPlr2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblHealthPlr2.Size = New System.Drawing.Size(385, 23)
        Me.lblHealthPlr2.TabIndex = 5
        Me.lblHealthPlr2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHealthPlr1
        '
        Me.lblHealthPlr1.BackColor = System.Drawing.Color.Lime
        Me.lblHealthPlr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHealthPlr1.Location = New System.Drawing.Point(12, 9)
        Me.lblHealthPlr1.Name = "lblHealthPlr1"
        Me.lblHealthPlr1.Size = New System.Drawing.Size(385, 23)
        Me.lblHealthPlr1.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        '
        'lblWinner
        '
        Me.lblWinner.BackColor = System.Drawing.Color.SkyBlue
        Me.lblWinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWinner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblWinner.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!)
        Me.lblWinner.ForeColor = System.Drawing.Color.Black
        Me.lblWinner.Location = New System.Drawing.Point(242, 92)
        Me.lblWinner.Name = "lblWinner"
        Me.lblWinner.Size = New System.Drawing.Size(319, 85)
        Me.lblWinner.TabIndex = 24
        Me.lblWinner.Text = "Player 1 Wins!"
        Me.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblWinner.Visible = False
        '
        'btnReplay
        '
        Me.btnReplay.BackColor = System.Drawing.Color.SkyBlue
        Me.btnReplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.btnReplay.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!)
        Me.btnReplay.ForeColor = System.Drawing.Color.Black
        Me.btnReplay.Location = New System.Drawing.Point(201, 201)
        Me.btnReplay.Name = "btnReplay"
        Me.btnReplay.Size = New System.Drawing.Size(196, 70)
        Me.btnReplay.TabIndex = 27
        Me.btnReplay.Text = "Replay"
        Me.btnReplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReplay.Visible = False
        '
        'btnMainMenu
        '
        Me.btnMainMenu.BackColor = System.Drawing.Color.SkyBlue
        Me.btnMainMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.btnMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMainMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.0!)
        Me.btnMainMenu.ForeColor = System.Drawing.Color.Black
        Me.btnMainMenu.Location = New System.Drawing.Point(403, 201)
        Me.btnMainMenu.Name = "btnMainMenu"
        Me.btnMainMenu.Size = New System.Drawing.Size(196, 70)
        Me.btnMainMenu.TabIndex = 28
        Me.btnMainMenu.Text = "Main Menu"
        Me.btnMainMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMainMenu.Visible = False
        '
        'picGuard6
        '
        Me.picGuard6.BackgroundImage = CType(resources.GetObject("picGuard6.BackgroundImage"), System.Drawing.Image)
        Me.picGuard6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard6.Location = New System.Drawing.Point(762, 92)
        Me.picGuard6.Name = "picGuard6"
        Me.picGuard6.Size = New System.Drawing.Size(26, 26)
        Me.picGuard6.TabIndex = 23
        Me.picGuard6.TabStop = False
        '
        'picGuard5
        '
        Me.picGuard5.BackgroundImage = CType(resources.GetObject("picGuard5.BackgroundImage"), System.Drawing.Image)
        Me.picGuard5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard5.Location = New System.Drawing.Point(730, 92)
        Me.picGuard5.Name = "picGuard5"
        Me.picGuard5.Size = New System.Drawing.Size(26, 26)
        Me.picGuard5.TabIndex = 22
        Me.picGuard5.TabStop = False
        '
        'picGuard4
        '
        Me.picGuard4.BackgroundImage = CType(resources.GetObject("picGuard4.BackgroundImage"), System.Drawing.Image)
        Me.picGuard4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard4.Location = New System.Drawing.Point(698, 92)
        Me.picGuard4.Name = "picGuard4"
        Me.picGuard4.Size = New System.Drawing.Size(26, 26)
        Me.picGuard4.TabIndex = 21
        Me.picGuard4.TabStop = False
        '
        'picGuard3
        '
        Me.picGuard3.BackgroundImage = CType(resources.GetObject("picGuard3.BackgroundImage"), System.Drawing.Image)
        Me.picGuard3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard3.Location = New System.Drawing.Point(76, 92)
        Me.picGuard3.Name = "picGuard3"
        Me.picGuard3.Size = New System.Drawing.Size(26, 26)
        Me.picGuard3.TabIndex = 20
        Me.picGuard3.TabStop = False
        '
        'picGuard2
        '
        Me.picGuard2.BackgroundImage = CType(resources.GetObject("picGuard2.BackgroundImage"), System.Drawing.Image)
        Me.picGuard2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard2.Location = New System.Drawing.Point(44, 92)
        Me.picGuard2.Name = "picGuard2"
        Me.picGuard2.Size = New System.Drawing.Size(26, 26)
        Me.picGuard2.TabIndex = 19
        Me.picGuard2.TabStop = False
        '
        'picGuard1
        '
        Me.picGuard1.BackgroundImage = CType(resources.GetObject("picGuard1.BackgroundImage"), System.Drawing.Image)
        Me.picGuard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picGuard1.Location = New System.Drawing.Point(12, 92)
        Me.picGuard1.Name = "picGuard1"
        Me.picGuard1.Size = New System.Drawing.Size(26, 26)
        Me.picGuard1.TabIndex = 18
        Me.picGuard1.TabStop = False
        '
        'picAmmo4
        '
        Me.picAmmo4.BackgroundImage = CType(resources.GetObject("picAmmo4.BackgroundImage"), System.Drawing.Image)
        Me.picAmmo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAmmo4.Location = New System.Drawing.Point(762, 60)
        Me.picAmmo4.Name = "picAmmo4"
        Me.picAmmo4.Size = New System.Drawing.Size(26, 26)
        Me.picAmmo4.TabIndex = 17
        Me.picAmmo4.TabStop = False
        '
        'picAmmo3
        '
        Me.picAmmo3.BackgroundImage = CType(resources.GetObject("picAmmo3.BackgroundImage"), System.Drawing.Image)
        Me.picAmmo3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAmmo3.Location = New System.Drawing.Point(730, 60)
        Me.picAmmo3.Name = "picAmmo3"
        Me.picAmmo3.Size = New System.Drawing.Size(26, 26)
        Me.picAmmo3.TabIndex = 16
        Me.picAmmo3.TabStop = False
        '
        'picAmmo2
        '
        Me.picAmmo2.BackgroundImage = CType(resources.GetObject("picAmmo2.BackgroundImage"), System.Drawing.Image)
        Me.picAmmo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAmmo2.Location = New System.Drawing.Point(44, 60)
        Me.picAmmo2.Name = "picAmmo2"
        Me.picAmmo2.Size = New System.Drawing.Size(26, 26)
        Me.picAmmo2.TabIndex = 15
        Me.picAmmo2.TabStop = False
        '
        'picAmmo1
        '
        Me.picAmmo1.BackgroundImage = CType(resources.GetObject("picAmmo1.BackgroundImage"), System.Drawing.Image)
        Me.picAmmo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAmmo1.Location = New System.Drawing.Point(12, 60)
        Me.picAmmo1.Name = "picAmmo1"
        Me.picAmmo1.Size = New System.Drawing.Size(26, 26)
        Me.picAmmo1.TabIndex = 14
        Me.picAmmo1.TabStop = False
        '
        'picEdgeL
        '
        Me.picEdgeL.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.picEdgeL.Location = New System.Drawing.Point(-50, 0)
        Me.picEdgeL.Name = "picEdgeL"
        Me.picEdgeL.Size = New System.Drawing.Size(50, 466)
        Me.picEdgeL.TabIndex = 13
        Me.picEdgeL.TabStop = False
        Me.picEdgeL.Tag = "floor"
        '
        'picEdgeR
        '
        Me.picEdgeR.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.picEdgeR.Location = New System.Drawing.Point(800, 0)
        Me.picEdgeR.Name = "picEdgeR"
        Me.picEdgeR.Size = New System.Drawing.Size(50, 456)
        Me.picEdgeR.TabIndex = 12
        Me.picEdgeR.TabStop = False
        Me.picEdgeR.Tag = "floor"
        '
        'picFloor
        '
        Me.picFloor.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.picFloor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picFloor.Location = New System.Drawing.Point(-7, 406)
        Me.picFloor.Name = "picFloor"
        Me.picFloor.Size = New System.Drawing.Size(811, 50)
        Me.picFloor.TabIndex = 0
        Me.picFloor.TabStop = False
        Me.picFloor.Tag = "floor"
        '
        'picPlr2
        '
        Me.picPlr2.BackColor = System.Drawing.Color.Transparent
        Me.picPlr2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picPlr2.Location = New System.Drawing.Point(716, 149)
        Me.picPlr2.Name = "picPlr2"
        Me.picPlr2.Size = New System.Drawing.Size(78, 111)
        Me.picPlr2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPlr2.TabIndex = 3
        Me.picPlr2.TabStop = False
        '
        'picPlr1
        '
        Me.picPlr1.BackColor = System.Drawing.Color.Transparent
        Me.picPlr1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picPlr1.Location = New System.Drawing.Point(12, 149)
        Me.picPlr1.Name = "picPlr1"
        Me.picPlr1.Size = New System.Drawing.Size(78, 111)
        Me.picPlr1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPlr1.TabIndex = 1
        Me.picPlr1.TabStop = False
        Me.picPlr1.Tag = "player"
        '
        'frmGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightBlue
        Me.ClientSize = New System.Drawing.Size(799, 442)
        Me.Controls.Add(Me.btnMainMenu)
        Me.Controls.Add(Me.btnReplay)
        Me.Controls.Add(Me.lblWinner)
        Me.Controls.Add(Me.picGuard6)
        Me.Controls.Add(Me.picGuard5)
        Me.Controls.Add(Me.picGuard4)
        Me.Controls.Add(Me.picGuard3)
        Me.Controls.Add(Me.picGuard2)
        Me.Controls.Add(Me.picGuard1)
        Me.Controls.Add(Me.picAmmo4)
        Me.Controls.Add(Me.picAmmo3)
        Me.Controls.Add(Me.picAmmo2)
        Me.Controls.Add(Me.picAmmo1)
        Me.Controls.Add(Me.picEdgeL)
        Me.Controls.Add(Me.picEdgeR)
        Me.Controls.Add(Me.picFloor)
        Me.Controls.Add(Me.lblPlr2Name)
        Me.Controls.Add(Me.lblPlr1Name)
        Me.Controls.Add(Me.lblHealthPlr2)
        Me.Controls.Add(Me.lblHealthPlr1)
        Me.Controls.Add(Me.lblHealthPlr1Red)
        Me.Controls.Add(Me.lblHealthPlr2Red)
        Me.Controls.Add(Me.picPlr2)
        Me.Controls.Add(Me.picPlr1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        CType(Me.picGuard6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuard5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuard4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuard3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuard2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuard1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmmo4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmmo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmmo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmmo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEdgeL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEdgeR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFloor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPlr2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPlr1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picFloor As PictureBox
    Friend WithEvents picPlr1 As PictureBox
    Friend WithEvents tmrPhysics As Timer
    Friend WithEvents picPlr2 As PictureBox
    Friend WithEvents lblPlr1Name As Label
    Friend WithEvents lblPlr2Name As Label
    Friend WithEvents lblHealthPlr2Red As Label
    Friend WithEvents lblHealthPlr1Red As Label
    Friend WithEvents lblHealthPlr2 As Label
    Friend WithEvents lblHealthPlr1 As Label
    Friend WithEvents picEdgeR As PictureBox
    Friend WithEvents picEdgeL As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents picAmmo1 As PictureBox
    Friend WithEvents picAmmo2 As PictureBox
    Friend WithEvents picAmmo4 As PictureBox
    Friend WithEvents picAmmo3 As PictureBox
    Friend WithEvents picGuard1 As PictureBox
    Friend WithEvents picGuard2 As PictureBox
    Friend WithEvents picGuard3 As PictureBox
    Friend WithEvents picGuard6 As PictureBox
    Friend WithEvents picGuard5 As PictureBox
    Friend WithEvents picGuard4 As PictureBox
    Friend WithEvents lblWinner As Label
    Friend WithEvents btnReplay As Label
    Friend WithEvents btnMainMenu As Label
End Class
