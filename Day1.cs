using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1 : IDay
    {
        public string GetResult(string[] inputs)
        {
            string[] numberNames = new string[10] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string resultingNumber = "";
            int sum = 0;
            for (int i = 0; i < inputs.Length-1; i++)
            {
                resultingNumber = "";
                string currentLetters = "";
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] >= 48 && inputs[i][j] <= 57)
                    {
                        resultingNumber += inputs[i][j];
                        break;
                    }
                    currentLetters += inputs[i][j].ToString();
                    bool hasNumber = StringContainsNumber(currentLetters, numberNames);
                    if (hasNumber)
                    {
                        resultingNumber += GetNumberFromString(currentLetters, numberNames);
                        break;
                    }
                }
                currentLetters = "";
                for (int j = inputs[i].Length-1; j >=0; j--)
                {
                    if (inputs[i][j] >= 48 && inputs[i][j] <= 57)
                    {
                        resultingNumber += inputs[i][j];
                        break;
                    }
                    currentLetters = inputs[i][j].ToString() + currentLetters;
                    bool hasNumber = StringContainsNumber(currentLetters, numberNames);
                    if (hasNumber)
                    {
                        resultingNumber += GetNumberFromString(currentLetters, numberNames);
                        break;
                    }
                }
                sum += Convert.ToInt32(resultingNumber);
            }
            return sum.ToString();
        }

        private int GetNumberFromString(string input, string[] numberNames)
        {
            for (int i = 0; i < numberNames.Length; i++)
            {
                if (input.Contains(numberNames[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool StringContainsNumber(string input, string[] numberNames)
        {
            for (int i = 0; i < numberNames.Length; i++)
            {
                if (input.Contains(numberNames[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
