

namespace ImageProcessing;


public static class ColourConverter{

    public static (float, float, float) RGB_To_HSL(float red, float green, float blue){
        blue  /= 255;
        green /= 255;
        red   /= 255;

        //convert rgb colour to hsv values
        float maxColour = Math.Max(blue, Math.Max(green,red));
        float minColour = Math.Min(blue, Math.Min(green,red));
        float luminance = (maxColour + minColour) / 2;

        float delta = maxColour - minColour;

        float saturation = 0;
        float hue = 0;

        if(maxColour != minColour){
            saturation = luminance <= 0.5f ? delta/(maxColour+minColour) : delta/(2-maxColour-minColour);

            if(maxColour == red){
                hue = (green - blue) / delta;
            }
            else if(maxColour == green){
                hue = 2 + (blue - red) / delta;
            }
            else if(maxColour == blue){
                hue = 4 + (red - green) / delta;
            }
            hue *= 60;
            if(hue < 0){
                hue += 360;
            }
        }

        return (hue, saturation, luminance);
    }

    public static (byte, byte, byte) HSL_To_RGB(double hue, double saturation, double luminance) {

        double r = 0, g = 0, b = 0;

        if(hue == 0 && saturation == 0){
            r = luminance * 255;
            b = r;
            g = b;
            return ((byte)Math.Round(r),(byte)Math.Round(g),(byte)Math.Round(b));
        }

        double temp = luminance < 0.5f ? luminance * (1 + saturation) : luminance + saturation - (luminance * saturation);
        double temp2 = 2 * luminance - temp;

        double hueNormalized = hue/360;

        double tempR = hueNormalized + 0.333f;
        if(tempR > 1){tempR -= 1;}
        else if(tempR < 0){tempR += 1;}

        double tempG = hueNormalized;
        if(tempG > 1){tempG -= 1;}
        else if(tempG < 0){tempG += 1;}

        double tempB = hueNormalized - 0.333f;
        if(tempB > 1){tempB -= 1;}
        else if(tempB < 0){tempB += 1;}

        if(tempR * 6 < 1)     {r = temp2 + (temp - temp2) * 6 * tempR;}
        else if(tempR * 2 < 1){r = temp;}
        else if(tempR * 3 < 2){r = temp2 + (temp - temp2) * (0.666f - tempR) * 6;}
        else                  {r = temp2;}

        if(tempG * 6 < 1)     {g = temp2 + (temp - temp2) * 6 * tempG;}
        else if(tempG * 2 < 1){g = temp;}
        else if(tempG * 3 < 2){g = temp2 + (temp - temp2) * (0.666f - tempG) * 6;}
        else                  {g = temp2;}

        if(tempB * 6 < 1)     {b = temp2 + (temp - temp2) * 6 * tempB;}
        else if(tempB * 2 < 1){b = temp;}
        else if(tempB * 3 < 2){b = temp2 + (temp - temp2) * (0.666f - tempB) * 6;}
        else                  {b = temp2;}

        r *= 255;
        g *= 255;
        b *= 255;

        g+=1; //for some reason green was always 1 value lower than it should have
        g = Math.Clamp(g, 0, 255);

        return ((byte)r, (byte)g, (byte)b);
    }
}