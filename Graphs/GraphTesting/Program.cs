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
            var g = Graph<string>.CreateUndirected();

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

#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadLine();
#endif
        }
    }
}
