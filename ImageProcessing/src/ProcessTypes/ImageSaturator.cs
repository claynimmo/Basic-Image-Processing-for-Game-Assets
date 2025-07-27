

namespace ImageProcessing;

/// <summary>
/// Contains functions to process an image into a pixel art
/// </summary>
public class ImageSaturator : BaseImageProcessor{
    
    private readonly float saturationBoost;

    public ImageSaturator(float saturationBoost){
        this.saturationBoost = saturationBoost;
    }
    
    protected override byte[] ProcessIndividualPixel(byte[] pixels, int index){
        byte blue  = pixels[index];
        byte green = pixels[index+1];
        byte red   = pixels[index+2];
        
        (float hue, float saturation, float luminance) = ColourConverter.RGB_To_HSL(red, green, blue);


        //boost the saturation
        saturation *= saturationBoost;
        saturation = Math.Min(saturation, 1);

        (red, green, blue) = ColourConverter.HSL_To_RGB(hue, saturation, luminance);

        pixels[index] = blue;
        pixels[index+1] = green;
        pixels[index+2] = red;
        return pixels;
    }
}