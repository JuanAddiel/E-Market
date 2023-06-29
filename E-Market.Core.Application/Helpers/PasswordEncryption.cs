using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            // Create a SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] ps = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new();
                //Convert byte a string
                for (int i = 0; i < ps.Length; i++)
                {
                    builder.Append(ps[i].ToString("x2"));

                }
                return builder.ToString();
            }


        }
    }
}
