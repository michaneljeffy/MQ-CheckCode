Public Class InfoLog

End Class

Public Class InfoLogA
    Inherits InfoLog
    Public Sub New(ByVal createdat As String, ByVal version As String, ByVal cpu As String, ByVal mem As String, ByVal os As String, Optional ByVal type As String = "init")
        atype = type
        aversion = version
        acreated_at = createdat
        acpu = cpu
        amem = mem
        aos = os
    End Sub
#Region "property"
    Private atype As String
    Private acreated_at As String
    Private aversion As String
    Private acpu As String
    Private amem As String
    Private aos As String

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

    Property Version As String
        Get
            Return aversion
        End Get
        Set(value As String)
            aversion = value
        End Set
    End Property

    Property CPU As String
        Get
            Return acpu
        End Get
        Set(value As String)
            acpu = value
        End Set
    End Property

    Property Memory As String
        Get
            Return amem
        End Get
        Set(value As String)
            amem = value
        End Set
    End Property

    Property OS As String
        Get
            Return aos
        End Get
        Set(value As String)
            aos = value
        End Set
    End Property
#End Region
End Class

Public Class InfoLogB
    Inherits InfoLog

#Region "property"
    Private btype As String
    Private bcreated_at As String
    Private bshow_data As String

    Public Sub New(ByVal createdat As String, ByVal showdata As String, Optional ByVal type As String = "change_shows")
        btype = type
        bcreated_at = createdat
        bshow_data = showdata
    End Sub

    Public Property type As String
        Get
            Return btype
        End Get
        Set(value As String)
            btype = value
        End Set
    End Property

    Public Property CreatedAt As String
        Get
            Return bcreated_at
        End Get
        Set(value As String)
            bcreated_at = value
        End Set
    End Property

    Public Property ShowData As String
        Get
            Return bshow_data
        End Get
        Set(value As String)
            bshow_data = value
        End Set
    End Property
#End Region

End Class

Public Class InfoLogC
    Inherits InfoLog
    Public Sub New(ByVal createdat As String, ByVal url As String, ByVal id As String, Optional ByVal type As String = "printer_photo")

    End Sub

#Region "property"
    Private ccType As String
    Private ccreated_at As String
    Private curl As String
    Private cid As String

    Public Property type As String
        Get
            Return ccType
        End Get
        Set(value As String)
            ccType = value
        End Set
    End Property

    Public Property created_at As String
        Get
            Return ccreated_at
        End Get
        Set(value As String)
            ccreated_at = value
        End Set
    End Property

    Public Property Url As String
        Get
            Return curl
        End Get
        Set(value As String)
            curl = value
        End Set
    End Property

    Public Property ID As String
        Get
            Return cid
        End Get
        Set(value As String)
            cid = value
        End Set
    End Property
#End Region
End Class

Public Class InfoLogD
    Inherits InfoLog
    Public Sub New(ByVal createdat As String, ByVal action As String, Optional ByVal type As String = "action")
        dtype = type
        dcreatedat = createdat
        daction = action
    End Sub

    Private dtype As String
    Private dcreatedat As String
    Private daction As String

    Public Property type As String
        Get
            Return dtype
        End Get
        Set(value As String)
            dtype = value
        End Set
    End Property

    Public Property created_at As String
        Get
            Return dcreatedat
        End Get
        Set(value As String)
            dcreatedat = value
        End Set
    End Property

    Public Property Action As String
        Get
            Return daction
        End Get
        Set(value As String)
            daction = value
        End Set
    End Property
End Class

Public Class InfoLogE
    Inherits InfoLog
    Public Sub New(ByVal createdat As String, ByVal oldstatus As String, ByVal newstatus As String, Optional ByVal type As String = "change_status")
        etype = type
        ecreatedat = createdat
        eoldstatus = oldstatus
        enewstatus = newstatus
    End Sub

    Private etype As String
    Private ecreatedat As String
    Private eoldstatus As String
    Private enewstatus As String

    Public Property type As String
        Get
            Return etype
        End Get
        Set(value As String)
            etype = value
        End Set
    End Property

    Public Property created_at As String
        Get
            Return ecreatedat
        End Get
        Set(value As String)
            ecreatedat = value
        End Set
    End Property

    Public Property OldStatus As String
        Get
            Return eoldstatus
        End Get
        Set(value As String)
            eoldstatus = value
        End Set
    End Property

    Public Property NewStatus As String
        Get
            Return enewstatus
        End Get
        Set(value As String)
            enewstatus = value
        End Set
    End Property
End Class

Public Class InfoLogF
    Inherits InfoLog
    Public Sub New(ByVal createdat As String)

    End Sub
End Class