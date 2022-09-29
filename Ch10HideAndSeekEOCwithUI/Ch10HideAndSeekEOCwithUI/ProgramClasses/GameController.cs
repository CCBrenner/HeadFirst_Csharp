using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ch10HideAndSeekEOCwithUI
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
        public List<Opponent> FoundOpponents = new List<Opponent>();
        public Location CurrentLocation { get; private set; }
        private IEnumerable<Location> locationHubs { get; }
        public int MoveNumber { get; set; } = 1;
        public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";
        public bool GameOver => Opponents.Count() == FoundOpponents.Count();
        private string status;
        public string Status => $"You are {HandleLandingGrammar(CurrentLocation.Name)} the {CurrentLocation.Name}. " +
            $"You see the following exits:" +
            $"{Environment.NewLine}" +
            $"{string.Join($"{Environment.NewLine}", CurrentLocation.ExitList)}" +
            $"{MentionHidingPlace(CurrentLocation)}" +
            $"{Environment.NewLine}You have found {FoundOpponents.Count()} of {Opponents.Count()} opponents: {string.Join(", ", FoundOpponents)}"; 
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
        private string Save(string nameForSavedFile)
        {
            SaveGame saveGame = new SaveGame();
            return saveGame.Save(this, nameForSavedFile);
        }
        private string Load(string nameOfFileToLoad)
        {
            SaveGame loadedGame = new SaveGame();
            string loadedGameResponse = loadedGame.Load(this, nameOfFileToLoad);
            if (loadedGameResponse != "") return loadedGameResponse;

            CurrentLocation = House.GetLocationByName(loadedGame.CurrentLocationName);
            MoveNumber = loadedGame.MoveNumber;
            status = loadedGame.Status;

            FoundOpponents.Clear();
            List<Opponent> foundOpponents = new List<Opponent>();
            foreach (string opponent in loadedGame.FoundOpponents) foundOpponents.Add(new Opponent(opponent));
            FoundOpponents = foundOpponents;

            foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
                (House.GetLocationByName(pair.Value) as LocationWithHidingPlace).OpponentsHiddenHere.Clear();
            foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
                (House.GetLocationByName(pair.Value) as LocationWithHidingPlace).OpponentsHiddenHere.Add(new Opponent(pair.Key));

            if (loadedGame != null)
                return $"Loaded game from \"{nameOfFileToLoad}.json\"";
            else
                return "An unknown error occurred.";
        }
        public string ParseInput(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = input.ToLower();
            string[] inputArr = input.Split(" ");

            if (inputArr[0] == "save")
                return Save(inputArr[1]);
            else if (inputArr[0] == "load")
                return Load(inputArr[1]);

            MoveNumber++;

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
                        FoundOpponents.AddRange(foundOpponents);
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
