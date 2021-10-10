using System;
using System.Collections.Generic;
using System.Linq;
using TreeBall.Entities;
using Xunit;

namespace TreeBall.UnitTests
{
    public class NodeTest
    {
        [Fact]
        public void CanCreateNewNode()
        {
            var node = new Node();
            Assert.IsType<Node>(node);
        }
        
        [Fact]
        public void NodeHasGate()
        {
            List<string> gateDirectionList = Enum.GetNames(typeof(GateDirection)).ToList();
            var node = new Node();
            Assert.Contains( node.Gate.ToString(), gateDirectionList);
        }
        
        [Fact]
        public void NodeCanInsertData()
        {
            var node = new Node();
            Assert.Null(node.NodeData);
            node.InsertData("*");
            Assert.Single(node.NodeData);
        }
        
        [Fact]
        public void NodeGateCanFlip()
        {
            var node = new Node();
            var nodeGate = node.Gate;
            node.FlipGate();
            Assert.NotEqual( nodeGate, node.Gate);
        }
    }
}
