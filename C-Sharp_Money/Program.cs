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


            Money m = new (1.90);
            Money m1 = new (1.9);
            Money m2 = new(int.MaxValue);
            

            Console.WriteLine($"M = {m}\nM1= {m1}");



            Console.WriteLine($"Тест виключення  \"переповнення змінної\" при + : {m} + {double.MaxValue} = {m = m + int.MaxValue}");


            //Console.WriteLine($"Тест виключення  ділення на 0 {m=m/0}");
            //Console.WriteLine($"Тест виключення банкрот {m} - 500 = {m = m - 500}");
            //m = new(12, 22);
            //Console.WriteLine($"Тест виключення  переповнення змінної при множенні: {m} * {int.MaxValue} = {m = m * int.MaxValue}");

        }
    }
}
