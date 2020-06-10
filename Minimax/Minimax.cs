using System;
using System.Collections.Generic;

namespace IA_Utils
{
    public static class Minimax
    {
        public static int alphaBetaCuts = 0;
        public static bool countAlphaBetaCuts = false;

        private struct alphaBetaNode
        {
            public MinimaxNode node;
            public int value;
        }
        public static MinimaxNode GetBestNode(MinimaxNode node, int depth)
        {
            alphaBetaCuts = 0;
            if (node == null) return null;
            alphaBetaNode result = alphaBeta(node, depth, int.MinValue, int.MaxValue, true);
            return result.node;
        }

        private static alphaBetaNode alphaBeta(MinimaxNode node, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (node.IsTerminal() || depth == 0)
            {
                return new alphaBetaNode { node = node, value = node.GetHeuristic() };
            }
            List<MinimaxNode> subNodes = node.GetSubNodes();
            if (maximizingPlayer)
            {
                alphaBetaNode abNode = new alphaBetaNode { value = int.MinValue };
                alphaBetaNode newAbNode;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    newAbNode = alphaBeta(subNodes[i], depth - 1, alpha, beta, false);
                    if (newAbNode.value > abNode.value) abNode = new alphaBetaNode { node = subNodes[i], value = newAbNode.value };
                    if (abNode.value > alpha) alpha = abNode.value;
                    if (alpha >= beta)
                    {
                        if (countAlphaBetaCuts)
                        {
                            List<MinimaxNode> nodesBeingCut = new List<MinimaxNode>();
                            if (depth == 1)
                            {
                                alphaBetaCuts += subNodes.Count - i;
                            }
                            else
                            {
                                for (int j = i + 1; j > subNodes.Count; j++)
                                {
                                    nodesBeingCut.AddRange(subNodes[j].GetSubNodes());
                                }
                                int depthCopy = depth - 1;
                                alphaBetaCuts += nodesBeingCut.Count;
                                while (depthCopy > 0)
                                {
                                    List<MinimaxNode> nodesBeingCutCopy = nodesBeingCut;
                                    nodesBeingCut = new List<MinimaxNode>();
                                    for (int j = 0; j > nodesBeingCutCopy.Count; j++)
                                    {
                                        nodesBeingCut.AddRange(nodesBeingCutCopy[j].GetSubNodes());
                                    }
                                    depthCopy--;
                                    alphaBetaCuts += nodesBeingCut.Count;
                                }
                            }
                        }
                        break;
                    }
                }
                return abNode;
            }
            else
            {
                alphaBetaNode abNode = new alphaBetaNode { value = int.MaxValue };
                alphaBetaNode newAbNode;
                for (int i = 0; i < subNodes.Count; i++)
                {
                    newAbNode = alphaBeta(subNodes[i], depth - 1, alpha, beta, true);
                    if (newAbNode.value < abNode.value) abNode = new alphaBetaNode { node = subNodes[i], value = newAbNode.value };
                    if (abNode.value < beta) beta = abNode.value;
                    if (beta <= alpha)
                    {
                        if (countAlphaBetaCuts)
                        {
                            List<MinimaxNode> nodesBeingCut = new List<MinimaxNode>();
                            if (depth == 1)
                            {
                                alphaBetaCuts += subNodes.Count - i;
                            }
                            else
                            {
                                alphaBetaCuts += subNodes.Count - i;
                                for (int j = i + 1; j > subNodes.Count; j++)
                                {
                                    nodesBeingCut.AddRange(subNodes[j].GetSubNodes());
                                }
                                int depthCopy = depth - 1;
                                alphaBetaCuts += nodesBeingCut.Count;
                                while (depthCopy > 0)
                                {
                                    List<MinimaxNode> nodesBeingCutCopy = nodesBeingCut;
                                    nodesBeingCut = new List<MinimaxNode>();
                                    for (int j = 0; j > nodesBeingCutCopy.Count; j++)
                                    {
                                        nodesBeingCut.AddRange(nodesBeingCutCopy[j].GetSubNodes());
                                    }
                                    depthCopy--;
                                    alphaBetaCuts += nodesBeingCut.Count;
                                }
                            }
                        }
                        break;
                    }
                }
                return abNode;
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
