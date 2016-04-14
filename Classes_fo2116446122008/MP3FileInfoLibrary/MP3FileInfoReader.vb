Imports System.IO
Imports System.Text
Imports System.Security

#Region " Public Enums "

Public Enum MpegType
    Mpeg1
    Mpeg2
    Mpeg25
End Enum

Public Enum LayerType
    LayerI
    LayerII
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="III")> _
    LayerIII
End Enum

Public Enum ProtectionType
    ProtectedByCRC
    NotProtected
End Enum

Public Enum Copyright
    Copyrighted
    NotCopyrighted
End Enum

Public Enum ChannelMode
    Stereo
    JointStereo
    DualChannel
    SingleChannel
End Enum

#End Region

Public Class MP3FileInfoReader
    Implements IDisposable

#Region " Private Members "

    Private _MP3HeaderBitsStr As MP3HeaderBitsString
    Private _MP3HeaderBits As BitArray
    Private _MP3FileInfo As FileInfo
    Private _XingHeaderBytes(115) As Byte
    Private _MP3HeaderPosition As Integer
    Private _MP3ClearSize As Integer
    Private _FileStream As FileStream

    ' Private values for properties.
    Private _IsVBR As Boolean
    Private _MpegType As MpegType
    Private _Layer As LayerType
    Private _Protection As ProtectionType
    Private _Bitrate As Integer
    Private _SamplingFrequency As Integer
    Private _ChannelMode As ChannelMode
    Private _Copyright As Copyright
    Private _Duration As Long
    Private _DurationString As String
    Private _GenreString As String

    Private Structure MP3HeaderBitsString
        Dim MPEGTypeBitsString As String
        Dim LayerBitsString As String
        Dim BitrateBitsString As String
        Dim FreqBitsString As String
        Dim ChannelModeBitsString As String
    End Structure

    Private Structure XingHeader
        Dim NumberOfFrames As Integer
        Dim FileLength As Integer
        Dim Toc() As Integer
        Dim VbrScale As Integer
    End Structure

#End Region

#Region " Public Properties "

    Public ReadOnly Property FileCreationTime() As Date
        Get
            Return _MP3FileInfo.CreationTime
        End Get
    End Property

    Public ReadOnly Property FileFullName() As String
        Get
            Return _MP3FileInfo.FullName
        End Get
    End Property

    Public ReadOnly Property FileIsReadOnly() As Boolean
        Get
            Return _MP3FileInfo.IsReadOnly
        End Get
    End Property

    Public ReadOnly Property FileLastAccessTime() As Date
        Get
            Return _MP3FileInfo.LastAccessTime
        End Get
    End Property

    Public ReadOnly Property FileLastWriteTime() As Date
        Get
            Return _MP3FileInfo.LastWriteTime
        End Get
    End Property

    Public ReadOnly Property FileLength() As Long
        Get
            Return _MP3FileInfo.Length
        End Get
    End Property

    Public ReadOnly Property IsVBR() As Boolean
        Get
            Return _IsVBR
        End Get
    End Property

    Public ReadOnly Property MpegType() As MpegType
        Get
            Return _MpegType
        End Get
    End Property

    Public ReadOnly Property Layer() As LayerType
        Get
            Return _Layer
        End Get
    End Property

    Public ReadOnly Property Protection() As ProtectionType
        Get
            Return _Protection
        End Get
    End Property

    Public ReadOnly Property Bitrate() As Integer
        Get
            Return _Bitrate
        End Get
    End Property

    Public ReadOnly Property SamplingFrequency() As Integer
        Get
            Return _SamplingFrequency
        End Get
    End Property

    Public ReadOnly Property ChannelMode() As ChannelMode
        Get
            Return _ChannelMode
        End Get
    End Property

    Public ReadOnly Property Copyright() As Copyright
        Get
            Return _Copyright
        End Get
    End Property

    Public ReadOnly Property Duration() As Long
        Get
            Return _Duration
        End Get
    End Property

    Public ReadOnly Property DurationString() As String
        Get
            Return _DurationString
        End Get
    End Property

    Public ReadOnly Property GenreString() As String
        Get
            Return _GenreString
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal fileName As String)

        'Check that the MP3 file exists.
        If File.Exists(fileName) Then

            'Take the MP3FileInfo from the given path.
            _MP3FileInfo = New FileInfo(fileName)

            If Not ReturnMP3HeaderBytes(_MP3FileInfo.FullName) Then
                Throw New ArgumentException(fileName & " is not a valid MP3 file.")
            End If

            ' Update property values.
            UpdatePropertyValues()

        Else
            Throw New FileNotFoundException(fileName & " cannot be found.")
        End If

    End Sub

    Public Sub New()
        ' Do nothing. Enter file path using method.
    End Sub

#End Region

#Region " Destructor "

    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso _FileStream IsNot Nothing Then
            _FileStream.Dispose()
        End If
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

#Region " Public Methods "

    Public Sub ReadMP3File(ByVal fileName As String)

        'Check that the MP3 file exists.
        If File.Exists(fileName) Then

            'Take the MP3FileInfo from the given path.
            _MP3FileInfo = New FileInfo(fileName)

            If Not ReturnMP3HeaderBytes(_MP3FileInfo.FullName) Then
                Throw New ArgumentException(fileName & " is not a valid MP3 file.")
            End If

            ' Update property values.
            UpdatePropertyValues()

        Else
            Throw New FileNotFoundException(fileName & " cannot be found.")
        End If

    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Update all private property strings with new values when a file is read.
    ''' </summary>
    Private Sub UpdatePropertyValues()

        ' Private values for properties.
        _IsVBR = ReturnIsVbr()
        _MpegType = ReturnMpegType()
        _Layer = ReturnLayer()
        _Protection = ReturnProtection()
        _Bitrate = ReturnBitrate()
        _SamplingFrequency = ReturnSamplingFrequency()
        _ChannelMode = ReturnChannelMode()
        _Copyright = ReturnCopyright()
        _Duration = ReturnDuration()
        _DurationString = ReturnDurationString()
        If String.IsNullOrEmpty(_DurationString) Then
            _DurationString = "00:00"
        End If

    End Sub

    ''' <summary>
    ''' Returns a System.IO.FileStream from the given path.
    ''' </summary>
    Private Function ReturnMP3FileStream(ByVal fileName As String) As FileStream

        Dim builder As StringBuilder = New StringBuilder
        Dim returnValue As FileStream = Nothing

        Try
            _FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)

            If _FileStream.CanRead Then
                _FileStream.Position = 0
            Else
                Throw New IOException("Can't read file '" & fileName & "'.")
            End If

            returnValue = _FileStream

        Catch ex As SecurityException
            builder.AppendLine(ex.Message)
        Catch ex As InvalidCastException
            builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            builder.AppendLine(ex.Message)
        Catch ex As UnauthorizedAccessException
            builder.AppendLine(ex.Message)
        Catch ex As DirectoryNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As FileNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As EndOfStreamException
            builder.AppendLine(ex.Message)
        Catch ex As IOException
            builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(builder.ToString) Then
                returnValue = Nothing
            End If
        End Try

        Return returnValue

    End Function

    ''' <summary>
    ''' Gets a BitArray with the MP3 header bits and an MP3HeaderBitsStr structure.
    ''' It returns a boolean value , false if it fails. 
    ''' For more info about MP3Header check http://www.mp3-tech.org/
    ''' </summary>
    Private Function ReturnMP3HeaderBytes(ByVal fileName As String) As Boolean

        Dim tempMP3HeaderBits As BitArray
        Dim mp3HeaderBytes(2) As Byte
        Dim isSyncByte As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim index As Integer
        Dim bitOffSet As Integer
        Dim builder As StringBuilder = New StringBuilder
        Dim returnValue As Boolean = False
        _MP3HeaderBits = New BitArray(24)

        Try
            _FileStream = ReturnMP3FileStream(fileName)

            While (_FileStream.Position + 4) <= _FileStream.Length

                'Read a byte from file and check if its bits are "11111111" 
                '(this corresponds to an integer with value = 255)
                If _FileStream.ReadByte = 255 Then
                    _MP3HeaderPosition = CInt(_FileStream.Position)
                    isSyncByte = True
                End If

                While isSyncByte

                    'Read the next 3 bytes
                    _FileStream.Read(mp3HeaderBytes, 0, 3)
                    tempMP3HeaderBits = New BitArray(mp3HeaderBytes)

                    'Check the bits no 9,10,11 to ensure we have the sync => the MP3 Header bytes
                    For i = 7 To 5 Step -1
                        If Not tempMP3HeaderBits.Item(i) Then
                            isSyncByte = False
                            _FileStream.Position -= 3
                            Exit While
                        End If
                    Next
                    index = 0
                    bitOffSet = 0

                    'Now we have the Header bits from the file but in the MP3HeaderBits array ,the bits are
                    'in the form of "8 7 6 5 4 3 2 1 16 15 14...", we just put them in the form of 
                    '"1 2 3 4 5 6 7 8 10 11..." with the following 2 loops.
                    For j = 0 To 2
                        For i = 7 To 0 Step -1
                            _MP3HeaderBits.Item(index) = tempMP3HeaderBits.Item(bitOffSet + i)
                            index += 1
                        Next
                        bitOffSet += 8
                    Next

                    'Get the MP3HeaderBitsStr structure 
                    With _MP3HeaderBitsStr
                        .MPEGTypeBitsString = ReturnBitsString(3, 4)
                        .LayerBitsString = ReturnBitsString(5, 6)
                        .BitrateBitsString = ReturnBitsString(8, 11)
                        .FreqBitsString = ReturnBitsString(12, 13)
                        .ChannelModeBitsString = ReturnBitsString(16, 17)

                        'Check the MPEGTypeBits for a bad value
                        If .MPEGTypeBitsString = "01" Then
                            isSyncByte = False
                            _FileStream.Position -= 3
                            Exit While
                        End If

                        'Check the LayerBits for a bad value, we only want LayerIII too!
                        If .LayerBitsString = "11" Or .LayerBitsString = "10" Or .LayerBitsString = "00" Then
                            isSyncByte = False
                            _FileStream.Position -= 3
                            Exit While
                        End If

                        'Check the bitrate bits for a bad value , they can't be "1111"
                        If .BitrateBitsString = "1111" Then
                            isSyncByte = False
                            _FileStream.Position -= 3
                            Exit While
                        End If

                        'Check the FrequencyBits for a bad value
                        If .FreqBitsString = "11" Then
                            isSyncByte = False
                            _FileStream.Position -= 3
                            Exit While
                        Else
                            'If all the above are ok , we have our header!
                            _MP3ClearSize = CInt(_FileStream.Length - _MP3HeaderPosition - 128)
                            returnValue = True
                            Exit While
                        End If
                    End With

                End While
                ' This causes the outside loop to exit.
                If returnValue = True Then
                    Exit While
                End If
            End While

            'returnValue = False

        Catch ex As SecurityException
            builder.AppendLine(ex.Message)
        Catch ex As InvalidCastException
            builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            builder.AppendLine(ex.Message)
        Catch ex As UnauthorizedAccessException
            builder.AppendLine(ex.Message)
        Catch ex As DirectoryNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As FileNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As EndOfStreamException
            builder.AppendLine(ex.Message)
        Catch ex As IOException
            builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(builder.ToString) Then
                returnValue = False
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Close()
            End If
        End Try

        Return returnValue

    End Function

    Private Function ReturnBitsString(ByVal startIndex As Integer, ByVal endIndex As Integer) As String

        Dim bitsString As String = ""
        Dim i As Integer

        For i = startIndex To endIndex
            Select Case _MP3HeaderBits.Item(i)
                Case True : bitsString += "1"
                Case False : bitsString += "0"
            End Select
        Next

        Return bitsString

    End Function

    ''' <summary>
    ''' Returns true if the mp3 is VBR.
    ''' For more info see : http://www.multiweb.cz/twoinches/MP3inside.htm#MP3FileStructure
    ''' </summary>
    Private Function ReturnIsVbr() As Boolean

        Dim byteToStringConv As New System.Text.UTF8Encoding()
        Dim xingBytes(3) As Byte
        Dim builder As StringBuilder = New StringBuilder
        Dim returnValue As Boolean = False

        Try
            _FileStream = ReturnMP3FileStream(_MP3FileInfo.FullName)

            If Not ReturnChannelMode() = ChannelMode.SingleChannel Then
                _FileStream.Position = _MP3HeaderPosition + 35
                _FileStream.Read(xingBytes, 0, 4)
            Else
                _FileStream.Position = _MP3HeaderPosition + 20
                _FileStream.Read(xingBytes, 0, 4)
            End If

            If byteToStringConv.GetString(xingBytes) = "Xing" Then
                _FileStream.Read(_XingHeaderBytes, 0, 116)
                returnValue = True
            Else
                returnValue = False
            End If

        Catch ex As SecurityException
            builder.AppendLine(ex.Message)
        Catch ex As InvalidCastException
            builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            builder.AppendLine(ex.Message)
        Catch ex As UnauthorizedAccessException
            builder.AppendLine(ex.Message)
        Catch ex As DirectoryNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As FileNotFoundException
            builder.AppendLine(ex.Message)
        Catch ex As EndOfStreamException
            builder.AppendLine(ex.Message)
        Catch ex As IOException
            builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(builder.ToString) Then
                returnValue = False
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Close()
            End If
        End Try

        Return returnValue

    End Function

    ''' <summary>
    ''' Returns a XingHeader structure.
    ''' For more info see : http://www.multiweb.cz/twoinches/MP3inside.htm#MP3FileStructure
    ''' </summary>
    Private Function ReturnXingHeader() As XingHeader

        Dim myXingHeader As XingHeader
        Dim frameCountBytes(3) As Byte
        Dim fileLenghtBytes(3) As Byte
        Dim vbrScaleBytes(3) As Byte
        Dim index, toc(99), i As Integer

        If IsVBR() Then
            For i = 3 To 0 Step -1
                vbrScaleBytes(index) = _XingHeaderBytes(112 + i)
                frameCountBytes(index) = _XingHeaderBytes(4 + i)
                fileLenghtBytes(index) = _XingHeaderBytes(8 + i)
                index += 1
            Next

            For i = 0 To 99
                toc(i) = CType(_XingHeaderBytes(12 + i), Integer)
            Next
        Else
            'Throw New ArgumentException("'" & _MP3FileInfo.FullName & "' is not VBR")
            Return Nothing
            Exit Function
        End If

        With myXingHeader
            .FileLength = BitConverter.ToInt32(fileLenghtBytes, 0)
            .NumberOfFrames = BitConverter.ToInt32(frameCountBytes, 0)
            .VbrScale = BitConverter.ToInt32(vbrScaleBytes, 0)
            .Toc = toc
        End With

        Return myXingHeader

    End Function

    ''' <summary>
    ''' Returns the Mpeg type of  the current Mpeg file (MP3) as an
    ''' MP3Info.MPEGType enumeration.
    ''' </summary>
    Private Function ReturnMpegType() As MpegType

        Select Case _MP3HeaderBitsStr.MPEGTypeBitsString
            Case "11" : Return MpegType.Mpeg1
            Case "10" : Return MpegType.Mpeg2
            Case "00" : Return MpegType.Mpeg25
        End Select

    End Function

    ''' <summary>
    ''' Returns the Layer type of  the current Mpeg file (MP3) as an 
    ''' MP3Info.LayerType enumeration.
    ''' </summary>
    Private Function ReturnLayer() As LayerType

        Select Case _MP3HeaderBitsStr.LayerBitsString
            Case "01"
                Return LayerType.LayerIII
            Case "10"
                Return LayerType.LayerII
            Case "11"
                Return LayerType.LayerI
        End Select

    End Function

    ''' <summary>
    ''' Returns the Protection type of the current MP3 file as an 
    ''' MP3Info.ProtectionType enumeration.
    ''' </summary>
    Private Function ReturnProtection() As ProtectionType

        If Not _MP3HeaderBits.Item(7) Then
            Return ProtectionType.ProtectedByCRC
        Else
            Return ProtectionType.NotProtected
        End If

    End Function

    ''' <summary>
    ''' Returns the bitrate of the current MP3 file in bits per second.
    ''' </summary>
    Private Function ReturnBitrate() As Integer

        Dim bitrateArray() As Integer

        If ReturnMpegType() = MpegType.Mpeg1 Then
            Dim tempBitrateArray() As Integer = {32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320}
            bitrateArray = tempBitrateArray
        Else
            Dim tempBitrateArray() As Integer = {8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160}
            bitrateArray = tempBitrateArray
        End If

        If Not IsVBR() Then
            Select Case _MP3HeaderBitsStr.BitrateBitsString
                Case "0001" : Return bitrateArray(0) * 1000
                Case "0010" : Return bitrateArray(1) * 1000
                Case "0011" : Return bitrateArray(2) * 1000
                Case "0100" : Return bitrateArray(3) * 1000
                Case "0101" : Return bitrateArray(4) * 1000
                Case "0110" : Return bitrateArray(5) * 1000
                Case "0111" : Return bitrateArray(6) * 1000
                Case "1000" : Return bitrateArray(7) * 1000
                Case "1001" : Return bitrateArray(8) * 1000
                Case "1010" : Return bitrateArray(9) * 1000
                Case "1011" : Return bitrateArray(10) * 1000
                Case "1100" : Return bitrateArray(11) * 1000
                Case "1101" : Return bitrateArray(12) * 1000
                Case "1110" : Return bitrateArray(13) * 1000
            End Select
        Else
            Dim myXingHeader As XingHeader = ReturnXingHeader()
            Dim averageFrameLenght, averageBitrate As Integer

            'For more info see : http://www.multiweb.cz/twoinches/MP3inside.htm#MP3FileStructure
            With myXingHeader
                averageFrameLenght = CInt(Math.Round(.FileLength / .NumberOfFrames, 0))
                averageBitrate = CInt(Math.Round(((averageFrameLenght * ReturnSamplingFrequency()) / 144) / 1000, 0))
            End With

            Return averageBitrate * 1000

        End If

    End Function

    ''' <summary>
    ''' Returns the sampling rate Frequency of the current MP3 file in Hz.
    ''' </summary>
    Private Function ReturnSamplingFrequency() As Integer

        Select Case _MP3HeaderBitsStr.FreqBitsString
            Case "00"
                Return 44100
            Case "01"
                Return 48000
            Case "10"
                Return 32000
        End Select

    End Function

    ''' <summary>
    ''' Returns the Channel mode of  the current MP3 file as an MP3Info.ChannelMode enumeration.
    ''' </summary>
    Private Function ReturnChannelMode() As ChannelMode

        Select Case _MP3HeaderBitsStr.ChannelModeBitsString
            Case "00"
                Return ChannelMode.Stereo
            Case "01"
                Return ChannelMode.JointStereo
            Case "10"
                Return ChannelMode.DualChannel
            Case "11"
                Return ChannelMode.SingleChannel
        End Select

    End Function

    ''' <summary>
    ''' Returns the Copyright type of the current MP3 file as an MP3Info.CopyRight enumeration.
    ''' </summary>
    Private Function ReturnCopyright() As Copyright

        If _MP3HeaderBits.Item(20) Then
            Return Copyright.Copyrighted
        Else
            Return Copyright.NotCopyrighted
        End If

    End Function

    ''' <summary>
    ''' Returns the duration of the current MP3 file in seconds.
    ''' </summary>
    Private Function ReturnDuration() As Long

        Dim myDuration As Long

        Try
            myDuration = CLng((Math.Round(((_MP3ClearSize * 8) / ReturnBitrate()), 0)))
        Catch ex As OverflowException
            myDuration = 0
        End Try

        Return myDuration

    End Function

    ''' <summary>
    ''' Returns a string with the duration of the current MP3 file in the form of : “hh:mm:ss”.
    ''' </summary>
    Private Function ReturnDurationString() As String

        Dim durationString As String = ""
        Dim currentDuration As Integer = CInt(ReturnDuration())
        Dim durationHour As Integer = currentDuration \ 3600
        Dim durationMinute As Integer
        Dim durationSecond As Integer

        If durationHour >= 1 Then
            durationMinute = ((currentDuration Mod 3600) \ 60)
            durationSecond = ((currentDuration Mod 3600) Mod 60)
            durationString = Format(durationHour, "00") & ":" & Format(durationMinute, "00") & ":" & Format(durationSecond, "00")
        Else
            durationMinute = currentDuration \ 60
            durationSecond = currentDuration Mod 60
            durationString = Format(durationMinute, "00") & ":" & Format(durationSecond, "00")
        End If

        Return durationString

    End Function

#End Region

End Class