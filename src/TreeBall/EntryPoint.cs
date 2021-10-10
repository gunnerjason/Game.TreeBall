using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TreeBall.Services;

namespace TreeBall
{
    public class EntryPoint
    {
        private readonly IConduitGameService _conduitGameService;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IConduitGameService conduitGameService, ILogger<EntryPoint> logger)
        {
            _conduitGameService = conduitGameService;
            _logger = logger;
        }

        internal void Run()
        {
            try
            {
                var predictedLastLeaf = _conduitGameService.PredictLastNode();
                _logger.LogInformation("Predicted empty container '{0}'", predictedLastLeaf.NodeName);
                _conduitGameService.RunBalls();
                var leaf = _conduitGameService.GetTreeLeafWithEmptyValue();
                _logger.LogInformation("Empty container '{0}' after run balls", leaf.NodeName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Run conduit game error.");
            }
        }
    }
}
