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
            g.AddVertice("CWB");
            g.AddVertice("SDU");
            g.AddEdge("POA", "CWB");
            g.AddEdge("POA", "SDU");

            Console.WriteLine(g.ToString());
            Console.WriteLine("Adjacents count: " + g.FindAllAdjacents("POA").Count());            

#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadLine();
#endif
        }
    }
}
