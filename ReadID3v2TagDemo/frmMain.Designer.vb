<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.lvTracks = New System.Windows.Forms.ListView
		Me.cmdCommand = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'lvTracks
		'
		Me.lvTracks.Location = New System.Drawing.Point(10, 50)
		Me.lvTracks.Name = "lvTracks"
		Me.lvTracks.Size = New System.Drawing.Size(515, 310)
		Me.lvTracks.TabIndex = 0
		Me.lvTracks.UseCompatibleStateImageBehavior = False
		'
		'cmdCommand
		'
		Me.cmdCommand.Location = New System.Drawing.Point(15, 15)
		Me.cmdCommand.Name = "cmdCommand"
		Me.cmdCommand.Size = New System.Drawing.Size(75, 23)
		Me.cmdCommand.TabIndex = 1
		Me.cmdCommand.Text = "Search Disk"
		Me.cmdCommand.UseVisualStyleBackColor = True
		'
		'frmMain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(534, 367)
		Me.Controls.Add(Me.cmdCommand)
		Me.Controls.Add(Me.lvTracks)
		Me.Name = "frmMain"
		Me.Text = "Form1"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lvTracks As System.Windows.Forms.ListView
	Friend WithEvents cmdCommand As System.Windows.Forms.Button

End Class
