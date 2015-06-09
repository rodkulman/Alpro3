using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;

namespace GraphTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = Graph<string>.CreateDirected();

            g.AddVertice("POA");
            g.AddVertice("GRU");
            g.AddVertice("CWB");
            g.AddVertice("SDU");
            g.AddVertice("JFK");

            g.AddEdge("POA", "CWB");
            g.AddEdge("POA", "SDU");
            g.AddEdge("GRU", "CWB");

            Console.WriteLine(g.ToString());

            Console.WriteLine("Reachable from POA: " + g.CountNodesReachableFrom("POA"));
            Console.WriteLine("Reachable from GRU: " + g.CountNodesReachableFrom("GRU"));
            Console.WriteLine("Reachable from CWB: " + g.CountNodesReachableFrom("CWB"));

            Console.WriteLine("1 levels ahead of POA: " + displayList(g.GetLevelsAhead("POA", 1)));
            Console.WriteLine("2 levels ahead of POA: " + displayList(g.GetLevelsAhead("POA", 2)));
            Console.WriteLine("3 levels ahead of POA: " + displayList(g.GetLevelsAhead("POA", 3)));

#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadLine();
#endif
        }

        private static string displayList<T>(IEnumerable<T> list)
        {
            if (list.Count() == 0) { return string.Empty; }

            var retVal = string.Empty;

            foreach (var item in list)
            {
                retVal += item + ",";
            }

            return retVal.Substring(0, retVal.Length - 1);
        }
    }
}
