using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace pucrs.TelDor
{
    public static class Algorithms
    {
        public static IEnumerable<int> DoWork(params int[] numbers)
        {
            var groups = numbers.Select(i => ToBase(i, 6)).OrderBy(s => s).GroupBy(s => s.Length);

            foreach (var group in groups)
            {
                for (int i = 0; i < group.Count() - 1; i++)
                {

                }
            }
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
    }
}
