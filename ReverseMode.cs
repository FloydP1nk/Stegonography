using System.Collections;


namespace Steganography;

public class ReverseMode
{
    public static BitArray PixelsToBits(string pathToImage)
    {
        List<bool> listOfBits = new List<bool>();
        Image<Argb32> inputImage = Image.Load<Argb32>(pathToImage);
        Tuple<int, int> info =
            GetLength.GetLenght(pathToImage);
        int length = info.Item1;
        int firstPixel = info.Item2;
        length *= 2; // считаем количество пикселей
        int pixelCount = 0;
        for (int y = 0; y < inputImage.Height; ++y)
        {
            for (int x = firstPixel; x < inputImage.Width; x+=2) // firstPixel + 1
            {
                if (pixelCount == length)
                {
                    break;
                }

                {
                    Argb32 pixel2 = inputImage[x+1, y];
                    listOfBits.Add(GetLength.ReadLastBit(pixel2.B));
                    listOfBits.Add(GetLength.ReadLastBit(pixel2.G));
                    listOfBits.Add(GetLength.ReadLastBit(pixel2.R));
                    listOfBits.Add(GetLength.ReadLastBit(pixel2.A));
                    pixelCount++;
                    Argb32 pixel1 = inputImage[x, y];
                    listOfBits.Add(GetLength.ReadLastBit(pixel1.B));
                    listOfBits.Add(GetLength.ReadLastBit(pixel1.G));
                    listOfBits.Add(GetLength.ReadLastBit(pixel1.R));
                    listOfBits.Add(GetLength.ReadLastBit(pixel1.A));
                    pixelCount++;
                    
                }
            }

            if (pixelCount == length)
            {
                break;
            }
        }

        return new BitArray(listOfBits.ToArray());
    }
}