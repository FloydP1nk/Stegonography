using System.Collections;
using System.Text;

namespace Steganography
{
    public class SecretText
    {
        public static BitArray TextToBytes(string massage)
        {
            string length = Convert.ToString(massage.Length);
            string newMassage = length;
            newMassage += "/";
            newMassage += massage;
            byte[] byteArray = Encoding.ASCII.GetBytes(newMassage);
            BitArray bitArray = new BitArray(byteArray);
            return bitArray;
        }
    }
}