REM Imports System
Imports System.IO
REM Imports System.Collections

Imports System.Data.SQLite

Module Module1

   Const MGdb As String = "C:\Users\Thomas\Dropbox\Developement\VStudio\Projects\MediaGorilla\MediaGorilla.db"

   Public Structure stcArtist
      Dim ArtistID As Int16
      Dim Name As String
      Dim NameSort As String
   End Structure
   Public Artist As New stcArtist

   REM ######################################################################################################################
   REM Beginn Structures just for Documentation
   REM ######################################################################################################################

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure flc0Frame
      Dim Flac() As Byte
   End Structure
   Public frmFlac0 As flc0Frame
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure flcBlkHdr    REM Flac - Blockheader
      Dim LstBlk As BitArray     REM   1b is last Metadata-block
      Dim BlkTyp As BitArray     REM   7b Block-Type Blocktype  0000000   0 Streaminfo
      REM                                                       0000001   1 Padding
      REM                                                       0000010   2 Application
      REM                                                       0000011   3 Seektable
      REM                                                       0000100   4 Vorbis_Comment
      REM                                                       0000101   5 Cuesheet
      REM                                                       0000110   6 Picture        |
      REM                                                       0000111   7
      REM                                                       ...         reserved
      REM                                                       1111110 126
      REM                                                       1111111 127 invalid, to avoid confusion with a frame sync code
      Dim BlkLng As BitArray     REM  24b Length (in bytes) of metadata to follow
   End Structure
   Public flBlkHdr As flcBlkHdr
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000000flc REM 0000000   0 Streaminfo
      Dim BlsMin As BitArray     REM  16b The minimum block size (in samples) used in the stream.
      Dim BlsMax As BitArray     REM  16b The maximum block size (in samples) used in the stream. (Minimum blocksize == maximum blocksize) implies a fixed-blocksize stream.
      REM                        FLAC specifies a minimum block size of 16 and a maximum block size of 65535, meaning the bit patterns corresponding to the numbers 0-15 in the minimum blocksize and maximum blocksize fields are invalid.
      Dim FrsMin As BitArray     REM  24b The minimum frame size (in bytes) used in the stream. May be 0 to imply the value is not known
      Dim FrsMax As BitArray     REM  24b The maximum frame size (in bytes) used in the stream. May be 0 to imply the value is not known
      Dim SmplRt As BitArray     REM  20b Sample rate in Hz. Though 20 bits are available, the maximum sample rate is limited by the structure of frame headers to 655350Hz. Also, a value of 0 is invalid.
      Dim NbrChl As BitArray     REM   3b (number of channels)-1. FLAC supports from 1 to 8 channels
      Dim SmplBt As BitArray     REM   5b (bits per sample)-1. FLAC supports from 4 to 32 bits per sample. Currently the reference encoder and decoders only support up to 24 bits per sample.
      Dim SmplNb As BitArray     REM  36b Total samples in stream. 'Samples' means inter-channel sample, i.e. one second of 44.1Khz audio will have 44100 samples regardless of the number of channels. A value of zero here means the number of total samples is unknown.
      Dim MD5Sgn As BitArray     REM 128b MD5 signature of the unencoded audio data. This allows the decoder to determine if an error exists in the audio data even when the error does not result in an invalid bitstream.
   End Structure
   Public flcMD0000000 As MD0000000flc
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ######################################################################################################################
   REM End Structures just for Documentation
   REM ######################################################################################################################

   Public ds As String = "Data Source=" & MGdb & ";"
   Public MGcnn As New SQLite.SQLiteConnection(ds)

   Sub Main()

      Dim Dummy As VariantType

      Dummy = GetTags()

      Exit Sub

      MGcnn.Open()
      Artist = GetArtist(1)

   End Sub

   Function GetArtist(ArtistID As Int16) As stcArtist

      Dim SqlStmt As String = "select * from Artist as Artist where ArtistID = " & Format(ArtistID, "0")

      Dim MGcmd As New SQLiteCommand(SqlStmt, MGcnn)
      Dim MGrdr As SQLiteDataReader = MGcmd.ExecuteReader()

      If Not MGrdr.HasRows Then
         Return Nothing
         Exit Function
      End If

      While MGrdr.Read()

         Artist.ArtistID = MGrdr.Item(0)
         Artist.Name = MGrdr.Item(1)
         Artist.NameSort = MGrdr.Item(2)

         Console.WriteLine("|" & Artist.ArtistID _
                         & ";" & Artist.Name _
                         & ";" & Artist.NameSort _
                         & "|")
      End While

      Return Artist

      MGcmd.Dispose()
      MGcnn.Close()

   End Function

   Function GetTags() As Boolean

      Dim DirLoc As String = "G:\A&V\Musik\flac\Sky\1981) Sky 3 (Re-Issue 2015)\1981) Sky 3 (Re-Issue 2015) (CD01)"
      Dim FilLst As String() = Directory.GetFiles(DirLoc)
      Dim FilNam As String

      Dim InpFil As FileStream

      For Each FilNam In FilLst

         InpFil = New FileStream(FilNam, FileMode.Open)

         If Not isFlac(InpFil) Then Return False
         If Not isFldEnd(InpFil) Then Return False

      Next FilNam

      Return True

   End Function

   Function isFlac(InpFil As FileStream) As Boolean

      Dim flMNbr(3) As Byte

      InpFil.Read(flMNbr, 0, 4)
      If flMNbr(0) = &H66 And flMNbr(1) = &H4C And flMNbr(2) = &H61 And flMNbr(3) = &H43 Then
         frmFlac0.Flac = flMNbr
         Return True
      End If

      Return False

   End Function

   Function isFldEnd(InpFil As FileStream) As Boolean

      Dim FldEnd(2) As Byte

      InpFil.Read(FldEnd, 0, 3)
      If FldEnd(0) = &H0 And FldEnd(1) = &H0 And FldEnd(2) = &H0 Then Return True
      Return False

   End Function

End Module