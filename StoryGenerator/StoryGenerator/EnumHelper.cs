using System;
using System.Collections.Generic;
using System.Text;
using StoryGenerator.Enums;

namespace StoryGenerator
{
    public class EnumHelper
    {
        public static Random Random { get; set; } = new Random();

        public static T GetRandomValue<T>() where T : struct, IConvertible
        {
            var ts = Enum.GetValues(typeof(T));
            return (T)ts.GetValue(Random.Next(ts.Length));
        }
    }
}
