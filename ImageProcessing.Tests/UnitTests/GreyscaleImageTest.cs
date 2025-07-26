using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class GreyscaleImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-greyscale.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.testOutputsPath,"image-greyscale.png");
        ImageLoader image = new ImageLoader(filePath);

        ImageGreyscaler greyscaler = new ImageGreyscaler(10,230,2);
        greyscaler.ProcessImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
