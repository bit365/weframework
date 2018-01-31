using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeFramework.Service.Security;

namespace WeFramework.Test.ServiceTest
{
    [TestClass]
    public class EncryptionServiceTest
    {
        private static IEncryptionService encryptionService;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            encryptionService = new EncryptionService();
        }

        [TestMethod]
        public void CanHashPassword()
        {
            Assert.IsNotNull(encryptionService.HashPassword(""));
        }

        [TestMethod]
        public void CanVerifyHashedPassword()
        {
            string hashPwd = encryptionService.HashPassword("gdsszws2017");
            Assert.IsTrue(encryptionService.VerifyHashedPassword(hashPwd, "bnwj005"));
        }

        [TestMethod]
        public void EncryptDecryptTest()
        {
            string key = "123456464hdhdhdhdhdghhdfghd78";

            string str = encryptionService.Encrypt("123456", key);

            string str2 = encryptionService.Decrypt(str, key);

            Assert.AreEqual("123456", str2);
        }
    }
}
