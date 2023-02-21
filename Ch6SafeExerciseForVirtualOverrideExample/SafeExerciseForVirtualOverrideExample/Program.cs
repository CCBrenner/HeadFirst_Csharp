using System;

class Safe
{
    private string contents = "precious jewels";
    private string safeCombination = "12345";

    public string Open(string combination)
    {
        if (combination == safeCombination) return contents;
        return "";
    }
    public void PickLock(Locksmith lockpicker)
    {
        lockpicker.Combination = safeCombination;
    }
}
class SafeOwner
{
    private string valuables = "";
    public void ReceiveContents(string SafeContents)
    {
        valuables = SafeContents;
        Console.WriteLine($"Thank you for returning my {valuables}!");
    }
}
class Locksmith
{
    public string Combination { private get; set; } = "";
    public void OpenSafe(Safe safe, SafeOwner owner)
    {
        safe.PickLock(this);
        string safeContents = safe.Open(Combination);
        ReturnContents(safeContents, owner);
    }
    protected virtual void ReturnContents(string valuables, SafeOwner owner)
    {
        owner.ReceiveContents(valuables);
    }
}
class JewelThief : Locksmith
{
    private string stolenJewels;
    protected override void ReturnContents(string safeContents, SafeOwner owner)
    {
        stolenJewels = safeContents;
        Console.WriteLine($"I'm stealing the jewels! I stole: {stolenJewels}");
    }
}
class Program
{
    public static void Main(string[] args)
    {
        SafeOwner owner = new SafeOwner();
        Safe safe = new Safe();
        JewelThief lockSmith = new JewelThief();
        // This calls the Locksmith.ReturnContents() method instead of the JewelThief.ReturnContents() method
        // lockSmith.ReturnContents(safe.Open("12345"), owner);
        lockSmith.OpenSafe(safe, owner);
        Console.ReadKey(true);
    }
}
