Imports System.Text
Imports System.IO
Imports System.Security.Permissions
Imports System.Security
Imports System.Runtime.InteropServices

''' <summary>
''' Reads ID3v2 tags and reports values as properties.
''' </summary>
''' <remarks>
''' This was written from scratch using the documentation on
''' id3.org. Although I found a complex ID3 source (in C#) that
''' worked properly, none of the simpler code samples worked in
''' a circumstances.
''' 
''' After studying the documentation, I found the problem. ID3 tags
''' are allowed to use different text encodings in each frame. For
''' example, "TALB" might use Unicode (bigendian), "COMM" might use
''' ANSI (ASCII), and "TIT2" might use UTF-8. So the encoding has
''' to be determined each time a frame is read.
''' 
''' Comments were also a problem. Comments are a variable length frame
''' that starts with complex header that includes a variable length description
''' that can have different text encoding from the comment itself. Unicode and
''' UTF-8 comments always end in zeros. But ANSI comments have no ending
''' delimiter and the length of the comment has to be carefully calculated.
''' </remarks>
Public NotInheritable Class Id3TagReader
    Implements IDisposable

#Region " Private Members "

    Private _FileStream As FileStream
    Private _BinaryReader As BinaryReader
    Private _FileName As String
    Private _Builder As StringBuilder

    ' Private variables for properties.
    Private _TrackName As String
    Private _TrackArtist As String
    Private _AlbumTitle As String
    Private _AlbumArtist As String
    Private _Genre As String
    Private _Publisher As String
    Private _Year As String
    Private _Subtitle As String
    Private _OriginalAlbumTitle As String
    Private _PartOfSet As String
    Private _Conductor As String
    Private _Lyricist As String
    Private _OriginalLyricist As String
    Private _Mood As String
    Private _Key As String
    Private _TrackNumber As String
    Private _CompactDiscId As String
    Private _Composer As String
    Private _BeatsPerMinute As String
    Private _OriginalArtist As String
    Private _Comment As String
    Private _Copyright As String
    Private _AttachedPicture As Image
    Private _AttachedPictureType As PictureType
    Private _AttachedPictureDescription As String
    Private _Id3Version As String

    ' These are the starting positions within the file for these tags.
    ' -1 means the tag is not present.
    Private _TrackNameStart As Integer              ' TIT2
    Private _TrackArtistStart As Integer            ' TPE1
    Private _AlbumTitleStart As Integer             ' TALB
    Private _AlbumArtistStart As Integer            ' TPE2
    Private _GenreStart As Integer                  ' TCON
    Private _PublisherStart As Integer              ' TPUB
    Private _YearStart As Integer                   ' TYER
    Private _SubtitleStart As Integer               ' TIT3
    Private _OriginalAlbumTitleStart As Integer     ' TOAL
    Private _PartOfSetStart As Integer              ' TPOS
    Private _ConductorStart As Integer              ' TPE3
    Private _LyricistStart As Integer               ' TEXT
    Private _OriginalLyricistStart As Integer       ' TOLY
    Private _MoodStart As Integer                   ' TMOO
    Private _KeyStart As Integer                    ' TKEY
    Private _TrackNumberStart As Integer            ' TRCK
    Private _CompactDiscIdStart As Integer          ' MCID
    Private _ComposerStart As Integer               ' TCOM
    Private _BeatsPerMinuteStart As Integer         ' TBPM
    Private _OriginalArtistStart As Integer         ' TOPE
    Private _CommentStart As Integer                ' COMM
    Private _CopyrightStart As Integer              ' TCOP
    Private _AttachedPictureStart As Integer        ' APIC

    ' ID3 Tag Frame Structure. 10 Bytes.
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure Id3Frame
        Dim FrameId() As Char       ' 4 bytes
        Dim FrameSize() As Byte     ' 4 bytes
        Dim Flag1 As Byte
        Dim Flag2 As Byte
        Dim TextEncoding As Byte
    End Structure

    ' ID3 Comment "COMM" Frame Structure. Variable length.
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure CommentFrame
        Dim FrameID() As Char       ' 4 bytes
        Dim FrameSize() As Byte     ' 4 bytes
        Dim Flag1 As Byte
        Dim Flag2 As Byte
        Dim TextEncoding As Byte
        Dim Language() As Char      ' 3 bytes
        Dim Description As Char()   ' Variable length. Ends in 0.
        Dim CommentText As Char()   ' Variable length. Ends in 0.
    End Structure

    ' String array of comment descriptions (to support multiple comments).
    ' Hopefully 10 will be enough.
    Private _CommentDescription(9) As String

    Private _CommentDescriptionIndex As Integer

    ' Text formatting constants.
    Private Const _ANSI As Byte = &H0               ' Terminated with &H0
    Private Const _UTF16WithBom As Byte = &H1       ' Terminated with &H0, &H0
    Private Const _UTF16NoBom As Byte = &H2         ' Terminated with &H0, &H0
    Private Const _UTF8 As Byte = &H3               ' Terminated with &H0

    ' ID3 Tag Frame for Attached Picture.
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure Id3Picture
        Dim FrameID() As Char       ' 4 bytes
        Dim FrameSize() As Byte     ' 4 bytes
        Dim Flag1 As Byte
        Dim Flag2 As Byte
        Dim TextEncoding As Byte
        Dim MimeType As String
        Dim PictureType As Byte
        Dim Description As String   ' Uses text encoding.
        Dim PictureData() As Byte
    End Structure

#End Region

#Region " Constructor "

    ''' <summary>
    ''' Use this constructor when the file name is passed
    ''' through the method.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Use to pass the file name without using method.
    ''' </summary>
    ''' <param name="fileName">File name to examine</param>
    Public Sub New(ByVal fileName As String)
        ReturnMetadata(fileName)
    End Sub

#End Region

#Region " Destructor "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing And _FileStream IsNot Nothing Then
            _FileStream.Dispose()
        End If
        If disposing And _BinaryReader IsNot Nothing Then
            _BinaryReader.Close()
        End If
    End Sub

#End Region

#Region " Genre Strings "

    Private _Genres() As String = {"Blues", "Classic Rock", "Country", "Dance", _
      "Disco", "Funk", "Grunge", "Hip-Hop", _
      "Jazz", "Metal", "New Age", "Oldies", _
      "Other", "Pop", "R&B", "Rap", _
      "Reggae", "Rock", "Techno", "Industrial", _
      "Alternative", "Ska", "Death Metal", "Pranks", _
      "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop", _
      "Vocal", "Jazz+Funk", "Fusion", "Trance", _
      "Classical", "Instrumental", "Acid", "House", _
      "Game", "Sound Clip", "Gospel", "Noise", _
      "Alt. Rock", "Bass", "Soul", "Punk", _
      "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", _
      "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", _
      "Electronic", "Pop-Folk", "Eurodance", "Dream", _
      "Southern Rock", "Comedy", "Cult", "Gangsta Rap", _
      "Top 40", "Christian Rap", "Pop/Funk", "Jungle", _
      "Native American", "Cabaret", "New Wave", "Psychedelic", _
      "Rave", "Showtunes", "Trailer", "Lo-Fi", _
      "Tribal", "Acid Punk", "Acid Jazz", "Polka", _
      "Retro", "Musical", "Rock & Roll", "Hard Rock", _
      "Folk", "Folk/Rock", "National Folk", "Swing", _
      "Fast-Fusion", "Bebop", "Latin", "Revival", _
      "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", _
      "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", _
      "Big Band", "Chorus", "Easy Listening", "Acoustic", _
      "Humour", "Speech", "Chanson", "Opera", _
      "Chamber Music", "Sonata", "Symphony", "Booty Bass", _
      "Primus", "Porn Groove", "Satire", "Slow Jam", _
      "Club", "Tango", "Samba", "Folklore", _
      "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle", _
      "Duet", "Punk Rock", "Drum Solo", "A Cappella", _
      "Euro-House", "Dance Hall", "Goa", "Drum & Bass", _
      "Club-House", "Hardcore", "Terror", "Indie", _
      "BritPop", "Negerpunk", "Polsk Punk", "Beat", _
      "Christian Gangsta Rap", "Heavy Metal", "Black Metal", "Crossover", _
      "Contemporary Christian", "Christian Rock", "Merengue", "Salsa", "Thrash Metal"}

#End Region

#Region " Public Properties "

    Public ReadOnly Property Copyright() As String
        Get
            Return _Copyright
        End Get
    End Property

    Public ReadOnly Property TrackName() As String
        Get
            Return _TrackName
        End Get
    End Property

    Public ReadOnly Property TrackArtist() As String
        Get
            Return _TrackArtist
        End Get
    End Property

    Public ReadOnly Property AlbumTitle() As String
        Get
            Return _AlbumTitle
        End Get
    End Property

    Public ReadOnly Property AlbumArtist() As String
        Get
            Return _AlbumArtist
        End Get
    End Property

    Public ReadOnly Property Genre() As String
        Get
            Return _Genre
        End Get
    End Property

    Public ReadOnly Property Publisher() As String
        Get
            Return _Publisher
        End Get
    End Property

    Public ReadOnly Property Year() As String
        Get
            Return _Year
        End Get
    End Property

    Public ReadOnly Property Subtitle() As String
        Get
            Return _Subtitle
        End Get
    End Property

    Public ReadOnly Property OriginalAlbumTitle() As String
        Get
            Return _OriginalAlbumTitle
        End Get
    End Property

    Public ReadOnly Property PartOfSet() As String
        Get
            Return _PartOfSet
        End Get
    End Property

    Public ReadOnly Property Conductor() As String
        Get
            Return _Conductor
        End Get
    End Property

    Public ReadOnly Property Lyricist() As String
        Get
            Return _Lyricist
        End Get
    End Property

    Public ReadOnly Property OriginalLyricist() As String
        Get
            Return _OriginalArtist
        End Get
    End Property

    Public ReadOnly Property Mood() As String
        Get
            Return _Mood
        End Get
    End Property

    Public ReadOnly Property Key() As String
        Get
            Return _Key
        End Get
    End Property

    Public ReadOnly Property TrackNumber() As String
        Get
            Return _TrackNumber
        End Get
    End Property

    Public ReadOnly Property CompactDiscId() As String
        Get
            Return _CompactDiscId
        End Get
    End Property

    Public ReadOnly Property Composer() As String
        Get
            Return _Composer
        End Get
    End Property

    Public ReadOnly Property BeatsPerMinute() As String
        Get
            Return _BeatsPerMinute
        End Get
    End Property

    Public ReadOnly Property OriginalArtist() As String
        Get
            Return _OriginalArtist
        End Get
    End Property

    Public ReadOnly Property Comment() As String
        Get
            Return _Comment

        End Get
    End Property

    Public ReadOnly Property Id3Version() As String
        Get
            Return _Id3Version
        End Get
    End Property

    Public ReadOnly Property AttachedPicture() As Image
        Get
            Return _AttachedPicture
        End Get
    End Property

    Public ReadOnly Property AttachedPictureType() As PictureType
        Get
            Return _AttachedPictureType
        End Get
    End Property

    Public ReadOnly Property AttachedPictureDescription() As String
        Get
            Return _AttachedPictureDescription
        End Get
    End Property

#End Region

#Region " Public Methods "

    Public Function ReturnMetadata(ByVal fileName As String) As String

        _Builder = New StringBuilder

        Try
            ' Validate file name.
            If Not String.IsNullOrEmpty(fileName) And File.Exists(fileName) Then
                _FileName = fileName
            ElseIf String.IsNullOrEmpty(fileName) Then
                _Builder.AppendLine("File Name is blank.")
                Return _Builder.ToString()
            ElseIf Not File.Exists(fileName) Then
                _Builder.AppendLine("File Name does not exist.")
                Return _Builder.ToString()
            End If

            ' Get ID3 tag info.
            Select Case ReturnID3TagVersion(_FileName)
                Case TagVersion.FileError
                    _Builder.AppendLine("File error")
                    Return _Builder.ToString()
                Case TagVersion.FileNotFound
                    _Builder.AppendLine("File not found")
                    Return _Builder.ToString()
                Case TagVersion.ID3V1X
                    ' Collect any errors in the return value.
                    _Builder.AppendLine(GetId3v1Metadata())
                Case TagVersion.ID3V22, TagVersion.ID3V23, TagVersion.ID3V24, TagVersion.ID3V2X
                    ' Collect any errors in the return value.
                    _Builder.AppendLine(GetId3v2Metadata())
                Case TagVersion.None
                    _Builder.AppendLine("File does not contain a valid ID3 tag.")
                    Return _Builder.ToString()
            End Select

        Catch ex As IOException
            _Builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            _Builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(_Builder.ToString()) Then
                _Builder.AppendLine("Unable to read tag information for because: " & _Builder.ToString())
            End If
        End Try

        Return _Builder.ToString()

    End Function

    ''' <summary>
    ''' Return an enumeration giving ID3 tag status/information
    ''' given the file name.
    ''' </summary>
    Public Function ReturnID3TagVersion(ByVal fileName As String) As TagVersion

        Dim fileError As Boolean = False
        Dim returnValue As TagVersion = TagVersion.FileNotFound
        Dim tempTag() As Char

        Try
            If Not String.IsNullOrEmpty(fileName) Or Not File.Exists(fileName) Then

                _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
                _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)

                ' Check that we have a vaild ID3 Tag.
                ReDim tempTag(2)
                tempTag = _BinaryReader.ReadChars(3)

                ' Check that we have the start of a valid ID3V2 tag.
                If tempTag = "ID3" Then

                    ' Get the ID3 version.
                    Dim tempVersion() As Byte = _BinaryReader.ReadBytes(2)

                    ' Return the version.
                    Select Case tempVersion(0).ToString()
                        Case "2"
                            returnValue = TagVersion.ID3V22
                        Case "3"
                            returnValue = TagVersion.ID3V23
                        Case "4"
                            returnValue = TagVersion.ID3V24
                        Case Else
                            returnValue = TagVersion.ID3V2X
                    End Select

                Else  ' Check for a ID3V1 tag.

                    ' ID3V1 is a 128 byte tag located at the end of the file.
                    _BinaryReader.BaseStream.Seek(-128, SeekOrigin.End)

                    ReDim tempTag(2)
                    tempTag = _BinaryReader.ReadChars(3)

                    If tempTag = "TAG" Then
                        returnValue = TagVersion.ID3V1X
                    Else
                        returnValue = TagVersion.None   ' No ID3 tag V1 or V2 found.
                    End If

                End If

            Else ' File no found.
                returnValue = TagVersion.FileNotFound
            End If

        Catch ex As SecurityException
            fileError = True
        Catch ex As InvalidCastException
            fileError = True
        Catch ex As ArgumentException
            fileError = True
        Catch ex As UnauthorizedAccessException
            fileError = True
        Catch ex As DirectoryNotFoundException
            fileError = True
        Catch ex As FileNotFoundException
            fileError = True
        Catch ex As EndOfStreamException
            fileError = True
        Catch ex As IOException
            fileError = True
        Finally
            If _BinaryReader IsNot Nothing Then
                _BinaryReader.Close()
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Close()
            End If
        End Try

        If fileError Then
            Return TagVersion.FileError
        Else
            Return returnValue
        End If

    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Reads the metadata located in the ID3V1 tag that
    ''' is located in the last 128 bytes of the file.
    ''' </summary>
    ''' <returns>
    ''' An empty string if there are no errors; error
    ''' message(s) if there are.
    ''' </returns>
    Private Function GetId3v1Metadata() As String

        Dim characters() As Char
        Dim byteRead As Byte

        _Builder = New StringBuilder

        Try
            _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
            _BinaryReader = New BinaryReader(_FileStream)
        Catch ex As Exception
            _Builder.AppendLine("Could not open " & _FileName)
        End Try

        Try
            ' ID3V1 is a 128 byte tag located at the end of the file.
            _FileStream.Seek(-128, SeekOrigin.End)

            ReDim characters(2)
            characters = _BinaryReader.ReadChars(3)
            If Not New String(characters) = "TAG" Then
                _Builder.AppendLine("No Valid ID3V1 tag information")
            End If

            ReDim characters(29)
            characters = _BinaryReader.ReadChars(30)
            _TrackName = New String(characters)

            characters = _BinaryReader.ReadChars(30)
            _TrackArtist = New String(characters)

            characters = _BinaryReader.ReadChars(30)
            _AlbumTitle = New String(characters)

            ReDim characters(3)
            characters = _BinaryReader.ReadChars(4)
            Dim value As String = New String(characters)
            If IsNumeric(value) Then
                _Year = value.ToString()
            End If

            ReDim characters(27)
            characters = _BinaryReader.ReadChars(28)
            _Comment = New String(characters)

            byteRead = _BinaryReader.ReadByte
            _TrackNumber = _BinaryReader.ReadChar

            byteRead = _BinaryReader.ReadByte
            _Genre = _Genres(CInt(byteRead))

        Catch ex As IOException
            _Builder.AppendLine("Could not open " & _FileName)
        Catch ex As Exception
            _Builder.AppendLine("ID3 V1.1 tag information not valid in " & _FileName)
        Finally
            If _BinaryReader IsNot Nothing Then
                _BinaryReader.Close()
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Close()
            End If
        End Try

        Return _Builder.ToString()

    End Function

    ''' <summary>
    ''' Reads the metadata contained in an ID3V2 tag.
    ''' </summary>
    ''' <returns>
    ''' Returns an empty string if sucessful; error
    ''' message(s) if not.
    ''' </returns>
    Private Function GetId3v2Metadata() As String

        _Builder = New StringBuilder

        Try
            ' Locate ID3 Frames.
            Dim returnValue As String = LocateFrames()
            If Not String.IsNullOrEmpty(returnValue) Then
                Throw New ArgumentException(returnValue)
            End If

            ' Process valid frames.
            returnValue = ProcessFrames()
            If Not String.IsNullOrEmpty(returnValue) Then
                Throw New ArgumentException(returnValue)
            End If

        Catch ex As IOException
            _Builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            _Builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(_Builder.ToString()) Then
                _Builder.AppendLine("Unable to read tag information for because: " & _Builder.ToString())
            End If
        End Try

        Return _Builder.ToString()

    End Function

    ''' <summary>
    ''' Get the starting position of each ID3 field.
    ''' Fields not found will have a value of -1.
    ''' </summary>
    Private Function LocateFrames() As String

        Dim header As String = ""
        Dim returnValue As String = ""

        _Builder = New StringBuilder

        Try
            _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
            _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)

            ' Check that we have a vaild ID3 Tag.
            Dim tempTag As Char() = _BinaryReader.ReadChars(3)

            ' Convert to a string.
            Dim id3tag As String = New String(tempTag)

            ' Get the ID3 version.
            Dim tempVersion() As Byte = _BinaryReader.ReadBytes(2)

            ' Extract the version.
            _Id3Version = "2." & CInt(tempVersion(0)).ToString() & "." & CInt(tempVersion(1)).ToString()

            ' Get the ID3 flags. We do nothing with this.
            Dim flags As Byte = _BinaryReader.ReadByte()

            ' Throw an exception if this byte is &HFF.
            If flags = &HFF Then Throw New ArgumentException("Wrong value for flag byte.")

            ' This header length size seems to be big enough to allow Unicode text and pictures.
            Dim headerLength As Integer = 262144

            ' Move back to the beginning of the file.
            _BinaryReader.BaseStream.Position = 0

            ' Make sure we have an ID3V2.2 tag or above!
            If id3tag = "ID3" AndAlso CDbl(_Id3Version.Substring(0, 3)) >= 2.2 Then

                ' Read the rest of the header.
                header = _BinaryReader.ReadChars(headerLength)

                ' Get the starting positions, -1 means does not exist.
                _TrackNameStart = header.IndexOf("TIT2")
                _TrackArtistStart = header.IndexOf("TPE1")
                _AlbumTitleStart = header.IndexOf("TALB")
                _AlbumArtistStart = header.IndexOf("TPE2")
                _GenreStart = header.IndexOf("TCON")
                _PublisherStart = header.IndexOf("TPUB")
                _YearStart = header.IndexOf("TYER")
                _SubtitleStart = header.IndexOf("TIT3")
                _OriginalAlbumTitleStart = header.IndexOf("TOAL")
                _PartOfSetStart = header.IndexOf("TPOS")
                _ConductorStart = header.IndexOf("TPE3")
                _LyricistStart = header.IndexOf("TEXT")
                _OriginalLyricistStart = header.IndexOf("TOLY")
                _MoodStart = header.IndexOf("TMOO")
                _KeyStart = header.IndexOf("TKEY")
                _TrackNumberStart = header.IndexOf("TRCK")
                _CompactDiscIdStart = header.IndexOf("MCID")
                _ComposerStart = header.IndexOf("TCOM")
                _BeatsPerMinuteStart = header.IndexOf("TBPM")
                _OriginalArtistStart = header.IndexOf("TOPE")
                _CommentStart = header.IndexOf("COMM")
                _CopyrightStart = header.IndexOf("TCOP")
                _AttachedPictureStart = header.IndexOf("APIC")

            Else    ' This is not an ID3 tag.
                _Builder.AppendLine("Not an MP3 file or does not contain an ID3 Tag.")
            End If

            ' Return any errors.
            returnValue = _Builder.ToString

        Catch ex As SecurityException
            _Builder.AppendLine(ex.Message)
        Catch ex As InvalidCastException
            _Builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            _Builder.AppendLine(ex.Message)
        Catch ex As UnauthorizedAccessException
            _Builder.AppendLine(ex.Message)
        Catch ex As DirectoryNotFoundException
            _Builder.AppendLine(ex.Message)
        Catch ex As FileNotFoundException
            _Builder.AppendLine(ex.Message)
        Catch ex As EndOfStreamException
            _Builder.AppendLine(ex.Message)
        Catch ex As IOException
            _Builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(_Builder.ToString) Then
                returnValue = "Unable to read tag information for because: " & _Builder.ToString()
            End If
        End Try

        Return returnValue.Trim()

    End Function

    Private Function ProcessFrames() As String

        Dim returnValue As String = ""

        _Builder = New StringBuilder

        Try
            If _TrackNameStart >= 0 Then
                ReadFrame(_TrackName, "TIT2", _TrackNameStart)
            End If
            If _TrackArtistStart >= 0 Then
                ReadFrame(_TrackArtist, "TPE1", _TrackArtistStart)
            End If
            If _AlbumTitleStart >= 0 Then
                ReadFrame(_AlbumTitle, "TALB", _AlbumTitleStart)
            End If
            If _AlbumArtistStart >= 0 Then
                ReadFrame(_AlbumArtist, "TPE2", _AlbumArtistStart)
            End If
            If _GenreStart >= 0 Then
                ReadFrame(_Genre, "TCON", _GenreStart)
            End If
            If _PublisherStart >= 0 Then
                ReadFrame(_Publisher, "TPUB", _PublisherStart)
            End If
            If _YearStart >= 0 Then
                ReadFrame(_Year, "TYER", _YearStart)
            End If
            If _SubtitleStart >= 0 Then
                ReadFrame(_Subtitle, "TIT3", _SubtitleStart)
            End If
            If _OriginalAlbumTitleStart >= 0 Then
                ReadFrame(_OriginalAlbumTitle, "TOAL", _OriginalAlbumTitleStart)
            End If
            If _PartOfSetStart >= 0 Then
                ReadFrame(_PartOfSet, "TPOS", _PartOfSetStart)
            End If
            If _ConductorStart >= 0 Then
                ReadFrame(_Conductor, "TPE3", _ConductorStart)
            End If
            If _LyricistStart >= 0 Then
                ReadFrame(_Lyricist, "TEXT", _LyricistStart)
            End If
            If _OriginalLyricistStart >= 0 Then
                ReadFrame(_OriginalLyricist, "TOLY", _OriginalLyricistStart)
            End If
            If _MoodStart >= 0 Then
                ReadFrame(_Mood, "TMOO", _MoodStart)
            End If
            If _KeyStart >= 0 Then
                ReadFrame(_Key, "TKEY", _KeyStart)
            End If
            If _TrackNumberStart >= 0 Then
                ReadFrame(_TrackNumber, "TRCK", _TrackNumberStart)
            End If
            If _CompactDiscIdStart >= 0 Then
                ReadFrame(_CompactDiscId, "MCID", _CompactDiscIdStart)
            End If
            If _ComposerStart >= 0 Then
                ReadFrame(_Composer, "TCOM", _ComposerStart)
            End If
            If _BeatsPerMinuteStart >= 0 Then
                ReadFrame(_BeatsPerMinute, "TBPM", _BeatsPerMinuteStart)
            End If
            If _OriginalArtistStart >= 0 Then
                ReadFrame(_OriginalArtist, "TOPE", _OriginalArtistStart)
            End If
            If _CommentStart >= 0 Then
                ReadComment(_Comment, _CommentStart)
            End If
            If _CopyrightStart >= 0 Then
                ReadFrame(_Copyright, "TCOP", _CopyrightStart)
            End If
            If _AttachedPictureStart >= 0 Then
                ReadAttachedPictureFrame(_AttachedPicture, "APIC", _AttachedPictureStart)
            End If

            ' Return any errors.
            returnValue = _Builder.ToString

        Catch ex As SecurityException
            _Builder.AppendLine(ex.Message)
        Catch ex As InvalidCastException
            _Builder.AppendLine(ex.Message)
        Catch ex As ArgumentException
            _Builder.AppendLine(ex.Message)
        Catch ex As UnauthorizedAccessException
            _Builder.AppendLine(ex.Message)
        Catch ex As DirectoryNotFoundException
            _Builder.AppendLine(ex.Message)
        Catch ex As FileNotFoundException
            _Builder.AppendLine(ex.Message)
        Catch ex As EndOfStreamException
            _Builder.AppendLine(ex.Message)
        Catch ex As IOException
            _Builder.AppendLine(ex.Message)
        Finally
            If Not String.IsNullOrEmpty(_Builder.ToString) Then
                returnValue = "Unable to read tag information for because: " & _Builder.ToString()
            End If
        End Try

        Return returnValue.Trim()

    End Function

    ''' <summary>
    ''' Reads an ID3 Frame. Compares the given to the found tag name to verify the correct position.
    ''' Decodes the frame header to get the frame size. Reads the text encoding byte to determine
    ''' the correct encoding to use for this frame. (They can differ with an ID3 tag!)
    ''' </summary>
    ''' <param name="frameData">text is returned by reference</param>
    ''' <param name="tagName">4 character frame tag, eg. "TCOP"</param>
    ''' <param name="startPosition">starting position within the ID3 tag for the frame tag</param>
    Private Sub ReadFrame(ByRef frameData As String, ByVal tagName As String, ByVal startPosition As Integer)

        ' Buffer for the string.
        Dim frameText As String = ""

        Try
            _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
            _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)

            ' Advance our binary reader to the indicated start position.
            _BinaryReader.BaseStream.Position = startPosition

            ' Make a copy of the frame structure.
            Dim header As New Id3Frame

            ' Fill it in.
            header.FrameId = _BinaryReader.ReadChars(4)
            header.FrameSize = _BinaryReader.ReadBytes(4)
            header.Flag1 = _BinaryReader.ReadByte
            header.Flag2 = _BinaryReader.ReadByte
            header.TextEncoding = _BinaryReader.ReadByte

            ' Convert header.FrameSize bytes to Integer.
            Dim frameSize As UInt32
            frameSize = (frameSize Or header.FrameSize(0))
            frameSize = (frameSize << 8) Or header.FrameSize(1)
            frameSize = (frameSize << 8) Or header.FrameSize(2)
            frameSize = (frameSize << 8) Or header.FrameSize(3)

            ' Verify that tagName given and read match.
            If tagName = header.FrameId Then

                ' Get the encoding and create a new binary reader.
                Select Case header.TextEncoding
                    Case _ANSI
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)
                        ' Move back to our startPosition + 11 (the size of the frame header).
                        _BinaryReader.BaseStream.Position = startPosition + 11
                    Case _UTF16NoBom
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                        ' Move back to our startPosition + 11 (the size of the frame header).
                        _BinaryReader.BaseStream.Position = startPosition + 11
                    Case _UTF16WithBom
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                        ' Move back to our startPosition + 13 (the size of the frame header + BOM).
                        _BinaryReader.BaseStream.Position = startPosition + 13
                    Case _UTF8
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.UTF8)
                        ' Move back to our startPosition + 1$ (the size of the frame header + BOM).
                        _BinaryReader.BaseStream.Position = startPosition + 14
                    Case Else
                        frameData = ""
                        Exit Sub
                End Select

                If header.TextEncoding = _ANSI Then
                    frameText = _BinaryReader.ReadChars(CInt(frameSize - 1)) ' Removes the text encoding byte.
                    frameText = CleanUpText(frameText)
                ElseIf header.TextEncoding = _UTF8 Then
                    frameText = _BinaryReader.ReadChars(CInt(frameSize - 4)) ' Removes the text encoding byte + 3 byte BOM.
                    frameText = CleanUpText(frameText)
                ElseIf header.TextEncoding = _UTF16NoBom Then
                    frameText = _BinaryReader.ReadChars(CInt(frameSize - 1)) ' Removes the text encoding byte.
                    frameText = CleanUpText(frameText)
                ElseIf header.TextEncoding = _UTF16WithBom Then
                    frameText = _BinaryReader.ReadChars(CInt(frameSize - 3)) ' Removes the text encoding byte + 2 byte BOM.
                    frameText = CleanUpText(frameText)
                End If
            Else
                ' Invalid or unsupported frame id.
                frameData = ""  ' Copy an empty string back to frameData.
            End If

            ' Return the decoded and cleaned up frame.
            frameData = frameText

        Catch ex As OverflowException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As OutOfMemoryException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As SecurityException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As InvalidCastException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As ArgumentException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As UnauthorizedAccessException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As DirectoryNotFoundException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As FileNotFoundException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As EndOfStreamException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As IOException
            frameData = ""  ' Copy an empty string back to frameData.
        Finally
            If _BinaryReader IsNot Nothing Then
                _BinaryReader.Close()
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Reads an ID3 Attached Picture (APIC) Frame. Compares the given to the found tag name to verify the correct position.
    ''' Decodes the frame header to get the frame size. Reads the text encoding byte to determine
    ''' the correct encoding to use for this frame. (They can differ with an ID3 tag!)
    ''' </summary>
    ''' <param name="picture">image is returned by reference</param>
    ''' <param name="tagName">4 character frame tag, eg. "TCOP"</param>
    ''' <param name="startPosition">starting position within the ID3 tag for the frame tag</param>
    Private Sub ReadAttachedPictureFrame(ByRef picture As Image, ByVal tagName As String, ByVal startPosition As Integer)

        Try
            _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
            _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)

            ' Advance our binary reader to the indicated start position.
            _BinaryReader.BaseStream.Position = startPosition

            ' Make a copy of the frame structure.
            Dim header As New Id3Picture

            ' Fill it in.
            header.FrameID = _BinaryReader.ReadChars(4)
            header.FrameSize = _BinaryReader.ReadBytes(4)
            header.Flag1 = _BinaryReader.ReadByte
            header.Flag2 = _BinaryReader.ReadByte
            header.TextEncoding = _BinaryReader.ReadByte

            ' Convert header.FrameSize bytes to Integer.
            Dim frameSize As UInt32
            frameSize = (frameSize Or header.FrameSize(0))
            frameSize = (frameSize << 8) Or header.FrameSize(1)
            frameSize = (frameSize << 8) Or header.FrameSize(2)
            frameSize = (frameSize << 8) Or header.FrameSize(3)

            ' Read the MIME type as a null (zero) terminated string.
            ' This is always ANSI.
            Dim i As Integer = 0
            Dim mimeType(0) As Char

            Do
                mimeType(i) = _BinaryReader.ReadChar()
                i += 1
                ReDim Preserve mimeType(i)
            Loop Until mimeType(i - 1) = Chr(0)

            header.PictureType = _BinaryReader.ReadByte

            ' Make the picture type available to the properties.
            _AttachedPictureType = CType(header.PictureType, PictureType)

            ' Verify that tagName given and read match.
            If tagName = header.FrameID Then

                ' Get the encoding and create a new binary reader to read the description.
                Select Case header.TextEncoding
                    Case _ANSI
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)
                    Case _UTF16NoBom
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                    Case _UTF16WithBom
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                    Case _UTF8
                        _BinaryReader = New BinaryReader(_FileStream, Encoding.UTF8)
                    Case Else
                        picture = Nothing
                        Exit Sub
                End Select

                ' Read the description with is terminated with a zero (ANSI/UTF-8) or a pair of zeros (UTF16).
                If header.TextEncoding = _ANSI Or header.TextEncoding = _UTF8 Then

                    i = 0
                    Dim description(0) As Char

                    Do
                        description(i) = _BinaryReader.ReadChar
                        i += 1  ' Advance character array by 1 since this is ANSI or UTF-8.
                        ReDim Preserve description(i)
                    Loop Until description(i - 1) = Chr(0)

                    ' Store the result in the structure.
                    header.Description = description

                    ' Also make the description available to the properties. Remove the trailing zero.
                    _AttachedPictureDescription = New String(description, 0, description.Length - 1)

                ElseIf header.TextEncoding = _UTF16NoBom Or header.TextEncoding = _UTF16WithBom Then

                    i = 0
                    Dim description(0) As Char

                    Do
                        description(i) = _BinaryReader.ReadChar
                        i += 2  ' Advance character array by 1 since this is UTF-16.
                        ReDim Preserve description(i)
                    Loop Until description(i - 1) = ChrW(0) ' Use a wide ending zero (00 00).

                    ' Store the result in the structure.
                    header.Description = description

                    ' Also make the description available to the properties. Remove the trailing zero.
                    _AttachedPictureDescription = New String(description, 0, description.Length - 2)

                End If
            Else
                ' Invalid or unsupported frame id.
                picture = Nothing
            End If

            ' Assuming we have a JPEG image, the next two bytes will be FF and D8. Check this now.
            ' Save the current position of the binary reader so we can come back to it.
            Dim originalPosition As Long = _BinaryReader.BaseStream.Position
            Dim check() As Byte
            check = _BinaryReader.ReadBytes(2)

            If check(0) = CByte(&HFF) And check(1) = CByte(&HD8) Then
                ' Move back two bytes to our original position.
                _BinaryReader.BaseStream.Position = originalPosition
                ' Calculate the number of bytes to read.
                Dim bytesToRead As Integer = CInt(frameSize - _BinaryReader.BaseStream.Position + 1)
                ' Read the picture bytes into an array.
                Dim pictureData() As Byte = _BinaryReader.ReadBytes(bytesToRead)
                ' Convert the byte array to an image.
                picture = ByteArrayToImage(pictureData)
            Else
                ' Error or picture is not a JPEG.
                picture = Nothing
            End If

        Catch ex As OverflowException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As OutOfMemoryException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As SecurityException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As InvalidCastException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As ArgumentException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As UnauthorizedAccessException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As DirectoryNotFoundException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As FileNotFoundException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As EndOfStreamException
            picture = Nothing  ' Copy an nothing back to picture.
        Catch ex As IOException
            picture = Nothing  ' Copy an nothing back to picture.
        Finally
            If _BinaryReader IsNot Nothing Then
                _BinaryReader.Close()
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Reads an ID3 Comment Frame. Compares the given to the found tag name to verify the correct position.
    ''' Decodes the frame header to get the frame size. Reads the text encoding byte to determine
    ''' the correct encoding to use for this frame. (They can differ with an ID3 tag!)
    ''' </summary>
    ''' <param name="frameData">text is returned by reference</param>
    ''' <param name="startPosition">starting position within the ID3 tag for the frame tag</param>
    Private Sub ReadComment(ByRef frameData As String, ByVal startPosition As Integer)

        ' Tag name.
        Const tagName As String = "COMM"

        ' Buffer for the string.
        Dim frameText As String = ""

        ' Byte order mark.
        Dim bom() As Byte

        Try
            _FileStream = New FileStream(_FileName, FileMode.Open, FileAccess.Read)
            _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)

            ' Advance our binary reader to the indicated start position.
            _BinaryReader.BaseStream.Position = startPosition

            ' Make a copy of the frame structure.
            Dim header As New CommentFrame

            ' Fill it in.
            header.FrameID = _BinaryReader.ReadChars(4)
            header.FrameSize = _BinaryReader.ReadBytes(4)
            header.Flag1 = _BinaryReader.ReadByte
            header.Flag2 = _BinaryReader.ReadByte
            header.TextEncoding = _BinaryReader.ReadByte
            header.Language = _BinaryReader.ReadChars(3)

            ' Convert header.FrameSize bytes to Integer.
            ' This encludes all bytes from and including
            ' TextEncoding to the end of the comment.
            Dim frameSize As UInt32
            frameSize = (frameSize Or header.FrameSize(0))
            frameSize = (frameSize << 8) Or header.FrameSize(1)
            frameSize = (frameSize << 8) Or header.FrameSize(2)
            frameSize = (frameSize << 8) Or header.FrameSize(3)

            ' Verify that tagName given and read match.
            If tagName = header.FrameID Then

                ' Loop twice to get both the description and the actual comment.
                For i As Integer = 0 To 1
                    ' Get the encoding and create a new binary reader.
                    Select Case header.TextEncoding
                        Case _ANSI
                            _BinaryReader = New BinaryReader(_FileStream, Encoding.ASCII)
                        Case _UTF16NoBom
                            _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                        Case _UTF16WithBom
                            ' Read the BOM (2 bytes).
                            bom = _BinaryReader.ReadBytes(2)
                            If bom(0) = &HFF And bom(1) = &HFE Then
                                _BinaryReader = New BinaryReader(_FileStream, Encoding.Unicode)
                            ElseIf bom(0) = &HFE And bom(1) = &HFF Then
                                _BinaryReader = New BinaryReader(_FileStream, Encoding.BigEndianUnicode)
                            Else
                                Throw New ArgumentException("Text encoding bytes states that encoding is UTF-16 with BOM. BOM is incorrect or missing.")
                            End If
                        Case _UTF8
                            ' Read the BOM (3 bytes).
                            bom = _BinaryReader.ReadBytes(3)
                            _BinaryReader = New BinaryReader(_FileStream, Encoding.UTF8)
                        Case Else
                            frameData = ""
                            Exit Sub
                    End Select

                    If header.TextEncoding = _ANSI Or header.TextEncoding = _UTF8 Then
                        Dim loopChar As Char = Chr(0)
                        frameText = ""
                        Dim position As Integer = 0
                        ' Loop to read all text up to terminator "0".
                        Do
                            loopChar = _BinaryReader.ReadChar
                            If Not Asc(loopChar) = 0 Then
                                frameText &= loopChar
                            End If
                            position += 1
                        Loop Until loopChar = Chr(0) Or position >= frameSize - 5   ' Note: this number was obtain by inspection with a hex editor and guessing!

                        ' Clean up any garbage.
                        frameText = CleanUpText(frameText)
                    ElseIf header.TextEncoding = _UTF16NoBom Or header.TextEncoding = _UTF16WithBom Then
                        Dim loopChar As Char = Chr(0)
                        frameText = ""
                        ' Loop to read all text up to terminator "00".
                        Do
                            loopChar = _BinaryReader.ReadChar
                            If Not AscW(loopChar) = 0 Then
                                frameText &= loopChar
                            End If
                        Loop Until loopChar = ChrW(0)

                        ' Clean up any garbage.
                        frameText = CleanUpText(frameText)
                    End If

                    ' On the first loop copy the text to the description.
                    ' On the second copy it to the comment.
                    If i = 0 Then
                        If _CommentDescriptionIndex > 0 And _CommentDescriptionIndex < 9 Then
                            _CommentDescriptionIndex += 1
                            _CommentDescription(_CommentDescriptionIndex) = frameText
                        ElseIf _CommentDescriptionIndex = 0 Then
                            _CommentDescription(0) = frameText
                        End If
                    ElseIf i = 1 Then
                        frameData = frameText
                    End If

                Next
            Else
                ' Invalid or unsupported frame id.
                frameData = ""  ' Copy an empty string back to frameData.
            End If

            ' This is a kludge to fix a minor problem. Sometimes if there is no comment, the app displays
            ' a "T" (which is the first character of the next field ID). This only a problem with ANSI comments
            ' because unlike the Unicode ones, they don't terminate with zero. You have use the frame size and
            ' do calculations. Ugh!
            If frameText.Length < 3 And frameText.StartsWith("T") Then
                frameText = ""
            End If

            ' Return the decoded and cleaned up frame.
            frameData = frameText

        Catch ex As OverflowException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As OutOfMemoryException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As SecurityException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As InvalidCastException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As ArgumentException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As UnauthorizedAccessException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As DirectoryNotFoundException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As FileNotFoundException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As EndOfStreamException
            frameData = ""  ' Copy an empty string back to frameData.
        Catch ex As IOException
            frameData = ""  ' Copy an empty string back to frameData.
        Finally
            If _BinaryReader IsNot Nothing Then
                _BinaryReader.Close()
            End If
            If _FileStream IsNot Nothing Then
                _FileStream.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' The purpose of this method is to remove any extraneous characters.
    ''' </summary>
    ''' <param name="textValue">Text to clean</param>
    ''' <returns>Cleaned text</returns>
    Private Shared Function CleanUpText(ByVal textValue As String) As String

        Dim characterArray() As Char = textValue.ToCharArray()
        Dim outputValue As String = ""

        ' Go through each character and only add back the ones within limits.
        For i As Integer = 0 To characterArray.Length - 1
            If AscW(characterArray(i)) = 0 Then
                Exit For    ' Abort since we have an end of line.
            ElseIf AscW(characterArray(i)) > 31 And AscW(characterArray(i)) <= 255 Then
                outputValue &= Chr(AscW(characterArray(i)))
            End If
        Next

        Return outputValue

    End Function

    ''' <summary>
    ''' Converts an image to a byte array.
    ''' </summary>
    Private Function ImageToByteArray(ByVal imageIn As Image) As Byte()

        Dim ms As MemoryStream = New MemoryStream()
        imageIn.Save(ms, Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()

    End Function

    ''' <summary>
    ''' Converts a byte array to an image.
    ''' </summary>
    Private Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image

        Dim ms As MemoryStream = New MemoryStream(byteArrayIn)
        Dim returnImage As Image = Image.FromStream(ms)
        Return returnImage

    End Function

#End Region

End Class

#Region " Public Enums "

''' <summary>
''' Version of ID3 tag.
''' </summary>
Public Enum TagVersion
    None = 0
    ID3V1X = 1
    ID3V22 = 2
    ID3V23 = 3
    ID3V24 = 4
    ID3V2X = 5
    FileNotFound = 6
    FileError = 7
End Enum

''' <summary>
''' Type of attached picture.
''' </summary>
Public Enum PictureType As Byte
    OtherPhoto = &H0
    FileIcon = &H1
    OtherFileIcon = &H2
    CoverFrontPhoto = &H3
    CoverBackPhoto = &H4
    LeafletPage = &H5
    MediaPhoto = &H6
    LeadArtistPhoto = &H7
    ArtistPerformerPhoto = &H8
    ConductorPhoto = &H9
    BandOrchestraPhoto = &HA
    ComposerPhoto = &HB
    LyricistPhoto = &HC
    RecordingLocation = &HD
    PhotoDuringRecording = &HE
    PhotoDuringPerformance = &HF
    MovieVideoScreenCapture = &H10
    ABrightColoredFish = &H11
    Illustration = &H12
    BandArtistLogotype = &H13
    PublisherStudioLogotype = &H14
End Enum

#End Region

