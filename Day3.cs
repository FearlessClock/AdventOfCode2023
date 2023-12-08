using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            List<Gear> gears = new List<Gear>();
            for (int y = 0; y < inputs.Length; y++)
            {
                string currentNumber = "";
                bool isPartNumber = false;
                bool isReadingNumber = false;
                Gear gear = new Gear();
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    if (inputs[y][x] >= 48 && inputs[y][x] <= 57)
                    {
                        if(!isReadingNumber)
                        {
                            isReadingNumber = true;
                            gear = new Gear();
                        }

                        currentNumber += inputs[y][x];
                        Vector2D pos = new Vector2D(0,0);
                        char symbol = ' ';
                        bool res = IsPartNumber(inputs, x, y,out symbol, out pos);
                        isPartNumber = res || isPartNumber;
                        if(res && symbol == '*')
                        {
                            gear.symbol = '*';
                            gear.pos = pos;
                        }
                    }
                    else
                    {
                        if(isReadingNumber && isPartNumber)
                        {
                            sum += Convert.ToInt32(currentNumber);
                            Console.WriteLine("Chosen number: " + currentNumber + " Total: " + sum);
                            if (gear.pos != null)
                            {
                                gear.value = Convert.ToInt32(currentNumber);
                                gears.Add(gear);
                            }
                        }
                        currentNumber = "";
                        isPartNumber = false;
                        isReadingNumber = false;
                    }
                }
                if(isReadingNumber && isPartNumber)
                {
                    sum += Convert.ToInt32(currentNumber);
                    Console.WriteLine("Chosen number: " + currentNumber + " Total: " + sum); 
                    if (gear.pos != null)
                    {
                        gear.value = Convert.ToInt32(currentNumber);
                        gears.Add(gear);
                    }
                }
                currentNumber = "";
                isPartNumber = false;
                isReadingNumber = false;

            }
            sum = 0;
            List<int> usedIndexes = new List<int>();
            for (int i = 0; i < gears.Count; i++)
            {
                if (usedIndexes.Contains(i)) continue;
                for (int j = 0; j < gears.Count; j++)
                {
                    if (j == i) continue;
                    if(usedIndexes.Contains(j)) continue;
                    if (gears[i].pos.Equals(gears[j].pos))
                    {
                        sum += gears[i].value * gears[j].value;
                        usedIndexes.Add(j);
                        usedIndexes.Add(i);
                    }
                }
            }
            Console.WriteLine(sum);
            return sum.ToString();
        }

        public bool IsPartNumber(string[] inputs, int x, int y,out char s, out Vector2D v)
        {
            v = new Vector2D(0, 0);
            s = ' ';
            Vector2D[] directions = new Vector2D[8] {new Vector2D(0,1),new Vector2D(1,1),new Vector2D(1,0),new Vector2D(1,-1),
                                                        new Vector2D(0,-1),  new Vector2D(-1,-1), new Vector2D(-1,0), new Vector2D(-1,1)};
            bool foundSymbol = false;
            for (int i = 0; i < directions.Length; i++)
            {
                if((x + directions[i].x >= 0 && x + directions[i].x < inputs[y].Length) && 
                    (y + directions[i].y >= 0 && y + directions[i].y < inputs.Length))
                {
                    char c = inputs[y + directions[i].y][x + directions[i].x];
                    if (c >= 48 && c <= 57 || c == '.')
                    {
                        continue;
                    }
                    else
                    {
                        foundSymbol = true;
                        if (c == '*')
                        {
                            s = '*';
                            v = new Vector2D(x + directions[i].x, y + directions[i].y);
                        }
                    }
                }
            }
            return foundSymbol;
        }
    }

    class Gear
    {
        public int value;
        public char symbol;
        public Vector2D? pos;
    }
}
