using TreeBall.Entities;
using Xunit;

namespace TreeBall.UnitTests
{
    public class TreeTest
    {
        private readonly Tree _tree;
        public TreeTest()
        {
            _tree = new Tree();
        }
        
        [Fact]
        public void CanCreateNewTree()
        {
            var depth = 1;
            _tree.BuildPerfectTree(depth);
            Assert.True(_tree.leafNodes.Count>0);
        }
        
        [Fact]
        public void TreeDepthAsExpected()
        {
            var depth = 10;
            _tree.BuildPerfectTree(depth);
            
            // Test depth with root level
            Assert.Equal(depth+1, _tree.GetTreeDepth());
        }
    }
}
