Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.ComponentModel

Public Class Form1

    Dim timer As System.Timers.Timer = New System.Timers.Timer()
    Public Shared downloadUrl As String = 0
    Dim downloadUrls As Queue(Of String) = New Queue(Of String)()
    Private Shared downloadFileName As String

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

    '特效数组
    Public eff_sz() As Integer = {AW_CENTER, AW_HOR_POSITIVE, AW_HOR_NEGATIVE, AW_VER_POSITIVE, AW_VER_NEGATIVE, AW_HOR_POSITIVE Or AW_VER_POSITIVE, AW_HOR_POSITIVE Or AW_VER_NEGATIVE, AW_HOR_NEGATIVE Or AW_VER_POSITIVE, AW_HOR_NEGATIVE Or AW_VER_NEGATIVE}
    Dim images As New List(Of Bitmap)
    Dim simages As New List(Of Bitmap)
    Dim simages2 As New List(Of Bitmap)
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim m As Integer = 0

    Dim j As Integer = 0

    Dim k As Integer = 0
    'Dim lcontrollers As List(Of Label) = New List(Of Label)()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Dim client As WebClient = New WebClient()
            'AddHandler client.DownloadFileCompleted, AddressOf Unzip
            '  AddHandler client.DownloadProgressChanged, AddressOf ProgressChanged
            '   AddHandler client.DownloadFileCompleted, AddressOf DownloadCompleted
            'Dim urlList As List(Of String) = New List(Of String)()
            'urlList.Add("http://static.in66.co/images/20170324/58d4dcd0a87e2.jpg")
            'urlList.Add("http://static.in66.co/images/20170504/590b0419d60b4.jpg")
            'urlList.Add("http://static.in66.co/images/20170508/590fbe01df4f3.jpg")

            'For Each item As String In urlList
            '    downloadUrls.Enqueue(item)
            'Next
            'DownloadFile()
            WebBrowser1.Height = 960
            WebBrowser1.Width = 1080
            WebBrowser1.Top = 960
            WebBrowser1.Left = 0
            WebBrowser1.Visible = False
            WebBrowser1.Navigate("http://www.iqiyi.com/a_19rrh99g55.html")

            Me.Height = 1920
            Me.Width = 1080
            PictureBox1.Height = Me.Height
            PictureBox1.Width = Me.Width
            PictureBox1.Left = 0
            PictureBox1.Top = 0
            PictureBox1.Visible = True
            PictureBox1_.Visible = True

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

            PictureBox1_.Image = images(0)
            PictureBox1.Image = images(0)

            PictureBox2.Height = 960
            PictureBox2.Width = 1080
            PictureBox2.Left = 0
            PictureBox2.Top = 0
            ' PictureBox2.Image = simages(0)

            PictureBox3.Width = 1080
            PictureBox3.Height = 960
            PictureBox3.Left = 0
            PictureBox3.Top = 960
            ' PictureBox3.Image = simages(0)

#Region "遮罩"
            PictureBox1_.Height = 1920
            PictureBox1_.Width = 1080
            PictureBox1_.Left = 0
            PictureBox1_.Top = 0

            PictureBox2_.Height = 960
            PictureBox2_.Width = 1080
            PictureBox2_.Left = 0
            PictureBox2_.Top = 0

            PictureBox3_.Height = 960
            PictureBox3_.Width = 1080
            PictureBox3_.Top = 960
            PictureBox3_.Left = 0
#End Region


            PictureBox2.Visible = False
            PictureBox3.Visible = False
            PictureBox2_.Visible = False
            PictureBox3_.Visible = False
            PictureBox2.Image = simages(0)
            PictureBox2_.Image = simages(0)
            PictureBox3.Image = simages2(0)
            PictureBox3_.Image = simages2(0)
            Timer1.Enabled = True
            Timer1.Interval = 4000
            ' Timer2.Enabled = True
            '   timer.Interval = 4
            '  Timer2.Enabled = True
            '   Timer2.Interval = 4000

            ' Timer3.Enabled = True
            '   Timer3.Interval = 6000
        Catch ex As Exception
            ' Me.Label1.Text = ex.Message
        End Try

    End Sub

    'Private Sub DownloadFile()
    '    If downloadUrls.Count > 0 Then
    '        Me.ProgressBar1.Value = 0
    '        Me.Label1.Text = ""
    '        Dim wc As New WebClient()
    '        AddHandler wc.DownloadProgressChanged, AddressOf ProgressChanged
    '        AddHandler wc.DownloadFileCompleted, AddressOf DownloadCompleted
    '        Dim url As String = downloadUrls.Dequeue()
    '        downloadUrl = url
    '        Dim fileName As String = url.Substring(url.LastIndexOf("/") + 1)
    '        wc.DownloadFileAsync(New Uri(url), fileName)
    '        downloadFileName = fileName
    '        Return
    '    End If
    'End Sub

    'Private Sub ProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
    '    Me.ProgressBar1.Value = e.BytesReceived / e.TotalBytesToReceive * 100
    '    Me.Label1.Text = e.BytesReceived & "/" & e.TotalBytesToReceive
    'End Sub

    'Private Sub DownloadCompleted(sender As Object, e As AsyncCompletedEventArgs)
    '    If (e.Error IsNot Nothing) Then
    '        Me.Label1.Text = e.Error.Message
    '        If System.IO.File.Exists(downloadFileName) Then '下载出错，删除临时文件重新下载
    '            System.IO.File.Delete(downloadFileName)
    '            downloadUrls.Enqueue(downloadUrl) '重新下载
    '        End If
    '    End If

    '    If (e.Cancelled) Then

    '    End If
    '    DownloadFile()
    '    Me.Label1.Text = "download comleted"
    'End Sub
    Private Sub Unzip()
        If Process.GetProcessesByName("7z").Length > 0 Then
            Dim processList As Process() = Process.GetProcessesByName("7z")
            For Each item As Process In processList
                item.Kill()
            Next
        End If
        Dim p As Process = New Process()
        If My.Computer.FileSystem.FileExists("C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\WindowsApplication1\bin\Debug\7z.exe") Then
            p.StartInfo.FileName = "C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\WindowsApplication1\bin\Debug\7z.exe"
            p.Start()
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If i < 2 Then
            i = i + 1
        Else
            i = 0
        End If
        If AdvIsHide Then
            PictureBox1_.Image = images(i)
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
        ' Timer1.Enabled = False
        '   Timer2.Enabled = True
        '  Timer2.Interval = 4000
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If j > 0 Then

            If n < 2 Then
                n = n + 1
            Else
                n = 0
            End If

            If AdvIsHide2 Then
                Dim r As System.Random = New System.Random()
                PictureBox2_.Image = simages(n)

                AnimateWindow(PictureBox2_.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  
                AdvIsHide2 = False
                PictureBox2.Image = simages(n）
            Else
                Dim r As System.Random = New System.Random()
                PictureBox2.Image = simages(n)

                AnimateWindow(PictureBox2_.Handle, 2000, AW_HIDE Or eff_sz(r.Next(9))) '窗体慢慢透明淡出  
                AdvIsHide2 = True

            End If
            Timer2.Enabled = False

            Timer3.Enabled = True
            Timer3.Interval = 4000
            j = 0
        Else
            AdvIsHide2 = True
            ' PictureBox1.BringToFront()
            ' PictureBox1.Visible = False
            PictureBox2.Visible = True
            PictureBox3.Visible = True
            Dim r As System.Random = New System.Random()
            AnimateWindow(PictureBox1.Handle, 2000, eff_sz(r.Next(9))) '中间扩散
            ' PictureBox2.Image = simages(0)
            ' PictureBox3.Image = simages(0)
            PictureBox1.Visible = False
            j = j + 1
        End If


        ' Timer2.Enabled = False
        '  Timer3.Enabled = True
        ' Timer3.Interval = 4000

        '  Timer1.Enabled = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        If k > 0 Then
            PictureBox1_.Visible = True
            PictureBox1.Visible = True
            PictureBox2.Visible = False
            PictureBox3.Visible = False
            PictureBox2_.Visible = False
            PictureBox3_.Visible = False

            AdvIsHide = False
            '   PictureBox1.Image = images(2)
            '  PictureBox1_.Image = images(2)
            Me.Timer1.Enabled = True
            Me.Timer3.Enabled = False
            k = 0
        Else
            'advIsHide3 = True
            'If m < 2 Then
            '    m = m + 1
            'Else
            '    m = 0
            'End If
            'If advIsHide3 Then
            '    PictureBox3_.Image = simages2(m)
            '    Dim r As System.Random = New System.Random()
            '    AnimateWindow(PictureBox3_.Handle, 2000, eff_sz(r.Next(9))) '中间扩散  

            '    advIsHide3 = False
            '    PictureBox3.Image = simages2(m)
            'Else
            '    PictureBox3.Image = simages2(m)
            '    Dim v As System.Random = New System.Random()
            '    AnimateWindow(PictureBox3_.Handle, 2000, AW_HIDE Or eff_sz(v.Next(9))) '窗体慢慢透明淡出  
            '    advIsHide3 = True
            'End If
            'k = k + 1
            WebBrowser1.Visible = True
        End If
    End Sub

    'Private Sub Install()
    '    Dim result As Integer = FindWindow(vbNullString, "7-Zip 17.00 beta Setup")
    '    If result <> 0 Then
    '        Dim subresult As Integer = FindWindowEx(result, 0, vbNullString, "&I" + "nstall")
    '        Dim path As Integer = FindWindowEx(result, 0, "Edit", vbNullString)

    '        Dim i As Integer = SendMessage(path, &HC, 0, "C:\PhotoPrint\7z")

    '        Dim j As Integer = SendMessage(subresult, &HF5, 0, 0)

    '        While True
    '            Dim close As Integer = FindWindowEx(result, 0, vbNullString, "Close")
    '            If (close <> 0) Then
    '                SendMessage(close, &HF5, 0, 0)
    '                timer.Stop()
    '                Return
    '            End If
    '        End While
    '    End If

    'End Sub

End Class
