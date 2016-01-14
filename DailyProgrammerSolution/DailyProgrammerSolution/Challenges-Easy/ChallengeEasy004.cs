using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges_Easy
{
    /// <summary>
    /// You're challenge for today is to create a random password generator!
    /// For extra credit, allow the user to specify the amount of passwords to generate.
    /// For even more extra credit, allow the user to specify the length of the strings he wants to generate!
    /// </summary>
    class ChallengeEasy004
    {
        private static readonly Random Random = new Random();
        private static readonly IList<string> Words = new List<string> {"Horse", "Batery", "Correct", "Stable", "Car", "House", "Child", "Man", "Woman", "Girl", "Skateboard", "Blue", "Green", "Yellow", "Red", "Yolo"}; 
        static void Main(string[] args)
        {
            Console.WriteLine(GeneratePassword(1));
            Console.WriteLine(GeneratePassword(2));
            Console.WriteLine(GeneratePassword(3));
            Console.WriteLine(GeneratePassword(4));
            Console.WriteLine(GeneratePassword(5));
            Console.WriteLine(GeneratePassword(6));
            Console.WriteLine(GeneratePassword(7));
            Console.WriteLine(GeneratePassword(8));
            Console.WriteLine(GeneratePassword(9));
            Console.WriteLine(GeneratePassword(10));
        }

        private static string GeneratePassword(int length)
        {
            return Enumerable.Range(0, length).Select(x => Words[Random.Next(0, Words.Count())]).Aggregate((x, y) => x + y);
        }
    }
}