<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ButtonBrowse = New System.Windows.Forms.Button
        Me.TableLayoutPanelFile = New System.Windows.Forms.TableLayoutPanel
        Me.PictureBoxFormat = New System.Windows.Forms.PictureBox
        Me.TextBoxFileName = New System.Windows.Forms.TextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.MenuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.MenuExit = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.LabelFileName = New System.Windows.Forms.Label
        Me.TableLayoutPanelListView = New System.Windows.Forms.TableLayoutPanel
        Me.ListViewTagData = New System.Windows.Forms.ListView
        Me.ColumnHeaderCategory = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeaderInformation = New System.Windows.Forms.ColumnHeader
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.PictureBoxImage = New System.Windows.Forms.PictureBox
        Me.LabelEmbeddedPicture = New System.Windows.Forms.Label
        Me.TableLayoutPanelFile.SuspendLayout()
        CType(Me.PictureBoxFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanelListView.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Title = "Open Audio File"
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ButtonBrowse.Location = New System.Drawing.Point(695, 7)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(60, 23)
        Me.ButtonBrowse.TabIndex = 0
        Me.ButtonBrowse.Text = "&Browse"
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'TableLayoutPanelFile
        '
        Me.TableLayoutPanelFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanelFile.ColumnCount = 3
        Me.TableLayoutPanelFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanelFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanelFile.Controls.Add(Me.ButtonBrowse, 2, 0)
        Me.TableLayoutPanelFile.Controls.Add(Me.PictureBoxFormat, 0, 0)
        Me.TableLayoutPanelFile.Controls.Add(Me.TextBoxFileName, 1, 0)
        Me.TableLayoutPanelFile.Location = New System.Drawing.Point(12, 47)
        Me.TableLayoutPanelFile.Name = "TableLayoutPanelFile"
        Me.TableLayoutPanelFile.RowCount = 1
        Me.TableLayoutPanelFile.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelFile.Size = New System.Drawing.Size(760, 38)
        Me.TableLayoutPanelFile.TabIndex = 2
        '
        'PictureBoxFormat
        '
        Me.PictureBoxFormat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBoxFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxFormat.Location = New System.Drawing.Point(3, 3)
        Me.PictureBoxFormat.Name = "PictureBoxFormat"
        Me.PictureBoxFormat.Size = New System.Drawing.Size(32, 32)
        Me.PictureBoxFormat.TabIndex = 5
        Me.PictureBoxFormat.TabStop = False
        '
        'TextBoxFileName
        '
        Me.TextBoxFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFileName.Location = New System.Drawing.Point(41, 7)
        Me.TextBoxFileName.Name = "TextBoxFileName"
        Me.TextBoxFileName.Size = New System.Drawing.Size(646, 23)
        Me.TextBoxFileName.TabIndex = 4
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuFile, Me.MenuHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuFile
        '
        Me.MenuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuOpen, Me.ToolStripSeparator1, Me.MenuExit})
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(37, 20)
        Me.MenuFile.Text = "&File"
        '
        'MenuOpen
        '
        Me.MenuOpen.Name = "MenuOpen"
        Me.MenuOpen.Size = New System.Drawing.Size(103, 22)
        Me.MenuOpen.Text = "&Open"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(100, 6)
        '
        'MenuExit
        '
        Me.MenuExit.Name = "MenuExit"
        Me.MenuExit.Size = New System.Drawing.Size(103, 22)
        Me.MenuExit.Text = "E&xit"
        '
        'MenuHelp
        '
        Me.MenuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuAbout})
        Me.MenuHelp.Name = "MenuHelp"
        Me.MenuHelp.Size = New System.Drawing.Size(44, 20)
        Me.MenuHelp.Text = "&Help"
        '
        'MenuAbout
        '
        Me.MenuAbout.Name = "MenuAbout"
        Me.MenuAbout.Size = New System.Drawing.Size(107, 22)
        Me.MenuAbout.Text = "&About"
        '
        'LabelFileName
        '
        Me.LabelFileName.AutoSize = True
        Me.LabelFileName.Location = New System.Drawing.Point(12, 29)
        Me.LabelFileName.Name = "LabelFileName"
        Me.LabelFileName.Size = New System.Drawing.Size(60, 15)
        Me.LabelFileName.TabIndex = 4
        Me.LabelFileName.Text = "File Name"
        '
        'TableLayoutPanelListView
        '
        Me.TableLayoutPanelListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanelListView.ColumnCount = 1
        Me.TableLayoutPanelListView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelListView.Controls.Add(Me.ListViewTagData, 0, 0)
        Me.TableLayoutPanelListView.Location = New System.Drawing.Point(12, 101)
        Me.TableLayoutPanelListView.Name = "TableLayoutPanelListView"
        Me.TableLayoutPanelListView.RowCount = 1
        Me.TableLayoutPanelListView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelListView.Size = New System.Drawing.Size(494, 339)
        Me.TableLayoutPanelListView.TabIndex = 5
        '
        'ListViewTagData
        '
        Me.ListViewTagData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderCategory, Me.ColumnHeaderInformation})
        Me.ListViewTagData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewTagData.Location = New System.Drawing.Point(3, 3)
        Me.ListViewTagData.Name = "ListViewTagData"
        Me.ListViewTagData.Size = New System.Drawing.Size(488, 333)
        Me.ListViewTagData.TabIndex = 0
        Me.ListViewTagData.UseCompatibleStateImageBehavior = False
        Me.ListViewTagData.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderCategory
        '
        Me.ColumnHeaderCategory.Text = "Category"
        Me.ColumnHeaderCategory.Width = 120
        '
        'ColumnHeaderInformation
        '
        Me.ColumnHeaderInformation.Text = "Information"
        Me.ColumnHeaderInformation.Width = 344
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 454)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(769, 17)
        Me.StatusLabel.Spring = True
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBoxImage
        '
        Me.PictureBoxImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxImage.BackColor = System.Drawing.Color.White
        Me.PictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxImage.Location = New System.Drawing.Point(517, 145)
        Me.PictureBoxImage.Name = "PictureBoxImage"
        Me.PictureBoxImage.Size = New System.Drawing.Size(250, 250)
        Me.PictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxImage.TabIndex = 7
        Me.PictureBoxImage.TabStop = False
        '
        'LabelEmbeddedPicture
        '
        Me.LabelEmbeddedPicture.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelEmbeddedPicture.AutoSize = True
        Me.LabelEmbeddedPicture.Location = New System.Drawing.Point(514, 127)
        Me.LabelEmbeddedPicture.Name = "LabelEmbeddedPicture"
        Me.LabelEmbeddedPicture.Size = New System.Drawing.Size(104, 15)
        Me.LabelEmbeddedPicture.TabIndex = 8
        Me.LabelEmbeddedPicture.Text = "Embedded Picture"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(784, 476)
        Me.Controls.Add(Me.LabelEmbeddedPicture)
        Me.Controls.Add(Me.PictureBoxImage)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TableLayoutPanelListView)
        Me.Controls.Add(Me.LabelFileName)
        Me.Controls.Add(Me.TableLayoutPanelFile)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MP3/WMA Tag Reader"
        Me.TableLayoutPanelFile.ResumeLayout(False)
        Me.TableLayoutPanelFile.PerformLayout()
        CType(Me.PictureBoxFormat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanelListView.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBoxImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ButtonBrowse As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanelFile As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBoxFileName As System.Windows.Forms.TextBox
    Friend WithEvents LabelFileName As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanelListView As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ListViewTagData As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeaderCategory As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderInformation As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBoxFormat As System.Windows.Forms.PictureBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PictureBoxImage As System.Windows.Forms.PictureBox
    Friend WithEvents LabelEmbeddedPicture As System.Windows.Forms.Label

End Class
