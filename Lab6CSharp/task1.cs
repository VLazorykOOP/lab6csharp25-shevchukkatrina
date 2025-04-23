using System;
using System.Collections;
using System.Collections.Generic;

// Користувацькі інтерфейси
public interface IShowable
{
    void Show();
}

public interface IIdentifiable
{
    string Name { get; set; }
}

// Базовий клас
public class Detail : IIdentifiable, IShowable, IComparable<Detail>, ICloneable
{
    public string Name { get; set; }
    public double Weight { get; set; }

    public Detail(string name, double weight)
    {
        Name = name;
        Weight = weight;
    }

    public virtual void Show()
    {
        Console.WriteLine($"[Деталь] Назва: {Name}, Вага: {Weight} кг");
    }

    public int CompareTo(Detail other)
    {
        return Weight.CompareTo(other.Weight);
    }

    public virtual object Clone()
    {
        return new Detail(Name, Weight);
    }
}

public class Mechanism : Detail
{
    public int NumberOfParts { get; set; }

    public Mechanism(string name, double weight, int numberOfParts)
        : base(name, weight)
    {
        NumberOfParts = numberOfParts;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"[Механізм] Кількість частин: {NumberOfParts}");
    }

    public override object Clone()
    {
        return new Mechanism(Name, Weight, NumberOfParts);
    }
}

public class Product : Detail
{
    public string Manufacturer { get; set; }

    public Product(string name, double weight, string manufacturer)
        : base(name, weight)
    {
        Manufacturer = manufacturer;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"[Виріб] Виробник: {Manufacturer}");
    }

    public override object Clone()
    {
        return new Product(Name, Weight, Manufacturer);
    }
}

public class Node : Detail
{
    public string Material { get; set; }

    public Node(string name, double weight, string material)
        : base(name, weight)
    {
        Material = material;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"[Вузол] Матеріал: {Material}");
    }

    public override object Clone()
    {
        return new Node(Name, Weight, Material);
    }
}

// Клас колекції вузлів 
public class NodeCollection : IEnumerable<Node>
{
    private List<Node> nodes = new List<Node>();

    public void Add(Node node)
    {
        nodes.Add(node);
    }

    public IEnumerator<Node> GetEnumerator()
    {
        return nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Testing
{
    public static void RunTask4()
    {
        var collection = new NodeCollection();

        collection.Add(new Node("Вузол A", 2.5, "Алюміній"));
        collection.Add(new Node("Вузол B", 1.2, "Сталь"));
        collection.Add(new Node("Вузол C", 3.1, "Пластик"));

        Console.WriteLine("=== Вивід вузлів через foreach ===");
        foreach (var node in collection)
        {
            node.Show();
            Console.WriteLine("---------------------");
        }
    }
}
