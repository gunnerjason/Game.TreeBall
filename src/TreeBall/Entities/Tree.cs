using System.Collections.Generic;
using System.Linq;

namespace TreeBall.Entities
{
    using System;
    
    // Binary tree
    public class Tree
    {
        public Node root { get; }
        private int level { get; set; }
        private int nodeCount { get; set; }
        private Queue<Char> leafNames { get; set; }
        private bool isBottomLevel { get; set; }
        public List<Node> leafNodes { get; set; } = new List<Node>();

        public Tree()
        {
            root = new Node();
            nodeCount = 1;
            level = 0;
        }

        public int GetTreeDepth()
        {
            return GetTreeDepth(root);
        }

        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.Left), GetTreeDepth(parent.Right)) + 1;
        }

        public void BuildPerfectTree(int depth)
        {
            InitLeafNames(depth);
            while (level < depth || !IsPerfectTree())
            {
                isBottomLevel |= (level==(depth-1)) && IsPerfectTree();
                AddNode();
            }
        }

        private void InitLeafNames(int depth)
        {
            int numberOfLeaf = (int) Math.Pow(2, depth);
            leafNames = new Queue<char>(Enumerable
                .Range('A', numberOfLeaf)
                .Select(i => (Char)i)
                .ToList());
        }

        public void AddNode(string data = null)
        {
            nodeCount++;
            Node current = root;
            if (nodeCount >= Math.Pow(2, level + 1)) level++;
            for (int n = level - 1; n > 0; n--)
            {
                current = CheckBit(nodeCount, n) ? current.Left : current.Right;
            }
            
            var newNode = new Node();
            newNode.InsertData(data);
            if (isBottomLevel)
            {
                SetLeafName(newNode);
                leafNodes.Add(newNode);
            }
            if (CheckBit(nodeCount, 0))
            {
                current.Left = newNode;
            }
            else
            {
                current.Right = newNode;
            }
        }

        public Node GetClosedLeaf()
        {
            return GetClosedNode(root);
        }

        private Node GetClosedNode(Node currentNode)
        {
            if (currentNode.IsLeaf())
            {
                return currentNode;
            }
             
            if (currentNode.Gate == GateDirection.Left)
            {
                return GetClosedNode(currentNode.Left);
            }
            else
            {
                return GetClosedNode(currentNode.Right);
            }
        }

        public void InsertDataIntoLeaf(string data)
        {
            InsertDataIntoLeaf(data, root);
        }

        private bool InsertDataIntoLeaf(string data, Node currentNode)
        {
            if (currentNode == null)
            {
                currentNode = root;
            }

            if (currentNode.IsLeaf())
            {
                currentNode.InsertData(data);
                return true;
            }

            if (currentNode.Gate == GateDirection.Left)
            {
                currentNode.FlipGate();
                return InsertDataIntoLeaf(data, currentNode.Right);
            }
            else
            {
                currentNode.FlipGate();
                return InsertDataIntoLeaf(data, currentNode.Left);
            }
        }

        private bool CheckBit(int num, int position)
        {
            return ((num >> position) & 1) == 0;
        }

        private void SetLeafName(Node node)
        {
            node.NodeName = leafNames.Dequeue().ToString();
        }

        public bool IsPerfectTree()  
        {  
            int treeDepth = GetTreeDepth();  
            return IsPerfectCheck(root, treeDepth, 0);  
        }  

        private bool IsPerfectCheck(Node node, int depth, int level)
        {  
            if (node == null)
            {
                return true;
            }  
          
            if (node.Left == null && node.Right == null)
            {
                return (depth == level+1);
            }  
          
            if (node.Left == null || node.Right == null)
            {
                return false;
            }  
          
            return IsPerfectCheck(node.Left, depth, level+1) &&  
                    IsPerfectCheck(node.Right, depth, level+1);  
        }  
    }
}
