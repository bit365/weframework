using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeFramework.Service.Security
{
    public class EncryptionService : IEncryptionService
    {
        public virtual string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] salt, bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            byte[] array = new byte[49];
            Buffer.BlockCopy(salt, 0, array, 1, 16);
            Buffer.BlockCopy(bytes, 0, array, 17, 32);
            return Convert.ToBase64String(array);
        }

        public virtual bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] array = Convert.FromBase64String(hashedPassword);
            if (array.Length != 49 || array[0] != 0)
            {
                return false;
            }
            byte[] array2 = new byte[16];
            Buffer.BlockCopy(array, 1, array2, 0, 16);
            byte[] array3 = new byte[32];
            Buffer.BlockCopy(array, 17, array3, 0, 32);
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array2, 1000))
            {
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            return ByteArraysEqual(array3, bytes);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            bool flag = true;
            for (int i = 0; i < a.Length; i++)
            {
                flag &= (a[i] == b[i]);
            }
            return flag;
        }

        public virtual string Encrypt(string plainText, string key)
        {
            byte[] salt = Encoding.Default.GetBytes(key);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key, salt);

            byte[] buffer = EncryptTextToMemory(plainText, deriveBytes.GetBytes(24), deriveBytes.GetBytes(8));

            return Convert.ToBase64String(buffer);
        }

        public virtual string Decrypt(string cypherText, string key)
        {
            byte[] salt = Encoding.Default.GetBytes(key);

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key, salt);

            byte[] buffer = Convert.FromBase64String(cypherText);

            return DecryptTextFromMemory(buffer, deriveBytes.GetBytes(24), deriveBytes.GetBytes(8));
        }

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                TripleDES tripleDes = TripleDES.Create();

                using (var cs = new CryptoStream(memoryStream, tripleDes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return memoryStream.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                TripleDES tripleDes = TripleDES.Create();

                using (var cs = new CryptoStream(memoryStream, tripleDes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cs, new UnicodeEncoding()))
                    {
                        return streamReader.ReadLine();
                    }
                }
            }
        }
    }
}