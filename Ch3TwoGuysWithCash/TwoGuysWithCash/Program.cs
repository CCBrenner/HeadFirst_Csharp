// See https://aka.ms/new-console-template for more information

class Program
{
    private static void Main(string[] args)
    {
        // Create a new Guy object in a variable called joe
        // Set its Name field to "Joe"
        // Set its Cash field to 50
        Guy joe = new Guy() { Name = "Joe", Cash = 50 };

        // Create a new Guy object in a variable called bob
        // Set its Name field to "Bob"
        // Set its Cash field to 100
        Guy bob = new Guy() { Name = "Bob", Cash = 100 };

        while (true)
        {
            // Call the WriteMyInfo methods for each Guy object
            joe.WriteMyInfo();
            bob.WriteMyInfo(); 

            Console.Write("Enter an amount: ");
            string howMuch = Console.ReadLine();

            // Use int.TryParse to try to convert the howMuch string to an int
            // if it was successful (just like you did earlier in the chapter)
            if (int.TryParse(howMuch, out int amtYetToGive))
            {
                Console.Write("Who should give the cash: ");
                string whichGuy = Console.ReadLine();
                if (whichGuy == "Joe")
                {
                    // Call the joe object's GiveCash method and save the results
                    int cashInTransition = joe.GiveCash(amtYetToGive);
                    // Call the bob object's ReceiveCash method with the saved results
                    bob.ReceiveCash(cashInTransition);
                }
                else if (whichGuy == "Bob")
                {
                    // Call the bob object's GiveCash method and save the results
                    int cashInTransition = bob.GiveCash(amtYetToGive);
                    // Call the joe object's ReceiveCash method with the saved results
                    joe.ReceiveCash(cashInTransition);
                }
                else
                {
                    Console.WriteLine("Please enter 'Joe' or 'Bob'.");
                }
            }
            else
            {
                Console.WriteLine("Please enter an amount (or a blank line to exit).");
            }
            Console.WriteLine("");
        }
    }
}
class Guy
{
    public string Name = "";
    public int Cash;

    /// <summary>
    /// Writes name and amount of bucks this guy has.
    /// </summary>
    public void WriteMyInfo()
    {
        Console.WriteLine("My name is " + Name + " and I have " + Cash + " bucks.");
    }

    /// <summary>
    /// Gives some of my cash, removing it from my wallet (or printing
    /// a message to the console if I don't have enough cash).
    /// </summary>
    /// <param name="amount">Amount of cash to give.</param>
    /// <returns>
    /// The amount of cash removed from my wallet, or 0 if I don't
    /// have enough cash (or if the amount is invalid).
    /// </returns>
    public int GiveCash(int amount)
    {
        // Will be thrown if Cash = 0 and Guy tries to give money; ultimately would include code for handling it
        if (Cash <= 0) throw new OutOfCashException($"{Name} ran out of cash");

        if (amount <= 0)
        {
            Console.WriteLine(Name + " says:" + amount + " isn't a valid amount.");
            return amount;
        } 
        if (amount > Cash)
        {
            Console.WriteLine(Name + " says: I don't have enough to give you " + amount + " bucks.");
            return 0;
        }
        else
        {
            Cash -= amount;
            return amount;
        }
    }

    /// <summary>
    /// Receive some cash, adding it to my wallet (or printing
    /// a message to the console if the amount is invalid).
    /// </summary>
    /// <param name="amount">Amount of cash to give.</param>
    public void ReceiveCash(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine(Name + " says:" + amount + " isn't a valid amount.");
        }
        else
        {
            Cash += amount;
        }
    }
}

class OutOfCashException : System.Exception
{
    public OutOfCashException(string message) : base(message) { }
}
