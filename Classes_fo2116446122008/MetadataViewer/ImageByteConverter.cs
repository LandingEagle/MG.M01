// Learn how to convert images to byte arrays and byte arrays back into images. 

private static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, 
ImageFormat formatOfImage) 
{ 
byte[] Ret; 

try 
{ 

using (MemoryStream ms = new MemoryStream()) 
{ 
imageToConvert.Save(ms,formatOfImage); 
Ret = ms.ToArray(); 
} 
} 
catch (Exception) { throw;} 

return Ret; 
} 


When you are ready to convert the byte array back 
to an image, you can include the following code 
in your method. 

System.Drawing.Image newImage; 


using (MemoryStream ms = new MemoryStream(myByteArray,0,myByteArray.Length)) 
{ 

ms.Write(myByteArray,0,myByteArray.Length); 

newImage = Image.FromStream(ms,true); 

// work with image here. 

// You'll need to keep the MemoryStream open for 
// as long as you want to work with your new image. 

} 
