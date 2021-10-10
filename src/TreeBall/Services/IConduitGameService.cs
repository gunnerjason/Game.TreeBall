using TreeBall.Entities;

namespace TreeBall.Services
{
    public interface IConduitGameService
    {
        public Node PredictLastNode();
        public void RunBalls();
        void FillLeavesWithBalls(char[] balls);
        Node GetTreeLeafWithEmptyValue();
    }
}
