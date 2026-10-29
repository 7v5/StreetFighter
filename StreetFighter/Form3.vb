' Name: Tutorial
' Purpose: Show the controls and tips to the user
' Programmer: Elijah Hunter on 5/10/2024
Public Class frmTutorial
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Hide the tutorial
        Me.Hide()
        ' Show the main menu
        frmMainMenu.Show()
    End Sub

    Private Sub Form3_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.ExitThread()
    End Sub
End Class