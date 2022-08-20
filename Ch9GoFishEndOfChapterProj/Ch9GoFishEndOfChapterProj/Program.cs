using System;
using Ch9GoFishEndOfChapterProj;

class Program
{

    static GameController? gameController;
    public static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string? humanName = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter the number of computer opponents: ");
            var response = Console.ReadLine();
            if (int.TryParse(response, out int numOpponents))
            {
                List<string> namesOfOpponents = new List<string>();
                for (int i = 1; i <= numOpponents; i++)
                {
                    namesOfOpponents.Add($"Computer #{i}");
                }
                gameController = new GameController(humanName, namesOfOpponents);
                break;
            }
        }

        int numBooksWon = 0;
        int totalValues = 13;

        while(numBooksWon != totalValues)
        {
            Value askValue = PromptForValue();

            Player playerBeingAsked = PromptForOpponent();

            gameController.NextRound(playerBeingAsked, askValue);

            Console.WriteLine($"{gameController.Status}\n");

            numBooksWon = 0;
            foreach(Player player in gameController.gameState.Players)
                numBooksWon += player.Books.Count();
        }
    }

    static Value PromptForValue()
    {
        string presentCards = "\nYour hand:";
        foreach(Card card in gameController.HumanPlayer.Hand)
            presentCards += $"\n{card}";
        Console.WriteLine(presentCards.ToString());
        while (true)
        {
            Console.Write("What card value do you want to ask for? ");
            string? askValue = Console.ReadLine();
            var cardPresent = gameController.HumanPlayer.Hand.Where(card => askValue == card.Value.ToString());
            if (cardPresent.Count() > 0)
                return cardPresent.First().Value;
            else
                Console.WriteLine("You have no cards in your hand matching that value");
        }
    }

    static Player PromptForOpponent()
    {
        string playersThatCanBeAsked = "";
        int i;
        for(i = 1; i < gameController.gameState.Players.Count(); i++)
        {
            playersThatCanBeAsked += $"\n{i}. {gameController.gameState.Players.Skip(i - 1).First().Name}";
        }
        i--;

        Console.Write($"{playersThatCanBeAsked}\nWho do you want to ask for a card? ");
        while (true)
        {
            if(int.TryParse(Console.ReadLine(), out int playerChosen))
            {
                if(playerChosen > i && playerChosen <= 0)
                {
                    return gameController.gameState.Players.Skip(playerChosen - 1).First();  // This might be wrong skip amt
                }
            }
            Console.WriteLine("\nNot a player option. Please enter a number for a player listed.");
        }
    }
}