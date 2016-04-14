REM http://www.freevbcode.com/ShowCode.asp?ID=1118

REM Create a new code module and paste this code into it:


Option Explicit

Public Type ID3Tag
    Header As String * 3
    SongTitle As String * 30
    Artist  As String * 30
    Album  As String * 30
    Year  As String * 4
    Comment As String * 30
    Genre  As Byte
End Type

Public Function GetID3Tag(FileName As String, Tag As ID3Tag) _
   As Boolean

'******************************
'Instructions:
'Pass an variable declared as type ID3Tag to Tag Parameter
'and read its member data after the function returns (assuming
'the function returns true)
'****************************

On Error GoTo GetID3TagError

Dim TempTag As ID3Tag
Dim FileNum As Long

    If Dir(FileName) = "" Then
        GetID3Tag = False
        Exit Function
    End If
    
    FileNum = FreeFile
    
    Open FileName For Binary As FileNum
    Get FileNum, LOF(1) - 127, TempTag
    Close FileNum
    
    If TempTag.Header <> "TAG" Then
        GetID3Tag = False
    Else
        Tag = TempTag
        GetID3Tag = True
    End If

    Exit Function

GetID3TagError:
    Close FileNum
    GetID3Tag = False
End Function