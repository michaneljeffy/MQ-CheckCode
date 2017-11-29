Public Class WarningLog
    Public Sub New()

    End Sub
End Class

Public Class WarningLogA
    Inherits WarningLog
    Public Sub New(ByVal createdat As String, ByVal url As String, Optional ByVal type As String = "not_found")
        atype = type
        acreated_at = createdat
        aurl = url
    End Sub
#Region "property"
    Private atype As String
    Private acreated_at As String
    Private aurl As String

    Property type As String
        Get
            Return atype
        End Get
        Set(value As String)
            atype = value
        End Set
    End Property

    Property created_at As String
        Get
            Return acreated_at
        End Get
        Set(value As String)
            acreated_at = value
        End Set
    End Property

    Property Url As String
        Get
            Return aurl
        End Get
        Set(value As String)
            aurl = value
        End Set
    End Property
#End Region

End Class

Public Class WarningLogB
    Inherits WarningLog

    Public Sub New(ByVal createdat As String, ByVal url As String, ByVal source As String, Optional type As String = "data_verify")
        btype = type
        bcreated_at = createdat
        burl = url
        bsource = source
    End Sub

#Region "property"
    Private btype As String
    Private bcreated_at As String
    Private burl As String
    Private bsource As String

    Property type As String
        Get
            Return btype
        End Get
        Set(value As String)
            btype = value
        End Set
    End Property

    Property created_at As String
        Get
            Return bcreated_at
        End Get
        Set(value As String)
            bcreated_at = value
        End Set
    End Property

    Property Url As String
        Get
            Return burl
        End Get
        Set(value As String)
            burl = value
        End Set
    End Property

    Property Source As String
        Get
            Return bsource
        End Get
        Set(value As String)
            bsource = value
        End Set
    End Property
#End Region

End Class
