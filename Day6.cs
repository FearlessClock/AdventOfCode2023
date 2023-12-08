using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day6 : IDay
    {
        public string GetResult(string[] inputs)
        {
            long sum = 1;
            string[] times = inputs[0].Split(' ');
            string[] distances = inputs[1].Split(' ');

            for (long j = 0; j < times.Length; j++)
            {
                long totalWins = 0;
                long time = long.Parse(times[j]);
                long distance = long.Parse(distances[j]);
                for (long i = 0; i < time; i++)
                {
                    long remainingTime = time - i;
                    long totalDistance = i * remainingTime;
                    if (totalDistance > distance)
                    {
                        totalWins++;
                    }
                }
                Console.WriteLine(totalWins);
                sum *= totalWins;
            }
            return sum.ToString();
        }
    }
}
