using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class PixelateImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-pixelated.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.testOutputsPath,"image-pixelated.png");
        ImageLoader image = new ImageLoader(filePath);

        ImagePixelator pixelator = new ImagePixelator(40);
        pixelator.PixelateImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
