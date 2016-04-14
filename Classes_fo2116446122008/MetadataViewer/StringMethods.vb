Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Text

Friend NotInheritable Class StringMethods

#Region " Contructor "

    Private Sub New()
        ' Just for the compiler.
    End Sub

#End Region

#Region " Split Camel Case "

    ''' <summary>
    ''' Parses a camel cased or pascal cased string and returns a new
    ''' string with spaces between the words in the string.
    ''' </summary>
    ''' <example>
    ''' The string "PascalCasing" will return an array with two
    ''' elements, "Pascal" and "Casing".
    ''' </example>
    Friend Shared Function SplitUppercaseToString(ByVal source As String) As String

        Return String.Join(" ", SplitUppercase(source)).Trim()

    End Function

    ''' <summary>
    ''' Parses a camel cased or pascal cased string and returns an array
    ''' of the words within the string.
    ''' </summary>
    ''' <example>
    ''' The string "PascalCasing" will return an array with two
    ''' elements, "Pascal" and "Casing".
    ''' </example>
    Friend Shared Function SplitUppercase(ByVal source As String) As String()

        Try
            If String.IsNullOrEmpty(source) Then
                Return New String() {} ' Return empty array.
            End If

            If source.Length = 0 Then
                Return New String() {""}
            End If

            Dim words As StringCollection = New StringCollection()
            Dim wordStartIndex As Integer = 0

            Dim letters As Char() = source.ToCharArray()

            '  Skip the first letter. we don't care what case it is.
            For i As Integer = 1 To letters.Length - 1

                ' This modification allows abbreviation to be output correctly. ie., "PNP" not "P N P"
                If Char.IsUpper(letters(i)) AndAlso i + 1 < letters.Length _
                    AndAlso Not Char.IsUpper(letters(i + 1)) Then
                    ' Grab everything before the current index.
                    words.Add(New String(letters, wordStartIndex, i - wordStartIndex))
                    wordStartIndex = i
                End If
            Next

            ' We need to have the last word.
            words.Add(New String(letters, wordStartIndex, letters.Length - wordStartIndex))

            ' Copy to a string array.
            Dim wordArray As String() = New String(words.Count) {}
            words.CopyTo(wordArray, 0)

            Return wordArray
        Catch ex As ArgumentOutOfRangeException
            Return New String() {""}
        Catch ex As NullReferenceException
            Return New String() {""}
        End Try

    End Function

#End Region

#Region " Resize ListView Columns "

    ''' <summary>
    ''' Auto resizes ListView columns.
    ''' </summary>
    ''' <param name="referenceListView">The referenced ListView</param>
    ''' <param name="resizeStyle">The Windows ColumnHeaderAutoResizeStyle</param>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")> _
    Public Shared Sub ResizeListViewColumns(ByRef referenceListView As ListView, ByVal resizeStyle As ColumnHeaderAutoResizeStyle)

        referenceListView.SuspendLayout()

        ' Resize columns or hide if option is set.
        For i As Integer = 0 To referenceListView.Columns.Count - 1

            referenceListView.Columns(i).AutoResize(resizeStyle)

        Next

        referenceListView.ResumeLayout()
        referenceListView.Refresh()

    End Sub

    ''' <summary>
    ''' Auto resizes ListView columns.
    ''' This overload provides the option to hide one column by setting its width to 0.
    ''' </summary>
    ''' <param name="referenceListView">The referenced ListView</param>
    ''' <param name="resizeStyle">The Windows ColumnHeaderAutoResizeStyle</param>
    ''' <param name="hideColumn">The column index to hide</param>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")> _
    Public Shared Sub ResizeListViewColumns(ByRef referenceListView As ListView, ByVal resizeStyle As ColumnHeaderAutoResizeStyle, ByVal hideColumn As Integer)

        referenceListView.SuspendLayout()

        ' Resize columns or hide if option is set.
        For i As Integer = 0 To referenceListView.Columns.Count - 1

            If hideColumn = i Then
                referenceListView.Columns(i).Width = 0
            Else
                referenceListView.Columns(i).AutoResize(resizeStyle)
            End If

        Next

        referenceListView.ResumeLayout()
        referenceListView.Refresh()

    End Sub

#End Region

End Class
