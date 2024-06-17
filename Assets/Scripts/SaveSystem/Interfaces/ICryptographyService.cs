using Cysharp.Threading.Tasks;

namespace SaveLoad
{
    public interface ICryptographyService
    {
        UniTask<byte[]> Encrypt(byte[] data);
        UniTask<byte[]> Decrypt(byte[] data);
    }
}