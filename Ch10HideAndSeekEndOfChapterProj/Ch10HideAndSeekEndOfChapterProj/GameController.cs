using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            Dictionary<string, string> opponentsInHidingLocations = new Dictionary<string, string>();
            foreach (Location location in House.Locations)
                if ((location as LocationWithHidingPlace).HidingPlace != "")
                    foreach (Opponent opponent in (location as LocationWithHidingPlace).OpponentsHiddenHere)
                        opponentsInHidingLocations.Add(opponent.Name, location.Name);
            string currentLocation = CurrentLocation.Name;
            int moveNumber = MoveNumber;
            string status = Status;
            List<string> foundOpponents = new List<string>();
            foreach (Opponent opponent in this.foundOpponents) foundOpponents.Add(opponent.Name);

            SaveGame saveGame = new SaveGame()
            {
                OpponentsInHidingLocations = opponentsInHidingLocations,
                FoundOpponents = foundOpponents,
                CurrentLocationName = currentLocation,
                MoveNumber = moveNumber,
                Status = status,
            };

            // string savedGame = JsonSerializer.Serialize(saveGame);  // Wasn't serializing the Opponent objects for some reason
            string savedGame = JsonConvert.SerializeObject(saveGame);  // Had to use this method from the Newtonsoft.Json NuGet package

            if (!nameForSavedFile.Contains('\\') && !nameForSavedFile.Contains(' '))
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                using (StreamWriter writer = new StreamWriter($"{folder}{Path.DirectorySeparatorChar}{nameForSavedFile}.json", false))
                    writer.WriteLine(savedGame);
                return $"Saved current game to {nameForSavedFile}.json";
            }
            return $"Could not save game to {nameForSavedFile}.json";
        }
        private string Load(string nameOfFileToLoad)
        {
            nameOfFileToLoad = nameOfFileToLoad.Split('.')[0];  // if loaded with extension attached to filename, take only the filename
            if (nameOfFileToLoad.Contains('\\') || nameOfFileToLoad.Contains(' '))
                return "Could not load game: Invalid characters detected. Please remove backslashes and/or spaces from file name.";

            string gameDataToDeserialize = "";
            try
            {
                // an alternative to the line below it:
                // string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}{Path.DirectorySeparatorChar}{nameOfFileToLoad}.json"))
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{nameOfFileToLoad}.json");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    gameDataToDeserialize = reader.ReadToEnd();
                }
                File.Delete(filePath);
            }
            catch
            {
                return $"Could not load game: Saved file of name \"{nameOfFileToLoad}.json\" not found";
            }

            SaveGame? loadedGame = new SaveGame();
            try
            {
                loadedGame = System.Text.Json.JsonSerializer.Deserialize<SaveGame>(gameDataToDeserialize);
            }
            catch
            {
                return $"Could not load game: Could not deserialize file with name \"{nameOfFileToLoad}.json\"";
            }

            foreach (LocationWithHidingPlace location in House.Locations) location.OpponentsHiddenHere = new List<Opponent>();

            CurrentLocation = House.GetLocationByName(loadedGame.CurrentLocationName);
            MoveNumber = loadedGame.MoveNumber;
            status = loadedGame.Status;
            foundOpponents.Clear();
            foreach (string opponent in loadedGame.FoundOpponents) foundOpponents.Add(new Opponent(opponent));


            Console.WriteLine($"Number of loaded hidden opponnets: {loadedGame.OpponentsInHidingLocations.Count()}");
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
