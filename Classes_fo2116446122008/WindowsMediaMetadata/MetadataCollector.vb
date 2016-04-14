Imports System
Imports System.Text
Imports System.IO
Imports System.Collections.Specialized
Imports System.Runtime.InteropServices

Imports WmfsdkWrapper.WmfsdkFunctions
Imports WmfsdkWrapper

''' <summary>
''' Uses wrapper class from Window Media Format SDK translated from C#.
''' This class was created using a C# console application in the
''' WMFSDK as a starting point. Converted to normal class with properties,
''' public methods, and constructors. (Also to Visual Basic!)
''' </summary>
Public NotInheritable Class MetadataCollector

#Region " Private Members "

    Private _Builder As StringBuilder
    Private _WmaFileInfo As FileInfo
    Private _FileName As String

    ' Private values for properties.
    Private _Attributes As StringCollection
    Private _DataTypes As StringCollection
    Private _Values As StringCollection
    Private _AlbumArtist As String
    Private _AlbumTitle As String
    Private _Author As String
    Private _BeatsPerMinute As String
    Private _Bitrate As Integer
    Private _Category As String
    Private _Composer As String
    Private _Conductor As String
    Private _Copyright As String
    Private _Description As String
    Private _Director As String
    Private _Duration As UInt64
    Private _FileSize As UInt64
    Private _Genre As String
    Private _IsVBR As Boolean
    Private _OriginalAlbumTitle As String
    Private _OriginalArtist As String
    Private _OriginalFileName As String
    Private _OriginalLyricist As String
    Private _OriginalReleaseYear As String
    Private _Picture As Image
    Private _Producer As String
    Private _Publisher As String
    Private _Rating As String
    Private _Title As String
    Private _TrackNumber As String
    Private _Writer As String
    Private _Year As String

#End Region

#Region " Private Constructor "

    ''' <summary>
    ''' Used when file name is passed with method.
    ''' </summary>
    Public Sub New()
        _FileName = ""
        _Builder = New StringBuilder()
        _Attributes = New StringCollection()
        _DataTypes = New StringCollection()
        _Values = New StringCollection()
    End Sub

    ''' <summary>
    ''' Used when file name is also initialized.
    ''' Then method is called.
    ''' </summary>
    ''' <param name="fileName">Return metadata from this file name</param>
    Public Sub New(ByVal fileName As String)
        _FileName = fileName
        _Builder = New StringBuilder()
        _Attributes = New StringCollection()
        _DataTypes = New StringCollection()
        _Values = New StringCollection()
        ReturnMetadata()
    End Sub

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' Time/Date file was created. Data type: Date.
    ''' </summary>
    Public ReadOnly Property FileCreationTime() As Date
        Get
            Return _WmaFileInfo.CreationTime
        End Get
    End Property

    ''' <summary>
    ''' Full file name, including path.
    ''' </summary>
    Public ReadOnly Property FileFullName() As String
        Get
            Return _WmaFileInfo.FullName
        End Get
    End Property

    ''' <summary>
    ''' Indicates if file is read-only.
    ''' </summary>
    Public ReadOnly Property FileIsReadOnly() As Boolean
        Get
            Return _WmaFileInfo.IsReadOnly
        End Get
    End Property

    Public ReadOnly Property FileLastAccessTime() As Date
        Get
            Return _WmaFileInfo.LastAccessTime
        End Get
    End Property

    Public ReadOnly Property FileLastWriteTime() As Date
        Get
            Return _WmaFileInfo.LastWriteTime
        End Get
    End Property

    Public ReadOnly Property FileLength() As Long
        Get
            Return _WmaFileInfo.Length
        End Get
    End Property

    ''' <summary>
    ''' Collection of the names of all Windows Media attributes
    ''' present in this file.
    ''' </summary>
    Public ReadOnly Property Attributes() As StringCollection
        Get
            Return _Attributes
        End Get
    End Property

    ''' <summary>
    ''' Collection of the data types of all of the values
    ''' present in this file. This, in some instances,
    ''' can be used to convert the strings back to the
    ''' original format.
    ''' </summary>
    Public ReadOnly Property DataTypes() As StringCollection
        Get
            Return _DataTypes
        End Get
    End Property

    ''' <summary>
    ''' Values (as strings) for all of the attributes.
    ''' </summary>
    Public ReadOnly Property Values() As StringCollection
        Get
            Return _Values
        End Get
    End Property

    Public ReadOnly Property AlbumArtist() As String
        Get
            Return _AlbumArtist
        End Get
    End Property

    Public ReadOnly Property AlbumTitle() As String
        Get
            Return _AlbumTitle
        End Get
    End Property

    Public ReadOnly Property Author() As String
        Get
            Return _Author
        End Get
    End Property

    Public ReadOnly Property BeatsPerMinute() As String
        Get
            Return _BeatsPerMinute
        End Get
    End Property

    Public ReadOnly Property Bitrate() As Integer
        Get
            Return _Bitrate
        End Get
    End Property

    Public ReadOnly Property Category() As String
        Get
            Return _Category
        End Get
    End Property

    Public ReadOnly Property Composer() As String
        Get
            Return _Composer
        End Get
    End Property

    Public ReadOnly Property Conductor() As String
        Get
            Return _Conductor
        End Get
    End Property

    Public ReadOnly Property Copyright() As String
        Get
            Return _Copyright
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return _Description
        End Get
    End Property

    Public ReadOnly Property Director() As String
        Get
            Return _Director
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property Duration() As UInt64
        Get
            Return _Duration
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property FileSize() As UInt64
        Get
            Return _FileSize
        End Get
    End Property

    Public ReadOnly Property Genre() As String
        Get
            Return _Genre
        End Get
    End Property

    Public ReadOnly Property IsVBR() As Boolean
        Get
            Return _IsVBR
        End Get
    End Property

    Public ReadOnly Property OriginalAlbumTitle() As String
        Get
            Return _OriginalAlbumTitle
        End Get
    End Property

    Public ReadOnly Property OriginalArtist() As String
        Get
            Return _OriginalArtist
        End Get
    End Property

    Public ReadOnly Property OriginalFileName() As String
        Get
            Return _OriginalFileName
        End Get
    End Property

    Public ReadOnly Property OriginalLyricist() As String
        Get
            Return _OriginalLyricist
        End Get
    End Property

    Public ReadOnly Property OriginalReleaseYear() As String
        Get
            Return _OriginalReleaseYear
        End Get
    End Property

    ' debug - todo: unable to get this to work.
    Public ReadOnly Property Picture() As Image
        Get
            Return _Picture
        End Get
    End Property

    Public ReadOnly Property Producer() As String
        Get
            Return _Producer
        End Get
    End Property

    Public ReadOnly Property Publisher() As String
        Get
            Return _Publisher
        End Get
    End Property

    Public ReadOnly Property Rating() As String
        Get
            Return _Rating
        End Get
    End Property

    Public ReadOnly Property Title() As String
        Get
            Return _Title
        End Get
    End Property

    Public ReadOnly Property TrackNumber() As String
        Get
            Return _TrackNumber
        End Get
    End Property

    Public ReadOnly Property Writer() As String
        Get
            Return _Writer
        End Get
    End Property

    Public ReadOnly Property Year() As String
        Get
            Return _Year
        End Get
    End Property

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Collects and returns windows media metadata about the file.
    ''' </summary>
    ''' <param name="fileName">File name to examine</param>
    ''' <returns>
    ''' Returns an empty string; otherwise returns error information.
    ''' </returns>
    Public Function ReturnMetadata(ByVal fileName As String) As String

        Try
            _Builder = New StringBuilder()

            'Check that the Wma file exists.
            If File.Exists(fileName) Then

                ' Save copy of file name.
                _FileName = fileName

                'Take the WmaFileInfo from the given path.
                _WmaFileInfo = New FileInfo(fileName)

                ' Always examine stream 0.
                Dim wStreamNum As UShort = 0US  ' "US" is type character for UShort.

                If Not String.IsNullOrEmpty(ShowAttributes(fileName, wStreamNum)) Then
                    _Builder.AppendLine("ShowAttributes failed.")
                    Return _Builder.ToString()
                Else
                    ' Everything is OK.
                    Return String.Empty
                End If
            Else
                ' File not found.
                _Builder.AppendLine(fileName(0) & " cannot be found.")
                Return _Builder.ToString()
            End If

            ' Catches any thrown exceptions.
        Catch ex As Exception
            _Builder.AppendLine("Unable to read file: " & fileName & vbCrLf & ex.Message)
            Return _Builder.ToString()
        End Try

    End Function

    ''' <summary>
    ''' Collects and returns windows media metadata about the file.
    ''' File name must be passed with new constructor.
    ''' </summary>
    ''' <returns>
    ''' Returns an empty string; otherwise returns error information.
    ''' </returns>
    Public Function ReturnMetadata() As String

        Try
            _Builder = New StringBuilder()

            'Check that the Wma file exists.
            If String.IsNullOrEmpty(_FileName) Then
                ' File name is blank.
                _Builder.AppendLine("File name must be supplied in ""New"" constructor.")
                Return _Builder.ToString()
            ElseIf File.Exists(_FileName) Then

                'Take the WmaFileInfo from the given path.
                _WmaFileInfo = New FileInfo(_FileName)

                ' Always examine stream 0.
                Dim wStreamNum As UShort = 0US  ' "US" is type character for UShort.

                If Not String.IsNullOrEmpty(ShowAttributes(_FileName, wStreamNum)) Then
                    _Builder.AppendLine("ShowAttributes failed.")
                    Return _Builder.ToString()
                Else
                    ' Everything is OK.
                    Return String.Empty
                End If
            Else
                ' File not found.
                _Builder.AppendLine(_FileName(0) & " cannot be found.")
                Return _Builder.ToString()
            End If

            ' Catches any thrown exceptions.
        Catch ex As Exception
            _Builder.AppendLine("Unable to read file: " & _FileName & vbCrLf & ex.Message)
            Return _Builder.ToString()
        End Try

    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Translates the specified attribute to string.
    ''' </summary>
    ''' <param name="attributeName">Name of attribute</param>
    ''' <param name="AttribDataType">Data type of attribute</param>
    ''' <param name="pbValue">Raw byte array value</param>
    ''' <param name="valueLength">Length of value</param>
    Private Sub TranslateAttribute(ByVal attributeName As String, ByVal AttribDataType As WMT_ATTR_DATATYPE, _
                               ByVal pbValue As Byte(), ByVal valueLength As UInteger)

        Dim stringValue As String = String.Empty

        ' Make the data type string 
        Dim dataType As String = "Unknown"
        Dim dataTypes As String() = {"DWORD", "STRING", "BINARY", "BOOL", "QWORD", "WORD", "GUID"}

        If dataTypes.Length > Convert.ToInt32(AttribDataType) Then
            dataType = dataTypes(Convert.ToInt32(AttribDataType))
        End If

        ' The attribute value. 
        Select Case AttribDataType
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_STRING
                ' String 
                If 0 = valueLength Then
                    stringValue = ""
                Else ' UTF-16LE
                    If (254 = Convert.ToInt16(pbValue(0))) AndAlso (255 = Convert.ToInt16(pbValue(1))) Then
                        If 4 <= valueLength Then
                            For i As Integer = 0 To pbValue.Length - 3 Step 2
                                stringValue += Convert.ToString(BitConverter.ToChar(pbValue, i))
                            Next
                        End If
                        ' UTF-16BE
                    ElseIf (255 = Convert.ToInt16(pbValue(0))) AndAlso (254 = Convert.ToInt16(pbValue(1))) Then
                        If 4 <= valueLength Then
                            For i As Integer = 0 To pbValue.Length - 3 Step 2
                                stringValue += Convert.ToString(BitConverter.ToChar(pbValue, i))
                            Next
                        End If
                    Else
                        If 2 <= valueLength Then
                            For i As Integer = 0 To pbValue.Length - 3 Step 2
                                stringValue += Convert.ToString(BitConverter.ToChar(pbValue, i))
                            Next
                        End If
                    End If
                End If
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_BINARY
                ' Binary 
                ' JPEG Images always start with &HFFD8. Find the
                ' first occurance of this byte pair and copy all
                ' the bytes from there to the end to a new array
                ' that will be converted to an image and passed
                ' directly to the _Picture property value.
                Try
                    Dim jpegStart As Integer = Array.IndexOf(pbValue, CByte(&HFF))
                    Dim picArray(pbValue.Length - jpegStart - 1) As Byte
                    ' If the next byte is &HD8, then we have a valid JPEG start.
                    If Array.IndexOf(pbValue, CByte(&HD8)) = jpegStart + 1 Then
                        Array.Copy(pbValue, jpegStart, picArray, 0, picArray.Length)
                        _Picture = ByteArrayToImage(picArray)
                    Else
                        _Picture = Nothing
                    End If
                Catch ex As Exception
                    _Picture = Nothing
                End Try
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_BOOL
                ' Boolean 
                If BitConverter.ToBoolean(pbValue, 0) Then
                    stringValue = "True"
                Else
                    stringValue = "False"
                End If
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_DWORD
                ' DWORD 
                Dim dwValue As UInteger = BitConverter.ToUInt32(pbValue, 0)
                stringValue = dwValue.ToString()
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_QWORD
                ' QWORD 
                Dim qwValue As ULong = BitConverter.ToUInt64(pbValue, 0)
                stringValue = qwValue.ToString()
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_WORD
                ' WORD 
                Dim wValue As UInteger = BitConverter.ToUInt16(pbValue, 0)
                stringValue = wValue.ToString()
            Case WmfsdkWrapper.WMT_ATTR_DATATYPE.WMT_TYPE_GUID
                ' GUID 
                stringValue = BitConverter.ToString(pbValue, 0, pbValue.Length)
            Case Else

        End Select

        ' There is a problem with the converter above in that it leaves an illegal character
        ' at the end of strings. This seems to fix the problem.
        attributeName = attributeName.Substring(0, attributeName.Length - 1)

        ' Add values to collections.
        _Attributes.Add(attributeName)
        _DataTypes.Add(dataType)
        _Values.Add(stringValue)

        ' Add values to properties.
        AddValuesToProperties(attributeName, stringValue)

    End Sub

    ''' <summary>
    ''' Adds the values to the class properties. Runs for each attribute
    ''' after TranslateAttribute changes the byte values to string.
    ''' </summary>
    ''' <param name="attributeName">Name of the attribute</param>
    ''' <param name="value">Value of the attribute as a string</param>
    ''' <remarks>
    ''' Numeric  and boolean values are convert back to original data types.
    ''' </remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")> _
    Private Sub AddValuesToProperties(ByVal attributeName As String, ByVal value As String)

        Select Case attributeName
            Case "Author"
                _Author = value
            Case "Bitrate"
                _Bitrate = ConvertToInteger(value)
            Case "Copyright"
                _Copyright = value
            Case "Description"
                _Description = value
            Case "Duration"
                _Duration = ConvertToUInt64(value)
            Case "FileSize"
                _FileSize = ConvertToUInt64(value)
            Case "IsVBR"
                _IsVBR = ConvertToBoolean(value)
            Case "Rating"
                _Rating = value
            Case "Title"
                _Title = value
            Case "WM/AlbumArtist"
                _AlbumArtist = value
            Case "WM/AlbumTitle"
                _AlbumTitle = value
            Case "WM/BeatsPerMinute"
                _BeatsPerMinute = value
            Case "WM/Category"
                _Category = value
            Case "WM/Composer"
                _Composer = value
            Case "WM/Conductor"
                _Conductor = value
            Case "WM/Director"
                _Director = value
            Case "WM/Genre"
                _Genre = value
            Case "WM/OriginalAlbumTitle"
                _OriginalAlbumTitle = value
            Case "WM/OriginalArtist"
                _OriginalArtist = value
            Case "WM/OriginalFilename"
                _OriginalFileName = value
            Case "WM/OriginalLyricist"
                _OriginalLyricist = value
            Case "WM/OriginalReleaseYear"
                _OriginalReleaseYear = value
            Case "WM/Picture"
                ' todo
            Case "WM/Producer"
                _Producer = value
            Case "WM/Publisher"
                _Publisher = value
            Case "WM/TrackNumber"
                _TrackNumber = value
            Case "WM/Writer"
                _Writer = value
            Case "WM/Year"
                _Year = value
        End Select

    End Sub

    Private Shared Function ConvertToInteger(ByVal value As String) As Integer
        If Not String.IsNullOrEmpty(value) And IsNumeric(value) Then
            Return CInt(value)
        Else
            Return 0
        End If
    End Function

    Private Shared Function ConvertToUInt64(ByVal value As String) As UInt64
        If Not String.IsNullOrEmpty(value) And IsNumeric(value) Then
            Return CType(value, UInt64)
        Else
            Return 0UL
        End If
    End Function

    Private Shared Function ConvertToBoolean(ByVal value As String) As Boolean
        If Not String.IsNullOrEmpty(value) Then
            Return CBool(value)
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Displays all attributes for the specified stream.
    ''' </summary>
    Private Function ShowAttributes(ByVal fileName As String, ByVal streamNumber As UShort) As String

        Try
            Dim MetadataEditor As IWMMetadataEditor = Nothing
            Dim HeaderInfo3 As IWMHeaderInfo3
            Dim attributeCount As UShort

            If NativeMethods.WMCreateEditor(MetadataEditor) <> 0 Then
                _Builder.AppendLine("Unable to create editor.")
                Return _Builder.ToString()
            End If

            MetadataEditor.Open(fileName)

            HeaderInfo3 = DirectCast(MetadataEditor, IWMHeaderInfo3)

            HeaderInfo3.GetAttributeCount(streamNumber, attributeCount)

            For attribIndex As UShort = 0 To CUShort(attributeCount - 1)

                Dim attributeType As WMT_ATTR_DATATYPE
                Dim attributeName As String = Nothing
                Dim byteAttribValue As Byte() = Nothing
                Dim attributeNameLength As UShort = 0
                Dim attributeValueLength As UShort = 0

                HeaderInfo3.GetAttributeByIndex(attribIndex, streamNumber, attributeName, _
                                                attributeNameLength, attributeType, byteAttribValue, _
                                                attributeValueLength)

                byteAttribValue = New Byte(attributeValueLength - 1) {}
                attributeName = New String(CChar(CStr(0)), attributeNameLength)

                HeaderInfo3.GetAttributeByIndex(attribIndex, streamNumber, attributeName, _
                                                attributeNameLength, attributeType, byteAttribValue, _
                                                attributeValueLength)

                ' Translate attribute to string, add to collections and properties.
                TranslateAttribute(attributeName, attributeType, byteAttribValue, attributeValueLength)

            Next
        Catch e As Exception
            _Builder.AppendLine("Unable to open file: " & fileName & vbCrLf & e.Message)
            Return _Builder.ToString()
            Throw
        End Try

        Return String.Empty ' true

    End Function

    Private Function ImageToByteArray(ByVal imageIn As Image) As Byte()

        Dim ms As MemoryStream = New MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()

    End Function

    Private Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image

        Try
            Dim ms As MemoryStream = New MemoryStream(byteArrayIn)
            Dim returnImage As Image = Image.FromStream(ms)
            Return returnImage
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

End Class
