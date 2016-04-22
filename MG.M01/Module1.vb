Imports System
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
   REM * FLAC-Metadata-Informations
   REM ----------------------------------------------------------------------------------------------------------------------
   Public flc000000000 As New FlacHeader.StreamHdrflc REM Streamheader
   Public flcBlckHeadr As New FlacHeader.BlckHeadrflc REM MetaData-blockheder
   Public flcMD0000000 As New FlacHeader.MD0000000flc REM 0000000   0 Streaminfo
   Public flcMD0000001 As New FlacHeader.MD0000001flc REM 0000001   1 Padding
   Public flcMD0000010 As New FlacHeader.MD0000010flc REM 0000010   2 Application
   Public flcMD0000011 As New FlacHeader.MD0000011flc REM 0000011   3 Seektable
   Public flcMDe000011 As New FlacHeader.MDe000011flc REM 0000011     Seekpoint
   Public flcMD0000100 As New FlacHeader.MD0000100flc REM 0000100   4 Vorbis_Comment
   Public flcMD0000101 As New FlacHeader.MD0000101flc REM 0000101   5 Cuesheet
   Public flcMDe000101 As New FlacHeader.MDe000101flc REM 0000101     CUESHEET_TRACK
   Public flcMDe100101 As New FlacHeader.MDe100101flc REM 0000101     CUESHEET_TRACK_INDEX
   Public flcMD0000110 As New FlacHeader.MD0000110flc REM 0000110   6 Picture METADATA_BLOCK_PICTURE
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

      flc000000000.isFLAC = False

      Dim flMNbr(3) As Byte

      InpFil.Read(flMNbr, 0, 4)
      If flMNbr(0) = &H66 And flMNbr(1) = &H4C And flMNbr(2) = &H61 And flMNbr(3) = &H43 Then
         flc000000000.isFLAC = True
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