namespace SaveLoad
{
    public interface ICryptographyService
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}