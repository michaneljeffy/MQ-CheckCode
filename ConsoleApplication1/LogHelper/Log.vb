Public Class Log
    Private merrorLog As ErrorLog
    Private mwarningLog As WarningLog
    Private minfoLog As InfoLog

    Public Property ErrorLog As ErrorLog
        Get
            Return merrorLog
        End Get
        Set(value As ErrorLog)
            merrorLog = value
        End Set
    End Property

    Public Property WarningLog As WarningLog
        Get
            Return mwarningLog
        End Get
        Set(value As WarningLog)
            mwarningLog = value
        End Set
    End Property

    Public Property InfoLog As InfoLog
        Get
            Return minfoLog
        End Get
        Set(value As InfoLog)
            minfoLog = value
        End Set
    End Property
End Class
