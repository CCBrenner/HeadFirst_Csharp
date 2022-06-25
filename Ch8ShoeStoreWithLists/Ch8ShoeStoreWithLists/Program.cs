using System;
using System.Collections.Generic;

enum Style {
    Sneaker, 
    Loafer,
    Sandal,
    FlipFlop,
    WingTip,
    Clog,
}

class Shoe
{
    public Style Style { get; private set; }
    public string Color { get; private set; }
    public Shoe(Style style, string color)
    {
        Style = style;
        Color = color;
    }
    public string Description()
    {
        return $"a {Color} {Style}";
    }
}

class ShoeCloset
{
    private readonly List<Shoe> shoes = new List<Shoe>();
    public void PrintShoes()
    {
        if (shoes.Count == 0)
        {
            Console.WriteLine("\nThe shoe closet is empty.");
        }
        else
        {
            Console.WriteLine("\nThe shoe closet contains:");
            int i = 1;
            foreach(Shoe shoe in shoes)
            {
                Console.WriteLine($"Shoe #{i++}: {shoe.Description()}");
            }
        }
    }
    public void AddShoe()
    {
        Console.WriteLine("\nAdd a shoe");
        for (int i = 0; i < 6; i++){
            Console.WriteLine($"Press {i} to add a {(Style)i}.");
        }
        Console.WriteLine("Enter a style: ");
        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int style))
        {
            Console.Write("\nEnter the color: ");
            string color = Console.ReadLine();
            Shoe shoe = new Shoe((Style)style, color);
            shoes.Add(shoe);
        }
    }
    public void RemoveShoe()
    {
        Console.WriteLine("\nEnter the number of the shoe you want to remove: ");
        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int shoeNumber) 
            && (shoeNumber >= 1) 
            && (shoeNumber <= shoes.Count))
        {
            Console.WriteLine($"Removing {shoes[shoeNumber - 1].Description()}.");
            shoes.RemoveAt(shoeNumber - 1);
        }
    }
}

class Program
{
    static ShoeCloset shoeCloset = new ShoeCloset();
    public static void Main(string[] args)
    {
        while (true)
        {
            shoeCloset.PrintShoes();
            Console.Write("\nPress 'a' to add or 'r' to remove a shoe: ");
            char key = Console.ReadKey().KeyChar;

            switch (key)
            {
                case 'a':
                case 'A':
                    shoeCloset.AddShoe();
                    break;
                case 'r':
                case 'R':
                    shoeCloset.RemoveShoe();
                    break;
                default:
                    return;
            }
        }
    }
}
