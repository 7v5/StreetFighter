' Name: Main Menu
' Purpose: Start the Game, or Exit
' Programmer: Elijah Hunter & Noah Kohn From 4/30/24 to 5/10/24

Public Class frmMainMenu
    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles picPlay.Click
        ' Mostly by Noah
        ' Comments from Elijah


        ' Start the game
        frmGame.start()
        ' Reset guard health
        frmGame.guardHealth(0) = 3
        frmGame.guardHealth(1) = 3
        ' Reset player health
        frmGame.plrHealth(0) = 250
        frmGame.plrHealth(1) = 250
        ' Reset player positions
        frmGame.picPlr1.Location = New Point(12, 93)
        frmGame.picPlr2.Location = New Point(716, 93)
        ' Reset duckin, height, and gravity

        ' Was originally 2 If statements, Elijah converted to a For Loop
        For x As Integer = 0 To 1
            If frmGame.blnDuck(x) Then
                frmGame.blnDuck(x) = False
                frmGame.picPlr(x).Height *= 2
                frmGame.dblGRAVITY(x) = 3
            End If
        Next
        ' Reset health bar size
        frmGame.lblHealthPlr1.Size = frmGame.lblHealthPlr1Red.Size
        frmGame.lblHealthPlr2.Size = frmGame.lblHealthPlr2Red.Size
        ' Reset health bar position
        frmGame.lblHealthPlr1.Location = frmGame.lblHealthPlr1Red.Location
        frmGame.lblHealthPlr2.Location = frmGame.lblHealthPlr2Red.Location
        ' Reset player dead flag
        frmGame.blnDead(0) = False
        frmGame.blnDead(1) = False
        ' Reset player stunned flag
        frmGame.blnStunned(0) = False
        frmGame.blnStunned(1) = False
        ' Reset player guard broke flag
        frmGame.guardBroke(0) = False
        frmGame.guardBroke(1) = False
        ' Reset player speed
        frmGame.plrSpeed(0) = 3
        frmGame.plrSpeed(1) = 3
        ' Hide replay controls
        frmGame.btnReplay.Visible = False
        frmGame.btnMainMenu.Visible = False
        frmGame.lblWinner.Visible = False
        ' Reset player animation
        frmGame.picPlr1.Image = My.Resources._1RIdle
        frmGame.picPlr2.Image = My.Resources._2LIdle
        ' Reset player velocities
        For x As Integer = 0 To 1
            frmGame.plrVelX(x) = 0
            frmGame.plrVelY(x) = 0
        Next
        ' Hide the main menu form
        Me.Hide()
        ' Show the game form
        frmGame.Show()
    End Sub

    Private Sub Form2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Close the application
        Application.ExitThread()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles picExit.Click
        ' Close the application
        Application.ExitThread()
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Me.Hide()
        frmTutorial.Show()
    End Sub
End Class