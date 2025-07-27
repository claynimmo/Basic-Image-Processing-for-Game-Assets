
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public class ImageLoader
    {
        private Bitmap imageMap;
        private byte[] pixels;

        private int imageStride;
        
        public byte[] Pixels{
            get{return pixels;}
        }

        public int ImageWidth{
            get{return imageMap.Width;}
        }

        public int ImageHeight{
            get{return imageMap.Height;}
        }
        
        public int ImageStride{
            get{return imageStride;}
        }

        public Bitmap ImageMap{
            get{return imageMap;}
        }

        public ImageLoader(string filePath){
            try{
                imageMap = new Bitmap(filePath);

                Rectangle rect = new Rectangle(0, 0, imageMap.Width, imageMap.Height);

                BitmapData imageData = imageMap.LockBits(rect, ImageLockMode.ReadWrite, imageMap.PixelFormat);

                IntPtr pixelPointer = imageData.Scan0;
                int bytes = Math.Abs(imageData.Stride) * imageMap.Height;
                pixels = new byte[bytes];

                imageStride = imageData.Stride;

                System.Runtime.InteropServices.Marshal.Copy(pixelPointer, pixels, 0, bytes);

                imageMap.UnlockBits(imageData);
            }
            catch{
                throw new Exception("Image failed to load");
            }
        }
        
        /// <summary>
        /// Function to modify the dimensions of the image
        /// </summary>
        /// <param name="newWidth">The new width of the image</param>
        /// <param name="newHeight">The new height of the image</param>
        public void ResizeImage(int newWidth, int newHeight){
            int bytesPerPixel = 4;
            

            //create the new map of the resized data
            Bitmap newMap = new Bitmap(newWidth,newHeight,imageMap.PixelFormat);
            BitmapData newData = newMap.LockBits(new Rectangle(0,0,newWidth,newHeight), ImageLockMode.ReadWrite, imageMap.PixelFormat);
            BitmapData originalData = imageMap.LockBits(new Rectangle(0,0,imageMap.Width,imageMap.Height), ImageLockMode.ReadWrite, imageMap.PixelFormat);


            int originalStride = originalData.Stride;
            int newStride = newData.Stride;
            
            IntPtr newPointer = newData.Scan0;
            IntPtr originalPointer = originalData.Scan0;

            int originalBytes = Math.Abs(originalData.Stride) * imageMap.Height;
            byte[] originalPixels = new byte[originalBytes];

            int newBytes = Math.Abs(newData.Stride) * newData.Height;
            byte[] newPixels = new byte[newBytes];
            Array.Fill(originalPixels, (byte)255);

            System.Runtime.InteropServices.Marshal.Copy(originalPointer, originalPixels, 0, originalBytes);

            // Copy original pixels into the new pixel array
            for(int y = 0; y < originalData.Height; y++){
                for(int x = 0; x < originalData.Width; x++){
                    int originalIndex = (y * originalStride) + (x * bytesPerPixel);
                    int newIndex = (y * newStride) + (x * bytesPerPixel);

                    for (int i = 0; i < bytesPerPixel; i++){
                        newPixels[newIndex + i] = originalPixels[originalIndex + i];
                    }
                }
                //fill new columns colour using the last pixel in the row
                int lastPixelIndex = (y * originalStride) + ((originalData.Width - 1) * bytesPerPixel);
                for (int x = originalData.Width; x < newWidth; x++){
                    int newIndex = (y * newStride) + (x * bytesPerPixel);
                    for (int i = 0; i < bytesPerPixel; i++){
                        newPixels[newIndex + i] = originalPixels[lastPixelIndex + i];
                    }
                }
            }

            //fill in the new bottom rows
            for(int y = originalData.Height; y < newHeight; y++){
                int lastRow = (originalData.Height-1)*newStride;
                for(int x = 0; x < newWidth; x++){
                    int newIndex = (y*newStride) + (x*bytesPerPixel);
                    for(int i = 0; i < bytesPerPixel; i++){
                        newPixels[newIndex + i] = newPixels[lastRow + (x*bytesPerPixel + i)];
                    }
                }
            }


            System.Runtime.InteropServices.Marshal.Copy(newPixels, 0, newPointer, newBytes);
            imageMap.UnlockBits(originalData);
            newMap.UnlockBits(newData);
            imageMap = newMap;
            pixels = newPixels;
        }

        /// <summary>
        /// Saves the image to a new file
        /// </summary>
        /// <param name="newFilePath">The file path which the image will be saved into</param>
        public void SaveImage(string newFilePath){
            string? directoryPath = Path.GetDirectoryName(newFilePath);

            if(directoryPath != string.Empty && directoryPath != null){
                Directory.CreateDirectory(directoryPath);
            }
            
            imageMap.Save(newFilePath);
        }

        /// <summary>
        /// Function to save modified pixel data to the image's data
        /// </summary>
        /// <param name="pixelData">Array of all bytes in the image</param>
        public void SavePixelData(byte[] pixelData){
            BitmapData bitmapData = imageMap.LockBits(new Rectangle(0, 0, imageMap.Width, imageMap.Height), 
            ImageLockMode.WriteOnly, imageMap.PixelFormat);

            int bytes = Math.Abs(bitmapData.Stride) * imageMap.Height;

            IntPtr pixelPointer = bitmapData.Scan0;

            pixels = pixelData;

            System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, pixelPointer, bytes);

            imageMap.UnlockBits(bitmapData);
            
        }
    }
}