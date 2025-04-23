using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class InvalidIndexException : Exception
{
    public InvalidIndexException(string message) : base(message) { }
}

public class NoPeopleWithGivenAgeException : Exception
{
    public NoPeopleWithGivenAgeException(string message) : base(message) { }
}

public class InvalidPhoneNumberException : Exception
{
    public InvalidPhoneNumberException(string message) : base(message) { }
}

public class NegativeAgeException : Exception
{
    public NegativeAgeException(string message) : base(message) { }
}

public class EmptyNameException : Exception
{
    public EmptyNameException(string message) : base(message) { }
}

public class TupleProcessor
{
    public static void RunTupleExample()
    {
        try
        {
            var people = new List<(string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age)>
            {
                ("Іваненко", "Іван", "Іванович", "вул. Шевченка 1", "0671234567", 25),
                ("", "Петро", "Петрович", "вул. Франка 2", "0672345678", 30), // Прізвище порожнє — викличе EmptyNameException
                ("Сидоренко", "Сидір", "Сидорович", "вул. Лесі Українки 3", "06734567", 25), // Неправильний телефон
                ("Коваленко", "Ольга", "Олегівна", "вул. Гончара 4", "0674567890", -5) // Від’ємний вік
            };

            ValidatePeople(people);

            Console.WriteLine("Початковий масив людей:");
            PrintPeople(people);

            int ageToRemove = 100; // Тут немає таких — викличе виняток
            RemovePeopleByAge(people, ageToRemove);

            int indexToAddAfter = 10; // Некоректний індекс — викличе виняток
            var newPerson = (
                LastName: "Новаченко",
                FirstName: "Марія",
                MiddleName: "Василівна",
                Address: "вул. Котляревського 5",
                PhoneNumber: "0675678901",
                Age: 28
            );

            AddPersonAfterIndex(people, indexToAddAfter, newPerson);

            Console.WriteLine("\nМасив після змін:");
            PrintPeople(people);
        }
        catch (InvalidIndexException ex)
        {
            Console.WriteLine($"[Помилка індексу]: {ex.Message}");
        }
        catch (NoPeopleWithGivenAgeException ex)
        {
            Console.WriteLine($"[Помилка видалення]: {ex.Message}");
        }
        catch (InvalidPhoneNumberException ex)
        {
            Console.WriteLine($"[Помилка телефону]: {ex.Message}");
        }
        catch (NegativeAgeException ex)
        {
            Console.WriteLine($"[Помилка віку]: {ex.Message}");
        }
        catch (EmptyNameException ex)
        {
            Console.WriteLine($"[Помилка імені]: {ex.Message}");
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("Недостатньо пам’яті для виконання операції.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Інша помилка]: {ex.Message}");
        }

        Console.ReadKey();
    }

    static void ValidatePeople(List<(string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age)> people)
    {
        foreach (var person in people)
        {
            if (string.IsNullOrWhiteSpace(person.LastName) || string.IsNullOrWhiteSpace(person.FirstName))
                throw new EmptyNameException("Прізвище або ім’я не може бути порожнім.");

            if (!Regex.IsMatch(person.PhoneNumber, @"^\d{10}$"))
                throw new InvalidPhoneNumberException($"Номер телефону '{person.PhoneNumber}' некоректний.");

            if (person.Age < 0)
                throw new NegativeAgeException($"Вік не може бути від’ємним. Задано: {person.Age}");
        }
    }

    static void PrintPeople(List<(string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age)> people)
    {
        for (int i = 0; i < people.Count; i++)
        {
            var person = people[i];
            Console.WriteLine($"{i}. {person.LastName} {person.FirstName} {person.MiddleName}, " +
                             $"Адреса: {person.Address}, Телефон: {person.PhoneNumber}, Вік: {person.Age}");
        }
    }

    static void RemovePeopleByAge(List<(string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age)> people, int age)
    {
        int removed = people.RemoveAll(p => p.Age == age);
        if (removed == 0)
        {
            throw new NoPeopleWithGivenAgeException($"Немає людей з віком {age} для видалення.");
        }
    }

    static void AddPersonAfterIndex(List<(string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age)> people,
                                  int index,
                                  (string LastName, string FirstName, string MiddleName, string Address, string PhoneNumber, int Age) person)
    {
        if (index >= 0 && index < people.Count)
        {
            people.Insert(index + 1, person);
        }
        else
        {
            throw new InvalidIndexException($"Індекс {index} некоректний для вставки.");
        }
    }
}

