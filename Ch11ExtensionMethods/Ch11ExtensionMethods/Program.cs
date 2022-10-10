using System;

sealed class OrdinaryHuman
{
    public OrdinaryHuman(int weight)
    {
        this.weight = weight;
    }
    private int age;
    int weight;

    public void GoToWork() { /* code for going to work */ }
    public void PayBills() { /* code ofr paying bills */ }
}

static class AmazeballSerum
{
    public static string BreakWalls(this OrdinaryHuman h, double wallDensity) => $"I broke through a wall of {wallDensity} density.";
}

class Program
{
    public static void Main(string[] args)
    {
        OrdinaryHuman steve = new OrdinaryHuman(185);
        Console.WriteLine(steve.BreakWalls(89.2));
    }
}