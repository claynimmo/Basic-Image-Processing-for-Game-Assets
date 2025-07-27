
namespace ImageProcessing{
    /// <summary>
    /// Contains functions to process an image into a pixel art
    /// </summary>
    public class ImagePixelator{

        private readonly int blockSize;

        public ImagePixelator(int blockSize){
            this.blockSize = blockSize;
        }
        /// <summary>
        /// Function to load and pixelate an image from a file path, saving it to a new file
        /// </summary>
        public void PixelateImage(ImageLoader image){
            try{
                int horizontalBlocks = (int)Math.Ceiling((double)(image.ImageWidth / blockSize))+1;
                int verticalBlocks = (int)Math.Ceiling((double)(image.ImageHeight / blockSize))+1;

                byte[] pixels = PixelatePixels(image.Pixels,image.ImageWidth, image.ImageHeight, horizontalBlocks, verticalBlocks);
            
                image.SavePixelData(pixels);

                //resize the image so that pixels are not clipped
                image.ResizeImage(horizontalBlocks*blockSize,verticalBlocks*blockSize);
            }
            catch (Exception e){
                Console.WriteLine("Error: " + e);
            }
        }

        private byte[] PixelatePixels(byte[] pixels,int imageWidth, int imageHeight, int horizontalBlocks, int verticalBlocks){

            for(int i=0; i < horizontalBlocks; i++){
                for(int j=0; j < verticalBlocks; j++){
                    pixels = AveragePixelData(pixels,i*blockSize,j*blockSize,imageWidth,imageHeight,blockSize);
                }
            }
            //pixels = AveragePixelData(pixels, 0, 0, imageWidth, blockSize);
            //pixels = AveragePixelData(pixels, 0, blockSize, imageWidth, blockSize);
            return pixels;
        }

        private byte[] AveragePixelData(byte[] pixels, int startX, int startY, int bitmapWidth, int bitmapHeight, int blockSize){
            int bytesPerPixel = 4; // ARGB format (4 bytes per pixel)
            int width = bitmapWidth * bytesPerPixel;
            int averageBlue=0;
            int averageGreen=0;
            int averageRed=0;
            int averageAlpha=0;

            int totalPixels = 0;
            int index = 0;
            
            //loop to add to the average colours
            for(int i = startX; i < startX + blockSize; i++){
                
                if(i >= bitmapWidth){break;}

                for(int j = startY; j <= startY + blockSize; j++){

                    if(j >= bitmapHeight){break;}

                    index = (j * width) + (i * bytesPerPixel);
                    if (index >= pixels.Length) {continue;}
                    totalPixels++;
                    averageBlue += pixels[index];

                    if(index + 1 >= pixels.Length){continue;}
                    averageGreen += pixels[index + 1];

                    if(index + 2 >= pixels.Length){continue;}
                    averageRed += pixels[index + 2];

                    if(index + 3 >= pixels.Length){continue;}
                    averageAlpha += pixels[index + 3];
                }
            }

            //get the average values, limited to 255
            averageBlue /= totalPixels;
            averageBlue = Math.Min(averageBlue,255);
            averageGreen /= totalPixels;
            averageGreen = Math.Min(averageGreen,255);
            averageRed /= totalPixels;
            averageRed = Math.Min(averageRed,255);
            averageAlpha /= totalPixels;
            averageAlpha = Math.Min(averageAlpha,255);

            //loop again to apply the colours
            for(int i=startX; i<startX+blockSize;i++){
                
                if(i >= bitmapWidth){break;} //exit eary when surpassing the boundaries of the image to avoid artifacts
                
                for(int j = startY; j <= startY + blockSize; j++){

                    if(j >= bitmapHeight){break;}

                    index = (j * width) + (i * bytesPerPixel);
                    
                    if(index >= pixels.Length){continue;}
                    pixels[index] = (byte)averageBlue;

                    if(index + 1 >= pixels.Length){continue;}
                    pixels[index + 1] = (byte)averageGreen;

                    if(index + 2 >= pixels.Length){continue;}
                    pixels[index + 2] = (byte)averageRed;

                    if(index + 3 >= pixels.Length){continue;}
                    pixels[index + 3] = (byte)averageAlpha;
                }
            }

            return pixels;
        }
    }
}