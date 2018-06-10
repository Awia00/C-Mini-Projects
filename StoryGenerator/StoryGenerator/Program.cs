using System;

namespace StoryGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            EnumHelper.Random = new Random();

            string cont;
            do
            {
                var plot = new Plot();
                var setting = new Setting();
                Console.WriteLine("In " + setting + ", " + plot);

                Console.WriteLine("Give me another? Y/N");
                cont = Console.In.ReadLine()?.Trim();
            } while (cont != null && (cont == "y" || cont == "Y"));
        }
    }
}
