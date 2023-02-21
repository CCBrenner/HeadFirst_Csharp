// See https://aka.ms/new-console-template for more information

class Program
{
    private static void Main(string[] args)
    {
        Random random = new Random();
        Guy player = new Guy() { Cash = 100, Name = "Player"};
        double odds = 0.75;
        bool playing = true;

        while (playing == true)
        {
            // Have the Guy object print the amount of cash it has.
            Console.WriteLine(player.Name + " currently has " + player.Cash + " bucks to their name.");

            Console.WriteLine("The odds to beat today are " + odds);

            bool howMuchIsValid = false;
            while(howMuchIsValid == false)
            {
                // Ask the user how much money to bet.
                Console.WriteLine("How much are you willing to wager? ");

                // Read line and assign to variable called howMuch
                string howMuch = Console.ReadLine();

                // Try to parse it into an int variable called amount.
                if (int.TryParse(howMuch, out int amount))
                {
                    // Data validation
                    if (amount > player.Cash)
                    {
                        Console.WriteLine("Player has only " + player.Cash + " bucks to wager! Choose a smaller amount.\n");
                        continue;
                    }
                    if (amount < 0)
                    {
                        Console.WriteLine("Please enter a positive number amount.\n");
                        continue;
                    }
                    if (amount == 0)
                    {
                        Console.WriteLine("Why are you betting zero bucks??? I should kick you out!\n");
                        continue;
                    }

                    // While loop is turned off once user input is valid as int
                    howMuchIsValid = true;

                    // If it parses, the player gives the amount to an int variable called pot. It gets multiplied by two, because it’s a double-or-nothing bet.
                    int pot = 2 * amount;

                    // The program picks a random number between 0 and 1.
                    double pickedRandomDouble = random.NextDouble();

                    // If the number is greater than odds, the player receives the pot.
                    if (pickedRandomDouble > odds)
                    {
                        Console.WriteLine("You beat the odds and won the pot! Here is " + pot + " bucks for you.\n");
                        player.Cash += pot;
                    }

                    // If not, the player loses the amount they bet.
                    else
                    {
                        Console.WriteLine("Sorry, you did not beat the odds. You lost " + amount + " bucks.\n");
                        player.Cash -= amount;
                    }
                }
                // Data validation (for "amount is not int")
                else
                {
                    Console.WriteLine("Please enter a non-decimal number amount.\n");
                    continue;
                }
            }
            
            // The program keeps running while the player has cash.
            if (player.Cash <= 0)
            {
                Console.WriteLine("Sorry, player is out of cash. See ya!\t(Press \'Enter\' to exit.)");
                playing = false;

                Console.ReadLine();
            }
            
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
