using System.Security.Cryptography;
using System.Text;

namespace TableReservation.Helpers
{
    internal class PassHash
    {
        public string HashedPassword { get; set; }

        /// <summary>
        /// Helper class for password hasing. storing values to HashedPassword property
        /// </summary>
        /// <param name="rawData">input password string</param>
        public PassHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                HashedPassword = builder.ToString();
            }
        }
    }
}
