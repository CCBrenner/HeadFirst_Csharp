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
        private readonly List<Opponent> foundOpponents = new List<Opponent>();
        public Location CurrentLocation { get; private set; }
        public int MoveNumber { get; set; } = 1;
        public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";
        public bool GameOver => Opponents.Count() == foundOpponents.Count();
        public string Status => $"You are {HandleLandingGrammar(CurrentLocation.Name)} the {CurrentLocation.Name}. " +
            $"You see the following exits:" +
            $"{Environment.NewLine}" +
            $"{string.Join($"{Environment.NewLine}", CurrentLocation.ExitList)}" +
            $"{MentionHidingPlace(CurrentLocation)}" +
            $"{Environment.NewLine}You have found {foundOpponents.Count()} of {Opponents.Count()} opponents: {string.Join(", ", foundOpponents)}";
        public IEnumerable<Opponent> Opponents = new List<Opponent>()
        {
            new Opponent("Joe"),
            new Opponent("Bob"),
            new Opponent("Ana"),
            new Opponent("Owen"),
            new Opponent("Jimmy"),
        };
        private string HandleLandingGrammar(string location) => location == "Landing" ? "on" : "in";
        public bool Move(Direction exitDirection)
        {
            bool canMove = CurrentLocation.Exits.ContainsKey(exitDirection);
            if (canMove) CurrentLocation = CurrentLocation.Exits[exitDirection];
            return canMove;
        }
        public string ParseInput(string input)
        {
            MoveNumber++;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = input.ToLower();
            string[] inputArr = input.Split(" ");
            for (int i = 0; i < inputArr.Length; i++)
                inputArr[i] = textInfo.ToTitleCase(inputArr[i]);
            string formattedInput = string.Join("", inputArr);

            if(formattedInput.ToLower() == "check")
            {
                if((CurrentLocation is LocationWithHidingPlace location) && location.HidingPlace != "")
                {
                    IEnumerable<Opponent> foundOpponents = location.CheckHidingPlace();
                    if(foundOpponents.Count() > 0)
                    {
                        this.foundOpponents.AddRange(foundOpponents);
                        return $"You found {foundOpponents.Count()} opponent{House.S(foundOpponents.Count())} hiding {location.HidingPlace}";
                    }
                    else
                        return $"Nobody was hiding {location.HidingPlace}";
                }
                else
                    return $"There is no hiding place in the {CurrentLocation.Name}";
            }
            else if (Enum.TryParse(formattedInput, out Direction direction))
            {
                if (Move(direction))
                    return $"Moving {direction}";
                else
                    return "There's no exit in that direction.";
            }
            else
                return "That's not a valid direction.";
        }
        private string MentionHidingPlace(Location location)
        {
            if (location is LocationWithHidingPlace locationWithHidingPlace && locationWithHidingPlace.HidingPlace != "")
                return $"{Environment.NewLine}Someone could hide {locationWithHidingPlace.HidingPlace}";
            else
                return "";
        }
    }
}
