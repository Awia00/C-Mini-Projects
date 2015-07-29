using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MariasCMDProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello what is your name?");
            string input= Console.ReadLine();
            Console.WriteLine("hello " + input); 
            Console.WriteLine("is maria the coolest person in the world?");
            string input1 = Console.ReadLine();
            if (input1 == "yes")
            {
                Console.WriteLine("well " + input);
                Console.WriteLine("you are completly rigth!");
                Console.WriteLine("please give her a kiss");

                for (int tal = 1; tal <= 10; tal = tal + 1)
                {
                    //Console.WriteLine(tal);
                    Console.WriteLine(input + " Elsker Maria!");
                }

            }
            else
            {
                Console.WriteLine("wrong anwser "+input);
                Console.WriteLine("please do not give her a kiss");
            }
            
            Metode1();
            Metode2();

            

            Console.ReadKey();
        }

        static void Metode1()
        {
            Console.WriteLine("Det her kaldes fra metode 1");
        }
        static void Metode2()
        {
            Console.WriteLine("Det her kaldes fra metode 2");
        }


    }
}
