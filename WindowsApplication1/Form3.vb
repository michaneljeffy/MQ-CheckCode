Public Class Form3

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

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 1920
        Me.Width = 1080

        PictureBox1.Height = 1920
        PictureBox1.Width = 1080
        PictureBox1.Left = 0
        PictureBox1.Top = 0

        PictureBox1_.Height = 1920
        PictureBox1_.Width = 1080
        PictureBox1_.Left = 0
        PictureBox1_.Top = 0
        PictureBox1_.Visible = False
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


        PictureBox1.Image = images(0)
        PictureBox1_.Image = images(0)
        Timer1.Enabled = True
        Timer1.Interval = 4000
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If i < 2 Then
            i = i + 1
        Else
            i = 0
        End If

        Dim tableChartImage As Bitmap = New Bitmap(Width, Height)
        Dim graph As Graphics = Graphics.FromImage(tableChartImage)
        '初始化这个大图
        graph.DrawImage(tableChartImage, Width, Height)
        '初始化当前宽
        Dim currentHeight As Integer = 0
        '拼图
        graph.DrawImage(simages(i), 0, currentHeight)
        graph.DrawImage(simages2(i), 0, 960)


        If AdvIsHide Then
            PictureBox1_.Image = tableChartImage
            Dim r As System.Random = New System.Random()
            AnimateWindow(PictureBox1_.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  
            AdvIsHide = False
            PictureBox1.Image = images(i)
        Else
            PictureBox1.Image = images(i)
            Dim v As System.Random = New System.Random()
            AnimateWindow(PictureBox1_.Handle, 2000, AW_HIDE Or eff_sz(v.Next(9))) '窗体慢慢透明淡出  
            AdvIsHide = True
        End If
    End Sub
End Class