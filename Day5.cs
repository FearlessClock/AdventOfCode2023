using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day5 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            string[] seedsInput = inputs[0].Split(' ');
            List<long> seeds = new List<long>();
            List<long> seedsRange = new List<long>();
            for (int i = 1; i < seedsInput.Length; i++)
            {
                if(i%2 == 1)
                {
                    seeds.Add(long.Parse(seedsInput[i]));
                }
                else
                {
                    seedsRange.Add(long.Parse(seedsInput[i]));
                }
            }

            Map map = new Map();
            List<Map> maps = new List<Map>();

            for (int i = 2; i < inputs.Length; i++)
            {
                if (inputs[i].Length == 0)
                {
                    if(map != null)
                    {
                        maps.Add(map);
                    }
                    map = new Map();
                    continue;
                }
                if (inputs[i][0] >= 48 && inputs[i][0] <= 57)
                {
                    string[] data = inputs[i].Split(" ");
                    map.destinationStart.Add(long.Parse(data[0]));
                    map.sourceStart.Add(long.Parse(data[1]));
                    map.range.Add(long.Parse(data[2]));
                }
                else
                {
                    map.name = inputs[i];
                }
            }

            long smallestLocation = long.MaxValue;

            for (int i = 0; i < seeds.Count; i++)
            {
                long seedValue = seeds[i];
                long seedRange = seedsRange[i];
                List<Tuple<long, long>> newRanges = new List<Tuple<long, long>>();
                newRanges.Add(new Tuple<long, long>(seedValue, seedRange));
                for (int j = 0; j < maps.Count; j++)
                {
                    List<Tuple<long, long>> nextRanges = new List<Tuple<long, long>>();
                    for (int rangeIndex = 0; rangeIndex < newRanges.Count; rangeIndex++)
                    {
                        Tuple<long, long> values = newRanges[rangeIndex];
                        nextRanges.AddRange(maps[j].GetMappedRange(values.Item1, values.Item2));
                    }
                    newRanges = nextRanges;
                }
                long smallTest = long.MaxValue;
                for (int rangeCheck = 0; rangeCheck < newRanges.Count; rangeCheck++)
                {
                    if(smallTest > newRanges[rangeCheck].Item1)
                    {
                        smallTest = newRanges[rangeCheck].Item1;
                    }
                }
                if (smallestLocation > smallTest)
                {
                    smallestLocation = smallTest;
                }
            }

            return smallestLocation.ToString();
        }
    }

    public class Map
    {
        public string name = "";
        public List<long> destinationStart = new();
        public List<long> sourceStart = new();
        public List<long> range = new();

        public List<Tuple<long, long>> GetMappedRange(long val, long seedRange)
        {
            List<Tuple<long, long>> rangesToCheck = new List<Tuple<long, long>>();
            List<Tuple<long, long>> finalRanges = new List<Tuple<long, long>>();
            rangesToCheck.Add(new Tuple<long, long>(val, seedRange));
            int counter = 10000;
            while (rangesToCheck.Count > 0)
            {
                counter--; if(counter < 0) { break; }

                long seedValue = rangesToCheck[0].Item1;
                long seedToCheckRange = rangesToCheck[0].Item2;
                rangesToCheck.RemoveAt(0);
                bool foundRange = false;
                for (int i = 0; i < sourceStart.Count; i++)
                {
                    if (seedValue >= sourceStart[i] && seedValue < sourceStart[i] + range[i])
                    {
                        foundRange = true;
                        if (seedValue + seedToCheckRange-1 < sourceStart[i] + range[i])
                        {
                            long destRange = seedValue - sourceStart[i];
                            long newSeedStart = destinationStart[i] + destRange;
                            Tuple<long, long> newRange = new Tuple<long, long>(newSeedStart, seedToCheckRange);
                            finalRanges.Add(newRange);
                        }
                        else
                        {
                            long destRange = seedValue - sourceStart[i];
                            long newSeedStart = destinationStart[i] + destRange;
                            Tuple<long, long> savedRange = new Tuple<long, long>(newSeedStart, range[i]- destRange);
                            Tuple<long, long> newRange = new Tuple<long, long>(sourceStart[i] + range[i], seedToCheckRange- (range[i] - destRange));
                            finalRanges.Add(savedRange);
                            rangesToCheck.Add(newRange);
                        }
                    }
                }
                if (!foundRange)
                {
                    // Find till where I can check the and make a new range
                    // Either they are smaller or they are bigger than all the starting sources
                    bool isBigger = true;
                    for (int i = 0; i < sourceStart.Count; i++)
                    {
                        if (seedValue < sourceStart[i] + range[i])
                        {
                            isBigger = false; 
                        }
                    }
                    if (isBigger)
                    {
                        finalRanges.Add(new Tuple<long, long>(seedValue, seedToCheckRange));
                    }
                    else
                    {
                        long smallestSourceValue = sourceStart[0];
                        for (int i = 0; i < sourceStart.Count; i++)
                        {
                            if(smallestSourceValue > sourceStart[i])
                            {
                                smallestSourceValue = sourceStart[i];
                            }
                        }

                        if(seedValue < smallestSourceValue)
                        {
                            long dist = smallestSourceValue - seedValue;
                            Tuple<long, long> savedRange = new Tuple<long, long>(seedValue, dist);
                            long diff = Math.Abs(dist - seedToCheckRange);
                            Tuple<long, long> notFoundRange = new Tuple<long, long>(seedValue + dist, diff);
                            rangesToCheck.Add(notFoundRange);
                            finalRanges.Add(savedRange);
                        }
                        else
                        {
                            // It is somewhere in no-mans land
                            finalRanges.Add(new Tuple<long, long>(seedValue, seedToCheckRange));
                        }
                    }
                }
            }
            return finalRanges;
        }
    }
}
