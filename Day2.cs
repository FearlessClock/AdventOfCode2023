using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;

                string[] blocks = inputs[i].Split(new char[1] { ':' });
                int id = Convert.ToInt32(blocks[0].Replace("Game ", ""));
                blocks = blocks[1].Split(';') ;
                for (int c = 0; c < blocks.Length; c++)
                {
                    int numRed = 0;
                    int numGreen = 0;
                    int numBlue = 0;

                    string[] cubes = blocks[c].Split(", ");
                    for (int j = 0; j < cubes.Length; j++)
                    {
                        if (cubes[j].Contains("red"))
                        {
                            numRed += Convert.ToInt32(cubes[j].Replace(" red", ""));
                        }
                        else if (cubes[j].Contains("blue"))
                        {
                            numBlue += Convert.ToInt32(cubes[j].Replace(" blue", ""));
                        }
                        else if (cubes[j].Contains("green"))
                        {
                            numGreen += Convert.ToInt32(cubes[j].Replace(" green", ""));
                        }
                    }
                    maxRed = Math.Max(maxRed, numRed);
                    maxGreen = Math.Max(maxGreen, numGreen);
                    maxBlue = Math.Max(maxBlue, numBlue);
                }
                sum += maxRed * maxGreen * maxBlue;
            }
            return sum.ToString();
        }
    }
}
