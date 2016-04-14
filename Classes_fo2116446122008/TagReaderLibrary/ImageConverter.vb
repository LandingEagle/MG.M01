Imports System
Imports System.Drawing
Imports System.IO
Imports System.Collections

''' <summary>
''' Description of ImageConverter.
''' </summary>
Public Class ImageConverter

    Public Sub New()
    End Sub

    Public Function ImageToByteArray(ByVal imageIn As Image) As Byte()

        Dim ms As MemoryStream = New MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()

    End Function

    Public Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image

        Dim ms As MemoryStream = New MemoryStream(byteArrayIn)
        Dim returnImage As Image = Image.FromStream(ms)
        Return returnImage

    End Function

End Class

