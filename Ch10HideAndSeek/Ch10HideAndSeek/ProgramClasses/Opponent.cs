using System;
using System.Diagnostics;

namespace Ch10HideAndSeek
{
    public class Opponent
    {
        public Opponent(string name) => Name = name;
        public readonly string Name;
        public override string ToString() => Name;
        public void Hide()
        {
            Location currentLocation = House.Entry;
            int locationsToMoveThrough = House.Random.Next(10, 20);

            for (int i = 0; i < locationsToMoveThrough; i++)
                currentLocation = House.RandomExit(currentLocation);
            while ((currentLocation as LocationWithHidingPlace).HidingPlace == "")
                currentLocation = House.RandomExit(currentLocation);

            (currentLocation as LocationWithHidingPlace).Hide(this);

            Debug.WriteLine($"{Name} is hiding " +
                $"{(currentLocation as LocationWithHidingPlace).HidingPlace} " +
                $"in the {currentLocation.Name}");
        }
    }
}
