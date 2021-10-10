using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TreeBall.Entities;

namespace TreeBall.Services
{
    public class ConduitGameService : IConduitGameService
    {
        private readonly Tree _tree;
        private readonly ConduitGameConfig _conduitGameConfig;
        private readonly ILogger<ConduitGameService> _logger;
        public ConduitGameService(IOptions<ConduitGameConfig> configOption, ILogger<ConduitGameService> logger)
        {
            _conduitGameConfig = configOption.Value;
            _logger = logger;

            _tree = new Tree();
            var treeLevel = _conduitGameConfig?.TreeLevel ?? 4;
            _tree.BuildPerfectTree(treeLevel);
            _logger.LogInformation("Created a perfect binary tree with {0} levels and {1} leaves", 
                treeLevel, 
                _tree.leafNodes.Count());
        }

        public Node PredictLastNode()
        {
            return _tree.GetClosedLeaf();
        }
        
        public void RunBalls()
        {
            // Ball size equals number of tree leaves minus one
            int ballSize = _tree.leafNodes.Count - 1;
            _logger.LogInformation("Run game with {0} balls", ballSize);

            char[] gameBalls = Enumerable
                .Range(1, ballSize)
                .Select(_ => '*').ToArray();
            FillLeavesWithBalls(gameBalls);
        }

        public void FillLeavesWithBalls(char[] balls)
        {
            foreach (var ball in balls)
            {
                _tree.InsertDataIntoLeaf(ball.ToString());
            }
        }

        public Node GetTreeLeafWithEmptyValue()
        {
            return _tree.leafNodes.Single(n => !n.NodeData.Any());
        }
    }
}
