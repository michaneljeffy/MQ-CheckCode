Public Class ErrorLog
    Public Sub New()

    End Sub
End Class

Public Class ErrorLogA
    Inherits ErrorLog
#Region "property"
    Private elogtype As String
    Private ecreate_at As String
    Private eversion As String
    Private ecpu As String
    Private emem As String
    Private eos As String

    Public Property LogType() As String
        Get
            Return elogtype
        End Get
        Set(value As String)
            elogtype = value
        End Set
    End Property

    Public Property CreatedAt() As String
        Get
            Return ecreate_at
        End Get
        Set(value As String)
            ecreate_at = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return eversion
        End Get
        Set(value As String)
            eversion = value
        End Set
    End Property

    Public Property CPU() As String
        Get
            Return ecpu
        End Get
        Set(value As String)
            ecpu = value
        End Set
    End Property

    Public Property Memory() As String
        Get
            Return emem
        End Get
        Set(value As String)
            emem = value
        End Set
    End Property

    Public Property OS() As String
        Get
            Return eos
        End Get
        Set(value As String)
            eos = value
        End Set
    End Property
#End Region
End Class

Public Class ErrorLogB
    Inherits ErrorLog
    Private btype As String
    Private bcreate_at As String
    Private bnetwork As String

    Public Property Type As String
        Get
            Return btype
        End Get
        Set(value As String)
            btype = value
        End Set
    End Property

    Public Property CreatedAt As String
        Get
            Return bcreate_at
        End Get
        Set(value As String)
            bcreate_at = value
        End Set
    End Property

    Public Property NetWork As String
        Get
            Return bnetwork
        End Get
        Set(value As String)
            bnetwork = value
        End Set
    End Property
End Class

Public Class ErrorLogC
    Inherits ErrorLog
#Region "property"
    Private c_type As String
    Private ccreated_at As String
    Private curl As String
    Private cstatus As String

    Property Type As String
        Get
            Return c_type
        End Get
        Set(value As String)
            c_type = value
        End Set
    End Property

    Property CreatedAt As String
        Get
            Return ccreated_at
        End Get
        Set(value As String)
            ccreated_at = value
        End Set
    End Property

    Property Url As String
        Get
            Return curl
        End Get
        Set(value As String)
            curl = value
        End Set
    End Property

    Property Status As String
        Get
            Return cstatus
        End Get
        Set(value As String)
            cstatus = value
        End Set
    End Property
#End Region

End Class
