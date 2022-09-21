using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;

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
        private List<Opponent> foundOpponents = new List<Opponent>();
        public Location CurrentLocation { get; private set; }
        private IEnumerable<Location> locationHubs { get; }
        public int MoveNumber { get; set; } = 1;
        public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";
        public bool GameOver => Opponents.Count() == foundOpponents.Count();
        private string status;
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
        private string Save(string nameForSavedFile)
        {
            SaveGame saveGame = new SaveGame(CurrentLocation as LocationWithHidingPlace, MoveNumber, Status, foundOpponents);

            // string savedGame = JsonSerializer.Serialize(saveGame);  // Wasn't serializing the Opponent objects for some reason
            string savedGame = JsonConvert.SerializeObject(saveGame);  // Had to use this method from the Newtonsoft.Json NuGet package

            if (!nameForSavedFile.Contains('\\') && !nameForSavedFile.Contains(' '))
            {
                using (StreamWriter writer = new StreamWriter($"{nameForSavedFile}.json", false))
                    writer.WriteLine(savedGame);
                return $"Saved current game to {nameForSavedFile}";
            }
            return $"Could not save game to {nameForSavedFile}";
        }
        private string Load(string nameOfFileToLoad)
        {
            if (nameOfFileToLoad.Contains('\\') || nameOfFileToLoad.Contains(' '))
                return "Could not load game: Invalid characters detected. Please remove backlashes and/or spaces from file name.";

            string gameDataToDeserialize = "";
            SaveGame? loadedGame;
            Console.WriteLine($"Name of parsed loaded file: \"{nameOfFileToLoad}\"");
            try
            {
                using (StreamReader reader = new StreamReader(nameOfFileToLoad))
                    gameDataToDeserialize = reader.ReadToEnd();
            }
            catch
            {
                return $"Could not load game: Saved file of name \"{nameOfFileToLoad}\" not found";
            }

            try
            {
                loadedGame = (SaveGame?)JsonConvert.DeserializeObject(gameDataToDeserialize);
            }
            catch
            {
                return $"Could not load game: Could not deserialize file with name \"{nameOfFileToLoad}\"";
            }

            CurrentLocation = loadedGame.CurrentLocation;
            MoveNumber = loadedGame.MoveNumber;
            status = loadedGame.Status;
            foundOpponents = loadedGame.FoundOpponents;
            foreach (LocationWithHidingPlace locationInHouse in House.Locations)
                foreach (KeyValuePair<string, string> pair in loadedGame.OpponentsInHidingLocations)
                    if (pair.Value == locationInHouse.Name)
                        locationInHouse.OpponentsHiddenHere.Add(new Opponent(pair.Key));

            if (loadedGame != null)
                return $"Loaded game from {nameOfFileToLoad}";
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
