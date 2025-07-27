

namespace ImageProcessing{

    public static class InputManager{

        public static string FilePathInput(){
            bool correctFilePath = false;
            string filePath;
            do{
                Console.WriteLine("Input image file path:");
                filePath = Console.ReadLine() ?? "";
                if (File.Exists(filePath)){
                    correctFilePath = true;
                }
                else{
                    Console.WriteLine("Invalid file path.");
                }
            }
            while (!correctFilePath);
            return filePath;
        }

        public static int NumberInput(int min, int max, string prompt){
            bool validNumber;
            int type;
            do{
                Console.WriteLine(prompt);
                validNumber = int.TryParse(Console.ReadLine() ?? "0",out type);
                if(!validNumber){
                    Console.WriteLine($"Input a number between {min} and {max}");
                }
            } while(!validNumber);
            return type;
        }

        public static decimal DecimalInput(string prompt){
            bool validNumber;
            decimal value;
            do{
                Console.WriteLine(prompt);
                string input = Console.ReadLine() ?? "";
                validNumber = decimal.TryParse(input, out value);
            } while(!validNumber);
            return value;
        }
    }
}