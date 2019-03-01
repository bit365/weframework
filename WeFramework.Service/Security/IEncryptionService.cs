using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeFramework.Service.Security
{
    public interface IEncryptionService
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string password);

        string Encrypt(string plainText, string key);

        string Decrypt(string cypherText, string key);
    }
}
