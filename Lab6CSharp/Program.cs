using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Введіть номер завдання:");
        Console.WriteLine("1 - Вивід об'єктів (Detail, Mechanism, Product, Node)");
        Console.WriteLine("2 - Завдання 2");
        Console.WriteLine("3 - Завдання 3");
        Console.WriteLine("4 - Завдання 4");
        Console.Write("Ваш вибір: ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Task1();
                break;

            case "2":
                Task2();
                break;

            case "3":
                TupleProcessor.RunTupleExample();
                break;
            case "4":
                Testing.RunTask4();
                break;
            default:
                Console.WriteLine("Невідомий вибір.");
                break;
        }
    }

    static void Task1()
    {
        List<IShowable> items = new List<IShowable>
        {
            new Detail("Гвинт", 0.05),
            new Mechanism("Редуктор", 12.5, 20),
            new Product("Болгарка", 4.2, "Bosch"),
            new Node("Кронштейн", 1.1, "Алюміній")
        };

        Console.WriteLine("\n Вивід об'єктів:");
        foreach (var item in items)
        {
            item.Show();
            Console.WriteLine();
        }
    }

    static void Task2()
    {
        List<Client> clients = new List<Client>
    {
        new Depositor("Іваненко", new DateTime(2023, 5, 10), 15000, 5),
        new Creditor("Петренко", new DateTime(2024, 1, 15), 20000, 10, 5000),
        new Organization("ТОВ Роса", new DateTime(2023, 5, 10), "UA12345678", 500000)
    };

        Console.WriteLine("\n Повна інформація про клієнтів:");
        foreach (var client in clients)
        {
            client.ShowInfo();
            Console.WriteLine();
        }

        Console.Write("\n Введіть дату для пошуку (у форматі рррр-мм-дд): ");
        string input = Console.ReadLine();
        if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime searchDate))
        {
            Console.WriteLine($"\nРезультати пошуку клієнтів на дату {searchDate.ToShortDateString()}:");
            bool found = false;
            foreach (var client in clients)
            {
                if (client.MatchesDate(searchDate))
                {
                    client.ShowInfo();
                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Немає клієнтів на цю дату.");
        }
        else
        {
            Console.WriteLine("Невірний формат дати.");
        }
    }

    static void Task3()
    {
        Console.WriteLine("Завдання 3 ще не реалізовано.");
    }
}
