
namespace Algorithms.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;

    [DebuggerDisplay("Label={Label}, Edges.Count={Edges.Count}")]
    class Node
    {
        internal uint Label { get; set; }
        internal Dictionary<char, Edge> Edges { get; private set; }

        internal Node SuffixPointer { get; set; }

        public Node(uint label)
        {
            this.Label = label;
            this.Edges = new Dictionary<char, Edge>();
            this.SuffixPointer = null;
        }

        /// <summary>
        /// finds next route starting from the current node
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        internal Tuple<Node, Edge> FindNextRoute(int start, bool followSuffixNode)
        {
            if (followSuffixNode && null != SuffixPointer)
            {
                return new Tuple<Node,Edge>(SuffixPointer, null);
            }

            var edge = FindEdgeByChar(start);
            if (null == edge)
            {
                return null;
            }

            // search terminated in a node
            if (edge.Route.Length == 1)
            {
                return new Tuple<Node, Edge>(edge.EndNode, edge);
            }

            //search did not terminate in a node
            return new Tuple<Node, Edge>(null, edge);
        }

        /// <summary>
        /// Adds a new node to the tree
        /// </summary>
        /// <param name="label">Node label</param>
        /// <param name="start">Start position in the text</param>
        /// <param name="end">End position in the text</param>
        internal void AddNode(uint label, int start, int end = -1)
        {
            var newNode = new Node(label);
            var newEdge = new Edge(newNode, start, end);
            this.Edges.Add(newEdge.Route[0], newEdge);
        }

        internal Edge FindEdgeByChar(int start)
        {
            //we have reached the end of the string
            if (start >= SuffixTree.Text.Length)
            {
                return null;
            }

            return FindEdgeByChar(SuffixTree.Text[start]);
        }

        internal Edge FindEdgeByChar(char c)
        {
            if (!this.Edges.ContainsKey(c))
            {
                return null;
            }

            return this.Edges[c];
        }
    }
}
