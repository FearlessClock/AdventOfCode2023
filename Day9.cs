using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day9 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                List<int[]> sequences = new List<int[]>();
                string[] data = inputs[i].Split(' ');
                int[] numbers = new int[data.Length+1];
                for (int j = 1; j < data.Length+1; j++)
                {
                    numbers[j] = Convert.ToInt32(data[j-1]);
                }

                sequences.Add(numbers);
                int nonZeroCheck = -1;

                while (nonZeroCheck != 0)
                {
                    numbers = new int[sequences[^1].Length-1];
                    nonZeroCheck = 0;
                    for (int j = 1; j < sequences[^1].Length-1; j++)
                    {
                        numbers[j] = sequences[^1][j+1] - sequences[^1][j];
                        nonZeroCheck += numbers[j];
                    }
                    if(nonZeroCheck == 0)
                    {
                        Console.WriteLine("Done");
                    }
                    sequences.Add(numbers);
                }
                for (int j = sequences.Count-1; j >= 1; j--)
                {
                    sequences[j-1][0] = sequences[j-1][1] - sequences[j][0];
                }
                Console.WriteLine(sequences[0][0]);
                sum += sequences[0][0];
            }
            return sum.ToString();
        }
    }
}
