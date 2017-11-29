Public Class LogManager
    Private merrorLog As New List(Of ErrorLog)
    Private mwarningLog As New List(Of WarningLog)
    Private minfoLog As New List(Of InfoLog)

    Public Sub New(ByVal errorlogs As List(Of ErrorLog), ByVal warninglogs As List(Of WarningLog), ByVal infologs As List(Of InfoLog))
        merrorLog = errorlogs
        mwarningLog = warninglogs
        minfoLog = infologs
    End Sub

    Public Property ErrorLog As List(Of ErrorLog)
        Get
            Return merrorLog
        End Get
        Set(value As List(Of ErrorLog))
            merrorLog = value
        End Set
    End Property

    Public Property WarningLog As List(Of WarningLog)
        Get
            Return mwarningLog
        End Get
        Set(value As List(Of WarningLog))
            mwarningLog = value
        End Set
    End Property

    Public Property InfoLog As List(Of InfoLog)
        Get
            Return minfoLog
        End Get
        Set(value As List(Of InfoLog))
            minfoLog = value
        End Set
    End Property
End Class
