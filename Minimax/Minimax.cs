using System;
using System.Collections.Generic;

namespace IA_Utils
{
    public static class Minimax
    {
        public static Heuristic GetBestNode(MinimaxNode node, int depth)
        {
            if (node == null) return null;
            return alphaBeta(node, depth, double.MinValue, double.MaxValue, true);
        }

        private static Heuristic alphaBeta(MinimaxNode node, int depth, double alpha, double beta, bool maximizingPlayer)
        {
            if (node.IsTerminal() || depth < 1)
            {
                return new Heuristic { node = node, value = node.GetHeuristic() };
            }
            List<MinimaxNode> subNodes = node.GetSubNodes();
            if (maximizingPlayer)
            {
                Heuristic abNode = new Heuristic { value = double.MinValue };
                Heuristic newAbNode;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    newAbNode = alphaBeta(subNodes[i], depth - 1, alpha, beta, false);
                    if (newAbNode.value > abNode.value) abNode = new Heuristic { node = subNodes[i], value = newAbNode.value };
                    if (abNode.value > alpha) alpha = abNode.value;
                    if (alpha >= beta) break;
                }
                return abNode;
            }
            else
            {
                Heuristic abNode = new Heuristic { value = double.MaxValue };
                Heuristic newAbNode;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    newAbNode = alphaBeta(subNodes[i], depth - 1, alpha, beta, true);
                    if (newAbNode.value < abNode.value) abNode = new Heuristic { node = subNodes[i], value = newAbNode.value };
                    if (abNode.value < beta) beta = abNode.value;
                    if (beta <= alpha) break;
                }
                return abNode;
            }
        }
    }

    public class Heuristic
    {
        public MinimaxNode node;
        public double value;
    }

    public interface MinimaxNode
    {
        List<MinimaxNode> GetSubNodes();
        bool IsTerminal();
        double GetHeuristic();
    }
}
