using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Money
{
    ////=====================================
    ////Клас гроші
    internal class Money
    {
        ////=====================================
        private int hrn;
        private int kop;
        ////=====================================


        ////=====================================
        ////Конструктори
        public Money(int h, int k)
        {
            SetMoney(h, k);
        }
        public Money(int h)
        {
            SetMoney(h);
        }
        public Money()
        {
            hrn = 0;
            kop = 0;
        }
        public Money(double uah)
        {
            SetMoney(uah);
        }
        ////=====================================


        ////=====================================
        ////Сетери
        public void SetMoney(int h) 
        {
            bool error = false;
            try
            {
                if (h == int.MaxValue)
                    throw new OligarhExeption();

                if (h < 0)
                    throw new BankrotExeption();
            }
            catch (OligarhExeption e)
            {
                Console.WriteLine(e.Message);
                error = true;
            }
            catch (BankrotExeption e)
            {
                Console.WriteLine(e.Message);
                error = true;
            }
            finally 
            {
                if(error)
                {
                    hrn = 0;
                    kop = 0;
                }
                else
                {
                    hrn = h;
                    kop = 0;
                }
            }
        }
        public void SetMoney(int h, int k) 
        {
            bool error = false;
            try
            {
                if (h == int.MaxValue)
                    throw new OligarhExeption();
                if (h < 0 || k < 0)
                {
                    throw new BankrotExeption();
                }

                hrn = h;
                kop = k;
            }
            catch (OligarhExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error)
                {
                    hrn = 0;
                    kop = 0;
                }
                else
                {
                    hrn = h;
                    kop = k;
                }
            }
            Recalculate();
        }
        public void SetMoney(double uah)
        {
            bool error = false;
            try
            {
                if (uah == double.MaxValue)
                    throw new OligarhExeption();
                
                if (uah < 0)
                    throw new BankrotExeption();
            }
            catch (OligarhExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error)
                {
                    hrn = 0;
                    kop = 0;
                }
                else
                {
                    hrn = (int)uah;
                    kop = (int)((uah *100) % 100);
                }
            }
            Recalculate();
        }
        ////=====================================

        ////=====================================
        ////Приватні методи
        private void Recalculate()
        {
            if (kop >= 100)
            {
                hrn += kop / 100;
                kop %=  100;
            }

            if (kop < 0 && hrn > 0)
            {
                hrn -= 1;
                kop += 100;
            }
        }
        ////=====================================


        ////=====================================
        ////Публічні методи
        public void ShowMoney()
        {
            Console.WriteLine($"Грн. {hrn} | {kop} коп.");
        }
        public override string ToString()
        {
            return $"|грн.: {hrn} | коп.: {kop}|";
        }
        ///------Перевантаження оператор додавання
        public static Money operator +(Money m, Money m1)
        {
            bool error = false;
            Money temp = new();
            try
            {
                temp.hrn = checked(m.hrn + m1.hrn);
                temp.kop = checked(m.kop + m1.kop);
                temp.Recalculate();
            }
            catch (OverflowException)
            {
                error = true;
                Console.WriteLine("\n\tВиключення: \"переповнення змінної\"\n"); 
            }
            finally 
            {
                if (error)
                {
                    temp.hrn = m.hrn;
                    temp.kop = m.kop;
                }
                else
                {
                    temp.hrn = m.hrn + m1.hrn;
                    temp.kop = m.kop + m1.kop;
                }
            }
            return temp;
        }
        public static Money operator +(Money m, int g)
        {
            bool error = false;
            Money temp = new();
            try
            {
                temp.hrn = checked(m.hrn + g);
                temp.Recalculate();
            }
            catch (OverflowException)
            {
                error = true;
                Console.WriteLine("\n\tВиключення: \"переповнення змінної\"\n");
            }
            finally
            {
                if (error)
                {
                    temp.hrn = m.hrn;
                    temp.kop = m.kop;
                }
                else
                {
                    temp.hrn = m.hrn + g;
                    temp.kop = 0;
                }
            }
                return temp;
        }
        public static Money operator +(Money m, double d)
        {
            bool error = false;
            Money temp = new();
            try
            {
                temp.hrn = checked(m.hrn + (int)(d));
                temp.kop = checked(m.kop + (int)((d * 100) % 100));
            }
            catch (OverflowException)
            {
                error = true;
                Console.WriteLine("\n\tВиключення: \"переповнення змінної\"\n");
            }
            finally
            {
                if (error)
                {
                    temp.hrn = m.hrn;
                    temp.kop = m.kop;
                }
                else
                {
                    temp.hrn = (m.hrn + (int)(d));
                    temp.kop = (m.kop + (int)((d * 100) % 100));
                    temp.Recalculate();
                }
            }
            return temp;
        }
        ///------Перевантаження оператор різниці
        public static Money operator -(Money m, Money m1)
        {
            bool error = false;
            Money temp = new()
            {
                hrn = m.hrn - m1.hrn,
                kop = m.kop - m1.kop,
            };
            temp.Recalculate();
            try
            {
                if (temp.hrn < 0 || temp.kop < 0)
                {
                    throw new BankrotExeption();
                }
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally 
            { 
                if (error)
                {
                    temp.hrn = 0; 
                    temp.kop = 0;
                }
            }
            return temp;
        }
        public static Money operator -(Money m, int g)
        {
            bool error = false;
            Money temp = new() { hrn = m.hrn - g};
            temp.Recalculate();
            try
            {
                if (temp.hrn < 0 || temp.kop < 0)
                {
                    throw new BankrotExeption();
                }
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error)
                {
                    temp.hrn = 0;
                    temp.kop = 0;
                }
            }
            return temp;
        }
        public static Money operator -(Money m, double d)
        {
            bool error = false;
            Money temp = new();

            try
            {
                temp.hrn = checked(m.hrn - (int)(d));
                temp.kop = checked(m.kop - (int)((d * 100) % 100));

                if (temp.hrn < 0 || temp.kop < 0)
                {
                    throw new BankrotExeption();
                }
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error)
                {
                    temp.hrn = 0;
                    temp.kop = 0;
                }
                else
                {
                    temp.hrn = (m.hrn - (int)(d));
                    temp.kop = (m.kop - (int)((d * 100) % 100));
                    temp.Recalculate();
                }
            }
            return temp;
        }
        ///------Перевантаження оператор ділення
        public static Money operator /(Money m, int g)
        {
            bool error = false;

            Money temp = new();
            try
            {
                if(g == 0) 
                { 
                    throw new DivideByZeroException("\n\tВиключення: ділення на нуль\n");
                }
                temp.hrn = m.hrn / g;
                temp.kop = m.kop / g;
               
                temp.Recalculate();
            }
            catch (DivideByZeroException e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error)
                {
                    g = 1;
                    temp.hrn = m.hrn / g;
                    temp.kop = m.kop / g;
                    Console.WriteLine("Значення 0 - замінено на 1");
                }

            }
            return temp;
        }
        ///------Перевантаження оператор множення
        public static Money operator *(Money m, int g)
        {
            bool error = false;
            Money temp = new();
            try
            {
                temp.hrn = checked(m.hrn * g);
                temp.kop = checked(m.kop * g);
            }
            catch (OverflowException)
            {
                error = true;
                Console.WriteLine("\n\tВиключення: \"переповнення змінної\"\n");
            }
            finally
            {
                if (error)
                { 
                    temp.hrn = m.hrn; 
                    temp.kop = m.kop; 
                }
                else
                {
                    temp.hrn = m.hrn * g;
                    temp.kop = m.kop * g;
                    temp.Recalculate();
                }
            }
            return temp; 
    }
        ///------Перевантаження оператор інкремент
        public static Money operator ++(Money m)
        {
            bool error = false;
            try
            {  
                m.kop = checked(m.kop++);
                m.Recalculate();

            }
            catch (OverflowException)
            {
                error = true;
                Console.WriteLine("\n\tВиключення: \"переповнення змінної\"\n");
            }
            finally
            {
                if (error)
                    m.kop = 0;
                else
                {
                    m.kop++;
                    m.Recalculate();
                }    
            }
            return m;
        }
        ///------Перевантаження оператор декремент
        public static Money operator --(Money m)
        {
            bool error = false;
            m.kop--;
            m.Recalculate();
            try
            {
                if (m.hrn < 0 || m.kop < 0)
                {
                    throw new BankrotExeption();
                }
            }
            catch (BankrotExeption e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (error) { m.hrn = 0; m.kop = 0; }
            }
            return m;
        }
        ///------Перевантаження операторів порівняння
        public static bool operator ==(Money m, Money m2)
        {
            m.Recalculate();
            m2.Recalculate();

            int temp1 = (m.hrn * 100) + m.kop;
            int temp2 = (m2.hrn * 100) + m2.kop;

            return (temp1 == temp2);
        }
        public static bool operator !=(Money m, Money m2)
        {
            m.Recalculate();
            m2.Recalculate();

            double temp1 = m.hrn + (m.kop / 100);
            double temp2 = m2.hrn + (m2.kop / 100);

            return !(temp1 == temp2);
        }
        public bool Equals(Money m)
        {
            return (m == this);
        }
        public override bool Equals(object obj)
        {
            return (obj as Money).Equals(this);
        }
        public override int GetHashCode()
        {
            return hrn ^ kop;
        }
        public static bool operator <(Money m1, Money m2)
        {
            m1.Recalculate();
            m2.Recalculate();

            int temp1 = (m1.hrn * 100) + m1.kop;
            int temp2 = (m2.hrn * 100) + m2.kop;

            return (temp1 < temp2);
        }
        public static bool operator >(Money m1, Money m2)
        {
            m1.Recalculate();
            m2.Recalculate();

            int temp1 = (m1.hrn * 100) + m1.kop;
            int temp2 = (m2.hrn * 100) + m2.kop;

            return (temp1 > temp2);
        }
        ////=====================================
    }

    ////=====================================
    ////Власний тип помилки банкрот
    public class BankrotExeption : Exception
    {
       public BankrotExeption() : base("\n\tВиключення: Банкрот!\n") { }
    }
    ////=====================================


    ////=====================================
    ////Власний тип помилки "олігарх"
    public class OligarhExeption : Exception
    {
        public OligarhExeption() : base("\n\tВиключення: Олігарх! (ваші кошти було націоналізовано НАБУ)\n ") { }
    }
    ////=====================================


    ////=====================================
    ////Клас грошове меню
    internal class MoneyMenu
    {
        ////=====================================
        private Money money = new();
        ////=====================================


        ////=====================================
        ////Публічні методи
        public void Menu() 
        {
            money = CreateMoney();
            Console.WriteLine(money);
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t\t Поточний баланс: {money}");
                Console.WriteLine("Виберіть дію:");
                Console.WriteLine("0 - Вийти");
                Console.WriteLine("1 - Перевірка роботи перевантаженого +,- (відносно класу гроші)");
                Console.WriteLine("2 - Перевірка роботи перевантаженого + - * / (відносно цілого числа)");
                Console.WriteLine("3 - Перевірка роботи перевантаженого + та - (відносно дабла числа)");
                Console.WriteLine("4 - Перевірка роботи перевантаженого ++ та -- ");
                Console.WriteLine("5 - Перевірка роботи перевантаженого == != < >");


                bool error = false;

                try
                {
                    char input = char.Parse(Console.ReadLine());

                    if (input == '0') { break; }
                    else if(input == '1')
                    {
                        Console.Clear();
                        Console.WriteLine("Перевірка роботи перевантаженого + - (відносно класу гроші)");
                        DemoMoney();
                    }
                    else if (input == '2')
                    {
                        Console.Clear();
                        Console.WriteLine("Перевірка роботи перевантаженого + - * / (відносно цілого числа)");
                        DemoInt();
                    }
                    else if (input == '3')
                    {
                        Console.Clear();
                        Console.WriteLine("Перевірка роботи перевантаженого + - (відносно дабл числа)");
                        DemoDouble();
                    }
                    else if (input == '4')
                    {
                        Console.Clear();
                        Console.WriteLine("Перевірка роботи перевантаженого ++ --");
                        DemoIncrDecr();
                    }
                    else if (input == '5')
                    {
                        Console.Clear();
                        Console.WriteLine("Перевірка роботи перевантаженого ++ --");
                        DemoCompare();
                    }

                }
                catch (System.Exception)
                {
                    error = true;
                    Console.WriteLine("\tВиключення: \"порожні данні вводу\"\n");
                }
                finally
                {
                    if (error)
                    {
                        Console.Clear();
                        Console.WriteLine("Спробуйте ще");
                        Console.ReadKey();
                    }
                }
            }
        }
        ////=====================================


        ////=====================================
        ////Приватні методи
        private Money CreateMoney()
        {
            Money temp = new();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введіть скільки грошей ви хочите мати");
                Console.WriteLine("Але спершу виберіть формат у якому ви хочите ввести данні:");
                Console.WriteLine("1- Якщо хочите ввести лише гривні");
                Console.WriteLine("2- Якщо хочите ввести гривні та копійки(цілими числами)");
                Console.WriteLine("3- Якщо хочите ввести гривні та копійки(числом із комою)");

                bool error = false;

                try
                {
                    char input = char.Parse(Console.ReadLine());

                    if (input == '1') 
                    {
                        Console.WriteLine("Введіть гривні");
                        int hrn = Int32.Parse(Console.ReadLine());
                        temp.SetMoney(hrn);
                        return temp;
                    }
                    else if (input == '2')
                    {
                        Console.WriteLine("Введіть гривні");
                        int hrn = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Введіть копійки");
                        int k = Int32.Parse(Console.ReadLine());
                        temp.SetMoney(hrn, k);
                        return temp;
                    }
                    else if (input == '3')
                    {
                        Console.WriteLine("Введіть значення у форматі ХХ,ХХ");
                        double uah = double.Parse(Console.ReadLine());
                        temp.SetMoney(uah);
                        return temp;
                    }
                }
                catch (System.Exception)
                {
                    error = true;
                    Console.WriteLine("\tВиключення: \"порожні данні вводу\"\n");
                }
                finally
                {
                    if (error)
                    {
                        Console.Clear();
                        Console.WriteLine("Спробуйте ще");
                        Console.ReadKey();
                    }
                }
            }

        }
        private void DemoMoney()
        {
            Console.WriteLine("Введіть операцію яку потрібно зробити між класами");
            char op = GetCharOperation("+-");

            Console.WriteLine("Створимо ще один тимчасовий клас гроші");
            Money temp = CreateMoney();
            Console.WriteLine($"\nПоточний баланс: {money}");
            Console.WriteLine($"Баланс на тимчасовому класі: {temp}\n");
            Console.WriteLine($"{money}\n\t{op}\n{temp}\n\t=");
            
            if (op == '+')
                money += temp;
            else if (op == '-')
                money -= temp;

            Console.WriteLine(money);
            Console.ReadKey();
        }
        private void DemoInt()
        {
            Console.WriteLine("Введіть операцію яку потрібно зробити між класом та цілим числом");
            char op = GetCharOperation("+-*/");

            Console.WriteLine("Введіть ціле число яке потрібно додати до ваших грошей");
            int temp = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine($"\nПоточний баланс: {money}");
            Console.WriteLine($"Баланс на тимчасовому класі: {temp}\n");
            Console.WriteLine($"{money}\n\t{op}\n{temp}\n\t=");

            if (op == '+')
                money += temp;
            else if (op == '-')
                money -= temp;
            else if(op == '*')
                money *= temp;
            else if(op == '/')
                money /= temp;

            Console.WriteLine(money);
            Console.ReadKey();
        }
        private void DemoDouble()
        {
            Console.WriteLine("Введіть операцію яку потрібно зробити між класом та дабл числом");
            char op = GetCharOperation("+-");

            Console.WriteLine("Введіть ціле число яке потрібно додати до ваших грошей");
            double temp = double.Parse(Console.ReadLine());

            Console.WriteLine($"\nПоточний баланс: {money}");
            Console.WriteLine($"Баланс на тимчасовому класі: {temp}\n");
            Console.WriteLine($"{money}\n\t{op}\n{temp}\n\t=");

            if (op == '+')
                money += temp;
            else if (op == '-')
                money -= temp;


            Console.WriteLine(money);
            Console.ReadKey();
        }
        private void DemoIncrDecr()
        {
            Console.WriteLine("Введіть операцію яку потрібно зробити +(інкремент) -(декремент)");
            char op = GetCharOperation("+-");
            
            Console.WriteLine($"\nПоточний баланс: {money}");
            
            Console.WriteLine($"{money}\n\t{op}{op}\n\t=");

            if (op == '+')
                money++;
            else if (op == '-')
                money--;

            Console.WriteLine(money);
            Console.ReadKey();
        }
        private void DemoCompare()
        {
            Console.WriteLine("Створимо ще один тимчасовий клас гроші");
            Money temp = CreateMoney();
            Console.WriteLine($"\nПоточний баланс: {money}");
            Console.WriteLine($"Баланс на тимчасовому класі: {temp}\n");

            Console.WriteLine($"{money} == {temp} : {money == temp}");
            Console.WriteLine($"{money} != {temp} : {money != temp}");
            Console.WriteLine($"{money} > {temp} : {money > temp}");
            Console.WriteLine($"{money} < {temp} : {money < temp}");

            Console.ReadKey();
        }
        private char GetCharOperation(string charlist)
        {         
            while (true)
            {
                Console.WriteLine("Виберіть дію: " +  charlist);
                bool error = false;

                try
                {
                    char operation = char.Parse(Console.ReadLine());

                    
                    if (operation == '+' && charlist.Contains(operation))
                        return '+';
                    else if (operation == '-' && charlist.Contains(operation))
                        return '-';
                    else if (operation == '*' && charlist.Contains(operation))
                        return '*';
                    else if (operation == '/' && charlist.Contains(operation))
                        return '/';
                }
                catch (System.Exception)
                {
                    error = true;
                    Console.WriteLine("\tВиключення: \"порожні данні вводу\"\n");
                }
                finally
                {
                    if (error)
                    {
                        Console.Clear();
                        Console.WriteLine("Спробуйте ще");
                        Console.ReadKey();
                    }
                }

            }
        }
        ////=====================================

    }
}
