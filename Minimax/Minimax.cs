using System.Collections.Generic;

namespace IA_Utils
{
    public static class Minimax
    {
        public static MinimaxNode GetBestNode(List<MinimaxNode> nodes, int depth)
        {
            if (nodes == null || nodes.Count == 0) return null;
            int actPoints;
            int maxPoints = int.MinValue;
            int maxIndex = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                actPoints = alphaBeta(nodes[i], depth - 1, int.MinValue, int.MaxValue, false);
                if (actPoints > maxPoints)
                {
                    maxPoints = actPoints;
                    maxIndex = i;
                }
            }
            return nodes[maxIndex];
        }

        private static int alphaBeta(MinimaxNode node, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (node.IsTerminal() || depth == 0)
            {
                return node.GetHeuristic();
            }
            List<MinimaxNode> subNodes = node.GetSubNodes();
            if (maximizingPlayer)
            {
                int value = int.MinValue;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    int newValue = alphaBeta(subNodes[i], depth - 1, alpha, beta, false);
                    if (newValue > value) value = newValue;
                    if (value > alpha) alpha = value;
                    if (alpha >= beta) break;
                }
                return value;
            }
            else
            {
                int value = int.MaxValue;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    int newValue = alphaBeta(subNodes[i], depth - 1, alpha, beta, true);
                    if (newValue < value) value = newValue;
                    if (value < beta) beta = value;
                    if (alpha >= beta) break;
                }
                return value;
            }
        }
    }

    public interface MinimaxNode
    {
        List<MinimaxNode> GetSubNodes();
        bool IsTerminal();
        int GetHeuristic();
    }
}
