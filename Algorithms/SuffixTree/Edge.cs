
namespace Algorithms.CSharp
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Start={Start}, End={End}, Route={Route}")]
    class Edge
    {
        internal Node EndNode { get; private set; }
        internal int Start { get; private set; }
        internal int End { get; private set; }

        internal string Route
        {
            get
            {
                return GetSubstring();
            }
        }
        /// <summary>
        /// constructor that takes relative text position
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Edge(Node node, int start, int end = -1)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "start cannot be negative");
            }

            // pretend that "end" can be infinite, and then compare with start
            if (start > (uint)end)
            {
                throw new ArgumentOutOfRangeException("start", "cannot start the string after its end");
            }

            // infinity is just -1
            if (end < 0)
            {
                end = -1;
            }

            this.Start = start;
            this.End = end;
            this.EndNode = node;
        }

        private int GetLength()
        {
            return this.End < 0 ? SuffixTree.Text.Length - this.Start : this.End - this.Start + 1;
        }

        private string GetSubstring()
        {
            return SuffixTree.Text.Substring(this.Start, GetLength());
        }

        /// <summary>
        /// Splits the edge into two new edges.
        /// </summary>
        /// <param name="end">Index of the end of the old edge</param>
        /// <returns></returns>
        internal Node Split(int end, uint currentNodeNumber)
        {
            int nextStart = end + 1;
            var oldNode = this.EndNode;
            
            var newEdge = new Edge(oldNode, nextStart, this.End);
            Node newNode = new Node(currentNodeNumber);

            this.End = end;
            this.EndNode = newNode;
            newNode.Edges.Add(newEdge.Route[0], newEdge);
            return newNode;
        }

        /// <summary>
        /// Keep comparing original text from position i
        /// with what is in the edge
        /// </summary>
        /// <param name="i">Index of comparison start in the original text</param>
        /// <param name="skipCharacters"> How many characters are guaranteed equal</param>
        /// <returns>(edge, index) - the edje the character in it where the walk ended</returns>
        internal Tuple<Edge,int> WalkTheEdge(int i, ref int activeLength, ref int minDistance, ref Node activeNode)
        {
            string text = SuffixTree.Text;
            int skipCharacters = minDistance;
            int index = i + activeLength;

            // we know we do not need any comparisons on this edge
            if (skipCharacters >= this.Route.Length)
            {
                var edge = this.EndNode.FindEdgeByChar(i + this.Route.Length);
                activeLength += this.Route.Length;
                minDistance -= this.Route.Length;

                activeNode = this.EndNode;
                return edge.WalkTheEdge(i, ref activeLength, ref minDistance, ref activeNode);
            }

            int j = Walk(text, index, skipCharacters);
            return new Tuple<Edge, int>(this, j);
        }

        /// <summary>
        /// Walk this single edge to see whether it matches the substring
        /// </summary>
        /// <param name="suffix">Search string</param>
        /// <param name="i">Starting index</param>
        /// <returns></returns>
        internal int Walk(string suffix, int i, int skip = 0)
        {
            int j;
            for (j = skip, i += j; j < Route.Length && i < suffix.Length; j++, i++)
            {
                if (Route[j] != suffix[i])
                {
                    break;
                }
            }

            return j;
        }
    }
}
