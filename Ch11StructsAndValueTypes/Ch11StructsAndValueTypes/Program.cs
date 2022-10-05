using System;

public struct Dog
{
    public Dog(string name, string breed)
    {
        this.Name = name;
        this.Breed = breed;
    }
    public string Name { get; set; }
    public string Breed { get; set; }

    public void Speak()
    {
        Console.WriteLine("My name is {0} and I'm a {1}", Name, Breed);
    }
}

public class Canine
{
    public Canine(string name, string breed)
    {
        this.Name = name;
        this.Breed = breed;
    }
    public string Name { get; set; }
    public string Breed { get; set; }

    public void Speak()
    {
        Console.WriteLine("My name is {0} and I'm a {1}", Name, Breed);
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Canine spot = new Canine("Spot", "pug");
        Canine bob = spot;
        bob.Name = "Spike";
        bob.Breed = "beagle";
        spot.Speak();
        Dog jake = new Dog("Jake", "poodle");
        Dog betty = jake;
        betty.Name = "Betty";
        betty.Breed = "pit bull";
        jake.Speak();
    }
}