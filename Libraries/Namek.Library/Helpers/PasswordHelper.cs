using System;

namespace Namek.Library.Helpers
{
    public static class PasswordHelper
    {
        //create constant strings for each type of characters
        private const string alphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";

        private static readonly string alphaLow = "qwertyuiopasdfghjklzxcvbnm";
        private static readonly string numerics = "1234567890";

        private static readonly string special = "!@#$";

        //create another string which is a concatenation of all above
        private static readonly string allChars = alphaCaps + alphaLow + numerics + special;

        private static readonly Random r = new Random();

        public static string GenerateStrongPassword(int length)
        {
            var generatedPassword = string.Empty;

            if (length < 4)
                throw new Exception("Number of characters should be greater than 4.");

            // Generate four repeating random numbers are postions of
            // lower, upper, numeric and special characters
            // By filling these positions with corresponding characters,
            // we can ensure the password has atleast one
            // character of those types
            int pLower, pUpper, pNumber, pSpecial;
            var posArray = "0123456789";
            if (length < posArray.Length)
                posArray = posArray.Substring(0, length);
            pLower = GetRandomPosition(ref posArray);
            pUpper = GetRandomPosition(ref posArray);
            pNumber = GetRandomPosition(ref posArray);
            pSpecial = GetRandomPosition(ref posArray);

            for (var i = 0; i < length; i++)
                if (i == pLower)
                    generatedPassword += GetRandomChar(alphaCaps);
                else if (i == pUpper)
                    generatedPassword += GetRandomChar(alphaLow);
                else if (i == pNumber)
                    generatedPassword += GetRandomChar(numerics);
                else if (i == pSpecial)
                    generatedPassword += GetRandomChar(special);
                else
                    generatedPassword += GetRandomChar(allChars);
            return generatedPassword;
        }

        private static string GetRandomChar(string fullString)
        {
            return fullString.ToCharArray()[(int) Math.Floor(r.NextDouble() * fullString.Length)].ToString();
        }

        private static int GetRandomPosition(ref string posArray)
        {
            int pos;
            var randomChar = posArray.ToCharArray()[(int) Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            pos = int.Parse(randomChar);
            posArray = posArray.Replace(randomChar, "");
            return pos;
        }
    }
}