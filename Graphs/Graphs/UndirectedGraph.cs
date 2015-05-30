using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    public class UndirectedGraph<T> : Graph<T>
    {
        // internal
        internal UndirectedGraph() : base() { }

        #region Graph Implementation

        public override void AddEdge(T origin, T destination)
        {
            if (origin == null) { throw new ArgumentNullException("origin"); }
            if (destination == null) { throw new ArgumentNullException("destination"); }

            var originIndex = vertices.IndexOf(origin);
            var destIndex = vertices.IndexOf(destination);

            base.matrix[originIndex, destIndex] = base.matrix[destIndex, originIndex] = true;
        }

        #endregion
    }
}
