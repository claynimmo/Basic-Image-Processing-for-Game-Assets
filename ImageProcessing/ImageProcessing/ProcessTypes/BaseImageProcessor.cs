

namespace ImageProcessing{

    public abstract class BaseImageProcessor{

        public virtual void ProcessImage(ImageLoader image){
            try{
                byte[] pixels = ProcessPixels(image.Pixels, image.ImageWidth, image.ImageHeight, image.ImageStride);
                image.SavePixelData(pixels);
               
            }
            catch (Exception e){
                Console.WriteLine("Error: " + e);
            }
        }

        protected virtual byte[] ProcessPixels(byte[] pixels,int imageWidth, int imageHeight, int imageStride){
            int bytesPerPixel = 4;

            for(int y = 0; y < imageHeight; y++){
                for(int x = 0; x < imageWidth; x++){
                    int index = (y*imageStride) + (x*bytesPerPixel);
                    pixels = ProcessIndividualPixel(pixels,index);
                }
            }
            return pixels;
        }

        protected abstract byte[] ProcessIndividualPixel(byte[] pixels, int index);
    }
}