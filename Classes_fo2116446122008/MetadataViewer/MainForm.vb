Imports System.Text
Imports System.IO
Imports System.Security
Imports Microsoft.Win32
Imports System.ComponentModel
Imports MP3InfoLibrary
Imports TagReaderLibrary
Imports System.Collections.Specialized
Imports VirtualSoftware.MetadataCollector

Public Class MainForm

#Region " Private Members "

    Private _LastOpenFolder As String
    Private _FileName As String
    Private _ListViewItem As ListViewItem
    Private _ListViewGroup As ListViewGroup
    Private _Ascending As Boolean = True    ' Use to toggle listview column sorting.
    Private _FileType As String             ' WMA or ID3
    Private _OpenFileFilterIndex As Integer = 1
    Private _Attributes As StringCollection = New StringCollection()
    Private _DataTypes As StringCollection = New StringCollection()
    Private _Values As StringCollection = New StringCollection()
    Private _WmaData As VirtualSoftware.MetadataCollector

#End Region

#Region " Form Event Handlers "

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing

        ' Save settings.
        My.Settings.LastOpenFolder = _LastOpenFolder
        My.Settings.Save()

    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Me.Load

        My.Settings.Reload()
        _LastOpenFolder = My.Settings.LastOpenFolder

    End Sub

#End Region

#Region " ListView Event Handlers "

    ''' <summary>
    ''' Set the ListViewItemSorter property to a new ListViewItemComparer 
    ''' object. Setting this property immediately sorts the 
    ''' ListView using the ListViewItemComparer object.
    ''' </summary>
    Private Sub ListViewTagData_ColumnClick(ByVal sender As Object, _
                                            ByVal e As System.Windows.Forms.ColumnClickEventArgs) _
                                            Handles ListViewTagData.ColumnClick

        ' Toggle sort order.
        If _Ascending = True Then
            _Ascending = False
        Else
            _Ascending = True
        End If

        ' Perform sort of items in specified column.
        ListViewTagData.ListViewItemSorter = New ListViewItemComparer(e.Column, _Ascending)

    End Sub

#End Region

#Region " Menu Event Handlers "

    Private Sub MenuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MenuOpen.Click, ButtonBrowse.Click

        Dim errorMessage As String = OpenFile()

        Try
            If Not String.IsNullOrEmpty(errorMessage) AndAlso String.IsNullOrEmpty(_FileName) Then
                StatusLabel.Text = "Error reading tag informtion: " & errorMessage
            ElseIf File.Exists(_FileName) AndAlso _FileType = "id3" Then
                TextBoxFileName.Text = _FileName
                DisplayId3TagData()
                DisplayMP3FileInfo()
                StatusLabel.Text = ""
            ElseIf File.Exists(_FileName) AndAlso (_FileType = "wma" Or _FileType = "wmv") Then
                TextBoxFileName.Text = _FileName

                _WmaData = New VirtualSoftware.MetadataCollector()

                Dim returnValue As String = _WmaData.ReturnMetadata(_FileName)

                ' Display any errors.
                If Not String.IsNullOrEmpty(returnValue) Then
                    MessageBox.Show(returnValue, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Else
                    ' Get the string collections.
                    _Attributes.Clear()
                    _DataTypes.Clear()
                    _Values.Clear()

                    _Attributes = _WmaData.Attributes
                    _DataTypes = _WmaData.DataTypes
                    _Values = _WmaData.Values

                    DisplayWmaTagData()
                    DisplayWmaFileProperties()

                    PictureBoxImage.Image = _WmaData.Picture

                End If

                StatusLabel.Text = ""
            ElseIf File.Exists(_FileName) AndAlso _FileType = "wmv" Then
                    TextBoxFileName.Text = _FileName
                    DisplayWmaTagData()
                    StatusLabel.Text = ""
            Else
                    TextBoxFileName.Text = ""
                    ListViewTagData.Items.Clear()
                    StatusLabel.Text = ""
            End If
        Catch ex As NullReferenceException
            StatusLabel.Text = "Error obtaining file information."
        End Try

    End Sub

    Private Sub MenuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MenuExit.Click

        Me.Close()

    End Sub

    Private Sub MenuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MenuAbout.Click

        AboutDialog.ShowDialog(Me)

    End Sub

#End Region

#Region " Private Methods "

    Private Function OpenFile() As String

        Dim errorMessage As String = ""

        Try
            ' Clear text box and picture box.
            TextBoxFileName.Text = ""
            PictureBoxFormat.Image = Nothing

            ' Clear listview.
            ListViewTagData.Items.Clear()

            With OpenFileDialog1
                .Filter = "All Formats (*.mp3, *.wma)|*.Mp3;*.Wma|Windows Media (*.wma)|*.Wma|MP3 Files (*.mp3)|*.Mp3|Windows Video (*.wmv)|*.wmv|All Files (*.*)|*.*"
                .FilterIndex = _OpenFileFilterIndex
                .FileName = ""
                .AddExtension = True
                .DefaultExt = ".mp3"
                .CheckFileExists = True
                .CheckPathExists = True
                If String.IsNullOrEmpty(_LastOpenFolder) Or Not Directory.Exists(_LastOpenFolder) Then
                    .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
                Else
                    .InitialDirectory = _LastOpenFolder
                End If
                .SupportMultiDottedExtensions = True
                .Multiselect = False

                ' Only enable upgraded interface if CLR SP1 or later is installed.
                If CDbl(GetFrameworkServicePack()) >= 1 Then
                    .AutoUpgradeEnabled = True
                Else
                    .AutoUpgradeEnabled = False
                End If

                If .ShowDialog() = Windows.Forms.DialogResult.OK Then

                    _FileName = .FileName
                    _LastOpenFolder = Path.GetDirectoryName(.FileName)
                    _OpenFileFilterIndex = .FilterIndex

                    ' Get the extension for the picture box.
                    Dim extension As String = Path.GetExtension(_FileName).ToLower

                    Select Case extension
                        Case ".mp3"
                            PictureBoxFormat.BackgroundImage = My.Resources.MP3File_32x32
                            _FileType = "id3"
                        Case ".wma"
                            PictureBoxFormat.BackgroundImage = My.Resources.WMAFile_32x32
                            _FileType = "wma"
                        Case ".wmv"
                            PictureBoxFormat.BackgroundImage = My.Resources.WMVFile_32x32
                            _FileType = "wmv"
                        Case Else
                            PictureBoxFormat.BackgroundImage = My.Resources.Unknown_32x32
                            _FileType = ""
                    End Select

                    Return ""
                Else
                    _FileName = ""
                End If
            End With

        Catch ex As UnauthorizedAccessException
            errorMessage &= ex.Message & vbCrLf
        Catch ex As SecurityException
            errorMessage &= ex.Message & vbCrLf
        Catch ex As FileNotFoundException
            errorMessage &= ex.Message & vbCrLf
        Catch ex As DirectoryNotFoundException
            errorMessage &= ex.Message & vbCrLf
        Catch ex As IOException
            errorMessage &= ex.Message & vbCrLf
        End Try

        If String.IsNullOrEmpty(errorMessage) Then
            Return ""
        Else
            Return errorMessage
        End If

    End Function

    Private Function DisplayId3TagData() As String

        Dim returnMessage As String = ""

        Dim tagReader As Id3TagReader = New Id3TagReader()

        ' Read the tag information and get any error code.
        ' An empty string means success.
        returnMessage = tagReader.ReturnMetadata(_FileName)

        If String.IsNullOrEmpty(returnMessage) Then

            With ListViewTagData
                ' Clear all items.
                .Items.Clear()

                ' Begin Update.
                .BeginUpdate()

                ' Create listview group.
                _ListViewGroup = ListViewTagData.Groups.Add("ID3v2 Tag Information", "ID3v2 Tag Information")

                ' Add items and subitems.
                ProcessPropertyValue(tagReader.Id3Version, "ID3 Version", _ListViewGroup)
                ProcessPropertyValue(tagReader.TrackName, "Track Name", _ListViewGroup)
                ProcessPropertyValue(tagReader.TrackArtist, "Track Artist", _ListViewGroup)
                ProcessPropertyValue(tagReader.AlbumTitle, "Album Title", _ListViewGroup)
                ProcessPropertyValue(tagReader.AlbumArtist, "Album Artist", _ListViewGroup)
                ProcessPropertyValue(tagReader.Genre, "Genre", _ListViewGroup)
                ProcessPropertyValue(tagReader.Publisher, "Publisher", _ListViewGroup)
                ProcessPropertyValue(tagReader.Year, "Year", _ListViewGroup)
                ProcessPropertyValue(tagReader.Subtitle, "Subtitle", _ListViewGroup)
                ProcessPropertyValue(tagReader.OriginalAlbumTitle, "Original Album Title", _ListViewGroup)
                ProcessPropertyValue(tagReader.PartOfSet, "Part of Set", _ListViewGroup)
                ProcessPropertyValue(tagReader.Conductor, "Conductor", _ListViewGroup)
                ProcessPropertyValue(tagReader.Lyricist, "Lyricist", _ListViewGroup)
                ProcessPropertyValue(tagReader.OriginalArtist, "Original Lyricist", _ListViewGroup)
                ProcessPropertyValue(tagReader.Mood, "Mood", _ListViewGroup)
                ProcessPropertyValue(tagReader.Key, "Key", _ListViewGroup)
                ProcessPropertyValue(tagReader.TrackNumber, "Track Number", _ListViewGroup)
                ProcessPropertyValue(tagReader.CompactDiscId, "Compact Disc ID", _ListViewGroup)
                ProcessPropertyValue(tagReader.Composer, "Composer", _ListViewGroup)
                ProcessPropertyValue(tagReader.BeatsPerMinute, "Beats per Minute", _ListViewGroup)
                ProcessPropertyValue(tagReader.OriginalArtist, "Original Artist", _ListViewGroup)
                ProcessPropertyValue(tagReader.Comment, "Comment", _ListViewGroup)
                ProcessPropertyValue(tagReader.Copyright, "Copyright", _ListViewGroup)

                ' Resize listview columns.
                StringMethods.ResizeListViewColumns(ListViewTagData, ColumnHeaderAutoResizeStyle.HeaderSize)

                ' End Update.
                .EndUpdate()
            End With

            ' Display any attached picture.
            PictureBoxImage.Image = tagReader.AttachedPicture

            ' Dispose of class.
            tagReader.Dispose()

            ' No Errors.
            Return ""
        Else
            StatusLabel.Text = "Error reading tag: " & returnMessage
            ' Dispose of class.
            tagReader.Dispose()
            Return returnMessage
        End If

    End Function

    Private Sub DisplayWmaTagData()

        Dim returnMessage As String = ""

        ' Clear all items.
        ListViewTagData.Items.Clear()

        ' Begin Update.
        ListViewTagData.BeginUpdate()

        ' Create listview group.
        _ListViewGroup = ListViewTagData.Groups.Add("WMA Tag Information", "WMA Tag Information")

        ' Add items and subitems - skip blank entries.
        ProcessPropertyValue(_WmaData.AlbumArtist, "Album Artist", _ListViewGroup)
        ProcessPropertyValue(_WmaData.AlbumTitle, "Album Title", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Author, "Author/Artist", _ListViewGroup)
        ProcessPropertyValue(_WmaData.BeatsPerMinute, "Beats per Minute", _ListViewGroup)
        ProcessPropertyValue(FormatBitsPerSecond(_WmaData.Bitrate), "Bitrate", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Category, "Category", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Composer, "Composer", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Conductor, "Conductor", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Copyright, "Copyright", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Description, "Description", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Director, "Director", _ListViewGroup)
        ProcessPropertyValue(FormatDuration(CInt(_WmaData.Duration / 10000000), True), "Duration", _ListViewGroup)
        ProcessPropertyValue(FormatBytes(CLng(_WmaData.FileSize)) & " (" & _
                             _WmaData.FileSize.ToString("#,#") & " bytes)", "Windows Media File Size", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Genre, "Genre", _ListViewGroup)
        ProcessPropertyValue(_WmaData.IsVBR.ToString(), "Is VBR (Variable Bit Rate)?", _ListViewGroup)
        ProcessPropertyValue(_WmaData.OriginalAlbumTitle, "Original Album Title", _ListViewGroup)
        ProcessPropertyValue(_WmaData.OriginalArtist, "Original Artist", _ListViewGroup)
        ProcessPropertyValue(_WmaData.OriginalFileName, "Original File Name", _ListViewGroup)
        ProcessPropertyValue(_WmaData.OriginalLyricist, "Original Lyricist", _ListViewGroup)
        ProcessPropertyValue(_WmaData.OriginalReleaseYear, "Original Release Year", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Producer, "Producer", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Publisher, "Publisher", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Rating, "Rating", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Title, "Title", _ListViewGroup)
        ProcessPropertyValue(_WmaData.TrackNumber, "Track Number", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Writer, "Writer", _ListViewGroup)
        ProcessPropertyValue(_WmaData.Year, "Year", _ListViewGroup)

        StringMethods.ResizeListViewColumns(ListViewTagData, ColumnHeaderAutoResizeStyle.HeaderSize)

        ' End Update.
        ListViewTagData.EndUpdate()

    End Sub

    Private Sub DisplayWmaFileProperties()

        With ListViewTagData

            ' Begin Update.
            .BeginUpdate()

            ' Create listview group.
            _ListViewGroup = .Groups.Add("File Properties", "File Properties")

            ' Add items and subitems.
            ProcessPropertyValue(_WmaData.FileFullName, "Full File Name", _ListViewGroup)
            ProcessPropertyValue(_WmaData.FileCreationTime.ToShortDateString & " " & _
                                 _WmaData.FileCreationTime.ToShortTimeString, _
                                 "File Creation Date/Time", _ListViewGroup)
            ProcessPropertyValue(_WmaData.FileLastAccessTime.ToShortDateString & " " & _
                                 _WmaData.FileLastAccessTime.ToShortTimeString, _
                                 "File Last Access Date/Time", _ListViewGroup)
            ProcessPropertyValue(_WmaData.FileLastWriteTime.ToShortDateString & " " & _
                                 _WmaData.FileLastWriteTime.ToShortTimeString, _
                                 "File Last Write Date/Time", _ListViewGroup)
            ProcessPropertyValue(CStr(_WmaData.FileIsReadOnly), "Is File Read-Only?", _ListViewGroup)
            ProcessPropertyValue(FormatBytes(_WmaData.FileLength) & " (" & _
                                 _WmaData.FileLength.ToString("#,#") & " bytes)", "Windows File Size", _ListViewGroup)

            ' Resize listview columns.
            StringMethods.ResizeListViewColumns(ListViewTagData, ColumnHeaderAutoResizeStyle.HeaderSize)

            ' End Update.
            .EndUpdate()

        End With

    End Sub

    Private Function DisplayMP3FileInfo() As String

        Dim returnMessage As String = ""

        ' Read the tag information.
        Dim tag As MP3FileInfoReader = New MP3FileInfoReader(_FileName)

        If String.IsNullOrEmpty(returnMessage) Then

            ' Begin Update.
            ListViewTagData.BeginUpdate()

            ' Create listview group.
            _ListViewGroup = ListViewTagData.Groups.Add("MP3 File Information", "MP3 File Information")

            ' Add items and subitems - skip blank entries.
            ProcessPropertyValue(FormatBitsPerSecond(tag.Bitrate), "Bitrate", _ListViewGroup)
            Dim value As String = ""
            ' Get Channel Mode and create string.
            Select Case tag.ChannelMode
                Case ChannelMode.DualChannel
                    value = "Dual Channel"
                Case ChannelMode.JointStereo
                    value = "Joint Stereo"
                Case ChannelMode.SingleChannel
                    value = "Single Channel"
                Case ChannelMode.Stereo
                    value = "Stereo"
            End Select
            ProcessPropertyValue(value, "Channel Mode", _ListViewGroup)
            ProcessPropertyValue(CStr(tag.Copyright), "Copyright", _ListViewGroup)
            ProcessPropertyValue(tag.Duration.ToString(), "Duration (seconds)", _ListViewGroup)
            ProcessPropertyValue(tag.DurationString, "Duration (formatted)", _ListViewGroup)
            ProcessPropertyValue(tag.FileCreationTime.ToShortDateString & " " & tag.FileCreationTime.ToShortTimeString, "File Creation Date/Time", _ListViewGroup)
            ProcessPropertyValue(tag.FileFullName, "Full File Name", _ListViewGroup)
            ProcessPropertyValue(tag.FileIsReadOnly.ToString(), "Is File Read-Only?", _ListViewGroup)
            ProcessPropertyValue(tag.FileLastAccessTime.ToShortDateString & " " & tag.FileLastAccessTime.ToShortTimeString, "File Last Access Date/Time", _ListViewGroup)
            ProcessPropertyValue(tag.FileLastWriteTime.ToShortDateString & " " & tag.FileLastWriteTime.ToShortTimeString, "File Last Write Date/Time", _ListViewGroup)
            ProcessPropertyValue(FormatBytes(tag.FileLength) & ", " & tag.FileLength.ToString("#.#") & " bytes", "File Size", _ListViewGroup)
            ProcessPropertyValue(tag.IsVBR.ToString(), "Is File VBR (Variable Bit Rate)?", _ListViewGroup)
            ' Get MPEG Layer.
            Select Case tag.Layer
                Case LayerType.LayerI
                    value = "MPEG Layer I"
                Case LayerType.LayerII
                    value = "MPEG Layer II"
                Case LayerType.LayerIII
                    value = "MPEG Layer III"
            End Select
            ProcessPropertyValue(value, "MPEG Layer", _ListViewGroup)
            ' Get MPEG Type.
            Select Case tag.MpegType
                Case MpegType.Mpeg1
                    value = "MPEG Type 1"
                Case MpegType.Mpeg2
                    value = "MPEG Type 2"
                Case MpegType.Mpeg25
                    value = "MPEG Type 2.5"
            End Select
            ProcessPropertyValue(value, "MPEG Type", _ListViewGroup)
            ' Get Protection Type.
            Select Case tag.Protection
                Case ProtectionType.NotProtected
                    value = "Not Protected"
                Case ProtectionType.ProtectedByCRC
                    value = "Protected by CRC (Cyclic Redundancy Check)"
            End Select
            ProcessPropertyValue(value, "Protection", _ListViewGroup)
            ProcessPropertyValue(FormatHertz(tag.SamplingFrequency), "Sampling Frequency", _ListViewGroup)

            StringMethods.ResizeListViewColumns(ListViewTagData, ColumnHeaderAutoResizeStyle.HeaderSize)

            ' End Update.
            ListViewTagData.EndUpdate()

            ' Dispose of class.
            tag.Dispose()

            ' No Errors.
            Return ""
        Else
            StatusLabel.Text = "Error reading tag: " & returnMessage
            Return returnMessage
        End If

    End Function

    Private Sub ProcessPropertyValue(ByVal propValue As String, ByVal description As String, ByVal group As ListViewGroup)

        If Not String.IsNullOrEmpty(propValue) Then
            _ListViewItem = New ListViewItem(group)
            _ListViewItem.Text = description
            _ListViewItem.SubItems.Add(propValue)
            ListViewTagData.Items.Add(_ListViewItem)
        End If

    End Sub

#End Region

#Region " Get NET Framework Information "

    ''' <summary>
    ''' Framework short version, ie: 2.0
    ''' </summary>
    ''' <returns> Returns a string with the version number, if there is an error an
    ''' empty string is returned. </returns>
    Private Shared Function GetFrameworkShortVersion() As String

        Return Environment.Version.ToString().Substring(0, 3)

    End Function

    ''' <summary>
    ''' A special section of the registry has to be querried to find out the service pack
    ''' of the .NET Framework. A different location was used for 1.0 and 1.1, but since this
    ''' application only runs on version 2.0, we won't worry about that.
    ''' </summary>
    ''' <returns> A string containing the version number, for example: "2.0" </returns>
    Private Shared Function GetFrameworkServicePack() As String

        Dim frameworkMajorVersion As String = Environment.Version.Major.ToString()
        Dim frameworkMinorVersion As String = Environment.Version.Minor.ToString()
        Dim frameworkVersion As String = "v" & frameworkMajorVersion & "." & frameworkMinorVersion _
                & "." & Environment.Version.Build.ToString()
        Dim rk As RegistryKey
        Dim temp As String

        Try
            '  try each registry key to determine the version, build, and service pack
            If frameworkMajorVersion = "2" And frameworkMinorVersion = "0" Then

                rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\NET Framework Setup\NDP\" _
                        & frameworkVersion, False)
                temp = rk.GetValue("SP").ToString()
                If temp <> "0" Then
                    Return temp
                Else
                    Return ""
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Win32Exception
            Return ""
        End Try

    End Function

    ''' <summary>
    ''' Queries the Registry and returns the highest .NET Framework version found or 0 if none is found.
    ''' </summary>
    ''' <returns>A double indicating the major and minor version of the .NET Framework.</returns>
    ''' <remarks>
    ''' This code is confirmed for versions 1.1, 2.0, 3.0 and 3.5. Later versions are simply educated guesses.
    ''' Hopefully beginning with version 3.5, Microsoft will assign the versioning task away from the
    ''' marketing department and this mess will not be necessary.
    ''' </remarks>
    Private Shared Function ReturnHighestFrameworkVersion() As String

        ' All values are "Install" except for V3.0. If version is installed, the DWORD = 1.
        Const netV11 As String = "Software\Microsoft\NET Framework Setup\NDP\v1.1.4322"
        Const netV20 As String = "Software\Microsoft\NET Framework Setup\NDP\v2.0.50727"
        Const netV30 As String = "Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup" ' Value is "InstallSuccess"
        ' Alternate versions for .NET Framework on x64.
        Const netV30a As String = "Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup\Windows Communication Foundation" ' Value is "InstallSuccess"
        Const netV30b As String = "Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup\Windows Workflow Foundation" ' Value is "InstallSuccess"
        Const netV35 As String = "Software\Microsoft\NET Framework Setup\NDP\v3.5"
        Const netV40 As String = "Software\Microsoft\NET Framework Setup\NDP\v4.0"  ' Guess. Not Verified.
        Const netV45 As String = "Software\Microsoft\NET Framework Setup\NDP\v4.5"  ' Guess. Not Verified.
        Const netV50 As String = "Software\Microsoft\NET Framework Setup\NDP\v5.0"  ' Guess. Not Verified.

        Dim version As Double = 0
        Dim rk As RegistryKey = Nothing

        ' Surround whole set with Try-Catch to catch generic and security errors.
        Try
            ' Check if version 5.0 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV50)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 5
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 4.5 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV45)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 4.5
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 4.0 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV40)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 4
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 3.5 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV35)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 3.5
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 3.0 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV30)
                    If rk.GetValue("InstallSuccess").ToString = "1" Then
                        version = 3
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 3.0a is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV30a)
                    If rk.GetValue("InstallSuccess").ToString = "1" Then
                        version = 3
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 3.0b is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV30b)
                    If rk.GetValue("InstallSuccess").ToString = "1" Then
                        version = 3
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 2.0 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV20)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 2
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

            ' Check if version 1.1 is installed. Don't check if version is already found.
            If version = 0 Then
                Try
                    rk = Registry.LocalMachine.OpenSubKey(netV11)
                    If rk.GetValue("Install").ToString = "1" Then
                        version = 1.1
                    End If
                Catch ex As ArgumentException
                    version = 0
                Catch ex As NullReferenceException
                    version = 0
                End Try
            End If

        Catch ex As SecurityException
            MainForm.StatusLabel.Text = "An error occurred reading the system registry."
        Finally
            If rk IsNot Nothing Then
                rk.Close()
            End If
        End Try

        Return version.ToString("0.0")

    End Function

#End Region

#Region " Formatting Subroutines "

    ''' <summary>
    ''' Returns a formatted string with the correct metric suffix given number of bytes.
    ''' </summary>
    Public Shared Function FormatBytes(ByVal bytes As Long) As String
        Dim temp As Double

        If bytes >= 1073741824 Then
            temp = bytes / 1073741824               ' GB
            Return String.Format("{0:N2}", temp) & " GB"
        ElseIf bytes >= 1048576 And bytes <= 1073741823 Then
            temp = bytes / 1048576                  'MB
            Return String.Format("{0:N2}", temp) & " MB"
        ElseIf bytes >= 1024 And bytes <= 1048575 Then
            temp = bytes / 1024                     ' KB
            Return String.Format("{0:N2}", temp) & " KB"
        ElseIf bytes >= 0 And bytes <= 1023 Then
            '                                       ' bytes
            Return String.Format("{0:N0}", bytes) & " bytes"
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Returns a formatted string with the correct metric suffix given frequency in hertz.
    ''' </summary>
    Public Shared Function FormatHertz(ByVal hertz As Double) As String
        Dim temp As Double

        If hertz >= 1000000000 Then 'GHz
            temp = hertz / 1000000000
            Return String.Format("{0:N1}", temp) & " GHz"
        ElseIf hertz >= 1048576 And hertz <= 1073741823 Then
            temp = hertz / 1000000 'MHz
            Return String.Format("{0:N1}", temp) & " MHz"
        ElseIf hertz >= 1024 And hertz <= 1048575 Then
            temp = hertz / 1000 'KHz
            Return String.Format("{0:N1}", temp) & " KHz"
        ElseIf hertz >= 0 And hertz <= 1023 Then
            temp = hertz ' Hz
            Return String.Format("{0:N0}", hertz) & " Hz"
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Returns a formatted string with the correct metric suffix given bit rate in bits/sec.
    ''' </summary>
    Public Shared Function FormatBitsPerSecond(ByVal bps As Long) As String
        Dim temp As Double

        If bps >= 1000000000 Then 'Gbps
            temp = bps / 1000000000
            Return String.Format("{0:N0}", temp) & " Gbps"
        ElseIf bps >= 1048576 And bps <= 1073741823 Then
            temp = bps / 1000000 'Mbps
            Return String.Format("{0:N0}", temp) & " Mbps"
        ElseIf bps >= 1024 And bps <= 1048575 Then
            temp = bps / 1000 'Kbps
            Return String.Format("{0:N0}", temp) & " Kbps"
        ElseIf bps >= 0 And bps <= 1023 Then
            temp = bps ' bps
            Return String.Format("{0:N0}", bps) & " bps"
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Returns a formatted string in the form "00:00:00" or "00:00" given the duration in seconds.
    ''' </summary>
    Public Shared Function FormatDuration(ByVal inSeconds As Integer, ByVal shortTime As Boolean) As String

        Try

            Dim seconds As Integer
            Dim minutes As Integer
            Dim hours As Integer
            Dim displayHours As String = ""
            Dim displayMinutes As String = ""
            Dim displaySeconds As String = ""

            seconds = inSeconds Mod 60
            minutes = CInt(Int(inSeconds / 60) Mod 60)
            hours = CInt(Int(inSeconds / 3600) Mod 24)

            '  format hours
            If hours = 0 Then
                displayHours = "00"
            ElseIf hours <= 9 Then
                displayHours = "0" & hours.ToString.Trim
            Else
                displayHours = hours.ToString.Trim
            End If

            '  format minutes
            If minutes = 0 Then
                displayMinutes = "00"
            ElseIf minutes <= 9 Then
                displayMinutes = "0" & minutes.ToString.Trim
            Else
                displayMinutes = minutes.ToString.Trim
            End If

            '  format seconds
            If seconds = 0 Then
                displaySeconds = "00"
            ElseIf seconds <= 9 Then
                displaySeconds = "0" & seconds.ToString.Trim
            Else
                displaySeconds = seconds.ToString.Trim
            End If

            '  return formatted duration.
            If shortTime Then
                Return displayMinutes & ":" & displaySeconds
            Else
                Return displayHours & ":" & displayMinutes & ":" & displaySeconds
            End If

        Catch ex As OverflowException
            Return ""
        End Try

    End Function

#End Region

End Class
