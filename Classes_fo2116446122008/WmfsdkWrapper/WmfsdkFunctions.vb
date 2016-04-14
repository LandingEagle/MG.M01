
Imports System 
Imports System.Runtime.InteropServices 


''' <summary>
''' Wrapper for accessing the WMFSDK functions from a managed-code base.
''' </summary>
Public NotInheritable Class WmfsdkFunctions

#Region " Consructor "

    ''' <summary>
    ''' No public constructor needed because all we have here is static interfaces.
    ''' </summary>
    Private Sub New()
    End Sub

#End Region

End Class

#Region " Public Interfaces "

<Guid("96406BD9-2B2B-11d3-B36B-00C04F6108FF"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Public Interface IWMMetadataEditor

    Function Open(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszFilename As String) As UInteger

    Function Close() As UInteger

    Function Flush() As UInteger

End Interface

<Guid("15CC68E3-27CC-4ecd-B222-3F5D02D80BD5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Public Interface IWMHeaderInfo3

    Function GetAttributeCount(<[In]()> ByVal wStreamNum As UShort, _
                               <Out()> ByRef pcAttributes As UShort) As UInteger

    Function GetAttributeByIndex(<[In]()> ByVal wIndex As UShort, <Out(), _
                                 [In]()> ByRef pwStreamNum As UShort, _
                                 <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszName As String, _
                                 <Out(), [In]()> ByRef pcchNameLen As UShort, _
                                 <Out()> ByRef pType As WMT_ATTR_DATATYPE, _
                                 <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                                 <Out(), [In]()> ByRef pcbLength As UShort) As UInteger

    Function GetAttributeByName(<Out(), [In]()> ByRef pwStreamNum As UShort, _
                                <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String, _
                                <Out()> ByRef pType As WMT_ATTR_DATATYPE, _
                                <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                                <Out(), [In]()> ByRef pcbLength As UShort) As UInteger

    Function SetAttribute(<[In]()> ByVal wStreamNum As UShort, _
                          <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String, _
                          <[In]()> ByVal Type As WMT_ATTR_DATATYPE, _
                          <[In](), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                          <[In]()> ByVal cbLength As UShort) As UInteger

    Function GetMarkerCount(<Out()> ByRef pcMarkers As UShort) As UInteger

    Function GetMarker(<[In]()> ByVal wIndex As UShort, _
                       <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszMarkerName As String, _
                       <Out(), [In]()> ByRef pcchMarkerNameLen As UShort, _
                       <Out()> ByRef pcnsMarkerTime As ULong) As UInteger

    Function AddMarker(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszMarkerName As String, _
                       <[In]()> ByVal cnsMarkerTime As ULong) As UInteger

    Function RemoveMarker(<[In]()> ByVal wIndex As UShort) As UInteger

    Function GetScriptCount(<Out()> ByRef pcScripts As UShort) As UInteger

    Function GetScript(<[In]()> ByVal wIndex As UShort, _
                       <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszType As String, _
                       <Out(), [In]()> ByRef pcchTypeLen As UShort, _
                       <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszCommand As String, _
                       <Out(), [In]()> ByRef pcchCommandLen As UShort, _
                       <Out()> ByRef pcnsScriptTime As ULong) As UInteger

    Function AddScript(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszType As String, _
                       <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszCommand As String, _
                       <[In]()> ByVal cnsScriptTime As ULong) As UInteger

    Function RemoveScript(<[In]()> ByVal wIndex As UShort) As UInteger

    Function GetCodecInfoCount(<Out()> ByRef pcCodecInfos As UInteger) As UInteger

    Function GetCodecInfo(<[In]()> ByVal wIndex As UInteger, <Out(), [In]()> ByRef pcchName As UShort, _
                          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszName As String, _
                          <Out(), [In]()> ByRef pcchDescription As UShort, _
                          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszDescription As String, _
                          <Out()> ByRef pCodecType As WMT_CODEC_INFO_TYPE, _
                          <Out(), [In]()> ByRef pcbCodecInfo As UShort, _
                          <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pbCodecInfo As Byte()) As UInteger

    Function GetAttributeCountEx(<[In]()> ByVal wStreamNum As UShort, _
                                 <Out()> ByRef pcAttributes As UShort) As UInteger

    Function GetAttributeIndices(<[In]()> ByVal wStreamNum As UShort, _
                                 <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszName As String, _
                                 <[In]()> ByRef pwLangIndex As UShort, _
                                 <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pwIndices As UShort(), _
                                 <Out(), [In]()> ByRef pwCount As UShort) As UInteger

    Function GetAttributeByIndexEx(<[In]()> ByVal wStreamNum As UShort, <[In]()> ByVal wIndex As UShort, _
                                   <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszName As String, _
                                   <Out(), [In]()> ByRef pwNameLen As UShort, _
                                   <Out()> ByRef pType As WMT_ATTR_DATATYPE, _
                                   <Out()> ByRef pwLangIndex As UShort, _
                                   <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                                   <Out(), [In]()> ByRef pdwDataLength As UInteger) As UInteger

    Function ModifyAttribute(<[In]()> ByVal wStreamNum As UShort, <[In]()> ByVal wIndex As UShort, _
                             <[In]()> ByVal Type As WMT_ATTR_DATATYPE, <[In]()> ByVal wLangIndex As UShort, _
                             <[In](), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                             <[In]()> ByVal dwLength As UInteger) As UInteger

    Function AddAttribute(<[In]()> ByVal wStreamNum As UShort, _
                          <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String, _
                          <Out()> ByRef pwIndex As UShort, <[In]()> ByVal Type As WMT_ATTR_DATATYPE, _
                          <[In]()> ByVal wLangIndex As UShort, _
                          <[In](), MarshalAs(UnmanagedType.LPArray)> ByVal pValue As Byte(), _
                          <[In]()> ByVal dwLength As UInteger) As UInteger

    Function DeleteAttribute(<[In]()> ByVal wStreamNum As UShort, _
                             <[In]()> ByVal wIndex As UShort) As UInteger

    Function AddCodecInfo(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String, _
                          <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal pwszDescription As String, _
                          <[In]()> ByVal codecType As WMT_CODEC_INFO_TYPE, _
                          <[In]()> ByVal cbCodecInfo As UShort, _
                          <[In](), MarshalAs(UnmanagedType.LPArray)> ByVal pbCodecInfo As Byte()) As UInteger

End Interface

#End Region

#Region " Public Enums "

Public Enum WMT_ATTR_DATATYPE
    WMT_TYPE_DWORD = 0
    WMT_TYPE_STRING = 1
    WMT_TYPE_BINARY = 2
    WMT_TYPE_BOOL = 3
    WMT_TYPE_QWORD = 4
    WMT_TYPE_WORD = 5
    WMT_TYPE_GUID = 6
End Enum

Public Enum WMT_CODEC_INFO_TYPE
    WMT_CODECINFO_AUDIO = 0
    WMT_CODECINFO_VIDEO = 1
    WMT_CODECINFO_UNKNOWN = 16777215
End Enum

#End Region
