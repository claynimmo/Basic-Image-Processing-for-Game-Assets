using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing.Tests;

public static class TestingUtils{

    public static bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2){
        if(bmp1.Size != bmp2.Size) return false;

        for(int x = 0; x < bmp1.Width; x++){
            for(int y = 0; y < bmp1.Height; y++){
                if(bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y)){
                    return false;
                }
            }
        }
        return true;
    }
}