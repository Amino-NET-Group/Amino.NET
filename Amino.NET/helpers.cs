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

        /// <summary>
        /// This value represents the baseURL to Aminos REST backend
        /// </summary>
        public static string BaseUrl = "https://service.aminoapps.com/api/v1";

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

        /// <summary>
        /// This function allows you to generate an Amino valid signiture based on JSON data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string : The signiture.</returns>
        public static string generate_signiture(string data)
        {
            string prefix = "19";
            string key = "DFA5ED192DDA6E88A12FE12130DC6206B1251E44".ToLower();
             
            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] buffer = Encoding.Default.GetBytes(data);
            byte[] result = hmac.ComputeHash(buffer);
            return Convert.ToBase64String(CombineTwoArrays(StringToByteArray(prefix), result));
        }



        public static string generate_transaction_id()
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[16];
            rng.GetBytes(buffer);
            var hex = BitConverter.ToString(buffer).Replace("-", "").ToLower();
            var uuid = Guid.ParseExact(hex, "N");
            return uuid.ToString();
        }


        /// <summary>
        /// Returns the current Amino compatible Timezone
        /// </summary>
        /// <returns></returns>
        public static int getTimezone()
        {
            return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Divide(1000).Seconds;
        }

        /// <summary>
        /// This function allows you to generate an Amino valid signiture for file uploads
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string : The signiture.</returns>
        public static string generate_file_signiture(byte[] data)
        {
            string prefix = "19";
            string key = "DFA5ED192DDA6E88A12FE12130DC6206B1251E44".ToLower();
            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] result = hmac.ComputeHash(data);
            return Convert.ToBase64String(CombineTwoArrays(StringToByteArray(prefix), result));
        }

        /// <summary>
        /// This function allows you to generate an Amino valid device ID
        /// </summary>
        /// <returns>string : The Device ID</returns>
        public static string generate_device_id()
        {
            string prefix = "19";
            string key = "E7309ECC0953C6FA60005B2765F99DBBC965C8E9".ToLower();

            Random rnd = new Random();
            byte[] identifier = new byte[20];
            rnd.NextBytes(identifier);
            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] buffer = CombineTwoArrays(StringToByteArray(prefix), identifier);
            string result = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
            return (prefix + BitConverter.ToString(identifier).Replace("-", "").ToLower() + result).ToUpper();
        }

        /// <summary>
        /// This function allows you to get the current UNIX Timestamp (NOT Amino valid)
        /// </summary>
        /// <returns>double : The current UNIX Timestamp</returns>
        public static double GetTimestamp()
        {

            TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            TimeSpan unixTicks = new TimeSpan(DateTime.UtcNow.Ticks) - epochTicks;
            double unixTime = unixTicks.TotalSeconds;

            return unixTime;
        }


        public static int TzFilter()
        {
            string localhour = DateTime.UtcNow.ToString("HH");
            string localminute = DateTime.UtcNow.ToString("mm");
            Dictionary<string, string> UTC = new Dictionary<string, string> {
                { "GMT0", "+0" },
                { "GMT1", "+60" },
                { "GMT2", "+120" },
                { "GMT3", "+180" },
                { "GMT4", "+240" },
                { "GMT5", "+300" },
                { "GMT6", "+360" },
                { "GMT7", "+420" },
                { "GMT8", "+480" },
                { "GMT9", "+540" },
                { "GMT10", "+600" },
                { "GMT11", "+660" },
                { "GMT12", "+720" },
                { "GMT13", "+780" },
                { "GMT-1", "-60" },
                { "GMT-2", "-120" },
                { "GMT-3", "-180" },
                { "GMT-4", "-240" },
                { "GMT-5", "-300" },
                { "GMT-6", "-360" },
                { "GMT-7", "-420" },
                { "GMT-8", "-480" },
                { "GMT-9", "-540" },
                { "GMT-10", "-600" },
                { "GMT-11", "-660" }
            };
            string[] hour = new string[] { localhour, localminute };
            switch (hour[0])
            {
                case "00":
                    return int.Parse(UTC["GMT-1"]);
                case "01":
                    return int.Parse(UTC["GMT-2"]);
                case "02":
                    return int.Parse(UTC["GMT-3"]);
                case "03":
                    return int.Parse(UTC["GMT-4"]);
                case "04":
                    return int.Parse(UTC["GMT-5"]);
                case "05":
                    return int.Parse(UTC["GMT-6"]);
                case "06":
                    return int.Parse(UTC["GMT-7"]);
                case "07":
                    return int.Parse(UTC["GMT-8"]);
                case "08":
                    return int.Parse(UTC["GMT-9"]);
                case "09":
                    return int.Parse(UTC["GMT-10"]);
                case "10":
                    return int.Parse(UTC["GMT13"]);
                case "11":
                    return int.Parse(UTC["GMT12"]);
                case "12":
                    return int.Parse(UTC["GMT11"]);
                case "13":
                    return int.Parse(UTC["GMT10"]);
                case "14":
                    return int.Parse(UTC["GMT9"]);
                case "15":
                    return int.Parse(UTC["GMT8"]);
                case "16":
                    return int.Parse(UTC["GMT7"]);
                case "17":
                    return int.Parse(UTC["GMT6"]);
                case "18":
                    return int.Parse(UTC["GMT5"]);
                case "19":
                    return int.Parse(UTC["GMT4"]);
                case "20":
                    return int.Parse(UTC["GMT3"]);
                case "21":
                    return int.Parse(UTC["GMT2"]);
                case "22":
                    return int.Parse(UTC["GMT1"]);
                case "23":
                    return int.Parse(UTC["GMT0"]);
                default:
                    return int.Parse(UTC["GMT-1"]);

            }
        }

        public static List<Dictionary<string, long>> getTimeData()
        {
            long t = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            List<Dictionary<string, long>> timers = new List<Dictionary<string, long>>();
            for (int i = 0; i < 50; i++)
            {
                long start = t + (i * 300);
                long end = start + 300;
                Dictionary<string, long> timer = new Dictionary<string, long>
                {
                    {"start", start},
                    {"end", end}
                };
                timers.Add(timer);
            }
            return timers;
        }

    }
}
