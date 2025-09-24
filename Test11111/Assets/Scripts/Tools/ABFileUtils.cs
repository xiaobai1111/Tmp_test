

using System.IO;
using System.Text;

public class ABFileUtils
{ 
    public static string MD5File(string filePath)
    {
        FileStream file = new FileStream(filePath, FileMode.Open);
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] retVal = md5.ComputeHash(file);
        file.Close();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < retVal.Length; i++)
        {
            sb.Append(retVal[i].ToString("x2"));
        }
          
        return sb.ToString();
    }
}