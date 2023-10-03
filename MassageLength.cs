namespace Steganography;

public class MassageLength
{
    public static bool LenthFlag(string massage, string pathToImage)
    {
        Image<Argb32> inputImage = Image.Load<Argb32>(pathToImage);
        long size = inputImage.Height * inputImage.Width;
        if (size >= massage.Length * 2 + 10)
        {
            return true;
        }

        Console.WriteLine($"Sorry, your massage is too big");
        return false;
    }
}