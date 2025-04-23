using System;
using System.Collections.Generic;
using System.Globalization;

public interface IBankClient : IComparable<IBankClient>, ICloneable
{
    void ShowInfo();
    bool MatchesDate(DateTime date);
}

public abstract class Client : IBankClient
{
    public abstract void ShowInfo();
    public abstract bool MatchesDate(DateTime date);
    public abstract int CompareTo(IBankClient other);
    public abstract object Clone();
}
public class Depositor : Client
{
    public string LastName { get; set; }
    public DateTime OpenDate { get; set; }
    public double Amount { get; set; }
    public double InterestRate { get; set; }

    public Depositor(string lastName, DateTime openDate, double amount, double rate)
    {
        LastName = lastName;
        OpenDate = openDate;
        Amount = amount;
        InterestRate = rate;
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"[Вкладник] {LastName}, Дата відкриття: {OpenDate.ToShortDateString()}, Внесок: {Amount} грн, Відсоток: {InterestRate}%");
    }

    public override bool MatchesDate(DateTime date)
    {
        return OpenDate.Date == date.Date;
    }

    public override int CompareTo(IBankClient other)
    {
        if (other is Depositor d)
            return Amount.CompareTo(d.Amount);
        return 0;
    }

    public override object Clone()
    {
        return new Depositor(LastName, OpenDate, Amount, InterestRate);
    }
}

public class Creditor : Client
{
    public string LastName { get; set; }
    public DateTime CreditDate { get; set; }
    public double CreditAmount { get; set; }
    public double InterestRate { get; set; }
    public double Debt { get; set; }

    public Creditor(string lastName, DateTime creditDate, double amount, double rate, double debt)
    {
        LastName = lastName;
        CreditDate = creditDate;
        CreditAmount = amount;
        InterestRate = rate;
        Debt = debt;
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"[Кредитор] {LastName}, Дата кредиту: {CreditDate.ToShortDateString()}, Сума: {CreditAmount} грн, Відсоток: {InterestRate}%, Остача боргу: {Debt} грн");
    }

    public override bool MatchesDate(DateTime date)
    {
        return CreditDate.Date == date.Date;
    }

    public override int CompareTo(IBankClient other)
    {
        if (other is Creditor c)
            return CreditAmount.CompareTo(c.CreditAmount);
        return 0;
    }

    public override object Clone()
    {
        return new Creditor(LastName, CreditDate, CreditAmount, InterestRate, Debt);
    }
}

public class Organization : Client
{
    public string Name { get; set; }
    public DateTime AccountDate { get; set; }
    public string AccountNumber { get; set; }
    public double Balance { get; set; }

    public Organization(string name, DateTime date, string number, double balance)
    {
        Name = name;
        AccountDate = date;
        AccountNumber = number;
        Balance = balance;
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"[Організація] {Name}, Дата відкриття рахунку: {AccountDate.ToShortDateString()}, Рахунок №{AccountNumber}, Сума: {Balance} грн");
    }

    public override bool MatchesDate(DateTime date)
    {
        return AccountDate.Date == date.Date;
    }

    public override int CompareTo(IBankClient other)
    {
        if (other is Organization o)
            return Balance.CompareTo(o.Balance);
        return 0;
    }

    public override object Clone()
    {
        return new Organization(Name, AccountDate, AccountNumber, Balance);
    }
}
