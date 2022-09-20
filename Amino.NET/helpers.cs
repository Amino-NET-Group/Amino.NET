using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Amino
{
    public class helpers
    {

        public static string BaseUrl = "https://service.narvii.com/api/v1";

        private static T[] CombineTwoArrays<T>(T[] a1, T[] a2)
        {
            T[] arrayCombined = new T[a1.Length + a2.Length];
            Array.Copy(a1, 0, arrayCombined, 0, a1.Length);
            Array.Copy(a2, 0, arrayCombined, a1.Length, a2.Length);
            return arrayCombined;
        }


        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string generate_signiture(string data)
        {
            string prefix = "42";
            string key = "f8e7a61ac3f725941e3ac7cae2d688be97f30b93";

            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] buffer = Encoding.Default.GetBytes(data);
            byte[] result = hmac.ComputeHash(buffer);
            return Convert.ToBase64String(CombineTwoArrays(StringToByteArray(prefix), result));
        }


        public static string generate_device_id()
        {
            string prefix = "42";
            string key = "02b258c63559d8804321c5d5065af320358d366f";

            Random rnd = new Random();
            byte[] identifier = new byte[20];
            rnd.NextBytes(identifier);
            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] buffer = CombineTwoArrays(StringToByteArray(prefix), identifier);
            string result = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
            return (prefix + BitConverter.ToString(identifier).Replace("-", "").ToLower() + result).ToUpper();
        }






    }
}
