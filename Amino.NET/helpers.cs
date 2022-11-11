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

        /// <summary>
        /// This function allows you to generate an Amino valid signiture based on JSON data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string : The signiture.</returns>
        public static string generate_signiture(string data)
        {
            string prefix = "42";
            string key = "f8e7a61ac3f725941e3ac7cae2d688be97f30b93";
             
            HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
            byte[] buffer = Encoding.Default.GetBytes(data);
            byte[] result = hmac.ComputeHash(buffer);
            return Convert.ToBase64String(CombineTwoArrays(StringToByteArray(prefix), result));
        }

        /// <summary>
        /// This function allows you to generate an Amino valid signiture for file uploads
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string : The signiture.</returns>
        public static string generate_file_signiture(byte[] data)
        {
            string prefix = "42";
            string key = "f8e7a61ac3f725941e3ac7cae2d688be97f30b93";
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

        /// <summary>
        /// This function allows you to get the Type ID based on Amino.Types.Object_Types
        /// </summary>
        /// <param name="type"></param>
        /// <returns>int : the object ID index thingie</returns>
        public static int get_ObjectTypeID(Types.Object_Types type)
        {
            int _type;
            switch (type)
            {
                case Types.Object_Types.User:
                    _type = 0;
                    break;
                case Types.Object_Types.Blog:
                    _type = 1;
                    break;
                case Types.Object_Types.Item:
                    _type = 2;
                    break;
                case Types.Object_Types.Comment:
                    _type = 3;
                    break;
                case Types.Object_Types.Blog_Category:
                    _type = 4;
                    break;
                case Types.Object_Types.Blog_Category_Item_Tag:
                    _type = 5;
                    break;
                case Types.Object_Types.Featured_Item:
                    _type = 6;
                    break;
                case Types.Object_Types.Chat_Message:
                    _type = 7;
                    break;
                case Types.Object_Types.Reputationlog_Item:
                    _type = 10;
                    break;
                case Types.Object_Types.Poll_Option:
                    _type = 11;
                    break;
                case Types.Object_Types.Chat_Thread:
                    _type = 12;
                    break;
                case Types.Object_Types.Community:
                    _type = 16;
                    break;
                case Types.Object_Types.Image:
                    _type = 100;
                    break;
                case Types.Object_Types.Music:
                    _type = 101;
                    break;
                case Types.Object_Types.Video:
                    _type = 102;
                    break;
                case Types.Object_Types.YouTube:
                    _type = 103;
                    break;
                case Types.Object_Types.Shared_Folder:
                    _type = 106;
                    break;
                case Types.Object_Types.Shared_File:
                    _type = 109;
                    break;
                case Types.Object_Types.Voice:
                    _type = 110;
                    break;
                case Types.Object_Types.Moderation_Task:
                    _type = 111;
                    break;
                case Types.Object_Types.Screenshot:
                    _type = 112;
                    break;
                case Types.Object_Types.Sticker:
                    _type = 113;
                    break;
                case Types.Object_Types.Sticker_Collection:
                    _type = 114;
                    break;
                case Types.Object_Types.Prop:
                    _type = 115;
                    break;
                case Types.Object_Types.Chat_Bubble:
                    _type = 116;
                    break;
                case Types.Object_Types.Video_Filter:
                    _type = 117;
                    break;
                case Types.Object_Types.Order:
                    _type = 118;
                    break;
                case Types.Object_Types.Share_Request:
                    _type = 119;
                    break;
                case Types.Object_Types.VV_Chat:
                    _type = 120;
                    break;
                case Types.Object_Types.P2A:
                    _type = 121;
                    break;
                case Types.Object_Types.Amino_Video:
                    _type = 123;
                    break;
                default:
                    _type = 0;
                    break;
            }
            return _type;
        }

    }
}
