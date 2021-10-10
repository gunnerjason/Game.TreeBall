using System;
using System.Collections.Generic;

namespace TreeBall.Entities
{ 
    public class Node
    {
        public List<string> NodeData { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
        
        public string NodeName { get; set; }

        public GateDirection Gate { get; set; }

        public Node()
        {
            Gate = GetRandomGateDirection();
            Left = Right = null;
        }

        public void InsertData(string data)
        {
            if (NodeData == null)
            {
                NodeData = new List<string>();
            }

            if (!string.IsNullOrEmpty(data))
            {
                NodeData.Add(data);
            }
        }

        public bool IsLeaf()
        {
            return Right == null && Left == null;
        }

        public void FlipGate()
        {
            if (Gate == GateDirection.Left)
            {
                Gate = GateDirection.Right;
            }
            else
            {
                Gate = GateDirection.Left;
            }
        }

        private GateDirection GetRandomGateDirection()
        {
            Array values = Enum.GetValues(typeof(GateDirection));
            Random random = new Random();
            return (GateDirection)values.GetValue(random.Next(values.Length));
        }
    }

    public enum GateDirection {
        Left,
        Right
    }
}