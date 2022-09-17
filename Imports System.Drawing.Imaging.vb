Imports System.Drawing.Imaging
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.VisualBasic.Devices

Public Class Form1
    ' Yeong Kim
    ' PacMan Project
    ' 2020/05/22

    Dim Title As Image
    ' PacMan
    Dim PacMan(5) As Image
    Dim PacDirection As String = "R"
    Dim NextPacDirection As String = "R"
    Dim PacSpeed As Short = 10

    ' Ghost
    Dim Ghosts(5) As Image
    Dim Ghost1Direction As String = "R"
    Dim Ghost2Direction As String = "R"
    Dim Ghost3Direction As String = "D"
    Dim Ghost4Direction As String = "D"

    Dim NextGhost1Direction As String = "D"
    Dim NextGhost2Direction As String = "D"
    Dim NextGhost3Direction As String = "R"
    Dim NextGhost4Direction As String = "R"

    Dim GhostSpeed As Short = 5

    ' Others
    Dim Counter As Long = 0
    Dim Wall(-1) As PictureBox
    Dim Edible(-1) As PictureBox
    Dim Score As Short = 0
    Dim MazeCompletion As Short
    Dim PowerUpClock As Short = 0
    Dim WallX(-1), WallY(-1) As Short
    Dim DotX(-1), DotY(-1) As Short

    Dim LvChangerCounter As Short = 0


    ' Form Load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Title = Image.FromFile("PacmanTitle.png")
        pbxTitle.Image = Title

        ' Ghost1
        Ghosts(1) = Image.FromFile("Ghost1.png")
        MyGhost1.Image = Ghosts(1)
        ' Ghost2
        Ghosts(2) = Image.FromFile("Ghost2.png")
        MyGhost2.Image = Ghosts(2)
        ' Ghost3
        Ghosts(3) = Image.FromFile("Ghost3.png")
        MyGhost3.Image = Ghosts(3)
        ' Ghost4
        Ghosts(4) = Image.FromFile("Ghost4.png")
        MyGhost4.Image = Ghosts(4)
        ' Vulnerable
        Ghosts(0) = Image.FromFile("GhostVu.png")

        ' PacMan
        PacMan(0) = Image.FromFile("B.png")
        PacMan(1) = Image.FromFile("U.png")
        PacMan(2) = Image.FromFile("D.png")
        PacMan(3) = Image.FromFile("L.png")
        PacMan(4) = Image.FromFile("R.png")

        MyPacMan.Left = -100
        MyPacMan.Top = -100
        MyGhost1.Left = -100
        MyGhost2.Left = -100
        MyGhost3.Left = -100
        MyGhost4.Left = -100

    End Sub


    ' Level 1
    Private Sub lblStart_Click(sender As Object, e As EventArgs) Handles lblLevel1.Click
        LoadLevel(1)
    End Sub


    ' Level 2
    Private Sub lblLevel2_Click(sender As Object, e As EventArgs) Handles lblLevel2.Click
        LoadLevel(2)
    End Sub


    ' Level 3
    Private Sub lblLevel3_Click(sender As Object, e As EventArgs) Handles lblLevel3.Click
        LoadLevel(3)
        GhostSpeed = 10
    End Sub


    ' Level Load
    Private Sub LoadLevel(ByVal shtLevel As Short)
        lblLevel1.Left -= 1500
        lblLevel2.Left -= 1500
        lblLevel3.Left -= 1500
        lblTitle.Left -= 1500
        pbxTitle.Left -= 1500
        lblLvl1Score.Left -= 1500
        lblLvl2Score.Left -= 1500
        lblLvl3Score.Left -= 1500

        If shtLevel = 1 Then
            ' I dpn't think the index in the map making program works
            ' so I just put the coordinates of fruits and pellets in the front
            WallX = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 50, 100, 150, 200, 250, 300, 350, 900, 850, 800, 750, 700, 650, 600, 550, 500, 450, 400, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 850, 800, 750, 700, 650, 600, 550, 500, 450, 400, 350, 300, 250, 200, 150, 100, 200, 300, 300, 350, 400, 350, 400, 500, 550, 600, 500, 550, 600, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 150, 200, 250, 300, 350, 400, 500, 550, 600, 650, 700, 750, 800, 800, 800, 800, 800, 800, 800, 800, 800, 800, 800, 800, 700, 750, 650, 600, 550, 500, 400, 350, 300, 250, 200, 150, 200, 200, 200, 200, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 700, 700, 700, 700, 700, 650, 600, 550, 500, 450, 400, 350, 300, 250}
            WallY = {0, 50, 100, 150, 200, 300, 250, 400, 450, 500, 550, 600, 650, 700, 700, 700, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 300, 400, 450, 500, 550, 600, 650, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 700, 100, 200, 300, 400, 400, 400, 300, 300, 300, 300, 300, 400, 400, 400, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 550, 500, 450, 400, 350, 300, 250, 200, 150, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 250, 300, 400, 450, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 450, 400, 300, 250, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200}
            ' For a test Purpose
            DotX = {61, 61, 361, 561, 861, 861, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 763, 813, 463, 463, 413, 363, 313, 263, 213, 163, 163, 163, 163, 163, 163, 163, 163, 163, 213, 263, 313, 363, 413, 463, 463, 513, 513, 563, 613, 663, 713, 763, 763, 763, 763, 763, 763, 763, 763, 763, 713, 663, 613, 563, 863, 863, 863, 863, 863, 863, 863, 863, 863, 863, 863, 813, 763, 713, 663, 613, 563, 513, 463, 413, 363, 313, 263, 213, 163, 113, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 263, 263, 213, 263, 263, 263, 313, 363, 413, 463, 513, 563, 613, 663, 663, 663, 713, 663, 663, 613, 563, 513, 463, 413, 363, 313, 463, 463, 413, 513, 463, 313, 613}
            DotY = {61, 661, 361, 361, 661, 61, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 113, 163, 163, 163, 163, 163, 163, 163, 213, 263, 313, 363, 413, 463, 513, 563, 563, 563, 563, 563, 563, 563, 613, 563, 163, 163, 163, 163, 163, 163, 213, 263, 313, 363, 413, 463, 513, 563, 563, 563, 563, 563, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 663, 613, 563, 513, 463, 413, 363, 313, 263, 213, 163, 113, 263, 313, 363, 363, 413, 463, 463, 463, 463, 463, 463, 463, 463, 463, 413, 363, 363, 313, 263, 263, 263, 263, 263, 263, 263, 263, 313, 363, 363, 363, 413, 363, 363}

            MyPacMan.Image = PacMan(4)
            MyPacMan.Left = 300
            MyPacMan.Top = 50

            MyGhost1.Left = 50
            MyGhost1.Top = 50
            MyGhost2.Left = 50
            MyGhost2.Top = 50
            MyGhost3.Left = 50
            MyGhost3.Top = 50
            MyGhost4.Left = 50
            MyGhost4.Top = 50

            Width = 965
            Height = 785
            Refresh()
            Score1Clock.Enabled = True

        ElseIf shtLevel = 2 Then
            WallX = {0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 450, 450, 350, 300, 250, 150, 100, 0, 0, 550, 600, 650, 750, 800, 900, 900, 750, 800, 100, 150, 250, 250, 250, 250, 250, 300, 350, 350, 400, 450, 500, 550, 450, 450, 550, 600, 650, 650, 650, 650, 650, 350, 400, 450, 500, 550, 350, 400, 450, 500, 550, 900, 750, 800, 900, 850, 900, 900, 750, 750, 800, 850, 900, 900, 850, 800, 750, 750, 750, 800, 850, 900, 800, 750, 800, 750, 750, 800, 850, 900, 900, 900, 900, 900, 900, 650, 650, 650, 650, 650, 600, 550, 450, 450, 450, 500, 550, 400, 350, 350, 300, 250, 250, 250, 250, 250, 650, 600, 550, 450, 450, 450, 500, 550, 600, 650, 700, 400, 350, 300, 250, 200, 150, 100, 50, 0, 350, 300, 250, 100, 150, 0, 0, 0, 0, 0, 0, 50, 100, 150, 100, 150, 150, 150, 100, 50, 0, 0, 50, 100, 150, 150, 150, 100, 50, 0, 0, 0, 0}
            WallY = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 100, 100, 100, 100, 100, 50, 100, 100, 100, 100, 100, 100, 50, 100, 200, 200, 200, 200, 200, 250, 300, 350, 400, 300, 300, 200, 200, 200, 200, 200, 250, 300, 300, 300, 200, 250, 300, 350, 400, 400, 400, 400, 400, 400, 500, 500, 500, 500, 500, 150, 300, 300, 300, 300, 250, 200, 350, 400, 400, 400, 400, 500, 500, 500, 500, 550, 600, 600, 600, 600, 700, 700, 800, 800, 900, 900, 900, 900, 850, 800, 750, 700, 650, 500, 550, 600, 650, 700, 600, 600, 600, 650, 700, 700, 700, 700, 700, 600, 600, 600, 550, 500, 650, 700, 800, 800, 800, 800, 850, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 900, 800, 800, 800, 800, 800, 850, 800, 750, 700, 650, 600, 600, 600, 600, 700, 700, 550, 500, 500, 500, 500, 400, 400, 400, 400, 350, 300, 300, 300, 300, 250, 200, 150}
            DotX = {61, 861, 511, 411, 113, 163, 213, 263, 313, 363, 413, 413, 113, 63, 63, 63, 63, 113, 163, 213, 213, 213, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 713, 713, 663, 613, 563, 513, 763, 813, 863, 863, 813, 763, 863, 863, 813, 763, 713, 713, 713, 713, 713, 713, 663, 263, 213, 213, 213, 213, 213, 213, 213, 213, 163, 113, 63, 63, 63, 63, 63, 113, 163, 113, 163, 213, 213, 213, 213, 263, 313, 363, 413, 413, 363, 313, 263, 863, 313, 313, 363, 413, 413, 513, 513, 563, 613, 613, 463, 513, 513, 513, 563, 563, 613, 663, 713, 863, 813, 763, 713, 663, 613, 713, 763, 813, 863, 863, 863, 813, 763, 713, 713, 713, 713, 713, 313, 313, 363, 413, 413, 513, 513, 563, 613, 613, 313, 313, 313, 363, 413, 463, 513, 563, 613, 613, 613, 563, 513, 463, 413, 363, 313, 313, 363, 413, 463, 513, 563, 613, 613}
            DotY = {61, 861, 61, 861, 63, 63, 63, 63, 63, 63, 63, 113, 163, 113, 163, 213, 263, 263, 263, 263, 213, 163, 163, 113, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 113, 63, 63, 63, 63, 113, 63, 63, 113, 163, 163, 163, 213, 263, 263, 263, 263, 213, 313, 363, 413, 463, 463, 463, 463, 413, 363, 313, 513, 563, 613, 663, 663, 663, 663, 713, 763, 813, 863, 863, 863, 763, 763, 763, 813, 713, 863, 863, 863, 863, 813, 763, 763, 763, 763, 63, 713, 663, 663, 663, 613, 613, 663, 663, 663, 713, 763, 763, 813, 863, 863, 763, 763, 763, 763, 813, 863, 863, 863, 863, 863, 813, 763, 763, 763, 713, 663, 663, 663, 663, 713, 613, 563, 513, 213, 263, 263, 263, 313, 313, 263, 263, 263, 213, 463, 413, 363, 363, 363, 363, 363, 363, 363, 413, 463, 463, 463, 463, 463, 463, 513, 563, 563, 563, 563, 563, 563, 563, 513}

            MyPacMan.Image = PacMan(4)
            MyPacMan.Left = 300
            MyPacMan.Top = 50

            MyGhost1.Left = 50
            MyGhost1.Top = 50
            MyGhost2.Left = 50
            MyGhost2.Top = 50
            MyGhost3.Left = 50
            MyGhost3.Top = 50
            MyGhost4.Left = 50
            MyGhost4.Top = 50

            Width = 965
            Height = 985
            Refresh()
            Score2Clock.Enabled = True

        ElseIf shtLevel = 3 Then
            WallX = {0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 900, 900, 900, 900, 900, 900, 850, 800, 800, 850, 900, 900, 900, 900, 900, 900, 850, 800, 800, 850, 900, 900, 900, 900, 900, 900, 900, 850, 800, 700, 750, 650, 600, 550, 500, 400, 450, 350, 300, 250, 200, 150, 100, 50, 0, 0, 0, 0, 0, 0, 0, 50, 100, 0, 50, 100, 0, 0, 0, 0, 0, 50, 100, 0, 50, 100, 0, 0, 0, 0, 0, 100, 150, 250, 100, 150, 250, 350, 350, 350, 450, 450, 550, 550, 550, 650, 650, 750, 800, 750, 800, 400, 450, 500, 450, 450, 450, 200, 250, 300, 200, 250, 350, 350, 550, 550, 600, 650, 700, 650, 700, 100, 100, 150, 200, 200, 150, 300, 300, 400, 450, 500, 500, 450, 400, 600, 600, 700, 750, 800, 800, 750, 700, 200, 250, 200, 250, 350, 350, 350, 450, 450, 550, 550, 550, 650, 700, 650, 700, 100, 150, 250, 100, 150, 250, 350, 350, 350, 350, 550, 550, 550, 550, 650, 650, 750, 800, 750, 800, 450, 450, 400, 450, 500}
            WallY = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 100, 150, 200, 250, 300, 300, 300, 400, 400, 400, 450, 500, 550, 600, 650, 650, 650, 750, 750, 750, 800, 850, 900, 950, 1000, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1050, 1000, 950, 900, 850, 800, 750, 750, 750, 650, 650, 650, 600, 550, 500, 450, 400, 400, 400, 300, 300, 300, 250, 200, 150, 100, 50, 100, 100, 100, 200, 200, 200, 100, 150, 200, 100, 150, 100, 150, 200, 100, 200, 100, 100, 200, 200, 250, 250, 250, 300, 350, 400, 300, 300, 300, 400, 400, 350, 400, 350, 400, 300, 300, 300, 400, 400, 500, 550, 500, 500, 550, 550, 500, 550, 500, 500, 500, 550, 550, 550, 500, 550, 500, 500, 500, 550, 550, 550, 650, 650, 750, 750, 650, 700, 750, 650, 700, 650, 700, 750, 650, 650, 750, 750, 850, 850, 850, 950, 950, 950, 800, 850, 900, 950, 800, 850, 900, 950, 850, 950, 850, 850, 950, 950, 900, 950, 800, 800, 800}
            DotX = {61, 861, 461, 461, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 763, 813, 863, 63, 213, 313, 413, 513, 613, 713, 863, 63, 113, 163, 213, 263, 313, 413, 513, 613, 663, 713, 763, 813, 863, 63, 213, 313, 413, 513, 613, 713, 863, 63, 113, 163, 213, 263, 313, 363, 563, 613, 663, 713, 763, 813, 863, 163, 363, 413, 513, 563, 763, 163, 213, 263, 313, 413, 513, 613, 663, 713, 763, 163, 313, 413, 513, 613, 763, 63, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 763, 813, 863, 63, 263, 363, 563, 663, 863, 63, 263, 363, 563, 663, 863, 63, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 763, 813, 863, 163, 313, 413, 513, 613, 763, 163, 213, 263, 313, 413, 513, 613, 663, 713, 763, 163, 313, 413, 463, 513, 613, 763, 63, 113, 163, 213, 263, 313, 613, 663, 713, 763, 813, 863, 63, 213, 313, 413, 513, 613, 713, 863, 63, 113, 163, 213, 263, 313, 413, 513, 613, 663, 713, 763, 813, 863, 63, 213, 313, 413, 513, 613, 713, 863, 63, 113, 163, 213, 263, 313, 363, 413, 463, 513, 563, 613, 663, 713, 763, 813}
            DotY = {61, 1011, 211, 861, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 113, 113, 113, 113, 113, 113, 113, 113, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 163, 213, 213, 213, 213, 213, 213, 213, 213, 263, 263, 263, 263, 263, 263, 263, 263, 263, 263, 263, 263, 263, 263, 313, 313, 313, 313, 313, 313, 363, 363, 363, 363, 363, 363, 363, 363, 363, 363, 413, 413, 413, 413, 413, 413, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 463, 513, 513, 513, 513, 513, 513, 563, 563, 563, 563, 563, 563, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 613, 663, 663, 663, 663, 663, 663, 713, 713, 713, 713, 713, 713, 713, 713, 713, 713, 763, 763, 763, 763, 763, 763, 763, 813, 813, 813, 813, 813, 813, 813, 813, 813, 813, 813, 813, 863, 863, 863, 863, 863, 863, 863, 863, 913, 913, 913, 913, 913, 913, 913, 913, 913, 913, 913, 913, 913, 913, 963, 963, 963, 963, 963, 963, 963, 963, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013, 1013}

            MyPacMan.Image = PacMan(4)
            MyPacMan.Left = 850
            MyPacMan.Top = 50

            MyGhost1.Left = 50
            MyGhost1.Top = 50
            MyGhost2.Left = 50
            MyGhost2.Top = 50
            MyGhost3.Left = 50
            MyGhost3.Top = 50
            MyGhost4.Left = 50
            MyGhost4.Top = 50

            Width = 965
            Height = 1135
            Refresh()
            Score3Clock.Enabled = True

        End If



        For index = 0 To WallX.Length() - 1
            Dim NewWallPiece As New PictureBox

            NewWallPiece.Width = 50
            NewWallPiece.Height = 50
            NewWallPiece.Left = WallX(index)
            NewWallPiece.Top = WallY(index)
            NewWallPiece.BackColor = Color.Blue

            ReDim Preserve Wall(Wall.Length())
            Wall(index) = NewWallPiece
            Controls.Add(Wall(index))
        Next

        For index = 0 To DotX.Length() - 1
            Dim NewDot As New PictureBox
            ' first 4 are fruits and pellets
            If index > 3 Then
                NewDot.BackColor = Color.White
                NewDot.Name = "Dot"
                NewDot.Width = 5
                NewDot.Height = 5
                NewDot.Left = DotX(index) + 12
                NewDot.Top = DotY(index) + 12
                ' first 2 are powerpellets
            ElseIf index < 2 Then
                NewDot.BackColor = Color.AliceBlue
                NewDot.Name = "PowerPellet"
                NewDot.Width = 20
                NewDot.Height = 20
                NewDot.Left = DotX(index) + 6
                NewDot.Top = DotY(index) + 6
                ' 2 after pellets are fruits
            Else
                NewDot.BackColor = Color.Red
                NewDot.Name = "Fruit"
                NewDot.Width = 20
                NewDot.Height = 20
                NewDot.Left = DotX(index) + 6
                NewDot.Top = DotY(index) + 6
            End If

            ReDim Preserve Edible(Edible.Length())
            Edible(index) = NewDot
            Controls.Add(Edible(index))
        Next

        PacClock.Enabled = True
        Ghost1MoveClock.Enabled = True
        Ghost2MoveClock.Enabled = True
        Ghost3MoveClock.Enabled = True
        Ghost4MoveClock.Enabled = True


    End Sub


    ' KeyDown
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown


        If e.KeyValue = Keys.Up Then
            NextPacDirection = "U"
        ElseIf e.KeyValue = Keys.Down Then
            NextPacDirection = "D"
        ElseIf e.KeyValue = Keys.Right Then
            NextPacDirection = "R"
        ElseIf e.KeyValue = Keys.Left Then
            NextPacDirection = "L"
        Else
            NextPacDirection = "Stop"
        End If
    End Sub


    ' PacClock
    Private Sub PacClock_Tick(sender As Object, e As System.EventArgs) Handles PacClock.Tick
        Counter += 1

        If HitWalls(MyPacMan, NextPacDirection, PacSpeed) Then

            If NextPacDirection = PacDirection Or HitWalls(MyPacMan, PacDirection, PacSpeed) Then
                PacDirection = "Stop"
            End If
        Else
            PacDirection = NextPacDirection
        End If

        MovePac(PacDirection, Counter)


        If MyPacMan.Left > 950 Then
            MyPacMan.Left = -40
        ElseIf MyPacMan.Left < -40 Then
            MyPacMan.Left = 950
        End If

        If HitGhost(MyPacMan, MyGhost1) = "Dead" Or HitGhost(MyPacMan, MyGhost2) = "Dead" Or HitGhost(MyPacMan, MyGhost3) = "Dead" Or HitGhost(MyPacMan, MyGhost4) = "Dead" Then
            My.Computer.Audio.Play(My.Resources.pacman_death, AudioPlayMode.Background)
            PacClock.Enabled = False
            Ghost1MoveClock.Enabled = False
            Ghost2MoveClock.Enabled = False
            Ghost3MoveClock.Enabled = False
            Ghost4MoveClock.Enabled = False
            PowerPelletClock.Enabled = False

            Score1Clock.Enabled = False
            Score2Clock.Enabled = False
            Score3Clock.Enabled = False

            lblLevel1.Left += 1500
            lblLevel2.Left += 1500
            lblLevel3.Left += 1500
            lblTitle.Left += 1500
            pbxTitle.Left += 1500
            lblLvl1Score.Left += 1500
            lblLvl2Score.Left += 1500
            lblLvl3Score.Left += 1500

            Score = 0

            For index = 0 To DotX.Length() - 1
                Controls.Remove(Edible(index))
            Next
            For index = 0 To WallX.Length() - 1
                Controls.Remove(Wall(index))
            Next

            MyPacMan.Left = -100
            MyPacMan.Top = -100
            MyGhost1.Left = -100
            MyGhost2.Left = -100
            MyGhost3.Left = -100
            MyGhost4.Left = -100

            Width = 851
            Height = 393
            Refresh()
            MessageBox.Show("Dead!")
        ElseIf (HitGhost(MyPacMan, MyGhost1)) = "Ghost Score" Then
            Score += 200
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            Ghost1MoveClock.Enabled = False
            MyGhost1.Left = -100
            MyGhost1.Top = -100
        ElseIf (HitGhost(MyPacMan, MyGhost2)) = "Ghost Score" Then
            Score += 200
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            Ghost2MoveClock.Enabled = False
            MyGhost2.Left = -100
            MyGhost2.Top = -100
        ElseIf (HitGhost(MyPacMan, MyGhost3)) = "Ghost Score" Then
            Score += 200
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            Ghost3MoveClock.Enabled = False
            MyGhost3.Left = -100
            MyGhost3.Top = -100
        ElseIf (HitGhost(MyPacMan, MyGhost4)) = "Ghost Score" Then
            Score += 200
            My.Computer.Audio.Play(My.Resources.pacman_eatghost, AudioPlayMode.Background)
            Ghost4MoveClock.Enabled = False
            MyGhost4.Left = -100
            MyGhost4.Top = -100
        End If

        Dim Eatable As String = Eats(MyPacMan)

        If Eatable = "Dot" Then
            Score += 10
            My.Computer.Audio.Play(My.Resources.pacman_chomp, AudioPlayMode.Background)
        ElseIf Eatable = "Fruit" Then
            Score += 1000
            My.Computer.Audio.Play(My.Resources.pacman_chomp, AudioPlayMode.Background)
        ElseIf Eatable = "PowerPellet" Then
            PowerPelletClock.Enabled = True
            My.Computer.Audio.Play(My.Resources.pacman_chomp, AudioPlayMode.Background)
        ElseIf Eatable = "Complete" Then
            PacClock.Enabled = False
            Ghost1MoveClock.Enabled = False
            Ghost2MoveClock.Enabled = False
            Ghost3MoveClock.Enabled = False
            Ghost4MoveClock.Enabled = False
            PowerPelletClock.Enabled = False

            Score1Clock.Enabled = False
            Score2Clock.Enabled = False
            Score3Clock.Enabled = False

            lblLevel1.Left += 1500
            lblLevel2.Left += 1500
            lblLevel3.Left += 1500
            lblTitle.Left += 1500
            pbxTitle.Left += 1500
            lblLvl1Score.Left += 1500
            lblLvl2Score.Left += 1500
            lblLvl3Score.Left += 1500

            Score = 0

            For index = 0 To DotX.Length() - 1
                Controls.Remove(Edible(index))
            Next
            For index = 0 To WallX.Length() - 1
                Controls.Remove(Wall(index))
            Next

            MyPacMan.Left = -100
            MyPacMan.Top = -100
            MyGhost1.Left = -100
            MyGhost2.Left = -100
            MyGhost3.Left = -100
            MyGhost4.Left = -100

            Width = 851
            Height = 393
            Refresh()

            MessageBox.Show("Level Complete!")
        End If

        lblScore.Text = "Score: " + Score.ToString
    End Sub


    ' MovePac
    Private Sub MovePac(ByVal PacDirection As String, ByVal Counter As Long)
        If PacDirection = "R" Then
            MyPacMan.Left += PacSpeed
            If Counter Mod 2 = 0 Then
                MyPacMan.Image = PacMan(4)
            Else
                MyPacMan.Image = PacMan(0)
            End If
        ElseIf PacDirection = "L" Then
            MyPacMan.Left -= PacSpeed
            If Counter Mod 2 = 0 Then
                MyPacMan.Image = PacMan(3)
            Else
                MyPacMan.Image = PacMan(0)
            End If
        ElseIf PacDirection = "D" Then
            MyPacMan.Top += PacSpeed
            If Counter Mod 2 = 0 Then
                MyPacMan.Image = PacMan(2)
            Else
                MyPacMan.Image = PacMan(0)
            End If
        ElseIf PacDirection = "U" Then
            MyPacMan.Top -= PacSpeed
            If Counter Mod 2 = 0 Then
                MyPacMan.Image = PacMan(1)
            Else
                MyPacMan.Image = PacMan(0)
            End If
        End If
    End Sub


    ' Eats Function
    Private Function Eats(ByVal PacMan As PictureBox)

        For index = 0 To DotX.Length - 1
            If MazeCompletion = Edible.Length Then
                Return "Complete"
            ElseIf PacMan.Bounds.IntersectsWith(Edible(index).Bounds) Then
                MazeCompletion += 1
                Edible(index).Left = -1000
                Return Edible(index).Name
            End If
        Next
        Return ""
    End Function


    ' HitGhost Function
    Private Function HitGhost(ByVal PacMan As PictureBox, ByVal Ghost As PictureBox)

        Dim DistanceApart As Short = Math.Sqrt((MyPacMan.Left - Ghost.Left) ^ 2 + (MyPacMan.Top - Ghost.Top) ^ 2)

        If DistanceApart <= 45 And PowerPelletClock.Enabled = False Then
            Return "Dead"
        ElseIf DistanceApart <= 45 And PowerPelletClock.Enabled = True Then
            Return "Ghost Score"
            Score += 1000
        Else
            Return ""
        End If

    End Function


    ' HitWall Function
    Private Function HitWalls(ByVal Sprite As PictureBox, ByVal NextDirection As String, ByVal Speed As Short)

        Dim SpriteCopy = New PictureBox()
        Dim XVal, YVal, Width, Height As Short
        XVal = Sprite.Left
        YVal = Sprite.Top
        Width = Sprite.Width
        Height = Sprite.Height

        SpriteCopy.Left = XVal
        SpriteCopy.Top = YVal
        SpriteCopy.Width = Width
        SpriteCopy.Height = Height

        If NextDirection = "R" Then
            SpriteCopy.Left += Speed
        ElseIf NextDirection = "L" Then
            SpriteCopy.Left -= Speed
        ElseIf NextDirection = "U" Then
            SpriteCopy.Top -= Speed
        ElseIf NextDirection = "D" Then
            SpriteCopy.Top += Speed
        End If

        For index = 0 To WallX.Length() - 1
            If SpriteCopy.Bounds.IntersectsWith(Wall(index).Bounds) Then
                Return True
            End If
        Next
        Return False

    End Function


    ' Ghost 1 Moveclock
    Private Sub Ghost1MoveClock_Tick(sender As Object, e As EventArgs) Handles Ghost1MoveClock.Tick

        GhostChooseDirection(MyGhost1, Ghost1Direction, GhostSpeed, 1)

        If Ghost1Direction = "L" Then
            MyGhost1.Left -= GhostSpeed
        ElseIf Ghost1Direction = "R" Then
            MyGhost1.Left += GhostSpeed
        ElseIf Ghost1Direction = "U" Then
            MyGhost1.Top -= GhostSpeed
        ElseIf Ghost1Direction = "D" Then
            MyGhost1.Top += GhostSpeed
        End If

        If MyGhost1.Left > 950 Then
            MyGhost1.Left = -40
        ElseIf MyGhost1.Left < -40 Then
            MyGhost1.Left = 950
        End If

    End Sub


    ' Ghost 2 Moveclock
    Private Sub Ghost2MoveClock_Tick(sender As Object, e As EventArgs) Handles Ghost2MoveClock.Tick

        GhostChooseDirection(MyGhost2, Ghost2Direction, GhostSpeed, 2)

        If Ghost2Direction = "L" Then
            MyGhost2.Left -= GhostSpeed
        ElseIf Ghost2Direction = "R" Then
            MyGhost2.Left += GhostSpeed
        ElseIf Ghost2Direction = "U" Then
            MyGhost2.Top -= GhostSpeed
        ElseIf Ghost2Direction = "D" Then
            MyGhost2.Top += GhostSpeed
        End If

        If MyGhost2.Left > 950 Then
            MyGhost2.Left = -40
        ElseIf MyGhost2.Left < -40 Then
            MyGhost2.Left = 950
        End If
    End Sub


    ' Ghost 3 Moveclock
    Private Sub Ghost3MoveClock_Tick(sender As Object, e As EventArgs) Handles Ghost3MoveClock.Tick

        GhostChooseDirection(MyGhost3, Ghost3Direction, GhostSpeed, 3)

        If Ghost3Direction = "L" Then
            MyGhost3.Left -= GhostSpeed
        ElseIf Ghost3Direction = "R" Then
            MyGhost3.Left += GhostSpeed
        ElseIf Ghost3Direction = "U" Then
            MyGhost3.Top -= GhostSpeed
        ElseIf Ghost3Direction = "D" Then
            MyGhost3.Top += GhostSpeed
        End If

        If MyGhost3.Left > 950 Then
            MyGhost3.Left = -40
        ElseIf MyGhost3.Left < -40 Then
            MyGhost3.Left = 950
        End If
    End Sub


    ' Ghost 4 Moveclock
    Private Sub Ghost4MoveClock_Tick(sender As Object, e As EventArgs) Handles Ghost2MoveClock.Tick

        GhostChooseDirection(MyGhost4, Ghost4Direction, GhostSpeed, 4)

        If Ghost4Direction = "L" Then
            MyGhost4.Left -= GhostSpeed
        ElseIf Ghost4Direction = "R" Then
            MyGhost4.Left += GhostSpeed
        ElseIf Ghost4Direction = "U" Then
            MyGhost4.Top -= GhostSpeed
        ElseIf Ghost4Direction = "D" Then
            MyGhost4.Top += GhostSpeed
        End If

        If MyGhost4.Left > 950 Then
            MyGhost4.Left = -40
        ElseIf MyGhost4.Left < -40 Then
            MyGhost4.Left = 950
        End If
    End Sub


    ' Ghost Direction Function
    Private Sub GhostChooseDirection(ByVal Ghost As PictureBox, ByVal Direction As String, ByVal Speed As Short, ByVal GhostNumber As Short)

        Dim DirectionsAvailable As String = ""
        Dim AbovePac As Boolean = Nothing
        Dim LeftOfPac As Boolean = Nothing

        ' I used these values to determine if the ghost is
        ' farther in X or Y and decide directions
        Dim DistanceApartX As Short = MyPacMan.Left - Ghost.Left
        Dim DistanceApartY As Short = MyPacMan.Top - Ghost.Top


        If Ghost.Left > Me.MyPacMan.Left Then
            LeftOfPac = False
        Else
            LeftOfPac = True
        End If
        If Ghost.Top > Me.MyPacMan.Top Then
            AbovePac = False
        Else
            AbovePac = True
        End If


        If HitWalls(Ghost, "U", Speed) = False And Direction <> "D" Then
            DirectionsAvailable += "U"
        End If
        If HitWalls(Ghost, "D", Speed) = False And Direction <> "U" Then
            DirectionsAvailable += "D"
        End If
        If HitWalls(Ghost, "L", Speed) = False And Direction <> "R" Then
            DirectionsAvailable += "L"
        End If
        If HitWalls(Ghost, "R", Speed) = False And Direction <> "L" Then
            DirectionsAvailable += "R"
        End If
        '
        ' Random Ghost Direction
        Randomize()
        Dim RndGhostDirection As String = DirectionsAvailable.Substring(Int(Rnd() * DirectionsAvailable.Length), 1)

        'Smart Ghost direction
        Dim SmartGhostDirection As String
        If LeftOfPac = True And AbovePac = True Then
            If DirectionsAvailable.IndexOf("R") > -1 Then
                SmartGhostDirection = "R"
            ElseIf DirectionsAvailable.IndexOf("D") > -1 Then
                SmartGhostDirection = "D"
            Else
                SmartGhostDirection = RndGhostDirection
            End If
        ElseIf LeftOfPac = True And AbovePac = False Then
            If DirectionsAvailable.IndexOf("R") > -1 Then
                SmartGhostDirection = "R"
            ElseIf DirectionsAvailable.IndexOf("U") > -1 Then
                SmartGhostDirection = "U"
            Else
                SmartGhostDirection = RndGhostDirection
            End If
        ElseIf LeftOfPac = False And AbovePac = True Then
            If DirectionsAvailable.IndexOf("L") > -1 Then
                SmartGhostDirection = "L"
            ElseIf DirectionsAvailable.IndexOf("D") > -1 Then
                SmartGhostDirection = "D"
            Else
                SmartGhostDirection = RndGhostDirection
            End If
        Else
            If DirectionsAvailable.IndexOf("L") > -1 Then
                SmartGhostDirection = "L"
            ElseIf DirectionsAvailable.IndexOf("U") > -1 Then
                SmartGhostDirection = "U"
            Else
                SmartGhostDirection = RndGhostDirection
            End If
        End If


        ' Superduper Smart Ghost direction
        ' it chooses direction based on LOP and AbvP and if it's farther LOP or farther AbvP 
        Dim SuperSmartGhostDirection As String
        If LeftOfPac = True And AbovePac = True Then
            If DirectionsAvailable.IndexOf("R") > -1 And DirectionsAvailable.IndexOf("D") > -1 Then
                If DistanceApartX > DistanceApartY Then
                    SuperSmartGhostDirection = "R"
                Else
                    SuperSmartGhostDirection = "D"
                End If
            ElseIf DirectionsAvailable.IndexOf("R") > -1 Then
                SuperSmartGhostDirection = "R"
            ElseIf DirectionsAvailable.IndexOf("D") > -1 Then
                SuperSmartGhostDirection = "D"
            Else
                SuperSmartGhostDirection = RndGhostDirection
            End If
        ElseIf LeftOfPac = True And AbovePac = False Then
            If DirectionsAvailable.IndexOf("R") > -1 And DirectionsAvailable.IndexOf("U") > -1 Then
                If DistanceApartX > DistanceApartY Then
                    SuperSmartGhostDirection = "R"
                Else
                    SuperSmartGhostDirection = "U"
                End If
            ElseIf DirectionsAvailable.IndexOf("R") > -1 Then
                SuperSmartGhostDirection = "R"
            ElseIf DirectionsAvailable.IndexOf("U") > -1 Then
                SuperSmartGhostDirection = "U"
            Else
                SuperSmartGhostDirection = RndGhostDirection
            End If
        ElseIf LeftOfPac = False And AbovePac = True Then
            If DirectionsAvailable.IndexOf("L") > -1 And DirectionsAvailable.IndexOf("D") > -1 Then
                If DistanceApartX > DistanceApartY Then
                    SuperSmartGhostDirection = "L"
                Else
                    SuperSmartGhostDirection = "D"
                End If
            ElseIf DirectionsAvailable.IndexOf("L") > -1 Then
                SuperSmartGhostDirection = "L"
            ElseIf DirectionsAvailable.IndexOf("D") > -1 Then
                SuperSmartGhostDirection = "D"
            Else
                SuperSmartGhostDirection = RndGhostDirection
            End If
        Else
            If DirectionsAvailable.IndexOf("L") > -1 And DirectionsAvailable.IndexOf("U") > -1 Then
                If DistanceApartX > DistanceApartY Then
                    SuperSmartGhostDirection = "L"
                Else
                    SuperSmartGhostDirection = "U"
                End If
            ElseIf DirectionsAvailable.IndexOf("L") > -1 Then
                SuperSmartGhostDirection = "L"
            ElseIf DirectionsAvailable.IndexOf("U") > -1 Then
                SuperSmartGhostDirection = "U"
            Else
                SuperSmartGhostDirection = RndGhostDirection
            End If
        End If


        ' Which Ghost's direction
        ' Ghost1, 2 is gonna move randomly
        ' Ghost3 is gonna move smart
        ' Ghost4 are gonna move superduper smart

        If GhostNumber = 1 Then
            Ghost1Direction = RndGhostDirection
        ElseIf GhostNumber = 2 Then
            Ghost2Direction = RndGhostDirection
        ElseIf GhostNumber = 3 Then
            Ghost3Direction = SmartGhostDirection
        ElseIf GhostNumber = 4 Then
            Ghost4Direction = SuperSmartGhostDirection
        End If

    End Sub


    ' Powerpellet Clock
    Private Sub PowerPelletClock_Tick(sender As Object, e As EventArgs) Handles PowerPelletClock.Tick
        PowerUpClock += 1
        If PowerUpClock Mod 2 = 1 Then
            MyGhost1.Image = Ghosts(0)
            MyGhost2.Image = Ghosts(0)
            MyGhost3.Image = Ghosts(0)
            MyGhost4.Image = Ghosts(0)
        Else
            MyGhost1.Image = Ghosts(1)
            MyGhost2.Image = Ghosts(2)
            MyGhost3.Image = Ghosts(3)
            MyGhost4.Image = Ghosts(4)
        End If
        If PowerUpClock = 50 Then
            PowerPelletClock.Enabled = False
            PowerUpClock = 0
        End If
    End Sub


    ' Score Recording Clocks
    Private Sub Score1Clock_Tick(sender As Object, e As EventArgs) Handles Score1Clock.Tick
        lblLvl1Score.Text = Score.ToString
    End Sub

    Private Sub Score2Clock_Tick(sender As Object, e As EventArgs) Handles Score2Clock.Tick
        lblLvl2Score.Text = Score.ToString
    End Sub

    Private Sub Score3Clock_Tick(sender As Object, e As EventArgs) Handles Score3Clock.Tick
        lblLvl3Score.Text = Score.ToString
    End Sub


    ' Easter Egg
    Private Sub pbxTitle_Click(sender As Object, e As EventArgs) Handles pbxTitle.Click
        MessageBox.Show("I am the best.")
        ' level 3 Map has my Initials "YH"
    End Sub
End Class
