using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10HideAndSeekEndOfChapterProj
{
    public class GameController
    {
        public GameController() => CurrentLocation = House.Entry;
        public Location CurrentLocation { get; private set; }
        public string Status => throw new NotImplementedException();
        public string Prompt => "Which direction do you want to go: ";
        public bool Move(Direction exitDirection)
        {
            bool canMove = CurrentLocation.Exits.ContainsKey(exitDirection);
            if (canMove) CurrentLocation = CurrentLocation.Exits[exitDirection];
            return canMove;
        }
        public string ParseInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}
