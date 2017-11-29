Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Threading
Imports ICSharpCode.SharpZipLib
Imports ICSharpCode
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Zip.Compression
Imports ICSharpCode.SharpZipLib.Core
Imports Ionic.Zip
Imports Newtonsoft.Json
Imports Newtonsoft
Imports System.IO
Imports System.ComponentModel
Imports System.Drawing.Imaging

Module Module1
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1Parent As Integer, ByVal hwndChildAfter As Integer, ByVal lpszClass As String, ByVal lpszWindowName As String) As Integer
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal Msg As Integer, ByVal ByValByValwParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal Msg As Integer, ByVal ByValByValwParam As Integer, ByVal lParam As String) As Integer

    Public Declare Function OpenPrinter Lib "winspoo.drv" Alias "OpenPrinterA" (ByVal szPrinter As String, ByRef hPrinter As IntPtr, ByVal pd As IntPtr)
    Public Declare Function ClosePrinter Lib "winspool.drv" Alias "ClosePrintA" (ByVal hPrinter As IntPtr)
    Public Declare Function StartDocPrinter Lib "winspool.drv" Alias "StartDocPrinterA" (ByVal hPrinter As IntPtr, ByVal level As Int32, <MarshalAs(UnmanagedType.LPStruct)> di As DOCINFOA)
    Public Declare Function EndDocPrinter Lib "winspool.drv" Alias "EndDocPrinterA" (ByVal hPrinter As IntPtr)
    Public Declare Function StartPagePrinter Lib "winspool.drv" Alias "StartPagePrinterA" (ByVal hPrinter As IntPtr)
    Public Declare Function EndPagePrinter Lib "winspool.drv" Alias "EndPagePrinterA" (ByVal hPrinter As IntPtr)
    Public Declare Function WritePrinter Lib "winspool.drv" Alias "WritePrinterA" (ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32)

    Public RequestCount As Integer = 0

    Sub Main()
        'Dim client As WebClient = New WebClient()
        'AddHandler client.DownloadFileCompleted, AddressOf Unzip
        'Try
        '    client.DownloadFileAsync(New Uri("http://d.7-zip.org/a/7z1700.exe"), "7z.exe")
        'Catch ex As Exception

        'End Try

        ''  Unzip()
        'Console.ReadLine()

        ''UnZipWithLib()
        'Dim datetimestamp As Integer = ConvertTimeToUnixStamp()
        '' FastExtractZip("C:\Users\Administrator\Desktop\7z.zip", "C:\Users\Administrator\Desktop\7z")

        'Dim wc As New WebClient()
        ''wc.Headers.Add(HttpRequestHeader.ContentType, "image/webp")
        '' Dim img As Bitmap = Image.FromStream(wc.OpenRead("http://static.in66.co/images/20170504/590b0419d60b4.jpg"))
        ''    img.Save("testa.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        '' img = New Bitmap(img, New Size(1080, 1440))
        '' img.Save("testb.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

        ''  wc.DownloadFileAsync(New Uri("http://static.in66.co/images/20170508/590fbe01df4f3.jpg?imageView2/2/w/1080/h/720/interlace/0/q/80"), "test.jpg")
        ''   AddHandler wc.DownloadFileCompleted, AddressOf fileDownloadCompleted
        ''"http://static.in66.co/images/20170508/590fbe01df4f3.jpg"
        'Dim url As String = "http://static.in66.co/images/20170508/590fbe01df4f3.jpg?imageView2/2/w/1080/h/720/interlace/0/q/80"
        'Dim array As String() = url.Split("/")
        'Dim fileName As String
        'For Each item As String In array
        '    If item.IndexOf(".jpg") > 0 Then
        '        fileName = item.Substring(0, item.LastIndexOf("?"))
        '    End If
        'Next
        'Dim log1 As New Log()
        'Dim errorLog As New ErrorLogA

        'errorLog.LogType = "crash"
        'errorLog.CreatedAt = DateTime.Now.ToString()
        'errorLog.Version = "1.0.28"
        'errorLog.Memory = "4080"
        'errorLog.OS = "win7"

        'Dim errorlog2 As New ErrorLogB()
        'errorlog2.Type = "network_error"
        'errorlog2.CreatedAt = DateTime.Now.ToString()
        'log1.ErrorLog.Add(errorLog)
        'log1.ErrorLog.Add(errorlog2)


        'Dim result As String = JsonConvert.SerializeObject(log1)
        '  Dim str As String = "{ErrorLog:[],WarningLog:[],InfoLog:[{type:init,created_at:1496630221,Version:120,cPU:4,Memory: 2226M,OS:Microsoft Windows NT 6.1.7601 Service Pack 1},{type:change_shows,createdAt:1496630223,ShowData:1},{type:change_status,created_at:1496630226,OldStatus:-1,NewStatus:0}]}"
        'Dim str = ""


        'Dim errorlogs As New List(Of ErrorLog)
        'Dim warninglogs As New List(Of WarningLog)
        'Dim infologs As New List(Of InfoLog)
        'Dim logManagerList As New List(Of LogManager)()

        'Dim result As Boolean = False
        'Dim jsonString As String = String.Empty
        'Dim logFile As String = ".\photo_json -" & DateAndTime.Year(Now) & "-" & DateAndTime.Month(Now) & "-" & DateAndTime.Day(Now) & ".json"
        'Try
        '    ' 存在就进行读操作
        '    'Logger.info1("logFile: " & logFile)
        '    If System.IO.File.Exists(logFile) Then
        '        Dim iFileSize As Integer = New System.IO.FileInfo(logFile).Length
        '        '  Logger.info1("log file size max is logFile : " & logFile & " fileSize: " & iFileSize)
        '        ' >= 30kb 就丢弃掉
        '        If iFileSize = 0 Or iFileSize > 30 * 1024 Then
        '            '   Logger.info1("log file size max is : " & iFileSize)
        '        End If
        '        jsonString = My.Computer.FileSystem.ReadAllText(logFile)
        '        '   Logger.info1("file tmp :" & jsonString)
        '        ' System.IO.File.Delete(logFile)         '删除该文件
        '        ' System.IO.File.Create(logFile)      '创建同名空文件
        '    End If
        '    Dim JsonList As List(Of String) = New List(Of String)()
        '    Dim array As String() = jsonString.Split(";")
        '    For i = 0 To array.Length - 1
        '        If Not String.IsNullOrEmpty(array(i)) Then
        '            JsonList.Add(array(i).Trim())
        '        End If
        '    Next

        '    Dim logMan As New LogManager(errorlogs, warninglogs, infologs)

        '    For Each item In logMan.InfoLog

        '    Next
        '    For Each item In JsonList
        '        logMan = JsonConvert.DeserializeObject(Of LogManager)(item)
        '        For Each info In logMan.InfoLog

        '        Next
        '        Dim count = logMan.InfoLog.Count
        '        logManagerList.Add(logMan)
        '        Dim str As String = JsonConvert.SerializeObject(logMan)
        '        Console.WriteLine(str)
        '    Next
        '    result = True

        'Catch ex As Exception
        '    result = False
        'End Try
        'Dim request As WebRequest
        'Dim response As HttpWebResponse
        'Dim stream As Stream
        'Dim sr As StreamReader
        'Dim str As String
        'Try
        '    Dim url As String = "http://box-qa.in66.co/adSwiper/index.html"


        '    request = WebRequest.Create(url)

        '    response = CType(request.GetResponse, HttpWebResponse)
        '    stream = request.GetResponse.GetResponseStream()
        '    sr = New StreamReader(stream, System.Text.Encoding.UTF8)
        '    str = sr.ReadToEnd.ToString

        '    Dim wc As New System.Net.WebClient()
        '    wc.DownloadFile(url, "index.html")
        'Catch ex As Exception

        'End Try


        ' Dim wc As New WebClient
        '  wc.DownloadFile(url, "index.html")
        Dim url As String = "http://inimg01.jiuyan.info//in/2017/08/23/56F3A48A-8A71-9E47-F82F-246027138591.jpg?imageMogr2/thumbnail/875x"
        'Get_Image_Checked(url)
        Dim bm As Bitmap = Nothing
        Try
            bm = Get_Image_Checked(url)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        If bm IsNot Nothing Then
            Console.WriteLine("下载成功")
        Else
            Console.WriteLine("下载失败")
        End If
        Console.ReadLine()

    End Sub

    Private Sub fileDownloadCompleted()
        ' Dim img As Bitmap
        ' img = Image.FromFile("test.jpg")
    End Sub

    Public Function Get_Image_Checked(ByVal imgUrl As String) As Bitmap
        If RequestCount > 2 Then
            RequestCount = 0
            Return Nothing
        End If
        Dim simage As Bitmap = Nothing
        Try
            Dim wc As WebClient = New WebClient()
            Dim stream As Stream = wc.OpenRead(imgUrl)

            Dim cps As MemoryStream = New MemoryStream()

            CopyStream(stream, cps)

            Dim totalStreamLength As Long = cps.Length

            Dim dataLength As Long = CType(wc.ResponseHeaders.Get("Content-Length"), Long)
            If totalStreamLength = dataLength Then
                simage = Bitmap.FromStream(cps)
            Else
                Get_Image_Checked(imgUrl)
                RequestCount = RequestCount + 1
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return simage
    End Function

    Private Sub CopyStream(ByVal input As Stream, ByVal output As Stream)
        Dim buffer(32767) As Byte
        Dim readCount As Integer = 0
        readCount = input.Read(buffer, 0, buffer.Length)
        While readCount > 0
            output.Write(buffer, 0, readCount)
            readCount = input.Read(buffer, 0, buffer.Length)
        End While
    End Sub

    Private Function Get_Stream_Length(ByVal stream As Stream) As Long
        Dim result As Long = 0
        Try
            Dim totalStreamLength As Long = 0
            Dim readCount As Integer = 0
            Dim buffer(4095) As Byte

            readCount = stream.Read(buffer, 0, 4096)

            While readCount > 0
                totalStreamLength = totalStreamLength + readCount
                readCount = stream.Read(buffer, 0, 4095)
            End While

            result = totalStreamLength
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            result = 0
        End Try
        Return result
    End Function

    Private Sub Unzip()
        Dim p As Process = New Process()
        p.StartInfo.FileName = "C:\Users\Administrator\Documents\Visual Studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\bin\Debug\7z.exe"
        p.Start()

        Dim result As Integer = FindWindow(vbNullString, "7-Zip 17.00 beta Setup")
        Dim subresult As Integer = FindWindowEx(result, 0, vbNullString, "&I" + "nstall")
        Dim path As Integer = FindWindowEx(result, 0, "Edit", vbNullString)

        Dim i As Integer = SendMessage(path, &HC, 0, "C:\PhotoPrint\7z")

        Dim j As Integer = SendMessage(subresult, &HF5, 0, 0)

        While True
            Dim close As Integer = FindWindowEx(result, 0, vbNullString, "Close")
            If (close <> 0) Then
                SendMessage(close, &HF5, 0, 0)
                Return
            End If
        End While
    End Sub

    Private Sub UnZipWithLib()
        Dim zip As Ionic.Zip.ZipFile = New Ionic.Zip.ZipFile("C:\Users\Administrator\Desktop\0-9.zip")
        zip.ExtractAll("C:\Users\Administrator\Desktop\7z", ExtractExistingFileAction.OverwriteSilently)
    End Sub

    Public Sub FastExtractZip(ByVal zipPath As String, ByVal folderPath As String, Optional ByVal fileFilter As String = "", Optional ByVal password As String = Nothing)
        Dim fz As New FastZip
        If password <> Nothing Then fz.Password = password
        fz.ExtractZip(zipPath, folderPath, fileFilter)
    End Sub

    Private Function ConvertTimeToUnixStamp() As Integer
        Dim startTime As DateTime = TimeZone.CurrentTimeZone.ToLocalTime(New System.DateTime(1970, 1, 1))
        Return (DateTime.Now - startTime).TotalSeconds
    End Function

    Public Function SendBytesToPrinter(ByVal szPrinterName As String, pBytes As IntPtr, dwCount As Int32) As Boolean
        Dim dwError As Int32 = 0
        Dim DwWritten As Int32 = 0
        Dim hPrinter = New IntPtr(0)
        Dim di As New DOCINFOA
        Dim bSuccess As Boolean
        di.PDocName = "MY VB.NET RAW DOCUMENT"
        di.pDataType = "RAW"
        If (OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero)) Then
            If (StartDocPrinter(hPrinter, 1, di)) Then
                If (StartPagePrinter(hPrinter)) Then
                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, DwWritten)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If
        If (bSuccess = False) Then
            dwError = Marshal.GetLastWin32Error()
        End If

        Return bSuccess
    End Function

    Public Function SendFileTOPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        Dim fs As FileStream = New FileStream(szFileName, FileMode.Open)

        Dim dr As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte()
        Dim bSuccess As Boolean = False
        Dim pUnmanagedBytes = New IntPtr(0)
        Dim nLength As Integer
        nLength = Convert.ToInt32(fs.Length)
        bytes = dr.ReadBytes(nLength)
        pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength)
        Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength)
        bSuccess = SendBytesToPrinter(szFileName, pUnmanagedBytes, nLength)
        Return True
    End Function

End Module

Public Class CopyedStream
    Implements ICloneable

    Private mstream As Stream

    Public Property CStream As Stream
        Get
            Return mstream
        End Get
        Set(value As Stream)
            mstream = value
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return mstream
    End Function


    Public Sub DownloadPhotoAsync(ByVal url As String)
        Try
            Dim wc As New System.Net.WebClient()
            AddHandler wc.DownloadFileCompleted, AddressOf DownloadPhotoCompleted
            wc.DownloadFileAsync(New Uri(url), "1.jpg")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public simage As Image
    Public Sub DownloadPhotoCompleted(sender As Object, e As AsyncCompletedEventArgs)
        If (String.IsNullOrEmpty(e.Error.Message)) Then '下载成功
            simage = Image.FromFile("1.jpg")
        Else
            'DownloadPhotoAsync()
        End If
    End Sub
End Class

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> Structure DOCINFOA
    <MarshalAs(UnmanagedType.LPStr)>
    Public PDocName As String

    <MarshalAs(UnmanagedType.LPStr)>
    Public pOutPutFile As String

    <MarshalAs(UnmanagedType.LPStr)>
    Public pDataType As String
End Structure
