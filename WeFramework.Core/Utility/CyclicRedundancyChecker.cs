using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeFramework.Core.Utility
{
    public class CyclicRedundancyChecker
    {
        public static UInt16 CreateCheckCrc(byte[] byteDatas)
        {
            UInt16 crcValue = 0xFFFF;

            for (int i = 0; i < byteDatas.Length; i++)
            {
                crcValue ^= byteDatas[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crcValue & 0x01) != 0)
                    {
                        crcValue >>= 1;
                        crcValue ^= 0xA001;
                    }
                    else
                    {
                        crcValue >>= 1;
                    }
                }
            }

            return crcValue;
        }

        public static byte[] AttachCrc(byte[] byteDatas)
        {
            List<byte> datas = byteDatas.ToList();
            UInt16 checkCode = CreateCheckCrc(byteDatas);
            byte[] checkCodeBytes = BitConverter.GetBytes(checkCode);
            datas.AddRange(checkCodeBytes);
            return datas.ToArray();
        }

        public static bool CheckCrc(byte[] byteDatas)
        {
            UInt16 crcValue = 0xFFFF;

            for (int i = 0; i < byteDatas.Length - 2; i++)
            {
                crcValue ^= byteDatas[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crcValue & 0x01) != 0)
                    {
                        crcValue >>= 1;
                        crcValue ^= 0xA001;
                    }
                    else
                    {
                        crcValue >>= 1;
                    }
                }
            }

            byte[] checkCodeBytes = BitConverter.GetBytes(crcValue);

            return byteDatas.Skip(byteDatas.Length - 2).SequenceEqual(checkCodeBytes);
        }
    }
}
