using System;
using System.Text.RegularExpressions;

namespace Pastime
{
    public static class Validator
    {
        //validate Email
        public static bool EmailRegex(string text)
        {
            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            if (matches.Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //validate Username
        public static bool UsernameRegex(string text)
        {
            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"/^[a-zA-Z- ]*$/",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(text);

            if (matches.Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //validate Password length
        public static bool PasswordLength(string text)
        {
            int password_length = text.Length;
            if (password_length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //compare password and verify_password
        public static bool ComparePasswords(string password, string verify_password)
        {
            if (password != verify_password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
