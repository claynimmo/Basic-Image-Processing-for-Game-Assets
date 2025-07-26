using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class BlurImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-blur.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.testOutputsPath,"image-blur.png");
        ImageLoader image = new ImageLoader(filePath);

        ImageBlur blur = new ImageBlur(7);
        blur.ProcessImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
