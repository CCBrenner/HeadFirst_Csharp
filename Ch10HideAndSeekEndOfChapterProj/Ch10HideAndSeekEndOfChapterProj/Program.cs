using Ch10HideAndSeekEndOfChapterProj;

class Program
{
    public static void Main(string[] args)
    {
        GameController gameController = new GameController();
        while (true)
        {
            Console.WriteLine(gameController.Status);
            Console.Write(gameController.Prompt);
            string input = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(gameController.ParseInput(input));
            Console.WriteLine();
        }
    }
}