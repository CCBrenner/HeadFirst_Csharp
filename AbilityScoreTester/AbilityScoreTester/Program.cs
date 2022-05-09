// See https://aka.ms/new-console-template for more information

class AbilityScoreCalculator 
{
    public int RollResult = 14;
    public double DivideBy = 1.75;
    public int AddAmount = 2;
    public int Minimum = 3;
    public int Score;

    public void CalculateAbilityScore()
    {
        // Divide the roll result by the Result field
        double divided = RollResult / DivideBy;

        // Add AddAmount ot the rsult of that division
        int added = AddAmount + (int) divided;

        // If the result is too small , use Minimum
        if (added < Minimum)
        {
            Score = Minimum;
        }
        else
        {
            Score = added;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        AbilityScoreCalculator calculator = new AbilityScoreCalculator();
        while (true)
        {
            calculator.RollResult = ReadInt(calculator.RollResult, "Starting 4d6 roll");
            calculator.DivideBy = ReadDouble(calculator.DivideBy, "Divide by");
            calculator.AddAmount = ReadInt(calculator.AddAmount, "Add amount");
            calculator.Minimum = ReadInt(calculator.Minimum, "Minimum");
            calculator.CalculateAbilityScore();
            Console.WriteLine("Calculated ability score: " + calculator.Score);
            Console.WriteLine("Press Q to quit, any other key to continue");
            char keyChar = Console.ReadKey(true).KeyChar;
            if ((keyChar == 'Q') || (keyChar == 'q')) return;
        }
    }

    /// <summary>
    /// Writes a prompt and reads an int value from the console.
    /// </summary>
    /// <param name="lastUsedValue">The default value.</param>
    /// <param name="prompt">Prompt to print to the console.</param>
    /// <returns>The int value read, or the default value if unable to parse.</returns>
    static int ReadInt(int lastUsedValue, string prompt)
    {
        // Write the prompt followed by [default value]:
        Console.WriteLine(prompt + " [" + lastUsedValue + "] ");

        // Read the line from the input and use int.TryParse to attempt to parse it
        string userInput = Console.ReadLine();
        if (int.TryParse(userInput, out int value))
        {
            // If it can be parsed, write " using value " + value to the console
            Console.WriteLine(" using value " + value);
            return value;
        }
        else
        {
            // Otherwise write " using default value" + lastUsedValue to the console
            Console.WriteLine(" using default value.");
            return lastUsedValue;
        }
    }

    /// <summary>
    /// Writes a prompt and reads a doble value from the console.
    /// </summary>
    /// <param name="lastUsedValue">The default value.</param>
    /// <param name="prompt">Prompt to print to the console.</param>
    /// <returns>The double value read, or the default value if unable to parse.</returns>
    static double ReadDouble(double lastUsedValue, string prompt)
    {
        // Write the prompt followed by [default value]:
        Console.WriteLine(prompt + " [" + lastUsedValue + "] ");

        // Read the line from the input and use int.TryParse to attempt to parse it
        string userInput = Console.ReadLine();
        if (double.TryParse(userInput, out double value))
        {
            // If it can be parsed, write " using value " + value to the console
            Console.WriteLine(" using value " + value);
            return value;
        }
        else
        {
            // Otherwise write " using default value" + lastUsedValue to the console
            Console.WriteLine(" using default value.");
            return lastUsedValue;
        }
    }
}
