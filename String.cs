using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityClasses
{
    public static class StringExtensions
    {
        public static string ToJavaScriptSafeString(this string unsafeString)
        {
            string SafeString = unsafeString;
            if (SafeString == null)
            {
                SafeString = "";
            }
            else
            {
                SafeString = SafeString.Replace("\\", "\\\\");
                SafeString = SafeString.Replace("'", "\\'");
                SafeString = SafeString.Replace("\"", "\\\"");
                SafeString = SafeString.Replace("\r", "\\r");
                SafeString = SafeString.Replace("\n", "\\n");
                SafeString = SafeString.Replace("\t", "\\t");
            }

            return SafeString;
        }

        public static string AppendOrOverwriteWithNumber(this string stringToAlter, int numberToAppend, int maxLength)
        {
            string Result = stringToAlter;
            int strLen1 = Result.Length;

            string StringToAppend = numberToAppend.ToString();
            StringToAppend = "_" + StringToAppend;
            int strLen2 = StringToAppend.Length;

            int CharsToCopyFromOriginalString = maxLength - (strLen1 + strLen2);

            if (CharsToCopyFromOriginalString > strLen1)
            {
                CharsToCopyFromOriginalString = strLen1;
            }
            Result = stringToAlter.Substring(0, CharsToCopyFromOriginalString);
            Result += StringToAppend;

            return Result;
        }

        public static string Left(this string originalString, int maxNumberOfChars)
        {
            string Result = originalString;

            if (Result != null)
            {
                if (Result.Length > maxNumberOfChars)
                {
                    Result = Result.Substring(0, maxNumberOfChars);
                }
            }

            return Result;
        }

        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find, StringComparison.Ordinal);

            if (place == -1)
                return source;

            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }
    }
}
