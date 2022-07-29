using System;

enum Critics
{
    MuddyCritic,
    RottenTornadoes,
}

enum PriceRange
{
    Cheap, 
    Expensive,
}

class Review
{
    public int Issue { get; set; }
    public Critics Critic { get; set; }
    public double Score { get; set; }

}

class Comic
{
    public string Name { get; set; }
    public int Issue { get; set; }
    public override string ToString() => $"{Name} (Issue: #{Issue})";
    public static readonly IEnumerable<Comic> Catelog = new List<Comic>
    {
        new Comic { Name = "Johnny America vs. the Pinko", Issue = 6 },
        new Comic { Name = "Rock and Roll (limited edition)", Issue = 19 },
        new Comic { Name = "Woman's Work", Issue = 36 },
        new Comic { Name = "Hippie Madness (misprinted)", Issue = 57 },
        new Comic { Name = "Revenge of the New Wave Freak (damaged)", Issue = 68 },
        new Comic { Name = "Black Monday", Issue = 74 },
        new Comic { Name = "Tribal Tatoo Madness", Issue = 83 },
        new Comic { Name = "The Death of the Object", Issue = 97 },
    };
    public static readonly IReadOnlyDictionary<int, decimal> Prices = new Dictionary<int, decimal>
    {
        { 6, 3600M },
        { 19, 500M },
        { 36, 650M },
        { 57, 13525M },
        { 68, 250M },
        { 74, 75M },
        { 83, 25.75M },
        { 97, 35.25M },
    };

    public static readonly IEnumerable<Review> Reviews = new[]
    {
        new Review() { Issue = 36, Critic = Critics.MuddyCritic, Score = 37.6 },
        new Review() { Issue = 74, Critic = Critics.RottenTornadoes, Score = 22.8 },
        new Review() { Issue = 74, Critic = Critics.MuddyCritic, Score = 84.2 },
        new Review() { Issue = 83, Critic = Critics.RottenTornadoes, Score = 89.4 },
        new Review() { Issue = 96, Critic = Critics.MuddyCritic, Score = 98.1 },
    };
}

class Program
{
    public static void Main(string[] args)
    {
        var done = false;
        Console.WriteLine("\nPress G to group comics by price, R to get reviews, and any other key quit.");
        while (!done)
        {
            switch (Console.ReadKey(true).KeyChar.ToString().ToUpper())
            {
                case 'G':
                    done = GroupComicsByPrice();
                    break;
                case 'R':
                    done = GetReviews();
                    break;
                default:
                    done = true;
            }
        }
    }
    private bool 
}