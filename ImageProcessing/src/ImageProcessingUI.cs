
using System.Drawing;

namespace ImageProcessing{

    public class ImageProcessingUI{

        public void InitializeUI(){
            while(true){
                Console.WriteLine("Would you like to close the program? (type 1 to close, any to continue)");
                string closeProgram = Console.ReadLine() ?? "";
                if(closeProgram == "1"){break;}
                string filePath = InputManager.FilePathInput();
                string typePrompt = "\nInput a number for the type of processing:\n"+
                    "1: Pixelate\n"+
                    "2: Greyscale\n"+
                    "3: Normalize (turn all colours within a range white, and the rest black)\n"+
                    "4: Normal Map\n"+
                    "5: Invert\n"+
                    "6: Blur\n"+
                    "7: Saturate";
                int type = InputManager.NumberInput(1,5,typePrompt);
                
                SelectProcessingType(type, filePath);
                
            }
        }

        private void SelectProcessingType(int type, string filePath){
            ImageLoader image = new ImageLoader(filePath);
            string newFilePath = filePath;
            switch(type){
                case 1:
                    PixelateImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-pixelated{Path.GetExtension(filePath)}";
                    break;
                case 2:
                    GreyscaleImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-greyscale{Path.GetExtension(filePath)}";
                    break;
                case 3:
                    NormalizeImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-normalized{Path.GetExtension(filePath)}";
                    break;
                case 4:
                    NormalMapImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-normal_map{Path.GetExtension(filePath)}";
                    break;
                case 5:
                    InvertImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-inverted{Path.GetExtension(filePath)}";
                    break;
                case 6:
                    BlurImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-blur{Path.GetExtension(filePath)}";
                    break;
                case 7:
                    SaturateImage(image);
                    newFilePath = $"{Path.GetFileNameWithoutExtension(filePath)}-saturated{Path.GetExtension(filePath)}";
                    break;
                default:
                    break;
            }
            newFilePath = Path.Combine("Outputs",newFilePath);
            SaveFile(newFilePath,image);
        }

        private void PixelateImage(ImageLoader image){
            string blockSizeInput = "\nInput the pixel size of each block:";
            int blockSize = InputManager.NumberInput(1,int.MaxValue,blockSizeInput);

            ImagePixelator pixelator = new ImagePixelator(blockSize);
            pixelator.PixelateImage(image);
        }

        private void GreyscaleImage(ImageLoader image){
            string minValueInput = "\nInput the minimum brightness value (0-255):";
            byte minValue = (byte)InputManager.NumberInput(0,255,minValueInput);

            string maxValueInput = "\nInput the maximum brightness value (0-255):";
            byte maxValue = (byte)InputManager.NumberInput(0,255,maxValueInput);

            string exposureInput = "\nInput the exposure coefficient (increases the average colour for each pixel):";
            decimal exposure = InputManager.DecimalInput(exposureInput);

            ImageGreyscaler greyscaler = new ImageGreyscaler(minValue,maxValue,exposure);
            greyscaler.ProcessImage(image);
        }

        private void NormalizeImage(ImageLoader image){
            string minValueInput = "\nInput the minimum average colour to turn white (0-255):";
            byte minValue = (byte)InputManager.NumberInput(0,255,minValueInput);

            string maxValueInput = "\nInput the maximum average colour to turn white (0-255):";
            byte maxValue = (byte)InputManager.NumberInput(0,255,maxValueInput);

            string exposureInput = "\nInput the exposure coefficient (increases the average colour for each pixel, applied before normalization):";
            decimal exposure = InputManager.DecimalInput(exposureInput);

            ImageNormalizer normalizer = new ImageNormalizer(minValue,maxValue,exposure);
            normalizer.ProcessImage(image);
        }

        private void InvertImage(ImageLoader image){
            ImageInverter inverter = new ImageInverter();
            inverter.ProcessImage(image);
        }

        private void NormalMapImage(ImageLoader image){
            ImageNormalMapper mapper = new ImageNormalMapper();
            mapper.ProcessImage(image);
        }

        private void BlurImage(ImageLoader image){

            string sampleSizeInput = "\nInput the size of the sample for bluring the image (works best for odd numbers stating at 3)";
            int sampleSize = InputManager.NumberInput(0,10000,sampleSizeInput);
            ImageBlur blur = new ImageBlur(sampleSize);
            blur.ProcessImage(image);
        }

        private void SaturateImage(ImageLoader image){

            string saturationBoostPrompt = "\nInput the saturation boost (a multiplyer to the pixel's saturation)";
            float saturationBoost = (float)InputManager.DecimalInput(saturationBoostPrompt);
            ImageSaturator saturator = new ImageSaturator(saturationBoost);
            saturator.ProcessImage(image);
        }


        private void SaveFile(string newFilePath, ImageLoader image){
            image.SaveImage(newFilePath);
            Console.WriteLine("File saved to " + newFilePath);

            //open the file to immediately show the results of the processing
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = newFilePath,
                UseShellExecute = true
            });
        }
    }
}