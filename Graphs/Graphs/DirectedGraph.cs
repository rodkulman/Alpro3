using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    public class DirectedGraph<T> : Graph<T>
    {
        public override void AddEdge(T origin, T destination)
        {
            if (origin == null) { throw new ArgumentNullException("origin"); }
            if (destination == null) { throw new ArgumentNullException("destination"); }

            var originIndex = base.vertices.IndexOf(origin);
            var destIndex = base.vertices.IndexOf(destination);

            base.matrix[originIndex, destIndex] = true;
        }

        public IEnumerable<T> GetSources()
        {
            yield break;
        }

        public IEnumerable<T> GetSinks()
        {
            yield break;
        }
    }
}
