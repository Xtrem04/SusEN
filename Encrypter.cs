using System.Text;

namespace SusEN
{
    internal class Encrypter
    {
        internal static string EncryptString(string StringToEncrypt)
        {
            byte[] Data = Encoding.ASCII.GetBytes(StringToEncrypt);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            string Hash = Encoding.ASCII.GetString(Data);
            return Hash;
        }
    }
}