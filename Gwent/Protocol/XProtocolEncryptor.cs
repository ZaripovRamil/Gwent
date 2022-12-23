namespace Protocol;

public class XProtocolEncryptor
{
    private static string Key { get; } = "2e985f930";

    [Obsolete("Obsolete")]
    public static byte[] Encrypt(byte[] data)
    {
        return RijndaelHandler.Encrypt(data, Key);
    }

    [Obsolete("Obsolete")]
    public static byte[] Decrypt(byte[] data)
    {
        return RijndaelHandler.Decrypt(data, Key);
    }
}