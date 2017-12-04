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
            /*
             * --- Day 2: Corruption Checksum ---

            As you walk through the door, a glowing humanoid shape yells in your direction. "You there! Your state appears to be idle. 
            Come help us repair the corruption in this spreadsheet - if we take another millisecond, we'll have to display an hourglass cursor!"

            The spreadsheet consists of rows of apparently-random numbers. To make sure the recovery process is on the right track, they need you to calculate the spreadsheet's checksum. 
            For each row, determine the difference between the largest value and the smallest value; the checksum is the sum of all of these differences.

            For example, given the following spreadsheet:
            5 1 9 5
            7 5 3
            2 4 6 8

            The first row's largest and smallest values are 9 and 1, and their difference is 8.
            The second row's largest and smallest values are 7 and 3, and their difference is 4.
            The third row's difference is 6.
            In this example, the spreadsheet's checksum would be 8 + 4 + 6 = 18.

            What is the checksum for the spreadsheet in your puzzle input?
             */

            // Get data from .txt file
            List<string> listStrLineElements = values.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            // Split it by the occurance of TAB(\t)
            List<string> rowList = listStrLineElements.SelectMany(s => s.Split('\t')).ToList();
            // Create a List of split values
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
                result += max - min;
            }

            return result;
        }

        static public int AdventOfCode_Part2(string values)
        {
            /*
             * --- Part Two ---

            "Great work; looks like we're on the right track after all. Here's a star for your effort." However, the program seems a little worried. Can programs be worried?

            "Based on what we're seeing, it looks like all the User wanted is some information about the evenly divisible values in the spreadsheet. Unfortunately, none of us are equipped for that kind of calculation - most of us specialize in bitwise operations."

            It sounds like the goal is to find the only two numbers in each row where one evenly divides the other - that is, where the result of the division operation is a whole number. They would like you to find those numbers on each line, divide them, and add up each line's result.

            For example, given the following spreadsheet:

            5 9 2 8
            9 4 7 3
            3 8 6 5
            In the first row, the only two numbers that evenly divide are 8 and 2; the result of this division is 4.
            In the second row, the two numbers are 9 and 3; the result is 3.
            In the third row, the result is 2.
            In this example, the sum of the results would be 4 + 3 + 2 = 9.

            What is the sum of each row's result in your puzzle input?
            */

            // Get data from .txt file
            List<string> listStrLineElements = values.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            // Split it by the occurance of TAB(\t)
            List<string> rowList = listStrLineElements.SelectMany(s => s.Split('\t')).ToList();
            // Create a List of split values
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
                // Check if all values have been retrieved. If true, end function
                if (count == (numbers.Count/16))
                {
                    break;
                }
                for (; i < pinPoint; i++)
                {
                    division = 0;
                    // Prevents the value to divide itself
                    if (numbers[j] != numbers[i])
                    {
                        // Check if Number1 evenly divides Number2
                        if (numbers[j] % numbers[i] == 0)
                        {
                            division = numbers[j] / numbers[i];
                            i = j = pinPoint; // Set the current index to the next row of data
                            pinPoint += 16; // Increase pinPoint to work on next 16 values instead of the same 
                            result += division;
                            count++;
                            break;
                        }
                        // Check if Number2 evenly divides Number1
                        else if (numbers[i] % numbers[j] == 0)
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

                    // If we get to the end of the row and there's no value retrieved,
                    // set index at the beginning of current row and move to the next
                    // value to compare
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
            // Get path to executable file (\bin\Debug)
            string path = AppDomain.CurrentDomain.BaseDirectory;
            // Read all data in file + go back 2 directories 
            // (had to do this since I delete \bin + \obj folders to ge the whole project uploaded)
            string input = File.ReadAllText(Path.GetFullPath(Path.Combine(path, @"..\..\input.txt"))); 
            
            Console.WriteLine("Advent of Code Day 2; Part One\nSum: " + AdventOfCode_Part1(input));
            Console.WriteLine("\n\nAdvent of Code Day 2; Part Two\nSum: " + AdventOfCode_Part2(input));
        }
    }
}
