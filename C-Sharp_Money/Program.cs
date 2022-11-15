using System;
using System.Text;

namespace C_Sharp_Money
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.OutputEncoding = Encoding.Unicode;


            Money m = new Money(0.98);
            Money m1 = new Money(1.13);

            m += m1;
            Console.WriteLine(m);
            m += 0.55;
            Console.WriteLine(m);

            
        }
    }
}
