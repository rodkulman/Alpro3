using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pucrs.TelDor;
using System.IO;

namespace pucrs.TelDor.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("Results");

            foreach (var testCaseFile in Directory.EnumerateFiles("Cases"))
            {
                var numbers = new List<int>();

                using (var reader = new StreamReader(testCaseFile))
                {
                    while (!reader.EndOfStream)
                    {
                        numbers.Add(int.Parse(reader.ReadLine()));
                    }
                }

                Console.WriteLine("Processing file: " + Path.GetFileName(testCaseFile));

                using (var writer = new StreamWriter(Path.Combine("Results", Path.GetFileName(testCaseFile)), false))
                {
                    writer.WriteLine("[" + inLine(Algorithms.DoWork(numbers)) + "]");
                    writer.WriteLine("[" + inLine(Algorithms.DoWork(numbers).Select(n => Algorithms.ToBase(n, 6))) + "]");
                }
            }

            Console.WriteLine("Done!");
#if DEBUG
            Console.ReadLine();
#endif
        }

        static string inLine<T>(IEnumerable<T> list)
        {
            var retVal = string.Empty;

            foreach (var item in list)
            {
                retVal += item + ", ";
            }

            return retVal.Substring(0, retVal.Length - 2);
        }
    }
}
