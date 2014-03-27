using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using ReversiLab.AI;
using ReversiLab.Rules;

namespace ReversiLab.Graph
{
    using Graph = AdjacencyGraph<GameNode, Edge<GameNode>>;

    internal class MinMax
    {
        private readonly int _depth;
        private readonly IBoardStateEvaluator _evaluator;
        private readonly int _player;


        public MinMax(IBoardStateEvaluator evaluator, int player, int depth)
        {
            _evaluator = evaluator;
            _player = player;
            _depth = depth;
        }

        public String GetBestMove(Game game)
        {
            Graph graph = BuildTree(game, _depth);
            //Console.WriteLine("Graph Size:" + graph.VertexCount);
            //long memory = GC.GetTotalMemory(true);
            //Console.WriteLine("Memory Usage" + memory/1000 + " Kb");
            GameNode root = graph.Vertices.First();
            GameNode gameNode = GetMax(graph, root, 0);
            return gameNode.Move;
        }

        private GameNode GetMax(Graph graph, GameNode node, int depth)
        {
            IList<Edge<GameNode>> outEdges = graph.OutEdges(node).ToList();
            if (!outEdges.Any())
            {
                return null;
            }

            foreach (var edge in outEdges)
            {
                GetMax(graph, edge.Target, depth + 1);
            }

            if (depth%2 == 0) //Max Level
            {
                
                GameNode maxOf = MaxOf(outEdges);
                node.Value = maxOf.Value;
                return maxOf;
            }

            GameNode minOf = MinOf(outEdges); //Min Level
            node.Value = minOf.Value;
            return minOf;
        }


        private GameNode MaxOf(IList<Edge<GameNode>> nodes)
        {
            if (nodes.First().Target.Player != _player)
            {
                return MinOf(nodes);
            }
            return nodes.OrderByDescending(i => i.Target.Value).FirstOrDefault().Target;
        }

        private GameNode MinOf(IList<Edge<GameNode>> nodes)
        {
            if (nodes.First().Target.Player == _player)
            {
                return MaxOf(nodes);
            }
            return nodes.OrderByDescending(i => i.Target.Value).LastOrDefault().Target;
        }

        private void Traverse(Graph graph, GameNode node)
        {
            Console.WriteLine(node.Id + " : " + node.Value);

            foreach (var edge in graph.OutEdges(node))
            {
                if (graph.OutEdges(edge.Target).Any())
                {
                    Traverse(graph, edge.Target);
                }
                else
                {
                    Console.WriteLine(edge.Target.Id + " : " + edge.Target.Value);
                }
            }
        }


        private Graph BuildTree(Game currentGame, int depth)
        {
            var graph = new Graph(false);
            var currentNode = new GameNode("00", null, _player);
            graph.AddVertex(currentNode);
            BuildTree(graph, currentGame, currentNode, depth);
            return graph;
        }

        private void BuildTree(Graph graph, Game parentGame, GameNode parentNode, int depth)
        {
            //A potantial next parentNode may have no available moves
            if (depth == 0 || parentGame.AvailableMoves == null)
            {
                return;
            }
            bool leafNode = depth == 1;
            foreach (string move in parentGame.AvailableMoves)
            {
                Game childGame = GameService.Generate(parentGame, move, leafNode);
                string id = parentNode.Id + "=>" + move;
                var childNode = new GameNode(id, move, parentGame.CurrentPlayer)
                {
                    Value = _evaluator.Evaluate(childGame.BoardState, _player)
                };
                graph.AddVertex(childNode);
                graph.AddEdge(new Edge<GameNode>(parentNode, childNode));
                BuildTree(graph, childGame, childNode, depth - 1);
            }
        }
    }
}