/*
using Ch10HideAndSeekEOCwithUI;

class ProgramOriginal
{
    public static void Main(string[] args)
    {
        while (true)
        {
            GameController gameController = new GameController();
            while (!gameController.GameOver)
            {
                Console.WriteLine(gameController.Status);
                Console.Write(gameController.Prompt);
                string input = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine(gameController.ParseInput(input));
                Console.WriteLine();
            }
            Console.WriteLine($"You won the game in {gameController.MoveNumber} moves!");
            Console.WriteLine("Press P to play again, any other key to quit.");
            if (Console.ReadKey(true).KeyChar.ToString().ToUpper() != "P") return;
        }
        
    }
}
*/