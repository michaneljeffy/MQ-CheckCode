Public Class Form2
    Declare Function AnimateWindow Lib "user32" Alias "AnimateWindow" (ByVal hwnd As IntPtr, ByVal dwTime As Int32, ByVal dwFlags As Int32) As Boolean
    '从左到右显示
    Public Const AW_HOR_POSITIVE As Int32 = &H1
    '从右到左显示  
    Public Const AW_HOR_NEGATIVE As Int32 = &H2
    '从上到下显示  
    Public Const AW_VER_POSITIVE As Int32 = &H4
    '从下到上显示  
    Public Const AW_VER_NEGATIVE As Int32 = &H8
    '若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口  
    Public Const AW_CENTER As Int32 = &H10
    Public Const AW_HIDE As Int32 = &H10000 '隐藏窗口，缺省则显示窗口  
    '激活窗口。在使用了AW_HIDE标志后不能使用这个标志  
    Public Const AW_ACTIVATE As Int32 = &H20000
    '使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略  
    Public Const AW_SLIDE As Int32 = &H40000
    '透明度从高到低  
    Public Const AW_BLEND As Int32 = &H80000
    '判断窗体是否隐藏  
    Public AdvIsHide As Boolean = False
    Public AdvIsHide2 As Boolean = False
    Public advIsHide3 As Boolean = False
    Public i As Integer = 0
    Public j As Integer = 0
    '特效数组
    Public eff_sz() As Integer = {AW_CENTER, AW_HOR_POSITIVE, AW_HOR_NEGATIVE, AW_VER_POSITIVE, AW_VER_NEGATIVE, AW_HOR_POSITIVE Or AW_VER_POSITIVE, AW_HOR_POSITIVE Or AW_VER_NEGATIVE, AW_HOR_NEGATIVE Or AW_VER_POSITIVE, AW_HOR_NEGATIVE Or AW_VER_NEGATIVE}
    Dim images As New List(Of Bitmap)
    Dim simages As New List(Of Bitmap)
    Dim simages2 As New List(Of Bitmap)
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 1920
        Me.Width = 1080
        Panel1.Height = 1920
        Panel1.Width = 1080
        Panel1.Left = 0
        Panel1.Top = 0
        PictureBox1.Height = 960
        PictureBox1.Width = 1080
        PictureBox1.Left = 0
        PictureBox1.Top = 0

        PictureBox1_.Height = 960
        PictureBox1_.Width = 1080
        PictureBox1_.Left = 0
        PictureBox1_.Top = 0

        Panel2.Height = 1920
        Panel2.Width = 1080
        Panel2.Left = 0
        Panel2.Top = 0

        PictureBox2_.Height = 960
        PictureBox2_.Width = 1080
        PictureBox2_.Left = 0
        PictureBox2_.Top = 960

        PictureBox2.Height = 960
        PictureBox2.Width = 1080
        PictureBox2.Left = 0
        PictureBox2.Top = 960


        PictureBox3.Height = 1920
        PictureBox3.Width = 1080
        PictureBox3.Left = 0
        PictureBox3.Top = 0

        PictureBox3_.Height = 1920
        PictureBox3_.Width = 1080
        PictureBox3_.Left = 0
        PictureBox3_.Top = 0


        PictureBox1.Visible = False
        PictureBox1_.Visible = False
        PictureBox2.Visible = False
        PictureBox2_.Visible = False


        Dim files = My.Computer.FileSystem.GetFiles("C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\WindowsApplication1\bin\Debug\Image")
        For Each item In files
            Dim imgmap = Bitmap.FromFile(item)
            imgmap = New Bitmap(imgmap, New Size(Me.Width, Me.Height))
            images.Add(imgmap)
        Next

        Dim sfiles1 = My.Computer.FileSystem.GetFiles("C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\WindowsApplication1\bin\Debug\sImage1")
        For Each item In sfiles1
            Dim imgmap = Bitmap.FromFile(item)
            Dim simgmap = New Bitmap(imgmap, New Size(Me.Width, Me.Height / 2))
            simages.Add(simgmap)
        Next

        Dim sfiles2 = My.Computer.FileSystem.GetFiles("C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\WindowsApplication1\bin\Debug\sImage2")
        For Each item In sfiles2
            Dim imgmap = Bitmap.FromFile(item)
            Dim simgmap = New Bitmap(imgmap, New Size(Me.Width, Me.Height / 2))
            simages2.Add(simgmap)
        Next

        PictureBox1.Image = simages(0)
        PictureBox2.Image = simages2(0)

        PictureBox1_.Image = simages(0)
        PictureBox2_.Image = simages2(0)

        PictureBox3.Image = images(0)
        PictureBox3_.Image = images(0)


        Timer1.Enabled = True
        Timer1.Interval = 4000
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If i < 2 Then
            i = i + 1
        Else
            i = 0
        End If
        If j Mod 2 = 0 Then
            PictureBox1.Visible = False
            PictureBox1_.Visible = False
            PictureBox2.Visible = False
            PictureBox2_.Visible = False
            PictureBox3.Visible = True
            PictureBox3_.Visible = True
            If AdvIsHide Then
                PictureBox3_.Image = images(i)
                Dim r As System.Random = New System.Random()
                AnimateWindow(Panel2.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  
                AdvIsHide = False
                PictureBox3.Image = images(i)
            Else
                PictureBox3_.Image = images(i)
                Dim v As System.Random = New System.Random()
                AnimateWindow(Panel2.Handle, 2000, AW_HIDE Or eff_sz(v.Next(9))) '窗体慢慢透明淡出  
                AdvIsHide = True
            End If
        End If

        If j Mod 2 <> 0 Then
            PictureBox1.Visible = True
            PictureBox1_.Visible = True
            PictureBox2.Visible = True
            PictureBox2_.Visible = True
            PictureBox3.Visible = False
            PictureBox3_.Visible = False
            If AdvIsHide2 Then
                PictureBox1_.Image = simages(i)
                PictureBox2_.Image = simages2(i)
                Dim r As System.Random = New System.Random()
                AnimateWindow(Panel2.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  
                AdvIsHide = False
                PictureBox1.Image = simages(i)
                PictureBox2.Image = simages2(i)
            Else
                PictureBox1_.Image = simages(i)
                PictureBox2_.Image = simages2(i)

                Dim v As System.Random = New System.Random()
                AnimateWindow(Panel2.Handle, 2000, AW_HIDE Or eff_sz(v.Next(9))) '窗体慢慢透明淡出  
                AdvIsHide2 = True
            End If
        End If

        j = j + 1
        '  Me.Timer1.Enabled = False
        '  Me.Timer2.Enabled = True
        '  Me.Timer2.Interval = 4000
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If i < 2 Then
            i = i + 1
        Else
            i = 0
        End If
        If AdvIsHide Then
            PictureBox1_.Image = simages(i)
            PictureBox2_.Image = simages2(i)
            Dim r As System.Random = New System.Random()
            AnimateWindow(Panel2.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  
            AdvIsHide = False
            PictureBox1.Image = simages(i)
            PictureBox2.Image = simages2(i)
        Else
            PictureBox1_.Image = simages(i)
            PictureBox2_.Image = simages2(i)

            Dim v As System.Random = New System.Random()
            AnimateWindow(Panel2.Handle, 2000, AW_HIDE Or eff_sz(v.Next(9))) '窗体慢慢透明淡出  
            AdvIsHide = True

        End If
        '   Me.Timer2.Enabled = False
        '   Me.Timer1.Enabled = True
        ' Me.Timer1.Interval = 4000
    End Sub
End Class