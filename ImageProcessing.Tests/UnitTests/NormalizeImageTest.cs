using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class NormalizeImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-normalized.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.testOutputsPath,"image-normalized.png");
        ImageLoader image = new ImageLoader(filePath);

        ImageNormalizer normalizer = new ImageNormalizer(20,120,1);
        normalizer.ProcessImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
