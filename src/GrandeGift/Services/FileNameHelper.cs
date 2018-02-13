using System;

namespace GrandeGift.Services
{
    public class FileNameHelper
    {
        public static string GetNameFormat(string str)
        {
            //remove all spaces, also in the middle
            var tokens = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                //make 1st letter uppercase, other letters - lowercase
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }
            return string.Join("", tokens);
        }
    }
}
