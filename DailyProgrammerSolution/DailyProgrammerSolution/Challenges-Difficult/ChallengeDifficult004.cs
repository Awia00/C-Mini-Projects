using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Challenges_Difficult
{
    /// <summary>
    /// today, your challenge is to create a program that will take a series of numbers (5, 3, 15), and find how those numbers can add, subtract, multiply, or divide in various ways to relate to eachother. This string of numbers should result in 5 * 3 = 15, or 15 /3 = 5, or 15/5 = 3. When you are done, test your numbers with the following strings:
    /// 4, 2, 8
    /// 6, 2, 12
    /// 6, 2, 3
    /// 9, 12, 108
    /// 4, 16, 64
    /// For extra credit, have the program list all possible combinations.
    /// for even more extra credit, allow the program to deal with strings of greater than three numbers.For example, an input of(3, 5, 5, 3) would be 3 * 5 = 15, 15/5 = 3. When you are finished, test them with the following strings.
    /// 2, 4, 6, 3
    /// 1, 1, 2, 3
    /// 4, 4, 3, 4
    /// 8, 4, 3, 6
    /// 9, 3, 1, 7
    /// </summary>
    class ChallengeDifficult004
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindPairs(new List<int>() { 3, 5, 15 }));
            Console.WriteLine(FindPairs(new List<int>() { 6, 2, 12 }));
            Console.WriteLine(FindPairs(new List<int>() { 6, 2, 3 }));
            Console.WriteLine(FindPairs(new List<int>() { 9, 12, 108 }));
            Console.WriteLine(FindPairs(new List<int>() { 4, 16, 64 }));
        }

        private static string FindPairs(IList<int> values)
        {
            var temp = "";
            var combis = new List<Tuple<int, int, int>>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        combis.Add(new Tuple<int, int, int>(values[i], values[j], values[k]));
                    }
                }
            }
            foreach (var combi in combis)
            {
                if (combi.Item1 + combi.Item2 == combi.Item3) temp += combi.Item1 + " + "+ combi.Item2 + " = "+ combi.Item3 + "\n";
                if (combi.Item1 - combi.Item2 == combi.Item3) temp += combi.Item1 + " - " + combi.Item2 + " = " + combi.Item3 + "\n";
                if (combi.Item1 * combi.Item2 == combi.Item3) temp += combi.Item1 + " * " + combi.Item2 + " = " + combi.Item3 + "\n";
                if (combi.Item1 / combi.Item2 == combi.Item3) temp += combi.Item1 + " / " + combi.Item2 + " = " + combi.Item3 + "\n";
            }
            return temp;
        }
    }
}
