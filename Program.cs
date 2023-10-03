// See https://aka.ms/new-console-template for more information

using static Steganography.SecretText;

namespace Steganography
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter program operating mode (1, 2 or 3 to escape)");
                byte mode = Convert.ToByte(Console.ReadLine());
                if (mode == 1) // HideMode
                {
                    Console.WriteLine("Please enter your message");
                    string massage = Console.ReadLine();

                    Console.WriteLine(
                        "Please enter path to the image");
                    string pathToInputImage = Console.ReadLine();
                    if (MassageLength.LenthFlag(massage, pathToInputImage)) // проверка на вместимость
                    {
                        Console.WriteLine("Please enter path to the secret image");
                        string pathToSecretImage = Console.ReadLine();
                        HideMode.hideMode(pathToInputImage, pathToSecretImage, TextToBytes(massage));
                    }
                }
                else if (mode == 2) // ReverseMode
                {
                    Console.WriteLine(
                        "Please enter path to the image");
                    string pathToSecretImage = Console.ReadLine();
                    Console.WriteLine(BitsToText.ToText(ReverseMode.PixelsToBits(pathToSecretImage)));
                }

                else if (mode == 3) // выход
                {
                    break;
                }
                else if (mode == 4) // test mode
                {
                }
            }
        }
    }
}