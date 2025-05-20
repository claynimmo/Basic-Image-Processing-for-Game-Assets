
namespace ImageProcessing{
    /// <summary>
    /// Contains functions to process an image into a pixel art
    /// </summary>
    public class ImageNormalMapper{
        
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

            float[] grads = new float[pixels.Length]; //since the length is 4 per pixel, 0 is used for x, and 1 is used for y

            for(int y = 0; y < imageHeight; y++){
                for(int x = 0; x < imageWidth; x++){
                    int index = (y*imageStride) + (x*bytesPerPixel);
                    
                    //edge case for right edge
                    int rightIndex;
                    if(x == imageWidth-1){
                        rightIndex = y * imageStride;
                    }
                    else{
                        rightIndex = (y*imageStride) + (x+1)*bytesPerPixel;
                    }

                    //edge case for left edge
                    int leftIndex;
                    if(x == 0){
                        leftIndex = (y*imageStride) + (imageWidth-1)*bytesPerPixel;
                    }
                    else{
                        leftIndex = (y*imageStride) + (x-1)*bytesPerPixel;
                    }
                    
                    //edge case for top row
                    int topIndex;
                    if(y == 0){
                        topIndex = ((imageHeight-1) * imageStride) + (x * bytesPerPixel);
                    }
                    else{
                        topIndex = ((y-1) * imageStride) + (x * bytesPerPixel);
                    }

                    //edge case for bottom row
                    int bottomIndex;
                    if(y == imageHeight - 1){
                        bottomIndex = x * bytesPerPixel;
                    }
                    else{
                        bottomIndex = ((y+1) * bytesPerPixel) + (x & bytesPerPixel);
                    }

                    float xGrad = GetPixelIntensity(pixels, rightIndex) - GetPixelIntensity(pixels, leftIndex);
                    float yGrad = GetPixelIntensity(pixels, bottomIndex) - GetPixelIntensity(pixels, topIndex);

                   
                    grads[index] = xGrad;
                    grads[index+1] = yGrad;  
                }
            }
            
            for(int y = 0; y < imageHeight; y++){
                for(int x = 0; x < imageWidth; x++){
                    int index = (y*imageStride) + (x*bytesPerPixel);
                    pixels = ApplyGradient(pixels,index,grads[index],grads[index+1]);
                }
            }
            return pixels;
        }
       
        private float GetPixelIntensity(byte[] pixels, int index){
            float blue = pixels[index];
            float green = pixels[index+1];
            float red = pixels[index+2];
            
            float value = (byte)Math.Min((decimal)((Math.Min(Math.Min(blue,green),red) + Math.Max(Math.Max(blue,green),red))/2),255);
            return value;
        }

        private byte[] ApplyGradient(byte[] pixels, int index, float xGrad, float yGrad){

            float vectorMag = MathF.Sqrt((xGrad*xGrad) + (yGrad*yGrad));
            float x = xGrad / vectorMag;
            float y = yGrad / vectorMag;
            pixels[index]   = 255;
            pixels[index+1] = (byte)((y + 1) / 2 * 255);
            pixels[index+2] = (byte)((x + 1) / 2 * 255);

            return pixels;
        }
    }
}