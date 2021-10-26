using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Common.Tools
{
    class MD5Encoder
    {
        public static string Encode(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            byte[] encodedBytes = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in encodedBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
