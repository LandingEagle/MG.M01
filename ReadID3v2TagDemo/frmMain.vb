Public Class frmMain

	Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		With lvTracks
			.HideSelection = False
			.View = View.Details
			.Columns.Add("Song", lvTracks.Width \ 3)
			.Columns.Add("Album", lvTracks.Width \ 3)
			.Columns.Add("Artist", lvTracks.Width \ 3)
		End With
	End Sub

	Private Sub cmdCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommand.Click

		Dim sStartFolder As String = ""

		If sStartFolder.Length > 0 Then
			mfcReadID3v2Tag.SearchForTracks(sStartFolder)
		Else
			MessageBox.Show("Please set your start path in frmMain.cmdCommand_Click()")
		End If

	End Sub

End Class
