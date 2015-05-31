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
        public static IEnumerable<int> DoWork(IEnumerable<int> numbers)
        {
            var groups = numbers.Select(i => ToBase(i, 6)).OrderBy(s => s).GroupBy(s => s.Length);

            var sequences = new List<List<int>>();

            foreach (var group in groups)
            {
                for (int i = 0; i < group.Count(); i++)
                {
                    var current = group.ElementAt(i);
                    var sequence = new List<int>();

                    sequence.Add(FromBase(current, 6));

                    for (int j = i + 1; j < group.Count(); j++)
                    {
                        var next = group.ElementAt(j);

                        if (differByOneDigit(current, next))
                        {
                            sequence.Add(FromBase(next, 6));
                            current = next;
                        }
                    }

                    sequences.Add(sequence);
                }
            }

            // returns the sequence with the most numbers
            return sequences.OrderBy(l => l.Count).Last();
        }

        private static bool differByOneDigit(string current, string next)
        {
            var ocurrences = 0;

            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] != next[i])
                {
                    ocurrences++;
                }
            }

            return ocurrences == 1;
        }

        #region Base Conversion

        public static string ToBase(int number, int workingBase)
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

        public static int FromBase(string number, int workingBase)
        {
            var retVal = 0;

            for (int i = 0; i < number.Length; i++)
            {
                retVal += (int)Math.Pow(workingBase, i) * int.Parse(number.Substring(number.Length - 1 - i, 1), NumberStyles.AllowHexSpecifier);
            }

            return retVal;
        }

        #endregion
    }
}
