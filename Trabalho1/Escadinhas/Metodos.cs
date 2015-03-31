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
        public static TimeSpan BruteForce(bool allowRepetition, int workingBase)
        {
            var initTime = DateTime.Now;

            var last = (long)Math.Pow(workingBase, workingBase);

            using (var stream = new StreamWriter(File.Open("numbers.log", FileMode.Create)))
            {
                for (long i = 0; i < last; i++)
                {
                    var number = ToBase(i, workingBase);
                    if (checkRules(number, allowRepetition))
                    {
                        stream.WriteLine(number);
                    }
                }
            }

            return DateTime.Now - initTime;
        }

        public static TimeSpan SmartList(bool allowRepetition, int workingBase)
        {
            var initTime = DateTime.Now;

            var last2Digits = Math.Pow(workingBase, 2);
            var listOfDigits = new List<long>[workingBase - 1];
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

            for (long i = workingBase; i < last2Digits; i++)
            {
                var number = ToBase(i, workingBase);
                if (checkRules(number, allowRepetition))
                {
                    listOfDigits[dimension - 2].Add(i);
                }
            }

            while (++dimension <= workingBase)
            {
                foreach (var verifiedNumber in listOfDigits[dimension - 3])
                {
                    var low = (int)FromBase(ToBase(verifiedNumber, workingBase).Substring(0, 1), workingBase) - 2;
                    var high = low + 4;

                    if (low < 1) { low = 1; }
                    if (high > workingBase - 1) { high = workingBase - 1; }

                    for (int i = low; i <= high; i++)
                    {
                        var number = ToBase(i, workingBase) + ToBase(verifiedNumber, workingBase);
                        if (checkRules(number, allowRepetition))
                        {
                            listOfDigits[dimension - 2].Add(FromBase(number, workingBase));
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
                        stream.WriteLine(ToBase(number, workingBase));
                    }
                }
            }

            return DateTime.Now - initTime;
        }

        #region Base Conversion

        private static string ToBase(long number, int workingBase)
        {
            var retVal = string.Empty;

            while (number >= workingBase)
            {
                var mod = number % workingBase;
                retVal += mod == 10 ? "A" : mod.ToString();

                number = number / workingBase;
            }

            retVal += number == 10 ? "A" : number.ToString();

            return new string(retVal.Reverse().ToArray());
        }

        private static long FromBase(string number, int workingBase)
        {
            var retVal = 0L;

            for (int i = 0; i < number.Length; i++)
            {
                retVal += (long)Math.Pow(workingBase, i) * long.Parse(number.Substring(number.Length - 1 - i, 1), NumberStyles.AllowHexSpecifier);
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
