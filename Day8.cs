using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day8 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            string steps = inputs[0];
            for (int i = 2; i < inputs.Length; i++)
            {
                string[] data = inputs[i].Split(" = (");

                Node node = null;
                if (nodes.ContainsKey(data[0]))
                {
                    node = nodes[data[0]];
                }
                else
                {
                    node = new Node();
                    node.nodeName = data[0];
                    nodes.Add(node.nodeName, node);
                }
                data[1] = data[1].Replace(")", "");
                string[] connections = data[1].Split(", ");
                Node leftNode = null;
                Node rightNode = null;
                if (nodes.ContainsKey(connections[0]))
                {
                    leftNode = nodes[connections[0]];
                }
                else
                {
                    leftNode = new Node();
                    leftNode.nodeName = connections[0];
                    nodes.Add(connections[0], leftNode);
                }
                if (nodes.ContainsKey(connections[1]))
                {
                    rightNode = nodes[connections[1]];
                }
                else
                {
                    rightNode = new Node();
                    rightNode.nodeName = connections[1];
                    nodes.Add(connections[1], rightNode);
                }
                node.leftNode = leftNode;
                node.rightNode = rightNode;
            }

            List<Node> startingNodes = new List<Node>();
            String[] keys = nodes.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i][2] == 'A')
                {
                    startingNodes.Add(nodes[keys[i]]);
                }
            }
            Node[] nextNodes = new Node[startingNodes.Count];
            int currentStepIndex = 0;
            long stepCounter = 0;

            List<long>[] stepsToReachAZ = new List<long>[startingNodes.Count];
            for (int i = 0; i < startingNodes.Count; i++)
            {
                stepsToReachAZ[i] = new List<long>();
            }
            while (stepCounter < 14189111)
            {
                for (int i = 0; i < startingNodes.Count; i++)
                {
                    if (steps[currentStepIndex] == 'L')
                    {
                        nextNodes[i]=(startingNodes[i].leftNode);
                    }
                    else if (steps[currentStepIndex] == 'R')
                    {
                        nextNodes[i] = (startingNodes[i].rightNode);
                    }
                }
                currentStepIndex++;
                if (currentStepIndex >= steps.Length)
                {
                    currentStepIndex = 0;
                }
                stepCounter++;

                for (int i = 0; i < nextNodes.Length; i++)
                {
                    if (nextNodes[i].nodeName[2] == 'Z')
                    {
                        //Console.WriteLine(nextNodes[i].nodeName + " " + stepCounter);
                        stepsToReachAZ[i].Add(stepCounter);
                    }
                }
                startingNodes.Clear();
                startingNodes.AddRange(nextNodes);
                nextNodes = new Node[startingNodes.Count];
            }

            long[] stepCounts = new long[6];
            for (int index = 0; index < stepsToReachAZ.Length; index++)
            {
                long[] reducedSteps = new long[stepsToReachAZ[index].Count];
                for (int i = 0; i < stepsToReachAZ[index].Count-1; i++)
                {
                    reducedSteps[i] = stepsToReachAZ[index][i+1] - stepsToReachAZ[index][i];
                    Console.WriteLine(reducedSteps[i]);
                }
                stepCounts[index] = reducedSteps[0];
            }
            
            long lcm = LeastCommonMultiplier(stepCounts[1], stepCounts[0]);
            long lcm1 = LeastCommonMultiplier(stepCounts[4], stepCounts[3]);
            long lcm2 = LeastCommonMultiplier(stepCounts[2], stepCounts[5]);
            long lcm4 = LeastCommonMultiplier(lcm, lcm1);
            long lcm5 = LeastCommonMultiplier(lcm4, lcm2);

            return lcm5.ToString();
        }

        private bool AllSame(long[] calculatedSteps)
        {
            bool allSame = true;
            for (int i = 0; i < calculatedSteps.Length-1; i++)
            {
                if(calculatedSteps[i] != calculatedSteps[i+1]){
                    allSame = false;
                    break;
                }
            }
            return allSame;
        }

        private long LeastCommonMultiplier(long a, long b)
        {
            long absNumber1 = Math.Abs(a);
            long absNumber2 = Math.Abs(b);
            long absHigherNumber = Math.Max(absNumber1, absNumber2);
            long absLowerNumber = Math.Min(absNumber1, absNumber2);
            long lcm = absHigherNumber;
            while (lcm % absLowerNumber != 0) {
                lcm += absHigherNumber;
            }
            return lcm;
        }

        private long GreatestCommonDivisor(long a, long b)
        {
            while(b != 0)
            {
                long t = b;
                b = a%b;
                a = t;
            }
            return a;
        }

        private bool IsAtEnd(List<Node> nodes)
        {
            int count = 0;
            bool isAtEnd = true;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].nodeName[2] != 'Z')
                {
                    isAtEnd = false;
                }
                else if (nodes[i].nodeName[2] == 'Z')
                {
                    count++;
                }
            }
            if(count>3) Console.WriteLine(count.ToString());
            return isAtEnd;
        }
    }

    public class Node
    {
        public Node leftNode; 
        public Node rightNode;
        public string nodeName;

        public override string ToString()
        {
            return nodeName + " | (" + leftNode.nodeName + ":" + rightNode.nodeName + ")"; 
        }
    }
}
