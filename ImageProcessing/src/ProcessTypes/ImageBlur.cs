
namespace ImageProcessing{
    /// <summary>
    /// Contains functions to process an image into a pixel art
    /// </summary>
    public class ImageBlur{
        
        private readonly int matrixWidth;

        public ImageBlur(int matrixWidth){
            this.matrixWidth = matrixWidth;
        }
        /// <summary>
        /// Function to load and greyscale an image, clamped between a min and max and saving to a file
        /// </summary>
        public void ProcessImage(ImageLoader image){
            try{
                byte[] pixels = ProcessPixels(image.Pixels, image.ImageWidth, image.ImageHeight, image.ImageStride);
                image.SavePixelData(pixels);
            }
            catch (Exception e){
                Console.WriteLine("Error: " + e);
            }
        }

        private byte[] ProcessPixels(byte[] pixels,int imageWidth, int imageHeight, int imageStride){
            int bytesPerPixel = 4;

            byte[] tempPixels = new byte[pixels.Length];

            //loop through each pixel to get the average colour surrounding them, storing it in a new array to avoid interferring with the average calculations
            for(int y = 0; y < imageHeight; y++){
                for(int x = 0; x < imageWidth; x++){
                    int index = (y*imageStride) + (x*bytesPerPixel);
                    
                    byte[] pixelColour = GetAverageColourByBlock(pixels,x,y,imageStride,bytesPerPixel,imageHeight,imageWidth);
                    for(int i = 0; i < pixelColour.Length; i++){
                        tempPixels[index + i] = pixelColour[i];
                    }
                }
            }
        
            return tempPixels;
        }
       
        private byte[] GetAverageColourByBlock(byte[] pixels, int xCoord, int yCoord, int imageStride, int bytesPerPixel,int imageHeight, int imageWidth){
            int newWidth = (int)Math.Floor((double)matrixWidth/2); //half the value, so it is added and subracted from the middle pixel (only works properly if odd, since the lost 0.5 is the starting pixel);
            int minX = (xCoord - newWidth >= 0) ? xCoord-newWidth : 0;
            int maxX = (xCoord + newWidth < imageWidth) ? xCoord+newWidth : imageWidth-1;
            int minY = (yCoord - newWidth >= 0) ? yCoord-newWidth : 0;
            int maxY = (yCoord + newWidth < imageHeight) ? yCoord+newWidth : imageHeight-1;

            float[] colours = new float[bytesPerPixel];

            //loop through each pixel within the xy bounds, to obtain their colour value for the average
            int numOfPixels = 0;
            for(int y = minY; y <= maxY; y++){
                for(int x = minX; x <= maxX; x++){
                    numOfPixels ++;
                    int index = (y*imageStride) + (x*bytesPerPixel);
                    for(int i = 0; i < colours.Length; i++){
                        colours[i] = colours[i] + pixels[index + i];
                    }
                }
            }

            //calculate the average pixel colour across the region and apply it to a new return array
            byte[] cols = new byte[colours.Length];
            for(int i = 0; i < colours.Length; i++){
                colours[i] = (byte)(colours[i] / numOfPixels);
                cols[i] = (byte)Math.Min(colours[i], 255);
            }
            return cols;
        }
    }
}