Imports System
Imports System.IO
Imports System.Collections

Imports System.Data.SQLite

Module Module1

   Const MGdb As String = "C:\Users\Thomas\Dropbox\Developement\VStudio\Projects\MediaGorilla\MediaGorilla.db"

   Public Structure stcArtist
      Dim ArtistID As Int16
      Dim Name As String
      Dim NameSort As String
   End Structure

   Public Artist As New stcArtist

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

      For Each FilNam In FilLst

         Dim InpFil = IO.File.Open(FilNam, FileMode.Open)
         Dim BytCnt = InpFil.Length
         Dim InpByt = New Byte(BytCnt - 1) {}
         Dim InpChr As String
         Dim InpOff As Long = 0

         Dim Idx As Long = 0

         While InpFil.Read(InpByt, 0, BytCnt) > 0

            InpChr = InpByt.ToString()
            If InpChr = "A" Then
               Console.WriteLine("Gerade ein A gelesen")
            End If

         End While

      Next FilNam

      Return True

   End Function

End Module