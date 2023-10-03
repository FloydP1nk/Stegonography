using System.Collections;

namespace Steganography;

public class GetLength
{
    public static bool ReadLastBit(byte pixelColor) // получение последнего бита цвета пикселя
    {
        if (pixelColor % 2 == 0)
        {
            return false;
        }

        return true;
    }

    public static bool EndOfNumbFlag(List<bool> numbBits)
    {
        int powerOfTwo = 0;
        byte symbol = 0;
        for (int i = numbBits.Count - 8; i < numbBits.Count; ++i) // переводим последний байт в число byte
        {
            if (!numbBits[i]) // 0
            {
                powerOfTwo++;
            }
            else if (numbBits[i]) // 1
            {
                symbol += Convert.ToByte(Math.Pow(2, powerOfTwo));
                powerOfTwo++;
            }
        }

        if (symbol == 47) // symbol == /
        {
            return true;
        }

        return false;
    }

    public static Tuple<int, int> GetLenght(string pathToImage) // получение длинны сообщения
    {
        Image<Argb32> inputImage = Image.Load<Argb32>(pathToImage);
        int x = 0; // координата x
        int slashPixel; // координата последнего пикселя символа "/"
        List<bool> numbBits = new List<bool>(); // List битов из изображения
        while (true) // получаем List битов, последниие 8 элементов которого кодируют символ "/"
        {
            Argb32 pixel = inputImage[x, 0];
            numbBits.Add(ReadLastBit(pixel.A)); // 1
            numbBits.Add(ReadLastBit(pixel.R)); // 2
            numbBits.Add(ReadLastBit(pixel.G)); // 3
            numbBits.Add(ReadLastBit(pixel.B)); // 4
            x++;
            pixel = inputImage[x, 0];
            numbBits.Add(ReadLastBit(pixel.A)); // 4
            numbBits.Add(ReadLastBit(pixel.R)); // 5
            numbBits.Add(ReadLastBit(pixel.G)); // 7
            numbBits.Add(ReadLastBit(pixel.B)); // 8
            x++;

            if (EndOfNumbFlag(numbBits) && numbBits.Count != 0) // если numBits не пустой последний байт кодирует "/"
            {
                slashPixel = x; // запоминаем пиксель "/"
                break;
            }
        }

        numbBits.RemoveRange(numbBits.Count - 8, 8); // удаляем из numbBits "/"
        BitArray bitArray = new BitArray(numbBits.ToArray()); // конвертируем numBits в BitArray
        byte[] byteArray = new byte[bitArray.Length / 8]; // получаем массив байтов
        bitArray.CopyTo(byteArray, 0);
        string number = "";
        for (int i = 0; i < byteArray.Length; i++)
        {
            char symbol = Convert.ToChar(byteArray[i]);
            number += symbol;
        }

        return new Tuple<int, int>(Convert.ToInt32(number), slashPixel);
    }
}