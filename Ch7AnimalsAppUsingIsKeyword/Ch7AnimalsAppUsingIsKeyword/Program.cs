using System;

interface ISwimmer
{
    void Swim();
}

interface IPackHunter
{
    void HuntInPack();
}

abstract class Animal
{
    public abstract void MakeNoise();
}

class Hippo : Animal, ISwimmer
{
    public override void MakeNoise()
    {
        Console.WriteLine("Grunt.");
    }
    public void Swim()
    {
        Console.WriteLine("Splash! I'm going for a swim!");
    }
}

abstract class Canine : Animal
{
    public bool BelongsToPack { get; protected set; }
}

class Wolf : Canine, IPackHunter
{
    public Wolf(bool belongsToPack)
    {
        BelongsToPack = belongsToPack;
    }
    public override void MakeNoise()
    {
        if (BelongsToPack)
        {
            Console.WriteLine("I'm in a pack.");
        }
        Console.WriteLine("Aroooooo!");
    }
    public void HuntInPack()
    {
        if (BelongsToPack)
        {
            Console.WriteLine("I'm going hunting with my pack!");
        }
        else
        {
            Console.WriteLine("I'm not in a pack.");
        }
    }
}
 class Program
{
    public static void Main(string[] args)
    {
        Animal[] animals =
        {
            new Wolf(false),
            new Hippo(),
            new Wolf(true),
            new Wolf(false),
            new Hippo(),
        };
        foreach(Animal animal in animals)
        {
            animal.MakeNoise();
            // Instead of checking type based on what it *IS* (animal is Hippo hippo), we check based on what it *CAN DO* (because if
            // it can, then we want it to - and the best way to reference multiple unrelated classes that
            // can/should be able to do the same thing is by using interfaces in their classes:) 
            if (animal is ISwimmer swimmer)
            {
                swimmer.Swim();
            }
            // Same here too:
            if (animal is IPackHunter packHunter)
            {
                packHunter.HuntInPack();
            }
            Console.WriteLine();
        }
    }
}