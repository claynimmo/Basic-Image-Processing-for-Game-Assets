using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class PixelateImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "image.png");
        string newFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Outputs/image-pixelated.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "TestOutputs/image-pixelated.png");
        ImageLoader image = new ImageLoader(filePath);

        ImagePixelator pixelator = new ImagePixelator(40);
        pixelator.PixelateImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
