using System.Drawing;
using System.Drawing.Imaging;


namespace ImageProcessing.Tests;

public class NormalMapTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-normal_map.png");
        string compareFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.testOutputsPath,"image-normal_map.png");
        ImageLoader image = new ImageLoader(filePath);

        ImageNormalMapper mapper = new ImageNormalMapper();
        mapper.ProcessImage(image);

        image.SaveImage(newFilePath);

        Bitmap bmp = new Bitmap(newFilePath);
        Bitmap bmp2 = new Bitmap(compareFilePath);
        Assert.True(TestingUtils.CompareBitmaps(bmp,bmp2));
    }
}
