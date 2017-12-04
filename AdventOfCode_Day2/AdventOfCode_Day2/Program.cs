using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode_Day2
{
    class Program
    {
        static public int AdventOfCode_Part1(string values)
        {
            List<string> listStrLineElements = values.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            List<string> rowList = listStrLineElements.SelectMany(s => s.Split('\t')).ToList();
            List<int> numbers = rowList.Select(s => int.Parse(s)).ToList();
            int j = 1;

            int min = 0;
            int max = 0;
            int result = 0;

            for (int i = 1; i <= 16; i++)
            {
                max = 0;
                min = 9999;
                while (j <= i * 16)
                {
                    // Get maximum value
                    if (max < numbers[j - 1])
                    {
                        max = numbers[j - 1];
                    }
                    // Get minimum value
                    if (min > numbers[j - 1])
                    {
                        min = numbers[j - 1];
                    }
                    j++;
                }
                //Console.WriteLine("\nMaximum: " + max + "\nMinimum: " + min);
                result += max - min;
            }

            return result;
        }

        static public int AdventOfCode_Part2(string values)
        {
            List<string> listStrLineElements = values.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            List<string> rowList = listStrLineElements.SelectMany(s => s.Split('\t')).ToList();
            List<int> numbers = rowList.Select(s => int.Parse(s)).ToList();

            int division = 0;
            int result = 0;
            int pinPoint = 16;
            int sum = 0;
            int count = 0;

            int i = 0;
            int j = 0;

            foreach (int num in numbers)
            {
                if (count == 16)
                {
                    break;
                }
                for (; i < pinPoint; i++)
                {
                    division = 0;
                    if (numbers[j] != numbers[i])
                    {
                        if (numbers[j] % numbers[i] == 0)
                        {
                            division = numbers[j] / numbers[i];
                            i = j = pinPoint;
                            pinPoint += 16;
                            result += division;
                            count++;
                            break;
                        }
                        else if(numbers[i] % numbers[j] == 0)
                        {
                            division = numbers[i] / numbers[j];
                            i = j = pinPoint;
                            pinPoint += 16;
                            result += division;
                            count++;
                            break;
                        }
                    }
                    sum = division;
                    
                    //Console.WriteLine("\nNum: " + numbers[j] + "\nArray Number: " + numbers[i] + "\nDivision: " + division + "\nIndex: " + i + "\nPin Point: " + pinPoint + "\nResult: " + result);

                    if (i == pinPoint - 1)
                    {
                        if (sum == 0)
                        {
                            i = count*16;
                            j++;   
                        }

                    }
                    continue;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string input = File.ReadAllText(Path.GetFullPath(Path.Combine(path, @"..\..\input.txt")));
            
            int partOne = AdventOfCode_Part1(input);
            Console.WriteLine("Advent of Code Day 2; Part One\nSum: " + partOne);
            
            int partTwo = AdventOfCode_Part2(input);
            Console.WriteLine("\n\nAdvent of Code Day 2; Part Two\nSum: " + partTwo);

        }
    }
}
