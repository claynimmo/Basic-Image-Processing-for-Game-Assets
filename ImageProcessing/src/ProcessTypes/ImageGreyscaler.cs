

namespace ImageProcessing{
    /// <summary>
    /// Contains functions to process an image into a pixel art
    /// </summary>
    public class ImageGreyscaler : BaseImageProcessor{

        private readonly byte minBrightness;
        private readonly byte maxBrightness;
        private readonly decimal exposure;

        public ImageGreyscaler(byte minBrightness, byte maxBrightness, decimal exposure){
            this.minBrightness = minBrightness;
            this.maxBrightness = maxBrightness;
            this.exposure = exposure;
        }
    

        protected override byte[] ProcessIndividualPixel(byte[] pixels, int index){
            float blue = pixels[index];
            float green = pixels[index+1];
            float red = pixels[index+2];
            
            byte value = (byte)Math.Min(exposure * (decimal)((Math.Min(Math.Min(blue,green),red) + Math.Max(Math.Max(blue,green),red))/2),255);

            //clamp the value between the min and max
            value = Math.Max(Math.Min(value,maxBrightness),minBrightness);

            for(int i=0; i < 3; i++){
                pixels[index + i] = value;
            }
            return pixels;
        }
    }
}