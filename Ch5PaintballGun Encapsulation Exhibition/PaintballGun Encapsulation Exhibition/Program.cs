using System;

namespace PaintballGun
{
    class PaintballGun
    {
        public PaintballGun(int balls, int magazineSize, bool loaded)
        {
            // 'this' is used because the class already has a field called 'balls', providing
            // the necessity for them to be differentiated
            this.balls = balls;
            MagazineSize = magazineSize;
            if (!loaded) Reload();
        }

        private int balls = 0;

        // Auto-implemented property (same as Balls property below minus Reload(), but
        // also 'private' making it a read-only property; if this were not marked 'private' for set then
        // this class would not be considered "well-encapsulated" since outside code could change
        // the instance's internal value with a reassignment)
        public int MagazineSize { get; private set; }

        // Using a Property here; can be used to get and set the private field; PascalCase
        // for properties instead of camelCase which is used for fields
        public int Balls
        {
            get { return balls; }
            set
            {
                if (value > 0) balls = value;
                Reload();
            }
        }

        // Auto-implemented property; 'private' makes this into a read-only property, maintaining encapsulation
        public int BallsLoaded { get; private set; }

        // Returns bool based on a conditional statement
        public bool IsEmpty() { return BallsLoaded == 0; }

        public void Reload()
        {
            if (balls > MagazineSize) BallsLoaded = MagazineSize;
            else BallsLoaded = balls;
        }
        public bool Shoot()
        {
            if (BallsLoaded == 0) return false;
            BallsLoaded--;
            balls--;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int numberOfBalls = ReadInt(20, "Number of Balls");
            int magazineSize = ReadInt(16, "Magazine size");

            Console.Write($"Loaded [false]: ");
            bool.TryParse(Console.ReadLine(), out bool isLoaded);

            PaintballGun gun = new PaintballGun(numberOfBalls, magazineSize, isLoaded);

            while (true)
            {
                // gun.Balls property is used here as a 'get' for the balls field
                Console.WriteLine($"{gun.Balls} balls, {gun.BallsLoaded} loaded.");
                if (gun.IsEmpty()) Console.WriteLine("Space to shoot, r to reload, + to add ammo, q to quit.");
                char key = Console.ReadKey(true).KeyChar;
                if (key == ' ') gun.Shoot();
                else if (key == 'r') gun.Reload();
                // This assignment uses the 'set' method of the property 'Balls' to
                // update the private field via the (public) Property
                else if (key == '+') gun.Balls += gun.MagazineSize;
                else if (key == 'q') return;
            }
        }

        /// <summary>
        /// Writes a prompt and reads an int value from the console.
        /// </summary>
        /// <param name="lastUsedValue">The default value.</param>
        /// <param name="prompt">Prompt to print to the console.</param>
        /// <returns>The int value read, or the default value if unable to parse.</returns>
        static int ReadInt(int lastUsedValue, string prompt)
        {
            // Write the prompt followed by [default value]:
            Console.WriteLine(prompt + " [" + lastUsedValue + "] ");

            // Read the line from the input and use int.TryParse to attempt to parse it
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int value))
            {
                // If it can be parsed, write " using value " + value to the console
                Console.WriteLine(" using value " + value);
                return value;
            }
            else
            {
                // Otherwise write " using default value" + lastUsedValue to the console
                Console.WriteLine(" using default value.");
                return lastUsedValue;
            }
        }
    }
}
