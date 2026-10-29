' Name: Streetfighter from Temu
' Purpose: A fighting game with Streetfighter sprites
' Programmers: Elijah Hunter & Noah Kohn From 4/30/24 to 5/10/24

' Art from Streetfighter 3
' Title Screen background from Streetfighter 3
' Title Screen buttons from Elijah

Option Explicit On
Option Strict On
Option Infer Off

Imports System.Media

Public Class frmGame
    ' Determines if the game should show hitboxes 
    Public blnDead() As Boolean = {False, False}

    ' Used to visualize hitboxes, off by default
    Public blnDebug As Boolean = False

    ' Determine which keys are being held by each player
    Public keysHeld() As Boolean = {False, False, False, False}
    '                                A      D      Left   Right

    ' Arrays to hold corresponding form objects
    Public picPlr(2) As PictureBox
    Public lblHealthPlr(2) As Label
    Public lblHealthPlrRed(2) As Label

    ' Arrays to hold information about the player
    Public plrSpeed() As Double = {0, 0}
    Public plrVelX() As Double = {0, 0}
    Public plrVelY() As Double = {0, 0}
    Public plrHealth() As Double = {250, 250}

    ' Arrays to hold information about the player's knockback
    Public plrKbVelX() As Double = {0, 0}
    Public plrKbVelY() As Double = {0, 0}

    ' Used to limit the players max speed
    Public Const plrMAXVEL As Double = 0.025

    ' Holds information about how fast/slow movement is etc.
    Public dblFRICTION As Double = 0.7
    Public dblACCELERATION As Double = 2.25
    Public dblJUMPPOWER() As Double = {-25, -25}
    Public dblGRAVITY() As Double = {3, 3}

    ' Array holding the player's hitbox
    Private plrCollider() As Rectangle = {New Rectangle(), New Rectangle()}
    ' Array holding the player's original hitbox color.
    Private originalColor() As Color = {Color.SeaGreen, Color.Orchid}

    ' Object that holds the hitboxes of the surrounding arena
    Private floorCollider As Rectangle
    Private wallColliderL As Rectangle
    Private wallColliderR As Rectangle

    ' Checks if each player is facing right or left
    Private blnFaceRight() As Boolean = {True, False}

    ' Checks if each player is ducking
    Public blnDuck() As Boolean = {False, False}

    ' Holds each hitbox for each player's attack
    Private attackHitbox(,) As Rectangle = {{New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle()
                                            }, {New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle(), New Rectangle()}}
    ' Holds each the image for each player's attack
    Private attackPicture(,) As PictureBox = {{New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox()
                                            }, {New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox(), New PictureBox()}}
    ' Checks if the attack is still active
    Private blnHitboxAlive(,) As Boolean = {{False, False, False}, {False, False, False}}

    ' Times how long the attack is active for
    Private aliveTmr() As Timer = {New Timer(), New Timer()}

    ' Used for timing cooldowns
    Private atkTmr() As Timer = {New Timer(), New Timer()}

    ' Used for checking if the player can attack again, or if they are still on cooldown
    Private blnPunchCooldown() As Boolean = {False, False}

    ' Checks if the player is stunned
    Public blnStunned() As Boolean = {False, False}
    ' Checks if the player got guard broke
    Public guardBroke() As Boolean = {False, False}

    ' Plays various sounds
    Private soundPunch As SoundPlayer = New SoundPlayer(My.Resources.punch)
    Private soundDie As SoundPlayer = New SoundPlayer(My.Resources.oof)
    Private soundKick As SoundPlayer = New SoundPlayer(My.Resources.Kicking)
    Private soundHadouken As SoundPlayer = New SoundPlayer(My.Resources.HadoukenNoise)

    ' Checks if the hadouken should move left or right
    Private projectileFaceRight(,) As Boolean = {{True, True}, {False, False}}

    ' Checks if the player is holding G (for guard) or not
    Private plrGuarding() As Boolean = {False, False}
    ' Used for holding each color for their shield's health ex. 3 health remaining is bright green, and 0 remaining is black
    Private guardColors(,) As Color = {{Color.Black, ColorTranslator.FromHtml("#1d291a"), ColorTranslator.FromHtml("#354a2f"), Color.ForestGreen},
                                       {Color.Black, ColorTranslator.FromHtml("#783e76"), ColorTranslator.FromHtml("#a655a3"), Color.Orchid}}

    ' Contains how much knockback/damage the player takes while blocking
    Private Const guardKnockback As Double = 0.2
    Private Const guardDamage As Double = 0.1

    ' Contains the hitbox for the player's shield
    Private guardingHitbox() As Rectangle = {New Rectangle(), New Rectangle()}

    ' Contains the image for the player's shield
    Private guardingPicture() As PictureBox = {New PictureBox(), New PictureBox()}

    ' Contains the health for the player's shield
    Public guardHealth() As Integer = {3, 3}

    ' Counts how long to refill the player's shield heatlh
    Private tmrRefillGuard() As Timer = {New Timer(), New Timer()}

    ' Counts how long until the player is no longer guard broke
    Private tmrGuardBreak() As Timer = {New Timer(), New Timer()}

    ' Holds the picture boxes for how many hadoukens charged, and how much shield health left
    Private ammoIcons(,) As PictureBox
    Private guardIcons(,) As PictureBox
    Private Sub tmrPhysics_Tick(sender As Object, e As EventArgs) Handles tmrPhysics.Tick
        ' Creates the player, floor, and wall hitboxes
        gatherColliders()

        ' Makes sure the round hasnt ended yet
        If Not blnDead(0) And Not blnDead(1) Then
            ' Makes sure the player isnt stunned or guard broke
            ' ( Because they shouldnt be able to move while stunned)
            If Not blnStunned(0) And Not guardBroke(0) Then
                ' Check if A is held, and apply left movement
                If keysHeld(0) Then
                    plrVelX(0) -= plrSpeed(0)
                    acceleratePlr1()
                End If
                ' Check if D is held, and apply right movement
                If keysHeld(1) Then
                    plrVelX(0) += plrSpeed(0)
                    acceleratePlr1()
                End If
                ' If the player is stunned, give them 0 velocity to disable movement
            ElseIf blnStunned(0) Or guardBroke(0) Then
                plrVelX(0) = 0
            End If
            ' Makes sure the player isnt stunned or guard broke
            ' ( Because they shouldnt be able to move while stunned)
            If Not blnStunned(1) And Not guardBroke(1) Then
                ' Check if left is held, and apply left movement
                If keysHeld(2) Then
                    plrVelX(1) -= plrSpeed(1)
                    acceleratePlr2()
                End If
                ' Check if right is held, and apply right movement
                If keysHeld(3) Then
                    plrVelX(1) += plrSpeed(1)
                    acceleratePlr2()
                End If
            ElseIf blnStunned(1) Or guardBroke(1) Then
                ' If the player is stunned, give them 0 velocity to disable movement
                plrVelX(1) = 0
            End If

            ' Move Player according to their X and Y velocity
            picPlr(0).Location = New Point(picPlr(0).Location.X + CInt(plrVelX(0) + plrKbVelX(0)), CInt(picPlr(0).Location.Y + plrVelY(0)))
            picPlr(1).Location = New Point(picPlr(1).Location.X + CInt(plrVelX(1) + plrKbVelX(1)), CInt(picPlr(1).Location.Y + plrVelY(1)))

            ' Apply Gravity
            plrVelY(0) += dblGRAVITY(0)
            plrVelY(1) += dblGRAVITY(1)
            ' Apply dampening to movement
            plrVelX(0) *= dblFRICTION
            plrVelX(1) *= dblFRICTION
            ' Apply dampening to knockback
            plrKbVelX(0) *= dblFRICTION
            plrKbVelX(1) *= dblFRICTION
            ' Create collider boxes
            gatherColliders()

            ' Check for collisions with floor
            If plrCollidingWithFloor(0) Then
                ' Stop player 1 moving through floor
                plrCollideWithFloor(0)
            End If
            If plrCollidingWithFloor(1) Then
                ' Stop player 2 moving through floor
                plrCollideWithFloor(1)
            End If
            'checks for collision
            If plrCollideWall(0) Then
                plrCollide(0)
            End If
            'detects collision
            If plrCollideWall(1) Then
                plrCollide(1)
            End If

            ' Moves the projectile in the corresponding direction
            moveProjectiles()
            ' Deletes the projectile if it hits the wall
            projectileCollideWall()
            ' Checks if either player was hit
            plr1CollidingAtk()
            plr2CollidingAtk()
            ' Checks if either player is guarding, or guard broke
            For x As Integer = 0 To 1
                If plrGuarding(x) Then
                    createGuardingHitbox(x)
                    If blnDebug Then
                        createGuardingPicture(x)
                    End If
                    plrVelX(x) *= 0.1
                End If
                If guardBroke(x) Then
                    updateGuardBrokeAnim(x)
                End If
            Next
            ' Checks if projectiles hit eachother and deletes them if they did
            projectileCollision()
            ' Updates the picture boxes that hold how many haodukens / shield health left
            updateIcons()
        Else
            ' Displays a winning message
            lblWinner.Text = "Player " & findWinner() & " Wins!"
            lblWinner.Visible = True

            btnReplay.Visible = True
            btnMainMenu.Visible = True
        End If
    End Sub

    Private Sub updateDeathImage(plr As Integer)
        ' Holds each player's death image
        Dim images(,) As Image = {{My.Resources._1LDead, My.Resources._1RDead}, {My.Resources._2LDead, My.Resources._2RDead}}
        ' Check if the player is facing right or left
        If blnFaceRight(plr) Then
            ' Apply the corresponding image
            picPlr(plr).Image = images(plr, 1)
        Else
            ' Apply the corresponding image
            picPlr(plr).Image = images(plr, 0)
        End If
    End Sub

    Private Function findWinner() As Integer
        ' Check if player 1 is dead, and if they are then player 2 is the winner and vice verca
        If blnDead(0) Then
            ' Display the image for being dead
            updateDeathImage(0)
            Return 2
        Else
            ' Display the image for being dead
            updateDeathImage(1)
            Return 1
        End If
    End Function

    Private Sub projectileCollision()
        ' Run through each player's projectile
        For x As Integer = 0 To 1
            ' Check if one player's projectile collides with the other player's projectie
            If attackHitbox(0, x + 4).IntersectsWith(attackHitbox(1, x + 4)) Then
                ' If it does, delete both projectiles
                deleteProjectile(x, 0)
                deleteProjectile(x, 1)
            End If
        Next
    End Sub

    Private Sub updateIcons()
        ' Run through both players
        For x As Integer = 0 To 1
            ' Display the corresponding shield health in the picture boxes
            Select Case guardHealth(x)
                Case 0
                    guardIcons(x, 0).Visible = False
                    guardIcons(x, 1).Visible = False
                    guardIcons(x, 2).Visible = False
                Case 1
                    guardIcons(x, 0).Visible = True
                    guardIcons(x, 1).Visible = False
                    guardIcons(x, 2).Visible = False
                Case 2
                    guardIcons(x, 0).Visible = True
                    guardIcons(x, 1).Visible = True
                    guardIcons(x, 2).Visible = False
                Case 3
                    guardIcons(x, 0).Visible = True
                    guardIcons(x, 1).Visible = True
                    guardIcons(x, 2).Visible = True
            End Select
            ' Display the corresponding hadoukens left in the picture boxes
            Select Case countProjectile(x)
                Case 0
                    ammoIcons(x, 0).Visible = True
                    ammoIcons(x, 1).Visible = True
                Case 1
                    ammoIcons(x, 0).Visible = True
                    ammoIcons(x, 1).Visible = False
                Case 2
                    ammoIcons(x, 0).Visible = False
                    ammoIcons(x, 1).Visible = False
            End Select
        Next
    End Sub

    Private Function countProjectile(plr As Integer) As Integer
        ' Initialize a variable for counting how many projectiles are on the field for that player
        Dim count As Integer = 0
        ' Run through that player's projectiles
        For x As Integer = 0 To 1
            ' Check if it exists or not
            If projectileExist(plr, x) Then
                ' If it does exist, increase the count.
                count += 1
            End If
        Next
        Return count
    End Function

    Private Function projectileExist(plr As Integer, num As Integer) As Boolean
        ' Check if the height is 0 or not, and if it isnt 0 then it exists
        ' (This works because the subprocedure for deleting attacks simply sets the X and Y coordinates to zero,
        '       then sets the width and height to zero.)
        If attackHitbox(plr, num + 4).Height <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub moveProjectiles()
        ' Run through each player
        For x As Integer = 0 To 1
            ' Run through both projectiles
            For y As Integer = 4 To 5
                ' Check if it exists
                If attackHitbox(x, y).Height <> 0 Then
                    ' Check if it is facing right or left
                    If projectileFaceRight(x, (y - 4)) Then
                        ' If its facing right, move it to the right
                        attackHitbox(x, y).Location = New Point(attackHitbox(x, y).Location.X + 10, attackHitbox(x, y).Location.Y)
                    Else
                        ' If its facing left, move it to the left
                        attackHitbox(x, y).Location = New Point(attackHitbox(x, y).Location.X - 10, attackHitbox(x, y).Location.Y)
                    End If
                    ' Move the image to the hitbox location so you can visually see the hadouken
                    attackPicture(x, y).Location = attackHitbox(x, y).Location
                End If
            Next
        Next
    End Sub

    Private Sub damagePlr(dmg As Double, plr As Integer)
        ' Remove the damage dealt from the player's health
        plrHealth(plr) -= dmg
        ' Uses a formula (that took me like an hour to create) to calculate the new size of the label
        Dim newWidth As Double = (plrHealth(plr) / 250) * lblHealthPlrRed(plr).Size.Width
        ' Apply the new size
        lblHealthPlr(plr).Size = New Size(CInt(newWidth), lblHealthPlrRed(plr).Size.Height)
        ' Check if its the second player being damaged
        ' (This is needed because you cant size from left to right, only right to left.
        '   So instead, we size it normally then move it to right.)
        If plr = 1 Then
            ' Calculate the new location of the health bar (using a formula that took me another hour to create)
            Dim newLocationX As Double = ((lblHealthPlrRed(plr).Location.X + lblHealthPlrRed(plr).Size.Width) - (lblHealthPlr(plr).Size.Width))
            ' Move the health bar to the new location
            lblHealthPlr(plr).Location = New Point(CInt(newLocationX), lblHealthPlrRed(plr).Location.Y)
        End If
    End Sub

    Private Sub acceleratePlr1()
        ' Check if the player's acceleration rate is under the max
        If plrSpeed(0) < plrMAXVEL Then
            ' If it is, increase it.
            plrSpeed(0) += dblACCELERATION
        End If
    End Sub

    Private Sub acceleratePlr2()
        ' Check if the player's acceleration rate is under the max
        If plrSpeed(1) < plrMAXVEL Then
            ' If it is, increase it.
            plrSpeed(1) += dblACCELERATION
        End If
    End Sub

    Private Sub gatherColliders()
        ' Create a hitbox in the exact position of the player's picturebox.
        plrCollider(0) = New Rectangle(picPlr(0).Location, New Size(picPlr(0).Size.Width, picPlr(0).Size.Height + 2))
        plrCollider(1) = New Rectangle(picPlr(1).Location, New Size(picPlr(1).Size.Width, picPlr(1).Size.Height + 2))
        ' Create a hitbox in the exact position of the floor's picturebox.
        floorCollider = New Rectangle(picFloor.Location, picFloor.Size)
    End Sub

    Private Function plrCollideWall(plr As Integer) As Boolean
        ' Check if the player's hitbox is colliding with either wall.
        If plrCollider(plr).IntersectsWith(wallColliderL) Or plrCollider(plr).IntersectsWith(wallColliderR) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub projectileCollideWall()
        ' Run through each player
        For x As Integer = 0 To 1
            ' Run through each projectile
            For y As Integer = 4 To 5
                ' Check if it collides with either wall
                If attackHitbox(x, y).IntersectsWith(wallColliderR) Or attackHitbox(x, y).IntersectsWith(wallColliderL) Then
                    ' If it does, delete it.
                    deleteProjectile(y - 4, x)
                End If
            Next
        Next
    End Sub

    Private Sub plrCollide(plr As Integer)
        ' If the player is colliding, reverse its velocity.
        plrVelX(plr) *= -1.5
        plrKbVelX(plr) *= -1.5
        ' After reversing its velocity, force it out of the wall.
        picPlr(plr).Location = New Point(picPlr(plr).Location.X + CInt(plrVelX(plr) + plrKbVelX(plr)), picPlr(plr).Location.Y + CInt(plrVelY(plr)))
        ' After being pushed out of the wall, reset their velocity to 0 to prevent them going through the wall again.
        plrVelX(plr) = 0
        plrKbVelX(plr) = 0
    End Sub

    Private Function plrCollidingWithFloor(plr As Integer) As Boolean
        ' Check if the player's hitbox collides with the floor.
        If plrCollider(plr).IntersectsWith(floorCollider) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub plrCollideWithFloor(plr As Integer)
        ' Move the player upwards by the player's height.
        picPlr(plr).Top = picFloor.Top - picPlr(plr).Height
        ' Reset the player's Y velocity to prevent them going through the floor again.
        plrVelY(plr) = 0
    End Sub

    Private Function plr1CollidingAtk() As Integer
        Dim mult As Double
        ' Used for dampening knockback and damage if the player is blocking
        Dim dmgMult As Double = 1
        Dim kbMult As Double = 1
        ' If facing right, apply positive knockback
        ' If facing left, apply negative knockback
        If blnFaceRight(1) Then
            mult = 1
        Else
            mult = -1
        End If

        Dim count As Integer = 0
        ' Run through each attack collider
        For x As Integer = 0 To attackHitbox.GetUpperBound(1)
            ' Check if the player is colliding with any attack
            If plrCollider(0).IntersectsWith(attackHitbox(1, x)) Then
                ' Check if the player is blocking
                If plrGuarding(0) Then
                    ' If they are blocking, create a new shield hitbox
                    createGuardingHitbox(0)
                    ' If debug mode is active, display the shield hitbox
                    If blnDebug Then
                        createGuardingPicture(0)
                    End If

                    ' Check if the attack collides with the shield hitbox
                    ' (This is used to prevent guarding working when hit from behind
                    If attackCollideguard(1, x, 0) Then
                        ' If it does collide with the shield, dampen the attack knockback and damage.
                        mult *= guardKnockback
                        dmgMult = guardDamage
                        kbMult = guardKnockback
                        ' Decrease the shield's health.
                        guardHealth(0) -= 1
                        updateGuardColor(0)
                    End If

                    ' Check if the shield broke after being hit
                    If guardHealth(0) = 0 Then
                        ' Force the player to stop blocking
                        plrGuarding(0) = False
                        deleteguard(0)
                        ' Stun the player and apply the guard break effect
                        blnStunned(0) = True
                        guardBroke(0) = True
                        ' Start regenerating their guard, and guard break them for 3 seconds.
                        tmrRefillGuard(0).Start()
                        tmrGuardBreak(0).Start()

                        ' Check if the player is facing right or left, and apply the corresponding image.
                        If blnFaceRight(0) Then
                            picPlr(0).Image = My.Resources._1RStun
                        Else
                            picPlr(0).Image = My.Resources._1LStun
                        End If
                    End If
                End If

                count += 1
                Dim hit As Boolean = False

                ' Delete the hitbox that damaged the player.
                Dim s As Object
                Dim e As EventArgs
                deleteHit2(s, e)
                ' Check what kind of attack hit the player
                Select Case x
                    Case 0
                        ' If the attack was a standing punch
                        deleteHit2(s, e)
                        damagePlr(5 * dmgMult, 0)

                        plrKbVelX(0) += 15 * mult
                        plrVelY(0) -= 10 * kbMult
                        hit = True
                    Case 1
                        ' If the attack was a standing kick
                        deleteHit2(s, e)
                        damagePlr(7.5 * dmgMult, 0)

                        plrKbVelX(0) += 25 * mult
                        plrVelY(0) -= 15 * kbMult
                        hit = True
                    Case 2
                        ' If the attack was a crouching punch
                        deleteHit2(s, e)
                        damagePlr(7.5 * dmgMult, 0)

                        plrKbVelX(0) += 10 * mult
                        plrVelY(0) -= 30 * kbMult
                        hit = True
                    Case 3
                        ' If the attack was a crouching kick
                        deleteHit2(s, e)
                        damagePlr(10 * dmgMult, 0)

                        plrKbVelX(0) += 35 * mult
                        plrVelY(0) -= 20 * kbMult
                        hit = True
                    Case 4
                        ' If the attack was a hadouken
                        deleteProjectile(0, 1)
                        damagePlr(2 * dmgMult, 0)

                        plrKbVelX(0) += 2 * mult
                        plrVelY(0) -= 2 * kbMult
                        hit = True
                    Case 5
                        ' If the attack was a hadouken
                        deleteProjectile(1, 1)
                        damagePlr(2 * dmgMult, 0)

                        plrKbVelX(0) += 2 * mult
                        plrVelY(0) -= 2 * kbMult
                        hit = True
                End Select

                If hit Then
                    ' Apply a damaged effect
                    damageFlicker(0)
                    ' Stun the player
                    damageStun(0, 330)
                    ' Find the new player's hitbox
                    gatherColliders()
                    ' Check if the player is through the wall after being attacked
                    While plrCollideWall(0)
                        ' Move the player out of the wall
                        picPlr(0).Location = New Point(picPlr(0).Location.X + CInt(-mult * 5), picPlr(0).Location.Y)
                        gatherColliders()
                    End While
                    ' Check if the player is dead
                    If plrHealth(0) <= 1 Then
                        ' Check if the player is facing right or left and apply the corresponding image
                        If blnFaceRight(0) Then
                            picPlr(0).Image = My.Resources._1RDead
                        Else
                            picPlr(0).Image = My.Resources._1LDead
                        End If
                        ' Raise the player 1 dead flag
                        blnDead(0) = True
                        ' Play the death noise
                        soundDie.Play()
                    End If
                End If

                Return x
            End If
        Next

        If count = 0 Then
            Return -1
        End If
    End Function

    Private Function plr2CollidingAtk() As Integer
        ' ------------------------------------------------------------------
        ' ------------------------------------------------------------------
        ' This is the exact same as player 1's colliding attack subprocedure
        ' ------------------------------------------------------------------
        ' ------------------------------------------------------------------
        Dim mult As Double = 1
        Dim dmgMult As Double = 1
        Dim kbMult As Double = 1
        ' If facing right, apply positive knockback
        ' If facing left, apply negative knockback
        If blnFaceRight(0) Then
            mult = 1
        Else
            mult = -1
        End If

        Dim count As Integer = 0
        For x As Integer = 0 To attackHitbox.GetUpperBound(1)
            If plrCollider(1).IntersectsWith(attackHitbox(0, x)) Then
                If plrGuarding(1) Then
                    createGuardingHitbox(1)
                    If blnDebug Then
                        createGuardingPicture(1)
                    End If

                    If attackCollideguard(0, x, 1) Then
                        mult *= guardKnockback
                        dmgMult = guardDamage
                        kbMult = guardKnockback

                        guardHealth(1) -= 1
                        updateGuardColor(1)

                        If guardHealth(1) = 0 Then
                            plrGuarding(1) = False
                            deleteguard(1)
                            plrVelX(1) = 0
                            blnStunned(1) = True
                            guardBroke(1) = True
                            tmrGuardBreak(1).Start()
                            tmrRefillGuard(1).Start()
                        End If
                    End If
                End If

                count += 1
                ' Damage the player and remove the hitbox to prevent being attack multiple times from one attack

                ' Determine which attack and how much knockback
                Dim s As Object
                Dim e As EventArgs
                deleteHit1(s, e)
                Select Case x
                    Case 0
                        deleteHit2(s, e)
                        damagePlr(5 * dmgMult, 1)

                        plrKbVelX(1) += 15 * mult
                        plrVelY(1) -= 10 * kbMult

                    Case 1
                        deleteHit2(s, e)
                        damagePlr(7.5 * dmgMult, 1)

                        plrKbVelX(1) += 25 * mult
                        plrVelY(1) -= 15 * kbMult

                    Case 2
                        deleteHit2(s, e)
                        damagePlr(7.5 * dmgMult, 1)

                        plrKbVelX(1) += 10 * mult
                        plrVelY(1) -= 30 * kbMult

                    Case 3
                        deleteHit2(s, e)
                        damagePlr(10 * dmgMult, 1)

                        plrKbVelX(1) += 35 * mult
                        plrVelY(1) -= 20 * kbMult
                    Case 4
                        deleteProjectile(0, 0)
                        damagePlr(2 * dmgMult, 1)

                        plrKbVelX(1) += 1 * mult
                        plrVelY(1) -= 1 * kbMult
                    Case 5
                        deleteProjectile(1, 0)
                        damagePlr(2 * dmgMult, 1)

                        plrKbVelX(1) += 1 * mult
                        plrVelY(1) -= 1 * kbMult
                End Select

                damageFlicker(1)
                damageStun(1, 330)

                gatherColliders()
                While plrCollideWall(1)
                    picPlr(1).Location = New Point(picPlr(1).Location.X + CInt((-mult * 5)), picPlr(1).Location.Y)
                    gatherColliders()
                End While

                If plrHealth(1) <= 0 Then
                    If blnFaceRight(1) Then
                        picPlr(1).Image = My.Resources._2RDead
                    Else
                        picPlr(1).Image = My.Resources._2LDead
                    End If

                    blnDead(1) = True

                    soundDie.Play()
                End If

                Return x
            End If
        Next

        If count = 0 Then
            Return -1
        End If
    End Function

    Private Function attackCollideguard(atkPlr As Integer, atkIndex As Integer, guardPlr As Integer) As Boolean
        ' Check if the enemy's attack collided with the player's shield
        If attackHitbox(atkPlr, atkIndex).IntersectsWith(guardingHitbox(guardPlr)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub updateGuardColor(plr As Integer)
        ' Apply the new color based off the player's shield health
        guardingPicture(plr).BackColor = guardColors(plr, guardHealth(plr))
    End Sub

    Private Sub damageFlicker(plr As Integer)
        ' Placeholder for the player's original image before being attacked
        Dim idle As Image
        ' Check if its player 1 or 2
        Select Case plr
            Case 0
                ' Check if the player is facing right or left
                If blnFaceRight(1) Then
                    ' Apply the damaged image
                    picPlr(0).Image = My.Resources._1LStun
                    ' Store the player's original image
                    idle = My.Resources._1LIdle
                Else
                    ' Apply the damaged image
                    picPlr(0).Image = My.Resources._1RStun
                    ' Store the player's original image
                    idle = My.Resources._1RIdle
                End If
            Case 1
                If blnFaceRight(0) Then
                    ' Apply the damaged image
                    picPlr(1).Image = My.Resources._2LStun
                    ' Store the player's original image
                    idle = My.Resources._2LIdle
                Else
                    ' Apply the damaged image
                    picPlr(1).Image = My.Resources._2RStun
                    ' Store the player's original image
                    idle = My.Resources._2RIdle
                End If
        End Select
        Dim tmrColor As Timer = New Timer()
        ' Apply the damaged image for 0.25 seconds
        tmrColor.Interval = 250
        tmrColor.Start()
        AddHandler tmrColor.Tick, Sub()
                                      ' Reapply the idle image after 0.25 seconds
                                      picPlr(plr).Image = idle
                                      tmrColor.Stop()
                                  End Sub
    End Sub

    Private Sub damageStun(plr As Integer, time As Integer)
        ' Reverse the player's velocity to stop them from moving
        plrVelX(plr) *= -2
        ' Stun the player
        blnStunned(plr) = True

        Dim tmrStun As Timer = New Timer()
        ' The time in this case is set from the calling subprocedure
        tmrStun.Interval = time
        tmrStun.Start()
        AddHandler tmrStun.Tick, Sub()
                                     ' Un-stun the player
                                     blnStunned(plr) = False
                                     tmrStun.Stop()
                                 End Sub
    End Sub

    Private Sub clipOutFloor(plr As Integer)
        ' Find the player's hitbox
        gatherColliders()
        ' Check if they are inside of the floor
        While plrCollider(plr).IntersectsWith(floorCollider)
            ' Move them out of the floor 1 pixel at a time
            picPlr(plr).Top -= 1
            ' After each loop, get the hitbox again
            gatherColliders()
        End While
    End Sub

    Public Sub updateMovementAnim(plr As Integer)
        ' Find which player is moving
        Select Case plr
            Case 0
                ' Movement Animations

                ' Check if the player is moving left, ducking, or guardbroke and apply the corresponding animations
                If keysHeld(0) Then
                    If Not guardBroke(plr) Then
                        If Not blnDuck(plr) Then
                            picPlr(plr).Image = My.Resources._1LForward
                        Else
                            picPlr(plr).Image = My.Resources._1LCrouch
                        End If
                    End If
                    ' Check if the player is moving right, ducking, or guardbroke and apply the corresponding animations
                ElseIf keysHeld(1) Then
                    If Not guardBroke(plr) Then
                        If Not blnDuck(plr) Then
                            picPlr(plr).Image = My.Resources._1RForward
                        Else
                            picPlr(plr).Image = My.Resources._1RCrouch
                        End If
                    End If
                    ' Check if the player is idle, ducking, or guardbroke and apply the corresponding animations
                ElseIf Not keysHeld(plr) And Not keysHeld(1) Then
                    If Not blnDuck(plr) Then
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._1RIdle
                        Else
                            picPlr(plr).Image = My.Resources._1LIdle
                        End If
                    Else
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._1RCrouch
                        Else
                            picPlr(plr).Image = My.Resources._1LCrouch
                        End If
                    End If
                End If
            Case 1
                ' Movement Animations

                ' Check if the player is moving left, ducking, or guardbroke and apply the corresponding animations
                If keysHeld(2) Then
                    If Not guardBroke(plr) Then
                        If Not blnDuck(plr) Then
                            picPlr(plr).Image = My.Resources._2LForward
                        Else
                            picPlr(plr).Image = My.Resources._2LCrouch
                        End If
                    End If

                    ' Check if the player is moving right, ducking, or guardbroke and apply the corresponding animations
                ElseIf keysHeld(3) Then
                    If Not guardBroke(plr) Then
                        If Not blnDuck(plr) Then
                            picPlr(plr).Image = My.Resources._2RForward
                        Else
                            picPlr(plr).Image = My.Resources._2RCrouch
                        End If
                    End If

                    ' Check if the player is moving idle, ducking, or guardbroke and apply the corresponding animations
                ElseIf Not keysHeld(2) And Not keysHeld(3) Then
                    If Not blnDuck(plr) Then
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._2RIdle
                        Else
                            picPlr(plr).Image = My.Resources._2LIdle
                        End If
                    Else
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._2RCrouch
                        Else
                            picPlr(plr).Image = My.Resources._2LCrouch
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub updateAtkAnim(plr As Integer)
        ' Find which attack the player is doing
        Dim index As Integer = findAttackIndex(plr)
        ' 99 is the default index for no attack

        ' If the player is NOT doing NO attack
        If index <> 99 Then
            ' Check which player is attacking
            Select Case plr
                Case 0
                    ' Attacking Animations
                    Select Case index
                    ' Applys the corresponding image for Idle Punch
                        Case 0
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RPunch
                            Else
                                picPlr(plr).Image = My.Resources._1LPunch
                            End If
                    ' Applys the corresponding image for Idle Kick
                        Case 1
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RKick
                            Else
                                picPlr(plr).Image = My.Resources._1LKick
                            End If
                    ' Applys the corresponding image for Crouching Punch
                        Case 2
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RCrouchPunch
                            Else
                                picPlr(plr).Image = My.Resources._1LCrouchPunch
                            End If
                    ' Applys the corresponding image for Crouching Kick
                        Case 3
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RCrouchKick
                            Else
                                picPlr(plr).Image = My.Resources._1LCrouchKick
                            End If
                    ' Applys the corresponding image for Hadouken
                        Case 4
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RHadouken
                            Else
                                picPlr(plr).Image = My.Resources._1LHadouken
                            End If
                        Case 5
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._1RHadouken
                            Else
                                picPlr(plr).Image = My.Resources._1LHadouken
                            End If
                        Case 99
                            ' Do Nothing
                    End Select
                Case 1
                    ' Attacking Animations
                    Select Case index
                    ' Applys the corresponding image for Idle Punch
                        Case 0
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RPunch
                            Else
                                picPlr(plr).Image = My.Resources._2LPunch
                            End If
                    ' Applys the corresponding image for Idle Kick
                        Case 1
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RKick
                            Else
                                picPlr(plr).Image = My.Resources._2LKick
                            End If
                    ' Applys the corresponding image for Crouching Punch
                        Case 2
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RCrouchPunch
                            Else
                                picPlr(plr).Image = My.Resources._2lCrouchPunch
                            End If
                    ' Applys the corresponding image for Crouching Kick
                        Case 3
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RCrouchKick
                            Else
                                picPlr(plr).Image = My.Resources._2lCrouchKick
                            End If
                    ' Applys the corresponding image for Hadouken
                        Case 4
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RHadouken
                            Else
                                picPlr(plr).Image = My.Resources._2LHudoken
                            End If
                        Case 5
                            If blnFaceRight(plr) Then
                                picPlr(plr).Image = My.Resources._2RHadouken
                            Else
                                picPlr(plr).Image = My.Resources._2LHudoken
                            End If
                        Case 99
                            ' Do Nothing
                    End Select
            End Select
        End If

        Dim animTmr As Timer = New Timer()
        ' After 400 ms
        animTmr.Interval = 400
        animTmr.Start()
        AddHandler animTmr.Tick, Sub()
                                     ' Reapply the corresponding movement image
                                     updateMovementAnim(plr)
                                     animTmr.Stop()
                                 End Sub
    End Sub

    Private Function findAttackIndex(plr As Integer) As Integer
        ' Apply the default index
        Dim index As Integer = 99
        ' Run through each type of attack
        For x As Integer = 0 To attackHitbox.GetUpperBound(1)
            ' Check if the attack exists or not
            If attackHitbox(plr, x).Height <> 0 Then
                ' If it does, get the index of it
                index = x
            End If
        Next
        ' Return the attack's index
        Return index
    End Function

    Private Sub updateGuardAnim(plr As Integer)
        ' Check which player is guarding
        Select Case plr
            Case 0
                ' Check if the player is guarding or not
                If plrGuarding(plr) Then
                    ' Check if the player is ducking or not
                    If blnDuck(plr) Then
                        ' Check if the player is facing left or right and apply the corresponding image
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._1RCrouchBlock
                        Else
                            picPlr(plr).Image = My.Resources._1LCrouchBlock
                        End If
                    Else
                        ' Check if the player is facing left or right and apply the corresponding image
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._1RBlock
                        Else
                            picPlr(plr).Image = My.Resources._1LBlock
                        End If
                    End If
                End If
            Case 1
                ' Check if the player is guarding or not
                If plrGuarding(plr) Then
                    ' Check if the player is ducking or not
                    If blnDuck(plr) Then
                        ' Check if the player is facing left or right and apply the corresponding image
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._2RCrouchBlock
                        Else
                            picPlr(plr).Image = My.Resources._2LCrouchBlock
                        End If
                    Else
                        ' Check if the player is facing left or right and apply the corresponding image
                        If blnFaceRight(plr) Then
                            picPlr(plr).Image = My.Resources._2RBlock
                        Else
                            picPlr(plr).Image = My.Resources._2LBlock
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub updateGuardBrokeAnim(plr As Integer)
        ' Check which player got guard broke
        Select Case plr
            Case 0
                ' Check if the player is facing left or right and apply the corresponding image
                If blnFaceRight(0) Then
                    picPlr(plr).Image = My.Resources._1RStun
                Else
                    picPlr(plr).Image = My.Resources._1LStun
                End If
            Case 1
                ' Check if the player is facing left or right and apply the corresponding image
                If blnFaceRight(1) Then
                    picPlr(plr).Image = My.Resources._2RStun
                Else
                    picPlr(plr).Image = My.Resources._2LStun
                End If
        End Select
    End Sub

    Private Sub updateImage(plr As Integer)
        ' Check if the BOTH players are alive
        If Not blnDead(0) And Not blnDead(1) Then
            ' Make sure the player isnt guard broke
            If Not guardBroke(plr) Then
                ' Check if the player is guarding or not
                If Not plrGuarding(plr) Then
                    ' Apply the corresponding attack / movement animation if they arent guarding
                    picPlr(plr).SizeMode = PictureBoxSizeMode.Zoom
                    updateMovementAnim(plr)
                    picPlr(plr).SizeMode = PictureBoxSizeMode.StretchImage
                    updateAtkAnim(plr)
                Else
                    ' If they are guarding, apply the guarding animation
                    picPlr(plr).SizeMode = PictureBoxSizeMode.StretchImage
                    updateGuardAnim(plr)
                End If
            Else
                ' If they are guard broke, apply the guard broke animation
                updateGuardBrokeAnim(plr)
            End If
        End If

    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not blnDead(0) And Not blnDead(1) Then
            ' Attack List: Punch, Kick, Down Punch, Down Kick, Fireball 1, Fireball 2
            '                0      1        2          3         4            5
            Select Case e.KeyCode
            ' Player 1 controls
                Case Keys.A
                    ' Raise the flag for holding the key
                    keysHeld(0) = True
                    ' Change the direction of the player
                    blnFaceRight(0) = False
                    ' Update the image of the player
                    updateImage(0)
                Case Keys.D
                    ' Raise the flag for holding the key
                    keysHeld(1) = True
                    ' Change the direction of the player
                    blnFaceRight(0) = True
                    ' Update the image of the player
                    updateImage(0)
                Case Keys.W
                    ' Check if the player is not mid-air, not guard broke, and not stunned
                    If plrCollidingWithFloor(0) And Not guardBroke(0) And Not blnStunned(0) Then
                        ' If these all come to true, change their Y-velocity to the jump power
                        plrVelY(0) = dblJUMPPOWER(0)
                    End If
                Case Keys.S
                    ' Make sure the player isnt ducking already
                    ' (This is because otherwise the player's height will be cut in half infinitely)
                    If Not blnDuck(0) And Not guardBroke(0) Then
                        blnDuck(0) = True
                        ' Make them have half of their original height
                        picPlr(0).Height = CInt(0.5 * picPlr(0).Height)
                        ' Move them downward so they arent floating in the air after ducking
                        picPlr(0).Location = New Point(picPlr(0).Location.X, picPlr(0).Location.Y + CInt((picPlr(0).Height * 0.5)))
                        ' Move them out of the floor incase they clip through on accident
                        clipOutFloor(0)
                        ' Apply the ducking image
                        updateImage(0)
                    End If
                Case Keys.R
                    ' Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(0) And Not blnStunned(0) And Not plrGuarding(0) And Not guardBroke(0) Then
                        ' If the player is ducking, apply the uppercut hitbox
                        If blnDuck(0) Then
                            createAttackHitbox(0, 2)
                            ' If debug mode is enabled, visualize the hitbox
                            If blnDebug Then
                                updateAttackPictures(0, 2)
                            End If

                            ' Enable the cooldown
                            startCooldown(0)
                            ' Play the punching sound
                            soundPunch.Play()
                        Else
                            ' If the player is standing, apply the punching hitbox
                            createAttackHitbox(0, 0)
                            ' If debug mode is enabled, visualize the hitbox
                            If blnDebug Then
                                updateAttackPictures(0, 0)
                            End If
                            ' Enable the cooldown
                            startCooldown(0)
                            ' Play the punching sound
                            soundPunch.Play()
                        End If
                        ' Apply the corresponding image
                        updateImage(0)
                    End If
                Case Keys.F
                    'Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(0) And Not blnStunned(0) And Not plrGuarding(0) And Not guardBroke(0) Then
                        If blnDuck(0) Then
                            createAttackHitbox(0, 3)

                            If blnDebug Then
                                updateAttackPictures(0, 3)
                            End If


                            startCooldown(0)
                        Else
                            createAttackHitbox(0, 1)
                            If blnDebug Then
                                updateAttackPictures(0, 1)
                            End If

                            startCooldown(0)
                        End If

                        soundKick.Play()
                        updateImage(0)
                    End If
                Case Keys.C
                    ' Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(0) And Not blnStunned(0) And Not plrGuarding(0) And Not blnDuck(0) And Not guardBroke(0) Then
                        If attackHitbox(0, 4).Height = 0 Then
                            ' Create the hadouken hitbox
                            createAttackHitbox(0, 4)
                            ' Create the hadouken image
                            updateAttackPictures(0, 4)
                            ' Apply the correct direction to the hadouken
                            projectileFaceRight(0, 0) = blnFaceRight(0)
                        ElseIf attackHitbox(0, 5).Height = 0 Then
                            ' Create the hadouken hitbox
                            createAttackHitbox(0, 5)
                            ' Create the hadouken image
                            updateAttackPictures(0, 5)
                            ' Apply the correct direction to the hadouken
                            projectileFaceRight(0, 1) = blnFaceRight(0)
                        End If
                        ' Apply the cooldown
                        startCooldown(0)
                        ' Apply the hadouken animation
                        updateImage(0)
                        ' Play the hadouken sound
                        soundHadouken.Play()
                    End If
                Case Keys.G
                    ' Make sure the player isnt guardbroke or stunned
                    If guardHealth(0) <> 0 And Not blnStunned(0) And Not guardBroke(0) Then
                        ' Make the guarding hitbox
                        createGuardingHitbox(0)
                        If blnDebug Then
                            ' If debug is enabled, visualize the guarding hitbox
                            createGuardingPicture(0)
                        End If
                        ' Raise the guarding flag
                        plrGuarding(0) = True
                        ' Decrease their jump height by half
                        dblJUMPPOWER(0) = -12.5
                        ' Stop increasing their guard
                        tmrRefillGuard(0).Stop()
                        ' Apply the guarding animation
                        updateImage(0)
                    End If
            End Select

            ' -------------------------------------------------------------
            ' -------------------------------------------------------------
            ' Code is the exact same as player 1 but with different indexes
            ' -------------------------------------------------------------
            ' -------------------------------------------------------------
            ' Player 2 Keys
            Select Case e.KeyCode
                Case Keys.Left
                    keysHeld(2) = True
                    blnFaceRight(1) = False

                    updateImage(1)

                Case Keys.Right
                    keysHeld(3) = True
                    blnFaceRight(1) = True

                    updateImage(1)

                Case Keys.Up
                    If plrCollidingWithFloor(1) And Not blnStunned(1) And Not guardBroke(1) Then
                        plrVelY(1) = dblJUMPPOWER(1)
                    End If
                Case Keys.Down
                    If Not blnDuck(1) And Not guardBroke(1) Then
                        blnDuck(1) = True
                        picPlr(1).Height = CInt(0.5 * picPlr(1).Height)
                        picPlr(1).Location = New Point(picPlr(1).Location.X, picPlr(1).Location.Y + CInt(picPlr(1).Height * 0.5))

                        updateImage(1)

                        clipOutFloor(1)
                    End If
                Case Keys.NumPad4
                    ' Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(1) And Not blnStunned(1) And Not plrGuarding(1) And Not guardBroke(1) Then
                        If blnDuck(1) Then
                            createAttackHitbox(1, 2)
                            If blnDebug Then
                                updateAttackPictures(1, 2)
                            End If
                            startCooldown(1)

                            updateImage(1)

                            soundPunch.Play()
                        Else
                            createAttackHitbox(1, 0)
                            If blnDebug Then
                                updateAttackPictures(1, 0)
                            End If

                            startCooldown(1)

                            updateImage(1)

                            soundPunch.Play()
                        End If
                    End If
                Case Keys.NumPad1
                    ' Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(1) And Not blnStunned(1) And Not plrGuarding(1) And Not guardBroke(1) Then
                        If blnDuck(1) Then
                            createAttackHitbox(1, 3)
                            If blnDebug Then
                                updateAttackPictures(1, 3)
                            End If

                            startCooldown(1)

                            updateImage(1)
                        Else
                            createAttackHitbox(1, 1)
                            If blnDebug Then
                                updateAttackPictures(1, 1)
                            End If

                            startCooldown(1)

                            updateImage(1)

                            soundKick.Play()
                        End If
                    End If
                Case Keys.NumPad0
                    ' Make sure the player isnt stunned, guarding, guard broke, or on cooldown.
                    If Not blnPunchCooldown(1) And Not blnStunned(1) And Not plrGuarding(1) And Not blnDuck(1) And Not guardBroke(1) Then
                        If attackHitbox(1, 4).Height = 0 Then
                            createAttackHitbox(1, 4)
                            updateAttackPictures(1, 4)
                            projectileFaceRight(1, 0) = blnFaceRight(1)
                        ElseIf attackHitbox(1, 5).Height = 0 Then
                            createAttackHitbox(1, 5)
                            updateAttackPictures(1, 5)
                            projectileFaceRight(1, 1) = blnFaceRight(1)
                        End If

                        updateImage(1)
                        startCooldown(1)
                        soundHadouken.Play()
                    End If
                Case Keys.NumPad5
                    ' Make sure the player isnt guardbroke or stunned
                    If guardHealth(1) <> 0 And Not blnStunned(1) And Not guardBroke(1) Then
                        createGuardingHitbox(1)
                        If blnDebug Then
                            createGuardingPicture(1)
                        End If
                        plrGuarding(1) = True
                        dblJUMPPOWER(1) = -12.5
                        tmrRefillGuard(1).Stop()

                        updateImage(1)
                    End If
            End Select
        End If
    End Sub

    Private Sub startCooldown(plr As Integer)
        ' Make the hitbox alive timer start counting
        aliveTmr(plr).Start()
        ' Make the cooldown timer start counting
        atkTmr(plr).Start()
        ' Put the player on cooldown
        blnPunchCooldown(plr) = True
    End Sub

    Private Sub createGuardingHitbox(plr As Integer)
        ' Check if the player is ducking or not
        If Not blnDuck(plr) Then
            ' Check if the player is facing right or left and apply the correct hitbox
            If blnFaceRight(plr) Then
                guardingHitbox(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), CInt(picPlr(plr).Size.Height * 0.5))
                guardingHitbox(plr).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
            Else
                guardingHitbox(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), CInt(picPlr(plr).Size.Height * 0.5))
                guardingHitbox(plr).Location = New Point(CInt(picPlr(plr).Location.X - (picPlr(plr).Size.Width * 0.5)), picPlr(plr).Location.Y)
            End If
        Else
            ' Check if the player is facing right or left and apply the correct hitbox
            If blnFaceRight(plr) Then
                guardingHitbox(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Size.Height)
                guardingHitbox(plr).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
            Else
                guardingHitbox(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Size.Height)
                guardingHitbox(plr).Location = New Point(picPlr(plr).Location.X - CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Location.Y)
            End If
        End If
    End Sub

    Private Sub createGuardingPicture(plr As Integer)
        ' Check if the player is ducking or not
        If Not blnDuck(plr) Then
            ' Check if the player is facing right or left and apply the correct hitbox
            If blnFaceRight(plr) Then
                guardingPicture(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), CInt(picPlr(plr).Size.Height * 0.5))
                guardingPicture(plr).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
            Else
                guardingPicture(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), CInt(picPlr(plr).Size.Height * 0.5))
                guardingPicture(plr).Location = New Point(CInt(picPlr(plr).Location.X - (picPlr(plr).Size.Width * 0.5)), picPlr(plr).Location.Y)
            End If
        Else
            ' Check if the player is facing right or left and apply the correct hitbox
            If blnFaceRight(plr) Then
                guardingPicture(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Size.Height)
                guardingPicture(plr).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
            Else
                guardingPicture(plr).Size = New Size(CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Size.Height)
                guardingPicture(plr).Location = New Point(picPlr(plr).Location.X - CInt(picPlr(plr).Size.Width * 0.5), picPlr(plr).Location.Y)
            End If
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If Not blnDead(0) And Not blnDead(1) Then
            Select Case e.KeyCode
            ' Player 1 Controls
                Case Keys.A
                    ' Show that the player isnt moving left anymore
                    keysHeld(0) = False
                    ' Apply the idle animation
                    updateImage(0)

                Case Keys.D
                    ' Make it so the player isnt moving right anymore
                    keysHeld(1) = False
                    ' Apply the idle animation
                    updateImage(0)
                Case Keys.S
                    ' Check if the player is ducking
                    If blnDuck(0) Then
                        ' If they are ducking, make them stand up
                        blnDuck(0) = False
                        picPlr(0).Height *= 2
                        ' Check if they are facing right or left and apply the correct idle animation
                        If blnFaceRight(0) Then
                            picPlr(0).Image = My.Resources._1RIdle
                        Else
                            picPlr(0).Image = My.Resources._1LIdle
                        End If

                        clipOutFloor(0)
                    End If
                Case Keys.G
                    ' Make it so they arent guarding anymore
                    plrGuarding(0) = False
                    ' Reset their jump height to default again
                    dblJUMPPOWER(0) = -25
                    ' Delete the guarding hitbox
                    deleteguard(0)
                    ' Start refilling their guard health again
                    tmrRefillGuard(0).Start()

            '--------------------------------------------------------------
            '--------------------------------------------------------------
            ' Player 2 code is same as Player 1 code with different indexes
            '--------------------------------------------------------------
            '--------------------------------------------------------------

            ' Player 2 Controls
                Case Keys.Left
                    keysHeld(2) = False

                    If blnDuck(1) Then
                        picPlr(1).Image = My.Resources._2LCrouch
                    Else
                        picPlr(1).Image = My.Resources._2LIdle
                    End If
                Case Keys.Right
                    keysHeld(3) = False

                    If blnDuck(1) Then
                        picPlr(1).Image = My.Resources._2RCrouch
                    Else
                        picPlr(1).Image = My.Resources._2RIdle
                    End If
                Case Keys.Down
                    If blnDuck(1) Then
                        blnDuck(1) = False
                        picPlr(1).Height *= 2

                        If blnFaceRight(1) Then
                            picPlr(1).Image = My.Resources._2RIdle
                        Else
                            picPlr(1).Image = My.Resources._2LIdle
                        End If

                        clipOutFloor(1)
                    End If

                Case Keys.NumPad5
                    plrGuarding(1) = False
                    dblJUMPPOWER(1) = -25
                    deleteguard(1)
                    tmrRefillGuard(1).Start()
            End Select
            ' Removes the player's velocity if they arent holding any keys
            If keysHeld(0) = False And keysHeld(1) = False Then
                plrVelX(0) = 0
            End If
            If keysHeld(2) = False And keysHeld(3) = False Then
                plrVelX(1) = 0
            End If
        End If
    End Sub

    Public Sub start()
        'creates wall colliders
        wallColliderL = New Rectangle(picEdgeL.Location, picEdgeL.Size)
        wallColliderR = New Rectangle(picEdgeR.Location, picEdgeR.Size)
        ' sets default intervals
        aliveTmr(0).Interval = 100
        aliveTmr(1).Interval = 100
        'Apply the timers subprocedure
        AddHandler aliveTmr(0).Tick, AddressOf deleteHit1
        AddHandler aliveTmr(1).Tick, AddressOf deleteHit2

        ' Set the default intervals
        atkTmr(0).Interval = 500
        atkTmr(1).Interval = 500

        ' Apply the timers subprocedure
        AddHandler atkTmr(0).Tick, AddressOf atkCooldown1
        AddHandler atkTmr(1).Tick, AddressOf atkCooldown2

        ' Allows the picture boxes to be used with the array
        picPlr(0) = Me.picPlr1
        picPlr(1) = Me.picPlr2

        ' Allows the labels to be used with the array
        lblHealthPlr(0) = Me.lblHealthPlr1
        lblHealthPlr(1) = Me.lblHealthPlr2

        ' Allows the labels to be used with the array
        lblHealthPlrRed(0) = Me.lblHealthPlr1Red
        lblHealthPlrRed(1) = Me.lblHealthPlr2Red

        ' Set default values for each attack image
        For x As Integer = 0 To attackPicture.GetUpperBound(1)
            attackPicture(0, x).BackColor = Color.ForestGreen
            attackPicture(1, x).BackColor = Color.Orchid

            Me.Controls.Add(attackPicture(0, x))
            Me.Controls.Add(attackPicture(1, x))

            attackPicture(0, x).BringToFront()
            attackPicture(1, x).BringToFront()

            attackPicture(0, x).Location = New Point(2000, 2000)
            attackPicture(1, x).Location = New Point(2000, 2000)
        Next

        ' Set default values for the guarding pictures
        guardingPicture(0).Location = New Point(2000, 2000)
        guardingPicture(1).Location = New Point(2000, 2000)
        guardingPicture(0).BackColor = Color.ForestGreen
        guardingPicture(1).BackColor = Color.Orchid
        guardingPicture(0).Size = New Size(5, 5)
        guardingPicture(1).Size = New Size(5, 5)
        guardingPicture(0).BringToFront()
        guardingPicture(1).BringToFront()

        ' Add the guarding pictures to the form
        Me.Controls.Add(guardingPicture(0))
        Me.Controls.Add(guardingPicture(1))

        ' Bring the pictures to the front to avoid z-fighting with the player
        guardingPicture(0).BringToFront()
        guardingPicture(1).BringToFront()

        ' Apply default intervals
        tmrRefillGuard(0).Interval = 1000
        tmrRefillGuard(1).Interval = 1000

        ' Use tags to determine which index the timer is in
        tmrRefillGuard(0).Tag = "0"
        tmrRefillGuard(1).Tag = "1"

        ' Set the timers subprocedure
        AddHandler tmrRefillGuard(0).Tick, AddressOf refillguard
        AddHandler tmrRefillGuard(1).Tick, AddressOf refillguard

        ' Set the picture boxes into the array
        guardIcons = {{Me.picGuard1, Me.picGuard2, Me.picGuard3}, {Me.picGuard4, Me.picGuard5, Me.picGuard6}}
        ammoIcons = {{Me.picAmmo1, Me.picAmmo2}, {Me.picAmmo3, Me.picAmmo4}}

        ' Use tags to determine which index the timer is in
        tmrGuardBreak(0).Tag = "0"
        tmrGuardBreak(1).Tag = "1"

        ' Apply default intervals
        tmrGuardBreak(0).Interval = 1500
        tmrGuardBreak(1).Interval = 1500

        ' Set the timers subprocedure
        AddHandler tmrGuardBreak(0).Tick, AddressOf regainSpeed
        AddHandler tmrGuardBreak(1).Tick, AddressOf regainSpeed

        ' Apply default idle animations
        picPlr(0).Image = My.Resources._1RIdle
        picPlr(1).Image = My.Resources._2LIdle

        ' Loop through each picture and make it transparent + stretched
        For x As Integer = 0 To 1
            For y As Integer = 4 To 5
                attackPicture(x, y).BackColor = Color.Transparent
                attackPicture(x, y).SizeMode = PictureBoxSizeMode.StretchImage
            Next
        Next

        ' Show the player's hitbox if debug mode is enabled
        If blnDebug Then
            picPlr(0).BackColor = originalColor(0)
            picPlr(1).BackColor = originalColor(1)
        End If
        ' Reset what keys are held
        For x As Integer = 0 To 3
            keysHeld(x) = False
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        start()
    End Sub

    Private Sub regainSpeed(sender As Object, e As EventArgs)
        ' Cast the sender into a timer to figure out which timer it is
        Dim tmr As Timer = DirectCast(sender, Timer)
        Select Case tmr.Tag.ToString
            Case "0"
                ' Allow the player to move again
                guardBroke(0) = False
                tmrGuardBreak(0).Stop()
            Case "1"
                guardBroke(1) = False
                tmrGuardBreak(1).Stop()
        End Select
    End Sub

    Private Sub refillguard(sender As Object, e As EventArgs)
        ' Cast the sender into a timer to figure out which timer it is
        Dim tmr As Timer = DirectCast(sender, Timer)
        Select Case tmr.Tag.ToString
            Case "0"
                ' If the player isnt at max guard, then slowly regenerate
                If guardHealth(0) < 3 Then
                    guardHealth(0) += 1
                End If
                updateGuardColor(0)
            Case "1"
                If guardHealth(1) < 3 Then
                    guardHealth(1) += 1
                End If
                updateGuardColor(1)
        End Select
    End Sub

    Private Sub createAttackHitbox(plr As Integer, atk As Integer)
        ' Stun the player for 100 ms
        damageStun(plr, 100)
        ' Check if the player is facing right or left and create the corresponding hitbox
        Select Case blnFaceRight(plr)
            Case False
                Select Case atk
                    Case 0
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 70, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.05))
                        attackHitbox(plr, atk).Size = New Size(70, 30)
                    Case 1
                        attackHitbox(plr, atk).Size = New Size(85, 40)
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 85, picPlr(plr).Location.Y + CInt((picPlr(plr).Height * 0.85) - 25))
                    Case 2
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 55, picPlr(plr).Location.Y)
                        attackHitbox(plr, atk).Size = New Size(55, 30)
                    Case 3
                        attackHitbox(plr, atk).Size = New Size(85, 40)
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 85, picPlr(plr).Location.Y + (picPlr(plr).Height - 40))
                    Case 4
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 40, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackHitbox(plr, atk).Size = New Size(40, 30)
                    Case 5
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X - 40, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackHitbox(plr, atk).Size = New Size(40, 30)
                End Select
            Case True
                Select Case atk
                    Case 0
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.05))
                        attackHitbox(plr, atk).Size = New Size(70, 30)
                    Case 1
                        attackHitbox(plr, atk).Size = New Size(85, 40)
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt((picPlr(plr).Height * 0.85) - 25))
                    Case 2
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
                        attackHitbox(plr, atk).Size = New Size(55, 30)
                    Case 3
                        attackHitbox(plr, atk).Size = New Size(85, 40)
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + (picPlr(plr).Height - 40))
                    Case 4
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackHitbox(plr, atk).Size = New Size(40, 30)
                    Case 5
                        attackHitbox(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackHitbox(plr, atk).Size = New Size(40, 30)
                End Select
        End Select
    End Sub

    Private Sub updateAttackPictures(plr As Integer, atk As Integer)
        ' Check if the player is facing right or left and create the corresponding hitbox
        Select Case blnFaceRight(plr)
            Case False
                Select Case atk
                    Case 0
                        attackPicture(plr, atk).Size = New Size(70, 30)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 70, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.05))
                    Case 1
                        attackPicture(plr, atk).Size = New Size(85, 40)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 85, picPlr(plr).Location.Y + CInt((picPlr(plr).Height * 0.85) - 25))
                    Case 2
                        attackPicture(plr, atk).Size = New Size(55, 30)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 55, picPlr(plr).Location.Y)
                    Case 3
                        attackPicture(plr, atk).Size = New Size(85, 40)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 85, picPlr(plr).Location.Y + (picPlr(plr).Height - 40))
                    Case 4
                        attackPicture(plr, atk).Image = My.Resources.HadoukenL
                        attackPicture(plr, atk).Size = New Size(40, 30)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 40, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))

                    Case 5
                        attackPicture(plr, atk).Image = My.Resources.HadoukenL
                        attackPicture(plr, atk).Size = New Size(40, 30)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X - 40, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                End Select
            Case True
                Select Case atk
                    Case 0
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.05))
                        attackPicture(plr, atk).Size = New Size(70, 30)
                    Case 1
                        attackPicture(plr, atk).Size = New Size(85, 40)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt((picPlr(plr).Height * 0.85) - 25))
                    Case 2
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y)
                        attackPicture(plr, atk).Size = New Size(55, 30)
                    Case 3
                        attackPicture(plr, atk).Size = New Size(85, 40)
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + (picPlr(plr).Height - 40))
                    Case 4
                        attackPicture(plr, atk).Image = My.Resources.HadoukenR
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackPicture(plr, atk).Size = New Size(40, 30)
                    Case 5
                        attackPicture(plr, atk).Image = My.Resources.HadoukenR
                        attackPicture(plr, atk).Location = New Point(picPlr(plr).Location.X + picPlr(plr).Width, picPlr(plr).Location.Y + CInt(picPlr(plr).Height * 0.2))
                        attackPicture(plr, atk).Size = New Size(40, 30)
                End Select
        End Select
    End Sub

    Private Sub atkCooldown1(sender As Object, e As EventArgs)
        ' Disable cooldown
        blnPunchCooldown(0) = False
    End Sub

    Private Sub atkCooldown2(sender As Object, e As EventArgs)
        ' Disable cooldown
        blnPunchCooldown(1) = False
    End Sub

    Private Sub deleteHit1(sender As Object, e As EventArgs)
        ' Make the hitbox have 0 size, then place the picture at the edge of the map
        For x As Integer = 0 To 3
            attackHitbox(0, x) = New Rectangle(0, 0, 0, 0)
            attackPicture(0, x).Location = New Point(2000, 2000)
        Next
        aliveTmr(0).Stop()
    End Sub

    Private Sub deleteProjectile(num As Integer, plr As Integer)
        ' Make the hitbox have 0 size, then place the picture at the edge of the map
        attackHitbox(plr, 4 + num) = New Rectangle(0, 0, 0, 0)
        attackPicture(plr, 4 + num).Location = New Point(2000, 2000)
    End Sub

    Private Sub deleteguard(plr As Integer)
        ' Make the hitbox have 0 size, then place the picture at the edge of the map
        guardingHitbox(plr) = New Rectangle(0, 0, 0, 0)
        guardingPicture(plr).Location = New Point(2000, 2000)
    End Sub

    Private Sub deleteHit2(sender As Object, e As EventArgs)
        ' Loop through every attack and put it at the edge of the map
        For x As Integer = 0 To 3
            attackHitbox(1, x) = New Rectangle(0, 0, 0, 0)
            attackPicture(1, x).Location = New Point(2000, 2000)
        Next
        aliveTmr(1).Stop()
    End Sub

    Private Sub btnReplay_Click(sender As Object, e As EventArgs) Handles btnReplay.Click
        ' Restart the game
        restart()
    End Sub

    Public Sub restart()
        ' Reset default guard health
        guardHealth(0) = 3
        guardHealth(1) = 3
        ' Reset default player health
        plrHealth(0) = 250
        plrHealth(1) = 250
        ' Reset player position
        picPlr(0).Location = New Point(12, 93)
        picPlr(1).Location = New Point(716, 93)
        ' Reset player ducking, height, and gravity
        For x As Integer = 0 To 1
            If blnDuck(x) Then
                blnDuck(x) = False
                picPlr(x).Height *= 2
                dblGRAVITY(x) = 3
            End If
        Next
        ' Reset health bar size
        lblHealthPlr1.Size = lblHealthPlr1Red.Size
        lblHealthPlr2.Size = lblHealthPlr2Red.Size
        ' Reset health bar position
        lblHealthPlr1.Location = lblHealthPlr1Red.Location
        lblHealthPlr2.Location = lblHealthPlr2Red.Location
        ' Reset death flag
        blnDead(0) = False
        blnDead(1) = False
        ' Reset stunned flag
        blnStunned(0) = False
        blnStunned(1) = False
        ' Reset guard broke flag
        guardBroke(0) = False
        guardBroke(1) = False
        ' Reset default speed
        plrSpeed(0) = 3
        plrSpeed(1) = 3
        ' Hide replay controls
        btnReplay.Visible = False
        btnMainMenu.Visible = False
        lblWinner.Visible = False
        ' Reset player speed
        plrVelX(0) = 0
        plrVelX(1) = 0
        plrVelY(0) = 0
        plrVelY(1) = 0
        ' Reset animation
        picPlr(0).Image = My.Resources._1RIdle
        picPlr(1).Image = My.Resources._2LIdle
        ' Reset keys held array
        For x As Integer = 0 To 3
            keysHeld(x) = False
        Next
    End Sub

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        ' Hide game form
        Me.Hide()
        ' Show main menu form
        frmMainMenu.Show()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Close the application fully
        Application.ExitThread()
    End Sub
End Class