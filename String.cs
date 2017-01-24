using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityClasses
{
    public static class StringExtensions
    {
        public static string ToJavaScriptSafeString(this string UnsafeString)
        {
            string SafeString = UnsafeString;
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

        public static string AppendOrOverwriteWithNumber(this string StringToAlter, int NumberToAppend, int MaxLength)
        {
            string Result = StringToAlter;
            int strLen1 = Result.Length;

            string StringToAppend = NumberToAppend.ToString();
            StringToAppend = "_" + StringToAppend;
            int strLen2 = StringToAppend.Length;

            int CharsToCopyFromOriginalString = MaxLength - (strLen1 + strLen2);

            if (CharsToCopyFromOriginalString > strLen1)
            {
                CharsToCopyFromOriginalString = strLen1;
            }
            Result = StringToAlter.Substring(0, CharsToCopyFromOriginalString);
            Result += StringToAppend;

            return Result;
        }

        public static string Left(this string OriginalString, int MaxNumberOfChars)
        {
            string Result = OriginalString;

            if (Result != null)
            {
                if (Result.Length > MaxNumberOfChars)
                {
                    Result = Result.Substring(0, MaxNumberOfChars);
                }
            }

            return Result;
        }
    }
}
