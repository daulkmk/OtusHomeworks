using System.IO;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public class AesCryptographyService : ICryptographyService
    {
        private readonly byte[] _key = new byte[] { 199, 5, 123, 36, 95, 156, 54, 234, 73, 117, 125, 0, 234, 60, 81, 186, 232, 181, 125, 171, 28, 91, 231, 248, 171, 141, 57, 225, 174, 163, 150, 149 };
        private readonly byte[] _iv = new byte[] { 207, 121, 59, 180, 223, 98, 163, 192, 141, 52, 37, 232, 46, 22, 247, 52 };

        public async UniTask<byte[]> Encrypt(byte[] data)
        {
            using var aes = Aes.Create();

            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.Zeros;

            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            return await PerformCryptography(data, encryptor);
        }

        public async UniTask<byte[]> Decrypt(byte[] data)
        {
            using var aes = Aes.Create();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.Zeros;

            aes.Key = _key;
            aes.IV = _iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            return await PerformCryptography(data, decryptor);
        }

        private async UniTask<byte[]> PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            await using var ms = new MemoryStream();
            await using var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

            await cryptoStream.WriteAsync(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            return ms.ToArray();
        }
    }
}