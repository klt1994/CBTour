using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BLL
{
   public class Hash
    {
        public string GetHash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < hashBytes.Length; index++)
            {
                sb.Append(hashBytes[index].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
