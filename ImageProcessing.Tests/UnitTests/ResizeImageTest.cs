using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing.Tests;

public class ResizeImageTest{
    [Fact]
    public void ImageIsEqual(){
        string filePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.filePath);
        string newFilePath = Path.Combine(AppContext.BaseDirectory, TestingUtils.outputsPath,"image-resized.png");
        ImageLoader image = new ImageLoader(filePath);

        float imageWidth = image.ImageWidth;
        float imageHeight = image.ImageHeight;

        //add 50 pixels to the width, and 100 to the height to test the resizing
        image.ResizeImage(image.ImageWidth + 50, image.ImageHeight + 100);
        image.SaveImage(newFilePath);

        Assert.True(image.ImageWidth == imageWidth + 50 && image.ImageHeight == imageHeight + 100);
    }
}
