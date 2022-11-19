using System;
using System.Numerics;
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

            MoneyMenu m = new();
            m.Menu();


            ////=====================================
            ////Тестування виключень :
            ////=====================================
            ////виключення ОЛІГАРХ
            //Money m1 = new(int.MaxValue);
            ////=====================================
            ////виключення Переповнення змінної
            //Money m1 = new(1);
            //m1 += int.MaxValue;
            ////=====================================
            ////виключення БАНКРОТ
            //Money m1 = new(1);
            //m1 -= 2;
            ////=====================================
            ////виключення ділення на 0
            //Money m1 = new(1);
            //m1 /= 0;

        }
    }
}
