using System;
using System.Net;
using static System.Net.WebRequestMethods;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            int currentDay = 8;

            string filename = string.Format("E:\\Projects\\AdventOfCode\\2023\\AdventOfCode\\day{0}.txt", currentDay);
            string text = System.IO.File.ReadAllText(filename);
            string[] inputs = text.Split("\r\n");

            IDay day = GetProgramForDay(currentDay);
            string res = day.GetResult(inputs);
            Console.WriteLine(res);
        }

        static IDay GetProgramForDay(int day)
        {
            switch (day) 
            {
                case 1:
                    return new Day1();
                case 2:
                    return new Day2();
                case 3:
                    return new Day3();
                case 4:
                    return new Day4();
                case 5:
                    return new Day5();
                case 6:
                    return new Day6();
                case 7:
                    return new Day7();
                case 8:
                    return new Day8();
            }
            return null;
        }
    }
}