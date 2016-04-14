Imports System 
Imports System.Runtime.InteropServices

Friend NotInheritable Class NativeMethods

#Region " Private Constructor "

    ''' <summary>
    ''' Just for the compiler.
    ''' </summary>
    Private Sub New()
    End Sub

#End Region

    <DllImport("WMVCore.dll", EntryPoint:="WMCreateEditor", SetLastError:=True, _
               CharSet:=CharSet.Unicode, ExactSpelling:=True, _
               CallingConvention:=CallingConvention.StdCall)> _
               Friend Shared Function WMCreateEditor(<Out(), MarshalAs(UnmanagedType.[Interface])> _
                                                     ByRef ppMetadataEditor As IWMMetadataEditor) As UInteger
    End Function

End Class
