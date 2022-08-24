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
            
            Value askValue;
            Player playerBeingAsked;

            if (gameController.HumanPlayer.Hand.Count() > 0 || gameController.gameState.Stock.Count() > 0)
            {
                askValue = PromptForValue();
                playerBeingAsked = PromptForOpponent();
            }
            else
            {
                askValue = Value.Null;
                playerBeingAsked = gameController.HumanPlayer;
            }

            Console.WriteLine();

            gameController.NextRound(playerBeingAsked, askValue);

            Console.WriteLine($"{gameController.Status}");

            numBooksWon = 0;
            foreach(Player player in gameController.gameState.Players)
                numBooksWon += player.Books.Count();
        }

        FinalResults();
    }

    static Value PromptForValue()
    {
        string presentCards = $"{Environment.NewLine}Your hand:";
        foreach(Card card in gameController.HumanPlayer.Hand)
            presentCards += $"{Environment.NewLine}{card}";
        Console.WriteLine(presentCards.ToString());

        while (true)
        {
            Console.Write("What card value do you want to ask for? ");
            string? askValue = Console.ReadLine();
            var cardPresent = gameController.HumanPlayer.Hand.Where(card => askValue.ToLower() == card.Value.ToString().ToLower());
            if (cardPresent.Count() > 0)
                return cardPresent.First().Value;
            else
                Console.WriteLine($"You have no cards in your hand matching that value. Please type the name of the value for the card you wish to ask for.{Environment.NewLine}");
        }
    }

    static Player PromptForOpponent()
    {
        string playersThatCanBeAsked = "";
        int i;
        for(i = 1; i <= gameController.Opponents.Count(); i++)
        {
            playersThatCanBeAsked += $"{Environment.NewLine}{i}. {gameController.Opponents.Skip(i - 1).First().Name}";
        }
        i--;
        Console.WriteLine(playersThatCanBeAsked);

        while (true)
        {
            Console.Write($"Who do you want to ask for a card? ");
            if (int.TryParse(Console.ReadLine(), out int playerChosen) && 0 < playerChosen && playerChosen <= i)
                return gameController.Opponents.Skip(playerChosen - 1).First();
            Console.WriteLine($"{Environment.NewLine}Not a player option. Please enter a number for a player listed.");
        }
    }

    static void FinalResults()
    {
        string finalTally = $"{Environment.NewLine}Final Results:";
        int j = 1;
        int k = 0;
        for (int i = 13; i >= 0; i--)
        {
            foreach(Player player in gameController.gameState.Players)
            {
                if(player.Books.Count() == i)
                {
                    finalTally += $"{Environment.NewLine}#{j} - {player.Name} ({i} book{Player.S(i)})";
                    k++;
                }
            }
            j += k;
            k = 0;
        }
        Console.WriteLine(finalTally);
    }
}