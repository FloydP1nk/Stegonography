using System.Collections;
using SixLabors.ImageSharp.Formats.Png;

namespace Steganography
{
    public class HideMode
    {
        public static byte СhangeLastBit(byte pixelColor, bool value)
        {
            byte newColor = pixelColor;
            if (!value) // inputBits[i] = 0
            {
                if (newColor % 2 != 0) // если число нечетное =>  самый малозначащий бит = 1 
                {
                    newColor--;
                }
            }
            else if (value) // inputBits[i] = 1
            {
                if (newColor % 2 == 0) // если число четное =>  самый малозначащий бит = 0
                {
                    newColor++;
                }
            }

            return newColor;
        }

        public static void hideMode(string pathToInputImage, string pathToSecretImage, BitArray inputBits)
        {
            int bitsCounter = 0;
            Image<Argb32> inputImage = Image.Load<Argb32>(pathToInputImage);
            for (int y = 0; y < inputImage.Height; ++y) // идем по пикселям изображения
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    if (bitsCounter == inputBits.Length)
                    {
                        break; // выходим из внешнего цикла, если сообщение закончилось
                    }

                    Argb32 pixel = inputImage[x, y];
                    byte pixelColor = pixel.A;
                    Console.WriteLine(inputBits[bitsCounter]);
                    pixel.A = СhangeLastBit(pixelColor, inputBits[bitsCounter]);
                    bitsCounter++;
                    pixelColor = pixel.R;
                    Console.WriteLine(inputBits[bitsCounter]);
                    pixel.R = СhangeLastBit(pixelColor, inputBits[bitsCounter]);
                    bitsCounter++;
                    pixelColor = pixel.G;
                    Console.WriteLine(inputBits[bitsCounter]);
                    pixel.G = СhangeLastBit(pixelColor, inputBits[bitsCounter]);
                    bitsCounter++;
                    pixelColor = pixel.B;
                    Console.WriteLine(inputBits[bitsCounter]);
                    pixel.B = СhangeLastBit(pixelColor, inputBits[bitsCounter]);
                    inputImage[x, y] = pixel;
                    bitsCounter++;
                }

                if (bitsCounter == inputBits.Length)
                {
                    break; // выходим из внешнего цикла, если сообщение закончилось
                }
            }

            inputImage.Save(pathToSecretImage, new PngEncoder()); // сохраняем изображение
        }
    }
}