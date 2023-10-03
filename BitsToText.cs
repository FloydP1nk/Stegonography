using System.Collections;

namespace Steganography;

public class BitsToText
{
    public static string ToText(BitArray bits)
    {
        string massage = "";
        for (int i = 7; i < bits.Length; i += 8)
        {
            int powerOfTwo = 0;
            byte symbol = 0;
            for (int j = i; j > i - 7; j--)
            {
                if (!bits[j]) // 0
                {
                    powerOfTwo++;
                }
                else if (bits[j]) // 1
                {
                    symbol += Convert.ToByte(Math.Pow(2, powerOfTwo));
                    powerOfTwo++;
                }
            }

            massage += (char)symbol;
        }

        return massage;
    }
}