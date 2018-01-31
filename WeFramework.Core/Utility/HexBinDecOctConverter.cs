using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeFramework.Core.Utility
{
    public class HexBinDecOctConverter
    {
        public static string BytesToBinaryString(byte[] bytes)
        {
            int capacity = bytes.Length * 8;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(ByteToBinaryString(bytes[i]));
            }
            return stringBuilder.ToString();
        }

        public static string ByteToBinaryString(byte ibyte)
        {
            return Convert.ToString(ibyte, 2).PadLeft(8, '0');
        }

        public static int BinaryStringToInt(string binaryString)
        {
            return Convert.ToInt32(binaryString, 2);
        }

        public static string BytesToHexString(byte[] bytes)
        {
            string result = "<";

            for (int i = 0; i < bytes.Length; i++)
            {
                result += bytes[i].ToString("x2") + " ";
            }

            return result.TrimEnd(' ') + ">";
        }
    }
}
