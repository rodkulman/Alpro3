using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class Graph<T>
    {
        #region Fields

        protected bool[,] matrix;
        protected List<T> vertices;

        #endregion

        #region Implementors

        public static UndirectedGraph<T> CreateUndirected()
        {
            return new UndirectedGraph<T>();
        }

        public static DirectedGraph<T> CreateDirected()
        {
            return new DirectedGraph<T>();
        }

        #endregion

        #region Constructors

        protected Graph()
        {
            this.matrix = new bool[8, 8];
            this.vertices = new List<T>();
        }

        #endregion

        #region Abstract Interface

        public abstract void AddEdge(T origin, T destination);

        #endregion

        public void AddVertice(T vertice)
        {
            if (vertice == null) { throw new ArgumentNullException("vertice"); }
            if (this.vertices.Contains(vertice)) { throw new Exception(); }

            this.vertices.Add(vertice);
            ensureMatrixSize();
        }

        public int GetDegree(T vertice)
        {
            return this.FindAllAdjacents(vertice).Count();
        }

        public IEnumerable<T> FindAllAdjacents(T vertice)
        {
            var verticeIndex = vertices.IndexOf(vertice);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[verticeIndex, i])
                {
                    yield return vertices[i];
                }
            }
        }

        #region Funcionality

        private string listItens(IEnumerable list)
        {
            var retVal = "[";

            foreach (var item in list)
            {
                retVal += item + ", ";
            }

            return retVal.Substring(0, retVal.Length - 2) + "]";
        }

        private void ensureMatrixSize()
        {
            if (this.vertices.Count > this.matrix.GetLength(0))
            {
                var newArray = new bool[this.matrix.GetLength(0) * 2, this.matrix.GetLength(0) * 2];
                Array.Copy(this.matrix, newArray, this.matrix.GetLength(0));
                this.matrix = newArray;
            }
        }

        public override string ToString()
        {
            var retVal = listItens(vertices) + "\r\n";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                retVal += "[";

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    retVal += (matrix[i, j] ? "1" : "0") + ", ";
                }

                retVal = retVal.Substring(0, retVal.Length - 2) + "]\r\n";
            }

            return retVal;
        }

        #endregion
    }
}
