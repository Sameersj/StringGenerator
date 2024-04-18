using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StringGenerator.Models
{
    public class StringProperties
    {
        public int ID { get; set; }
        public int StringCount { get; set; }
        public int StringLength { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsUpperCase { get; set; }
        public bool IsLowerCase { get; set; }
        public bool IsUniqueString { get; set; }
        public string RandomString { get; set; }

        public static List<string> GenerateRandomString(int stringCount, int stringLength, bool isNumeric, bool isUpperCase, bool isLowerCase, bool isUniqueString)
        {
            List<string> randomStrings = new List<string>();
            Random random = new Random();
            string chars = "";
            if (isNumeric)
            {
                chars += "0123456789";
            }
            if (isUpperCase)
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (isLowerCase)
            {
                chars += "abcdefghijklmnopqrstuvwxyz";
            }
            if (chars == "")
            {
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            }
            for (int i = 0; i < stringCount; i++)
            {
                string randomString = "";
                for (int j = 0; j < stringLength; j++)
                {
                    randomString += chars[random.Next(chars.Length)];
                }
                randomStrings.Add(randomString);
            }
            if (isUniqueString)
            {
                randomStrings = randomStrings.Distinct().ToList();
            }
            return randomStrings;
        }
    }
   
}