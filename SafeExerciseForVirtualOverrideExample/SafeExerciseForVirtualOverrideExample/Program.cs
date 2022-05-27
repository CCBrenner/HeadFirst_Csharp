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
    protected void ReturnContents(string valuables, SafeOwner owner)
    {
        owner.ReceiveContents(valuables);
    }
}
