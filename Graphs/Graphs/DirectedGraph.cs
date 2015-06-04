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
            for (int i = 0; i < base.vertices.Count; i++)
            {
                if (hasEntry(i)) { continue; }
                if (hasExit(i)) { yield return base.vertices[i]; }
            }
        }

        private bool hasExit(int verticeIndex)
        {
            for (int i = 0; i < base.vertices.Count; i++)
            {
                if (base.matrix[verticeIndex, i]) { return true; }
            }

            return false;
        }

        private bool hasEntry(int verticeIndex)
        {
            for (int i = 0; i < base.vertices.Count; i++)
            {
                if (base.matrix[i, verticeIndex]) { return true; }
            }

            return false;
        }

        public IEnumerable<T> GetSinks()
        {
            for (int i = 0; i < base.vertices.Count; i++)
            {
                if (hasExit(i)) { continue; }
                if (hasEntry(i)) { yield return base.vertices[i]; }
            }
        }
    }
}
