// See https://aka.ms/new-console-template for more information
using System;

static class Program
{
    public class Elephant
    {
        public string Name;
        public int EarSizeInInches;

        public string WhoAmI()
        {
            return "My name is " + Name + ".\nMy ears are " + EarSizeInInches + " inches tall.\n";
        }
        public string HearMessage(string message, Elephant whoSaidIt)
        {
            return Name + " heard a message.\n" + whoSaidIt.Name + " said this: " + message + "\n";
        }
        public string SpeakTo(Elephant whoToTalkTo, string message)
        {
            // Use of 'this' refers to the object instance who's method is being called.
            // For a successful transfer of this metho's return data, this method
            // also needs to return the data returned to it by the method it calls
            return whoToTalkTo.HearMessage(message, this);
        }
    }

    public static void Main(string[] args)
    {
        // Create elephants with fields
        Elephant elephant1 = new Elephant() { Name = "Skippy", EarSizeInInches = 30 };
        Elephant elephant2 = new Elephant() { Name = "Donald", EarSizeInInches = 24 };

        // Loop exit variable
        bool programLoopOn = true;

        // Loop conditional
        while (programLoopOn)
        {
            // Write options
            Console.WriteLine("Press 1 for " + elephant1.Name + " (elephant1),\n      2 for " + elephant2.Name + " (elephant2),\n      3 to swap references of objects,\n      4 to make references share an object,\n      5 to share a message.");

            // Take in choice
            char input = Console.ReadKey(true).KeyChar;

            // Note: For char conditionals, type char needs to be compared using single quotation marks (not double)
            if (input == '1')
            {
                Console.WriteLine("You chose 1.\nCalling elephant1.WhoAmI()\n" + elephant1.WhoAmI());
            }
            else if (input == '2')
            {
                Console.WriteLine("You chose 2.\nCalling elephant2.WhoAmI()\n" + elephant2.WhoAmI());
            }
            else if (input == '3')
            {
                Console.WriteLine("You chose 3.\nSwitching object references.\n(Now, don't get it twisted!)\n");
                Elephant[] newElephants = SwitchElephantReferences(elephant1, elephant2);
                elephant1 = newElephants[0];
                elephant2 = newElephants[1];
            }
            else if (input == '4')
            {
                Console.WriteLine("You chose 4.\nAssigning reference \"elephant1\" to \"elephant2\" object.\n");
                elephant1 = elephant2;
            }
            else if (input == '5')
            {
                Console.WriteLine("You chose 5.\n" + elephant1.SpeakTo(elephant2, "You're a nerd!"));
            }
            // Could also write the following lines with an outter if statement verifying correct
            // input and skipping the code below if it matches any of the above cases - but
            // this way here is more succinct/clear
            else
            {
                continue;
            }
            Console.WriteLine("To exit press \"q\". Otherwise press \"Enter\" to continue.\n");
            if (Console.ReadKey(true).KeyChar == 'q')
            {
                programLoopOn = false;
            }
        }
    }

    public static Elephant[] SwitchElephantReferences(Elephant elephant1, Elephant elephant2)
    {
        // You don't need to create a new elephant object - just a new reference to
        // save the object from garbage collection during the transition (no "new" keyword is used)
        Elephant placeholderElephantReference = elephant1;
        elephant1 = elephant2;
        elephant2 = placeholderElephantReference;
        Elephant[] newIdentities = { elephant1, elephant2 };
        return newIdentities;
    }
}
