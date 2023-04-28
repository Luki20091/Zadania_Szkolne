using ConsoleApp16.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace App
{
    internal class Program
    {
        public static void Main()
        {
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("------> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("MENU ZADAŃ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" <------\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Wpisz numer zadania");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("! (");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("1");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("3");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(")\n");
                int chose = 0;
                try {
                    chose = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception)
                {
                    Main();
                };
                Console.Clear();
                switch (chose)
                {
                    case 1:
                        Zad1();
                        break;
                    case 2:
                        Zad2();
                        break;
                    case 3:
                        Zad3();
                        break;
                }
            } while (true);
        }



        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n*");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Kliknij dowolny przycisk aby wrócić do menu");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("!");
            Console.ReadKey();
        }

        static void Zad1()
        {
            Console.WriteLine("Text: To Tylko Gra");
            Console.WriteLine("Zaszyfrowany: " + Szyfrowanie.ZaszyfrujGADERYPOLUKI("To Tylko Gra"));
            
            Menu();
        }

        public static void Zad2()
        {
            Uzytkownik user = new Uzytkownik("Łukasz", "Kucharczyk");
            Console.WriteLine(user.Login);
            Console.WriteLine(user.Haslo);

            Menu();
        }
        public static void Zad3()
        {
            //Student stud = new Student(new Uzytkownik("Piotr", "Test"), 2017);
            Student stud = new Student("Piotr", "Test", 2017);
            Console.WriteLine(stud.ToString());
            //Console.WriteLine(stud.Equals(user));

            Menu();
        }
    }
}

