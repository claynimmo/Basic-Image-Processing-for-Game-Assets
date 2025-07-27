
namespace ImageProcessing{
    /// <summary>
    /// Contains functions to process an image into a pixel art
    /// </summary>
    public class ImageInverter : BaseImageProcessor{
    
        protected override byte[] ProcessIndividualPixel(byte[] pixels, int index){
            for(int i = 0; i < 3; i++){
                pixels[index + i] = (byte)(255 - pixels[index + i]);
            }
            return pixels;
        }
    }
}