Imports System.IO

Public Class mfcReadID3v2Tag

    Private Structure mcsFrame
        Dim Tag As String
        Dim Flag As Integer
        Dim Size As Integer
        Dim Data1 As Byte()
        Dim Data2 As String
    End Structure
    
    Public Shared Function ReadID3v2Tag(ByVal sFilename As String) As mcTrack
        Try
            ' --------------------------------------------------
            Dim oFS As FileStream
            Dim baHeader(15) As Byte
            Dim oTrack As New mcTrack

            ' Load object with file path
            oTrack.Filename = sFilename

            oFS = New FileStream(sFilename, FileMode.Open)

            ' Get first 10 bytes, which holds the header tag, size and version.
            oFS.Read(baHeader, 0, 10)

            If (baHeader(0) = &H49) And (baHeader(1) = &H44) And (baHeader(2) = &H33) Then ' It's a proper MP3

                Dim oFrame As mcsFrame

                ' Start reading tags. I only use 5. There are more!
                oFrame = GetFrame(oFS)
                Do While Not IsNothing(oFrame.Tag)
                    Select Case oFrame.Tag
                        Case "TIT2" : oTrack.Track = oFrame.Data2    ' Song Title
                        Case "TPE1" : oTrack.Artist = oFrame.Data2    ' Artist
                        Case "TALB" : oTrack.Album = oFrame.Data2    ' Album
                        Case "TRCK" : oTrack.CDTrack = CInt(oFrame.Data2.Replace("/", "")) ' CD Track number
                        Case "TCON" : oTrack.Genre = oFrame.Data2    ' Genre Description
                    End Select

                    oFrame = New mcsFrame()
                    oFrame = GetFrame(oFS)
                Loop
            End If
            ReadID3v2Tag = oTrack

            oFS.Close()

            ' --------------------------------------------------
        Catch oError As Exception
            ReadID3v2Tag = Nothing
        End Try

    End Function
    Private Shared Function GetFrame(ByVal oFile As Stream) As mcsFrame
        Try
            ' --------------------------------------------------

            Dim oFrame As New mcsFrame()
            Dim baFrame() As Byte
            Dim oEncoding As New System.Text.ASCIIEncoding()

            ReDim baFrame(5)

            ' Pull frame name
            oFile.Read(baFrame, 0, 4)
            oFrame.Tag = oEncoding.GetString(baFrame)

            If baFrame(0) <> 0 Then
                oFrame.Tag = oFrame.Tag.Substring(0, 4).Trim

                ' Get 4 bytes for frame size
                oFile.Read(baFrame, 0, 4)
                oFrame.Size = (65536 * (baFrame(0) * 256 + baFrame(1))) + (baFrame(2) * 256 + baFrame(3))

                ' Skip padding
                oFile.Read(baFrame, 0, 3)

                If oFrame.Size > 0 Then
                    ReDim baFrame(oFrame.Size + 1)

                    oFile.Read(baFrame, 0, oFrame.Size - 1)
                    oFrame.Data1 = baFrame

                    If oFrame.Tag.Substring(0, 1) = "T" Then
                        oFrame.Data2 = oEncoding.GetString(baFrame).Trim.Replace(Chr(0), "")
                    End If
                End If

                GetFrame = oFrame
            Else
                GetFrame = Nothing
            End If

            ' --------------------------------------------------
        Catch oError As Exception
            GetFrame = Nothing
        End Try

    End Function

End Class

Public Class mcTrack

#Region "Public Declarations"

    Private psAlbum As String = ""            ' dbo.Album.Album_Name
    Private piAlbumID As Integer = 0        ' dbo.Album.Album_ID PK
    Private psArtist As String = ""            ' dbo.Artist.Artist_Name
    Private piArtistID As Integer = 0        ' dbo.Artist.Artist_ID PK
    Private pbBookmarked As Boolean = False
    Private piCDTrack As Integer = 0
    Private psFileName As String = ""
    Private psGenre As String = ""
    Private piID As Integer = 0                ' dbo.Tracks.Track_ID PK
    Private piIndex As Integer = -1            ' Listview index (To enable playing tracks in order regardless of listview's item focus, selections, ect)
    Private piMaxVolume As Integer = 100    ' To allow per-track volume so a particularly loud track can be kicked down a few decibles.
    Private pbPlayed As Boolean = False
    Private psPlayedOn As String = ""
    Private piPlayCount As Integer = 0
    Private psTrack As String = ""

#Region "-- Properties"

    Public Property Album() As String
        <DebuggerStepThrough()> _
        Get
            Album = psAlbum
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sAlbum As String)
            psAlbum = sAlbum
        End Set
    End Property
    Public Property AlbumID() As Integer
        <DebuggerStepThrough()> _
        Get
            AlbumID = piAlbumID
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iAlbumID As Integer)
            piAlbumID = iAlbumID
        End Set
    End Property
    Public Property Artist() As String
        <DebuggerStepThrough()> _
        Get
            Artist = psArtist
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sArtist As String)
            psArtist = sArtist
        End Set
    End Property
    Public Property ArtistID() As Integer
        <DebuggerStepThrough()> _
        Get
            ArtistID = piArtistID
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iArtistID As Integer)
            piArtistID = iArtistID
        End Set
    End Property
    Public Property Bookmarked() As Boolean
        <DebuggerStepThrough()> _
        Get
            Bookmarked = pbBookmarked
        End Get
        <DebuggerStepThrough()> _
         Set(ByVal bBookmarked As Boolean)
            pbBookmarked = bBookmarked
        End Set
    End Property
    Public Property CDTrack() As Integer
        <DebuggerStepThrough()> _
        Get
            CDTrack = piCDTrack
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iCDTrack As Integer)
            piCDTrack = iCDTrack
        End Set
    End Property
    Public Property Filename() As String
        <DebuggerStepThrough()> _
        Get
            Filename = psFileName
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sFileName As String)
            psFileName = sFileName
        End Set
    End Property
    Public Property Genre() As String
        <DebuggerStepThrough()> _
        Get
            Genre = psGenre
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sGenre As String)
            psGenre = sGenre
        End Set
    End Property
    Public Property ID() As Integer
        <DebuggerStepThrough()> _
        Get
            Return piID
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iID As Integer)
            piID = iID
        End Set
    End Property
    Public Property Index() As Integer
        <DebuggerStepThrough()> _
        Get
            Index = piIndex
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iIndex As Integer)
            piIndex = iIndex
        End Set
    End Property
    Public Property MaxVolume() As Integer
        <DebuggerStepThrough()> _
        Get
            MaxVolume = piMaxVolume
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iMaxVolume As Integer)
            piMaxVolume = iMaxVolume
        End Set
    End Property
    Public Property PlayCount() As Integer
        <DebuggerStepThrough()> _
        Get
            PlayCount = piPlayCount
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal iPlayCount As Integer)
            piPlayCount = iPlayCount
        End Set
    End Property
    Public Property Played() As Boolean
        <DebuggerStepThrough()> _
        Get
            Played = pbPlayed
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal bPlayed As Boolean)
            pbPlayed = bPlayed
        End Set
    End Property
    Public Property PlayedOn() As String
        <DebuggerStepThrough()> _
        Get
            PlayedOn = psPlayedOn
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sPlayedOn As String)
            psPlayedOn = sPlayedOn
        End Set
    End Property
    Public Property Track() As String
        <DebuggerStepThrough()> _
        Get
            Track = psTrack
        End Get
        <DebuggerStepThrough()> _
        Set(ByVal sTrack As String)
            psTrack = sTrack
        End Set
    End Property

#End Region

    Public Sub New()
        MyBase.New()
        piID = -1
    End Sub

#End Region

End Class





    Public Function ReadID3v2Tag(ByVal sFile As String) As Track  
      
            Try ' --------------------------------------------------  
      
                ' MP3 Spec:  
                ' --------------------------------------------------  
                ' Field Name        ID3v1   ID3v1.1 ID3v2.2 ID3v2.3     ID3v2.4     Lyrics3v1   Lyrics3v2  
                ' Song Title        title   title   TT2     TIT2        TIT2        --          ETT   
                ' Lead Artist       artist  artist  TP1     TPE1        TPE1        --          EAR  
                ' Album Title       album   album   TAL     TALB        TALB        --          EAL  
                ' Year Released     year    year    TYE     TYER        TDRC        --          --  
                ' Comment           comment comment COM     COMM        COMM        --          INF  
                ' Song Genre        genre   genre   TCO     TCON        TCON        --          --  
                ' Track number      --      track   TRK     TRCK        TRCK        --          --  
                ' Lyrics            --      --      SYL/ULT SYLT/USLT   SYLT/USLT   lyric       LYR  
                ' Auth/Composer     --      --      TCM     TCOM        TCOM        --          AUT  
                ' Popularimeter                             POPM  
      
                ' POPM frame in the ID3v2 specification - most software all map roughly the same ranges of 0–255 to a 0–5 stars value for display.  
                'This chart details how windows explorer reads and writes the POPM frame:  
                '    224-255 = 5 stars when READ with windows explorer, writes 255  
                '    160-223 = 4 stars when READ with windows explorer, writes 196  
                '    096-159 = 3 stars when READ with windows explorer, writes 128  
                '    032-095 = 2 stars when READ with windows explorer, writes 64  
                '    001-031 = 1 stars when READ with windows explorer, writes 1  
                ' --------------------------------------------------  
                Dim oFS As FileStream  
                Dim baHeader As Byte()  
                Dim oTrack As New Track  
      
                ' Load object with file path  
                oTrack.Filename = sFile  
                oTrack.Title = sFile.Substring(sFile.LastIndexOf("\"c) + 1) ' Default Track to file name, in case nothing is picked up below.  
      
                oFS = New FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)  
      
                ' Get first 10 bytes, which holds the header tag, size and version.  
                ' The ID3v2 tag header, which should be the first information in the file, is 10 bytes as follows:  
                ' ID3v2/file identifier   "ID3"             - 3 bytes  
                ' ID3v2 version           $03 00            - 2 bytes  
                ' ID3v2 flags             %abc00000         - 1 byte  
                ' ID3v2 size              4 * %0xxxxxxx     - 4 bytes  
                ReDim baHeader(2)  
                oFS.Read(baHeader, 0, 3) ' Read MP3 header info  
                If (baHeader(0) = &H49) And (baHeader(1) = &H44) And (baHeader(2) = &H33) Then ' It's a proper MP3  
      
                    ReDim baHeader(1)  
                    oFS.Read(baHeader, 0, 2)  
      
                    If baHeader(0) = 2 And baHeader(1) = 3 Then ' ID3v2.3.0  
      
                    End If  
      
                    ReDim baHeader(5)  
                    oFS.Read(baHeader, 0, 5) ' Not currently using flags or size.  
      
                    Dim oFrame As Frame  
      
                    ' Start reading tags.  
                    oFrame = GetFrame(oFS)  
                    If oFrame.Data2.Length > 255 Then oFrame.Data2 = oFrame.Data2.Substring(0, 255)  
      
                    Do While Not IsNothing(oFrame.Tag)  
                        Select Case oFrame.Tag  
                            Case "TIT2", "TT2" : oTrack.Title = oFrame.Data2    ' Song Title  
                            Case "TPE1", "TP1" : oTrack.Artist = oFrame.Data2   ' Artist  
                            Case "TALB", "TAL" : oTrack.Album = oFrame.Data2    ' Album  
                            Case "TRCK", "TRK"  
                                Dim sCD As String = oFrame.Data2  
                                sCD = sCD.Replace("/", "").Replace("?", "")  
                                If sCD.Length > 2 Then sCD = sCD.Substring(0, sCD.Length - 2)  
                                If IsNumeric(sCD) Then oTrack.TrackNumber = CInt(sCD) ' CD Track number  
                            Case "TCON", "TCO" : oTrack.Genre = oFrame.Data2    ' Genre Description  
                            Case "POPM"  
                        End Select  
      
                        oFrame = GetFrame(oFS)  
                        If IsNothing(oFrame) Then goLog.Post("Frame Clip", sFile)  
                    Loop  
                End If  
      
                oFS.Close()  
      
                Return oTrack  
                ' --------------------------------------------------  
            Catch oError As Exception ' Only log on Errors  
                goLog.StartProc("Metallisoft.MP3Player.Audio.ReadID3v2Tag(" & sFile & ")") : goLog.StopProc("", True, oError, False) ': If gbIsIDE Then Stop  
                Return Nothing  
            End Try  
      
        End Function  
      
        <DebuggerStepThrough()> _  
        Private Function GetFrame(ByVal oFile As Stream) As Frame  
      
            Try ' --------------------------------------------------  
      
                Dim oFrame As New Frame  
                Dim baFrame As Byte()  
                Dim oEncoding As New System.Text.ASCIIEncoding()  
      
                ReDim baFrame(4)  
      
                ' Pull frame name  
                oFile.Read(baFrame, 0, 4)  
                oFrame.Tag = oEncoding.GetString(baFrame)  
      
                If baFrame(0) <> 0 Then  
                    oFrame.Tag = oFrame.Tag.Substring(0, 4).Trim.Replace(Chr(0), "")  
                    If Not oFrame.Tag.Contains("?") Then  
                        ' Get 4 bytes for frame size  
                        oFile.Read(baFrame, 0, 4)  
                        oFrame.Size = (65536 * (baFrame(0) * 256 + baFrame(1))) + (baFrame(2) * 256 + baFrame(3))  
      
                        ' Skip padding  
                        oFile.Read(baFrame, 0, 3)  
      
                        If oFrame.Size > 0 Then  
                            ReDim baFrame(oFrame.Size + 1)  
                            oFile.Read(baFrame, 0, oFrame.Size - 1)  
      
                            If oFrame.Tag.Substring(0, 1) = "T" Then  
                                oFrame.Data1 = baFrame  
                                oFrame.Data2 = oEncoding.GetString(baFrame).Trim.Replace(Chr(0), "")  
                            End If  
                        End If  
                    End If  
                    Return oFrame  
                Else  
                    Return Nothing  
                End If  
      
                ' --------------------------------------------------  
            Catch oError As Exception ' Only log on Errors  
                goLog.StartProc("Metallisoft.MP3Player.Audio.GetFrame()") : goLog.StopProc("", True, oError, False) ': If gbIsIDE Then Stop  
                Return Nothing  
            End Try  
        End Function  


Imports System.IO

Public Class MP3Info
    Private mFileName As String

    ''' <summary>
    ''' This function reads the ID3v1 tag of a MP3 file
    ''' </summary>
    ''' <param name="FileName">MP3 file to be read</param>
    ''' <returns>A object with the ID3v1 tag information</returns>
    Public Function GetID3v1Tag(Optional ByVal FileName As String = "") As ID3V1
        Dim FS As FileStream
        Dim BinReader As BinaryReader
        Dim Buffer() As Byte
        Dim Tag As New ID3V1

        If FileName = "" Then FileName = mFileName

        FS = New FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

        BinReader = New BinaryReader(FS)

        'ID3v1 tag is in the last 128 bytes of the file
        FS.Seek(-128, SeekOrigin.End)
        If (ByteToString(BinReader.ReadBytes(3)).ToUpper = "TAG") Then

            Tag.TagAvailable = True

            With Tag
                .Title = ByteToString(BinReader.ReadBytes(30)).Replace(Chr(0), "")
                .Artist = ByteToString(BinReader.ReadBytes(30)).Replace(Chr(0), "")
                .Album = ByteToString(BinReader.ReadBytes(30)).Replace(Chr(0), "")
                .Year = ByteToString(BinReader.ReadBytes(4)).Replace(Chr(0), "")

                'If byte 28 == 0 and byte 29 <> 0 then version 1.1, else 1.0
                Buffer = BinReader.ReadBytes(30)
                If ((Buffer(28) = 0) And (Buffer(29) <> 0)) Then
                    .TagVersion = ID3V1.ID3Version.ID3V11
                    .Comment = ByteToString(Buffer, 0, 28).Replace(Chr(0), "")
                    .Track = Buffer(29)
                Else
                    .TagVersion = ID3V1.ID3Version.ID3V10
                    .Comment = ByteToString(Buffer).Replace(Chr(0), "")
                    .Track = 0
                End If

                .Genre = BinReader.ReadByte
            End With

        End If

        BinReader.Close()
        FS.Close()

        Return Tag
    End Function

    ''' <summary>
    ''' This function writes ID3v1 data from a ID3v1 object to a MP3 file
    ''' </summary>
    ''' <param name="Tag">The object with the ID3v1 tag data</param>
    ''' <param name="FileName">MP3 file in which the information is to be written</param>
    Public Sub SetID3v1Tag(ByVal Tag As ID3V1, Optional ByVal FileName As String = "")
        Dim FS As FileStream
        Dim BinReader As BinaryReader
        Dim BinWriter As BinaryWriter

        If FileName = "" Then FileName = mFileName

        FS = New FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)

        BinReader = New BinaryReader(FS)

        FS.Seek(-128, SeekOrigin.End)
        If (ByteToString(BinReader.ReadBytes(3)).ToUpper = "TAG") Then
            'TAG found - go 3 steps back in the file
            FS.Seek(-3, SeekOrigin.Current)
        Else
            'No TAG found - write at the end of file
            FS.Seek(0, SeekOrigin.End)
        End If

        BinWriter = New BinaryWriter(FS)

        BinWriter.Write("TAG".ToCharArray)
        BinWriter.Write(Tag.Title.PadRight(30, Chr(0)).ToCharArray)
        BinWriter.Write(Tag.Artist.PadRight(30, Chr(0)).ToCharArray)
        BinWriter.Write(Tag.Album.PadRight(30, Chr(0)).ToCharArray)
        BinWriter.Write(Tag.Year.PadRight(4, Chr(0)).ToCharArray)
        Select Case Tag.TagVersion
            Case ID3V1.ID3Version.ID3V10
                BinWriter.Write(Tag.Comment.PadRight(30, Chr(0)).ToCharArray)
            Case ID3V1.ID3Version.ID3V11
                BinWriter.Write(Tag.Comment.PadRight(28, Chr(0)).ToCharArray)
                BinWriter.Write(Chr(0))
                BinWriter.Write(Tag.Track)
        End Select
        BinWriter.Write(Tag.Genre)
        BinWriter.Flush()

        BinWriter.Close()
        BinReader.Close()
    End Sub

    Private Function ByteToString(ByVal Expression() As Byte, Optional ByVal Index As Integer = 0, Optional ByVal Length As Integer = 0) As String
        If Length = 0 Then Length = Expression.Length
        Return System.Text.Encoding.Default.GetString(Expression, Index, Length)
    End Function
End Class

''' <summary>
''' A class to store ID3v1 information of MP3 files
''' </summary>
Public Class ID3V1
    Private mTagAvailable As Boolean
    Private mTagVersion As ID3Version
    Private mArtist As String
    Private mTitle As String
    Private mAlbum As String
    Private mYear As String
    Private mComment As String
    Private mGenre As Byte
    Private mGenreName As String
    Private mTrack As Byte

    Public Property TagAvailable() As Boolean
        Get
            Return mTagAvailable
        End Get
        Set(ByVal value As Boolean)
            mTagAvailable = value
        End Set
    End Property

    Public Property TagVersion() As ID3Version
        Get
            Return mTagVersion
        End Get
        Set(ByVal value As ID3Version)
            mTagVersion = value
        End Set
    End Property

    Public Property Artist() As String
        Get
            Return mArtist
        End Get
        Set(ByVal value As String)
            mArtist = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public Property Album() As String
        Get
            Return mAlbum
        End Get
        Set(ByVal value As String)
            mAlbum = value
        End Set
    End Property

    Public Property Year() As String
        Get
            Return mYear
        End Get
        Set(ByVal value As String)
            mYear = value
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return mComment
        End Get
        Set(ByVal value As String)
            mComment = value
        End Set
    End Property

    Public Property Genre() As Byte
        Get
            Return mGenre
        End Get
        Set(ByVal value As Byte)
            mGenre = value
            mGenreName = GetGenreName(value)
        End Set
    End Property

    Public ReadOnly Property GenreName() As String
        Get
            Return mGenreName
        End Get
    End Property

    Public Property Track() As Byte
        Get
            Return mTrack
        End Get
        Set(ByVal value As Byte)
            mTrack = value
        End Set
    End Property

    Public Enum ID3Version As Integer
        ID3V10 = 10
        ID3V11 = 11
    End Enum

    Public Function GetGenreName(ByVal Number As Byte) As String
        Dim Genres() As String = {"Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", _
        "Hip - Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", _
        "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro -Techno", "Ambient", _
        "Trip -Hop", "Vocal", "Jazz Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", _
        "Sound Clip", "Gospel", "Noise", "AlternRock", "Bass", "Soul", "Punk", "Space", "Meditative", _
        "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno -Industrial", "Electronic", _
        "Pop -Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", _
        "Pop/Funk", "Jungle", "Native American", "Cabaret", "New Wave", "Psychadelic", "Rave", "Showtunes", "Trailer", _
        "Lo - Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", _
        "Folk", "Folk/Rock", "National Folk", "Swing", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", _
        "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", _
        "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", _
        "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", _
        "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "A Cappella", "Euro - House", _
         "Dance Hall", "Goa", "Drum & Bass", "Club - House", "Hardcore", "Terror", "Indie", "BritPop", "Negerpunk", _
        "Polsk Punk", "Beat", "Christian Gangsta Rap", "Heavy Metal", "Black Metal", "Crossover", "Contemporary Christian", _
        "Christian Rock", "Merengue", "Salsa", "Thrash Metal", "Anime", "JPop", "Synthpop"}
        Return Genres(Number)
    End Function
End Class
