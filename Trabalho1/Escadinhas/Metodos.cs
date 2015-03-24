using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Escadinhas
{
    public static class Metodos
    {
        public static TimeSpan BruteForce()
        {
            var initTime = DateTime.Now;

            var last = FromBase11("AAAAAAAAAAA");

            using (var stream = new StreamWriter(File.Open("numbers.log", FileMode.Create)))
            {
                for (long i = 0; i <= last; i++)
                {
                    var number = ToBase11(i);
                    if (checkRules(number, false))
                    {
                        stream.WriteLine(number);
                    }
                }
            }

            return DateTime.Now - initTime;
        }

        public static TimeSpan SmartList()
        {
            var initTime = DateTime.Now;

            var last2Digits = FromBase11("AA");
            var listOfDigits = new List<long>[10];
            var dimension = 2;

            /*
             * The ideia behind this smart list is that
             * Every matching number ends with the number from the previous interations
             * The numbers that have n-1 digits
             * 
             * e.g.
             * 
             * In 2-digits numbers, we can't have 11, therefore we can't have a 3-digit number that ends with 11
             * Therefore, we don't need to check those
             * 
             * **** WE JUST HAVE TO CHECK THOSE NUMBERS THAT END WITH DIGITS THAT WE ALREADY CHECKED ****
             * 
             * The only exception to this rule is 2-digit, because we can't have 1 digit numbers
             * Those have to be brute forced.
             * 
             */ 

            for (int i = 0; i < listOfDigits.Length; i++)
            {
                listOfDigits[i] = new List<long>();
            }

            for (long i = 0; i < last2Digits; i++)
            {
                var number = ToBase11(i);
                if (checkRules(number, false))
                {
                    listOfDigits[dimension - 2].Add(i);
                }
            }

            while (++dimension <= 11)
            {
                for (int i = 0; i < 11; i++)
                {
                    foreach (var verifiedNumber in listOfDigits[dimension - 3])
                    {
                        var number = ToBase11(i) + ToBase11(verifiedNumber);
                        if (checkRules(number, false))
                        {
                            listOfDigits[dimension - 2].Add(FromBase11(number));
                        }
                    }
                }
            }

            using (var stream = new StreamWriter(File.Open("numbers.log", FileMode.Create)))
            {
                foreach (var lst in listOfDigits)
                {
                    foreach (var number in lst)
                    {
                        stream.WriteLine(ToBase11(number));
                    }
                }
            }

            return DateTime.Now - initTime;
        }

        #region Base 11 Conversion

        private static string ToBase11(long number)
        {
            var retVal = string.Empty;

            while (number >= 11)
            {
                var mod = number % 11;
                retVal += mod == 10 ? "A" : mod.ToString();

                number = number / 11;
            }

            retVal += number == 10 ? "A" : number.ToString();

            return new string(retVal.Reverse().ToArray());
        }

        private static long FromBase11(string number)
        {
            var retVal = 0L;

            for (int i = 0; i < number.Length; i++)
            {
                retVal += (long)Math.Pow(11, i) * long.Parse(number.Substring(number.Length - 1 - i, 1), NumberStyles.AllowHexSpecifier);
            }

            return retVal;
        }

        #endregion

        /// <complexity>N²</complexity>
        private static bool checkRules(string number, bool allowRepetitions)
        {
            var chars = new char[number.Length];

            for (int i = 0; i < number.Length; i++)
            {
                if (!allowRepetitions && chars.Contains(number[i])) { return false; }
                chars[i] = number[i];

                if (i > 0)
                {
                    if (Math.Abs(long.Parse(number.Substring(i, 1), NumberStyles.AllowHexSpecifier) - long.Parse(number.Substring(i - 1, 1), NumberStyles.AllowHexSpecifier)) > 2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
