using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class GameController
    {
        public GameController() 
        {
            House.ClearHidingPlaces();
            foreach (Opponent opponent in Opponents)
                opponent.Hide();

            CurrentLocation = House.Entry;
        }
        public Location CurrentLocation { get; private set; }
        public string Status => $"You are {HandleLandingGrammar(CurrentLocation.Name)} the {CurrentLocation.Name}. " +
            $"You see the following exits:" +
            $"{Environment.NewLine}" +
            $"{string.Join($"{Environment.NewLine}", CurrentLocation.ExitList)}";
        public int MoveNumber { get; set; } = 1;
        public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";
        public IEnumerable<Opponent> Opponents = new List<Opponent>()
        {
            new Opponent("Joe"),
            new Opponent("Bob"),
            new Opponent("Ana"),
            new Opponent("Owen"),
            new Opponent("Jimmy"),
        };
        private readonly IEnumerable<Opponent> foundOpponents = new List<Opponent>();
        public bool GameOver => Opponents.Count() == foundOpponents.Count();
        private string HandleLandingGrammar(string location) => location == "Landing" ? "on" : "in";
        public bool Move(Direction exitDirection)
        {
            bool canMove = CurrentLocation.Exits.ContainsKey(exitDirection);
            if (canMove) CurrentLocation = CurrentLocation.Exits[exitDirection];
            return canMove;
        }
        public string ParseInput(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = input.ToLower();
            string[] inputArr = input.Split(" ");
            for (int i = 0; i < inputArr.Length; i++)
                inputArr[i] = textInfo.ToTitleCase(inputArr[i]);
            string formattedInput = string.Join("", inputArr);

            if (Enum.TryParse(formattedInput, out Direction direction))
            {
                if (Move(direction))
                    return $"Moving {direction}";
                else
                    return "There's no exit in that direction.";
            }
            else
                return "That's not a valid direction.";
        }

    }
}
