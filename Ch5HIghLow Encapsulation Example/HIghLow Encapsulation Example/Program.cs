using System;

namespace Program
{
    static class HiLoGame
    {
        // A constant integer MAXIMUM that defaults to 10. Remember, you can’t use the static keyword with constants.
        public const int MAXIMUM = 10;

        // An instance of Random called random.
        private static Random random = new Random();

        // An integer field called currentNumber that’s initialized to the first random number to guess.
        private static int currentNumber = random.Next(1, MAXIMUM + 1);

        // An integer field called pot with the number of bucks in the pot. Make this field private.
        private static int pot = 10;
        public static int GetPot() {return pot;}

        // A method called Guess with a bool parameter called higher that does the following(look closely at the Main method to see how it’s called):
        public static void Guess(bool higher)
        {
            // It picks the next random number for the player to guess.
            int nextNumber = random.Next();

            // If the player guessed higher and the next number is >= the current number OR if the player guessed lower and the next number is <= the current number, write “You guessed right!” to the console and increment the pot.
            if (higher && currentNumber >= nextNumber || higher != true && currentNumber < nextNumber)
            {
                Console.WriteLine("You guessed right!");
                pot++;
            }

            // Otherwise, write “Bad luck, you guessed wrong.” to the console and decrement the pot.
            else
            {
                Console.WriteLine("Bad luck, you guessed wrong.");
                pot--;
            }

            // Replace the current number with the one chosen at the beginning of the method and writes “The current number is” followed by the number to the console.
            currentNumber = nextNumber;
            Console.WriteLine($"The current number is {currentNumber}.");
        }

        // A method called Hint that finds half the maximum, then writes either “The number is at least { half}” or “The number is at most { half }” to the console and decrements the pot.
        public static void Hint()
        {
            int half = MAXIMUM / 2;
            if (currentNumber > half)
            {
                Console.WriteLine($"The number is at least {half}.");
            }
            else
            {
                Console.WriteLine($"The number is at most {half}.");
            }
        }
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to HiLo.");
        Console.WriteLine($"Guess numbers between 1 and {HiLoGame.MAXIMUM}.");
        HiLoGame.Hint();
        while (HiLoGame.GetPot() > 0)
        {
            Console.WriteLine("Press \'h\' for higher, \'l\' for lower, \'?\' to buy a hint,");
            Console.WriteLine($"or any other key to wuit with {HiLoGame.GetPot()}.");
            char key = Console.ReadKey(true).KeyChar;
            if (key == 'h') HiLoGame.Guess(true);
            else if (key == 'l') HiLoGame.Guess(false);
            else if (key == '?') HiLoGame.Hint();
            else return;
        }
        Console.WriteLine("The pot is empty. Bye!");
    }
}
