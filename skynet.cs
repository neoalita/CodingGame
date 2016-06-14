using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CodingGame.Moyen.SkynetLeVirus
{
    class Player
    {
        static void Main(string[] args)
        {
            Action<object> writeLine = Console.WriteLine;
            Func<string> readLine = Console.ReadLine;

            var inputs = readLine().Split(' ');

            var N = int.Parse(inputs[0]);
            var L = int.Parse(inputs[1]);
            var G = int.Parse(inputs[2]);

            var graph = Enumerable.Range(0, N).ToDictionary(i => i, i => new Node(i));

            // Add links on nodes
            for (var i = 0; i < L; i++)
            {
                inputs = readLine().Split(' ');
                var from = int.Parse(inputs[0]);
                var to = int.Parse(inputs[1]);
                graph[from].Childs.Add(to);
                graph[to].Childs.Add(from);
            }

            // Set gateway
            for (var i = 0; i < G; i++)
            {
                graph[int.Parse(readLine())].SetAsGateway();
            }

            var skynet = new Skynet(graph);
            while (true)
            {
                writeLine(skynet.Move(int.Parse(readLine())));
            }
        }

        class Node
        {
            public bool IsGateway { get; private set; }

            public int Id { get; }

            public IList<int> Childs { get; } = new List<int>();

            public int Weight { get; set; } = int.MaxValue;

            public Node(int id)
            {
                Id = id;
            }

            public void SetAsGateway()
            {
                IsGateway = true;
                Weight = 1;
            }

            protected bool Equals(Node other)
            {
                return Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Node)obj);
            }

            public override int GetHashCode()
            {
                return Id;
            }
        }

        class Skynet
        {
            private readonly IDictionary<int, Node> graph;

            public Skynet(IDictionary<int, Node> graph)
            {
                this.graph = graph;
            }

            public string Move(int virusNodeId)
            {
                Tuple<int, int> result;
                if (TryFindGatewayLink(virusNodeId, out result))
                {
                    return $"{result.Item1} {result.Item2}";
                }

                // Find risky nodes (i.e node that have more than one gateway exit)

                // If nodes found 
                //      For each risky nodes, find path (dijkstra)
                //      For each path, find node with


                // If nodes not found
                //      Remove random link

                return string.Empty;
            }

            private bool TryFindGatewayLink(int virusNodeId, out Tuple<int, int> result)
            {
                result = null;

                var gateways = graph[virusNodeId].Childs.Select(x => graph[x]).Where(x => x.IsGateway).ToList();
                if (gateways.Count > 1)
                {
                    throw new Exception("More than one gateway was found.");
                }

                if (gateways.Count == 0)
                {
                    return false;
                }

                var id = gateways[0].Id;
                result = Tuple.Create(Math.Min(id, virusNodeId), Math.Max(id, virusNodeId));
                return true;
            }

        }
    }

}
