using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing.Tests;

public class HSLColourTest{
    
    [Theory]
    [InlineData(255, 0, 0)]     // pure red
    [InlineData(0, 255, 0)]     // pure green
    [InlineData(0, 0, 255)]     // pure blue
    [InlineData(0, 0, 0)]       // black
    [InlineData(255, 255, 255)] // white
    [InlineData(100, 100, 100)] // grey
    [InlineData(112, 231, 31)]  // mixed

    public void TestConversion(byte red, byte green, byte blue){

        (float hue, float saturation, float luminance) = ColourConverter.RGB_To_HSL(red,green,blue);

        (byte newRed, byte newGreen, byte newBlue) = ColourConverter.HSL_To_RGB(hue,saturation,luminance);

        Console.WriteLine($"red:{newRed};green:{newGreen};blue:{newBlue}");

        Assert.InRange(newRed, red - 1, red + 1);
        Assert.InRange(newGreen, green - 1, green + 1);
        Assert.InRange(newBlue, blue - 1, blue + 1);

    }
}
