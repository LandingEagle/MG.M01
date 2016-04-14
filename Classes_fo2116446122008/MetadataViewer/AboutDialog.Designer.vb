<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AboutDialog
    Inherits System.Windows.Forms.Form

    ' Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

#Region " Windows Form Designer generated code "

    ' NOTE: The following procedure is required by the Windows Form Designer
    ' It can be modified using the Windows Form Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutDialog))
        Me.ButtonOK = New System.Windows.Forms.Button
        Me.TimerTickCount = New System.Windows.Forms.Timer(Me.components)
        Me.LabelTopSeparator = New System.Windows.Forms.Label
        Me.LabelBottomSeperator = New System.Windows.Forms.Label
        Me.ButtonSysInfo = New System.Windows.Forms.Button
        Me.StatusStripAbout = New System.Windows.Forms.StatusStrip
        Me.ToolStripLabelClock = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripLabelDateTime = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripLabelUpTime = New System.Windows.Forms.ToolStripStatusLabel
        Me.PanelHeader = New System.Windows.Forms.Panel
        Me.LabelTitle = New System.Windows.Forms.Label
        Me.PictureApplication = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.LabelVersion = New System.Windows.Forms.Label
        Me.LabelDescription = New System.Windows.Forms.Label
        Me.LabelCopyright = New System.Windows.Forms.Label
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.LabelOrganization = New System.Windows.Forms.Label
        Me.LabelUser = New System.Windows.Forms.Label
        Me.LinkLabelEULA = New System.Windows.Forms.LinkLabel
        Me.LabelLicense = New System.Windows.Forms.Label
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.LabelSupportPhone = New System.Windows.Forms.Label
        Me.LinkLabelWebsite = New System.Windows.Forms.LinkLabel
        Me.LinkLabelEmail = New System.Windows.Forms.LinkLabel
        Me.LabelSupportAvailable = New System.Windows.Forms.Label
        Me.LabelCompany = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.LabelWindowsVersion = New System.Windows.Forms.Label
        Me.LabelOSDescription = New System.Windows.Forms.Label
        Me.LabelFramework = New System.Windows.Forms.Label
        Me.LabelClrVersion = New System.Windows.Forms.Label
        Me.StatusStripAbout.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureApplication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonOK.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonOK.Location = New System.Drawing.Point(277, 453)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(73, 25)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "&OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'TimerTickCount
        '
        Me.TimerTickCount.Interval = 1000
        '
        'LabelTopSeparator
        '
        Me.LabelTopSeparator.BackColor = System.Drawing.SystemColors.Control
        Me.LabelTopSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTopSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.LabelTopSeparator.Location = New System.Drawing.Point(0, 60)
        Me.LabelTopSeparator.Name = "LabelTopSeparator"
        Me.LabelTopSeparator.Size = New System.Drawing.Size(432, 3)
        Me.LabelTopSeparator.TabIndex = 20
        '
        'LabelBottomSeperator
        '
        Me.LabelBottomSeperator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelBottomSeperator.BackColor = System.Drawing.SystemColors.Control
        Me.LabelBottomSeperator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelBottomSeperator.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.LabelBottomSeperator.Location = New System.Drawing.Point(11, 438)
        Me.LabelBottomSeperator.Name = "LabelBottomSeperator"
        Me.LabelBottomSeperator.Size = New System.Drawing.Size(411, 3)
        Me.LabelBottomSeperator.TabIndex = 11
        '
        'ButtonSysInfo
        '
        Me.ButtonSysInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSysInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSysInfo.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonSysInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSysInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSysInfo.Location = New System.Drawing.Point(83, 453)
        Me.ButtonSysInfo.Name = "ButtonSysInfo"
        Me.ButtonSysInfo.Size = New System.Drawing.Size(127, 25)
        Me.ButtonSysInfo.TabIndex = 1
        Me.ButtonSysInfo.Text = "&System Infomation"
        Me.ButtonSysInfo.UseVisualStyleBackColor = True
        '
        'StatusStripAbout
        '
        Me.StatusStripAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStripAbout.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStripAbout.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelClock, Me.ToolStripLabelDateTime, Me.ToolStripLabelUpTime})
        Me.StatusStripAbout.Location = New System.Drawing.Point(0, 491)
        Me.StatusStripAbout.Name = "StatusStripAbout"
        Me.StatusStripAbout.Size = New System.Drawing.Size(432, 22)
        Me.StatusStripAbout.SizingGrip = False
        Me.StatusStripAbout.TabIndex = 12
        '
        'ToolStripLabelClock
        '
        Me.ToolStripLabelClock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabelClock.Name = "ToolStripLabelClock"
        Me.ToolStripLabelClock.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripLabelDateTime
        '
        Me.ToolStripLabelDateTime.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripLabelDateTime.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabelDateTime.Name = "ToolStripLabelDateTime"
        Me.ToolStripLabelDateTime.Size = New System.Drawing.Size(233, 17)
        Me.ToolStripLabelDateTime.Text = "Wednesday, December 07, 2005 8:08:03 PM"
        '
        'ToolStripLabelUpTime
        '
        Me.ToolStripLabelUpTime.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripLabelUpTime.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabelUpTime.Name = "ToolStripLabelUpTime"
        Me.ToolStripLabelUpTime.Size = New System.Drawing.Size(109, 17)
        Me.ToolStripLabelUpTime.Text = "Up Time: 1:12:45:36"
        '
        'PanelHeader
        '
        Me.PanelHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PanelHeader.Controls.Add(Me.LabelTitle)
        Me.PanelHeader.Controls.Add(Me.PictureApplication)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(432, 64)
        Me.PanelHeader.TabIndex = 21
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelTitle.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelTitle.Location = New System.Drawing.Point(76, 21)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(166, 23)
        Me.LabelTitle.TabIndex = 0
        Me.LabelTitle.Text = "Application Title"
        '
        'PictureApplication
        '
        Me.PictureApplication.BackColor = System.Drawing.Color.Transparent
        Me.PictureApplication.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureApplication.Location = New System.Drawing.Point(6, 8)
        Me.PictureApplication.Name = "PictureApplication"
        Me.PictureApplication.Size = New System.Drawing.Size(48, 48)
        Me.PictureApplication.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureApplication.TabIndex = 23
        Me.PictureApplication.TabStop = False
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.AutoSize = True
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LabelVersion, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelDescription, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelCopyright, 0, 2)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(11, 75)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(410, 64)
        Me.TableLayoutPanel4.TabIndex = 29
        '
        'LabelVersion
        '
        Me.LabelVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabelVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelVersion.Location = New System.Drawing.Point(2, 3)
        Me.LabelVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(406, 15)
        Me.LabelVersion.TabIndex = 19
        Me.LabelVersion.Text = "Application Version: "
        Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelDescription
        '
        Me.LabelDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDescription.AutoSize = True
        Me.LabelDescription.BackColor = System.Drawing.Color.Transparent
        Me.LabelDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelDescription.Location = New System.Drawing.Point(2, 24)
        Me.LabelDescription.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelDescription.Name = "LabelDescription"
        Me.LabelDescription.Size = New System.Drawing.Size(406, 15)
        Me.LabelDescription.TabIndex = 18
        Me.LabelDescription.Text = "Application Description: "
        Me.LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCopyright
        '
        Me.LabelCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCopyright.AutoSize = True
        Me.LabelCopyright.BackColor = System.Drawing.Color.Transparent
        Me.LabelCopyright.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelCopyright.Location = New System.Drawing.Point(2, 45)
        Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New System.Drawing.Size(406, 15)
        Me.LabelCopyright.TabIndex = 17
        Me.LabelCopyright.Text = "Application Copyright: "
        Me.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.AutoSize = True
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelOrganization, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelUser, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.LinkLabelEULA, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelLicense, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(11, 144)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(410, 85)
        Me.TableLayoutPanel3.TabIndex = 28
        '
        'LabelOrganization
        '
        Me.LabelOrganization.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelOrganization.AutoSize = True
        Me.LabelOrganization.BackColor = System.Drawing.Color.Transparent
        Me.LabelOrganization.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelOrganization.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelOrganization.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelOrganization.Location = New System.Drawing.Point(2, 66)
        Me.LabelOrganization.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelOrganization.Name = "LabelOrganization"
        Me.LabelOrganization.Size = New System.Drawing.Size(406, 15)
        Me.LabelOrganization.TabIndex = 14
        Me.LabelOrganization.Text = "Virtual Software"
        Me.LabelOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelUser
        '
        Me.LabelUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelUser.AutoSize = True
        Me.LabelUser.BackColor = System.Drawing.Color.Transparent
        Me.LabelUser.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelUser.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelUser.Location = New System.Drawing.Point(2, 45)
        Me.LabelUser.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(406, 15)
        Me.LabelUser.TabIndex = 15
        Me.LabelUser.Text = "Herbert N Swearengen III"
        Me.LabelUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LinkLabelEULA
        '
        Me.LinkLabelEULA.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabelEULA.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabelEULA.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabelEULA.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabelEULA.LinkColor = System.Drawing.Color.MediumBlue
        Me.LinkLabelEULA.Location = New System.Drawing.Point(2, 23)
        Me.LinkLabelEULA.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LinkLabelEULA.Name = "LinkLabelEULA"
        Me.LinkLabelEULA.Size = New System.Drawing.Size(406, 16)
        Me.LinkLabelEULA.TabIndex = 3
        Me.LinkLabelEULA.TabStop = True
        Me.LinkLabelEULA.Text = "End User License Agreement (EULA) to:"
        Me.LinkLabelEULA.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.LinkLabelEULA.UseCompatibleTextRendering = True
        '
        'LabelLicense
        '
        Me.LabelLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelLicense.AutoSize = True
        Me.LabelLicense.BackColor = System.Drawing.Color.Transparent
        Me.LabelLicense.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelLicense.Location = New System.Drawing.Point(2, 3)
        Me.LabelLicense.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelLicense.Name = "LabelLicense"
        Me.LabelLicense.Size = New System.Drawing.Size(406, 15)
        Me.LabelLicense.TabIndex = 16
        Me.LabelLicense.Text = "This product is licensed under the terms of the"
        Me.LabelLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.LabelSupportPhone, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.LinkLabelWebsite, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.LinkLabelEmail, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelSupportAvailable, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelCompany, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(11, 234)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(410, 105)
        Me.TableLayoutPanel2.TabIndex = 27
        '
        'LabelSupportPhone
        '
        Me.LabelSupportPhone.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSupportPhone.BackColor = System.Drawing.Color.Transparent
        Me.LabelSupportPhone.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSupportPhone.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelSupportPhone.Location = New System.Drawing.Point(2, 87)
        Me.LabelSupportPhone.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSupportPhone.Name = "LabelSupportPhone"
        Me.LabelSupportPhone.Size = New System.Drawing.Size(406, 15)
        Me.LabelSupportPhone.TabIndex = 21
        Me.LabelSupportPhone.Text = "Phone:"
        Me.LabelSupportPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LinkLabelWebsite
        '
        Me.LinkLabelWebsite.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabelWebsite.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabelWebsite.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabelWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabelWebsite.LinkColor = System.Drawing.Color.MediumBlue
        Me.LinkLabelWebsite.Location = New System.Drawing.Point(2, 64)
        Me.LinkLabelWebsite.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LinkLabelWebsite.Name = "LinkLabelWebsite"
        Me.LinkLabelWebsite.Size = New System.Drawing.Size(406, 18)
        Me.LinkLabelWebsite.TabIndex = 5
        Me.LinkLabelWebsite.TabStop = True
        Me.LinkLabelWebsite.Text = "Website:"
        Me.LinkLabelWebsite.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.LinkLabelWebsite.UseCompatibleTextRendering = True
        '
        'LinkLabelEmail
        '
        Me.LinkLabelEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabelEmail.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabelEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabelEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabelEmail.LinkColor = System.Drawing.Color.MediumBlue
        Me.LinkLabelEmail.Location = New System.Drawing.Point(2, 43)
        Me.LinkLabelEmail.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LinkLabelEmail.Name = "LinkLabelEmail"
        Me.LinkLabelEmail.Size = New System.Drawing.Size(406, 18)
        Me.LinkLabelEmail.TabIndex = 4
        Me.LinkLabelEmail.TabStop = True
        Me.LinkLabelEmail.Text = "Email:"
        Me.LinkLabelEmail.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.LinkLabelEmail.UseCompatibleTextRendering = True
        '
        'LabelSupportAvailable
        '
        Me.LabelSupportAvailable.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSupportAvailable.AutoSize = True
        Me.LabelSupportAvailable.BackColor = System.Drawing.Color.Transparent
        Me.LabelSupportAvailable.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSupportAvailable.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelSupportAvailable.Location = New System.Drawing.Point(2, 24)
        Me.LabelSupportAvailable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelSupportAvailable.Name = "LabelSupportAvailable"
        Me.LabelSupportAvailable.Size = New System.Drawing.Size(406, 15)
        Me.LabelSupportAvailable.TabIndex = 20
        Me.LabelSupportAvailable.Text = "Support is available:"
        Me.LabelSupportAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCompany
        '
        Me.LabelCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCompany.AutoSize = True
        Me.LabelCompany.BackColor = System.Drawing.Color.Transparent
        Me.LabelCompany.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelCompany.Location = New System.Drawing.Point(2, 3)
        Me.LabelCompany.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelCompany.Name = "LabelCompany"
        Me.LabelCompany.Size = New System.Drawing.Size(406, 15)
        Me.LabelCompany.TabIndex = 13
        Me.LabelCompany.Text = "Application Title is a product of Company Name"
        Me.LabelCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelWindowsVersion, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelOSDescription, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelFramework, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelClrVersion, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(11, 344)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(410, 85)
        Me.TableLayoutPanel1.TabIndex = 26
        '
        'LabelWindowsVersion
        '
        Me.LabelWindowsVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelWindowsVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabelWindowsVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWindowsVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelWindowsVersion.Location = New System.Drawing.Point(2, 2)
        Me.LabelWindowsVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelWindowsVersion.Name = "LabelWindowsVersion"
        Me.LabelWindowsVersion.Size = New System.Drawing.Size(406, 16)
        Me.LabelWindowsVersion.TabIndex = 7
        Me.LabelWindowsVersion.Text = "Windows Version: "
        Me.LabelWindowsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelOSDescription
        '
        Me.LabelOSDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelOSDescription.BackColor = System.Drawing.Color.Transparent
        Me.LabelOSDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelOSDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelOSDescription.Location = New System.Drawing.Point(2, 23)
        Me.LabelOSDescription.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelOSDescription.Name = "LabelOSDescription"
        Me.LabelOSDescription.Size = New System.Drawing.Size(406, 16)
        Me.LabelOSDescription.TabIndex = 8
        Me.LabelOSDescription.Text = "Operating System: "
        Me.LabelOSDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelFramework
        '
        Me.LabelFramework.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFramework.BackColor = System.Drawing.Color.Transparent
        Me.LabelFramework.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelFramework.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelFramework.Location = New System.Drawing.Point(2, 44)
        Me.LabelFramework.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelFramework.Name = "LabelFramework"
        Me.LabelFramework.Size = New System.Drawing.Size(406, 16)
        Me.LabelFramework.TabIndex = 9
        Me.LabelFramework.Text = "NET Framework:"
        Me.LabelFramework.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelClrVersion
        '
        Me.LabelClrVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelClrVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabelClrVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelClrVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelClrVersion.Location = New System.Drawing.Point(2, 66)
        Me.LabelClrVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelClrVersion.Name = "LabelClrVersion"
        Me.LabelClrVersion.Size = New System.Drawing.Size(406, 16)
        Me.LabelClrVersion.TabIndex = 10
        Me.LabelClrVersion.Text = "Common Language Runtime Version:"
        Me.LabelClrVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AboutDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CancelButton = Me.ButtonOK
        Me.ClientSize = New System.Drawing.Size(432, 513)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.StatusStripAbout)
        Me.Controls.Add(Me.ButtonSysInfo)
        Me.Controls.Add(Me.LabelBottomSeperator)
        Me.Controls.Add(Me.LabelTopSeparator)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About ..."
        Me.StatusStripAbout.ResumeLayout(False)
        Me.StatusStripAbout.PerformLayout()
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureApplication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents TimerTickCount As System.Windows.Forms.Timer
    Friend WithEvents LabelTopSeparator As System.Windows.Forms.Label
    Friend WithEvents LabelBottomSeperator As System.Windows.Forms.Label
    Friend WithEvents ButtonSysInfo As System.Windows.Forms.Button
    Friend WithEvents StatusStripAbout As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripLabelClock As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLabelDateTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLabelUpTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PanelHeader As System.Windows.Forms.Panel
    Friend WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents PictureApplication As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents LabelDescription As System.Windows.Forms.Label
    Friend WithEvents LabelCopyright As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelOrganization As System.Windows.Forms.Label
    Friend WithEvents LabelUser As System.Windows.Forms.Label
    Friend WithEvents LinkLabelEULA As System.Windows.Forms.LinkLabel
    Friend WithEvents LabelLicense As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelSupportPhone As System.Windows.Forms.Label
    Friend WithEvents LinkLabelWebsite As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabelEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents LabelSupportAvailable As System.Windows.Forms.Label
    Friend WithEvents LabelCompany As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelWindowsVersion As System.Windows.Forms.Label
    Friend WithEvents LabelOSDescription As System.Windows.Forms.Label
    Friend WithEvents LabelFramework As System.Windows.Forms.Label
    Friend WithEvents LabelClrVersion As System.Windows.Forms.Label

End Class