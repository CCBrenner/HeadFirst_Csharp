using System;

enum HairColor
{
    Auburn,
    Black,
    Blonde,
    Blue,
    Brown,
    Gray,
    Platinum,
    Purple,
    Red,
    White,
}

class Guy
{
    public string Name { get; set; }
    public HairStyle Hair { get; set; }
    public Outfit Clothes { get; set; }
    public override string ToString() => $"{Name} with {Hair} wearing {Clothes}.";
}

class HairStyle
{
    public HairColor Color { get; set; }
    public float Length { get; set; }
    public override string ToString() => $"{Length:0.0} inch {Color} hair";
}

class Outfit
{
    public string Top { get; set; }
    public string Bottom { get; set; }
    public string ToString() => $"{Top} and {Bottom}";
}

class Program
{
    public static void Main(string[] args)
    {
        var guys = new List<Guy>()
        {
            new Guy(){Name = "Bpb", Clothes = new Outfit(0)
        }
    }
}