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
        public static void BruteForce()
        {
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
                    if (Math.Abs(long.Parse(number.Substring(i, 1), NumberStyles.AllowHexSpecifier) - long.Parse(number.Substring(i-1, 1), NumberStyles.AllowHexSpecifier)) > 2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
