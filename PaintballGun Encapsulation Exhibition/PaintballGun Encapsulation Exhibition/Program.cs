using System;

namespace PaintballGun
{
    class PaintballGun
    {
        public const int MAGAZINE_SIZE = 16;

        private int balls = 0;
        private int ballsLoaded = 0;

        // Using a Property here; can be used to get and set the private field; PascalCase
        // for properties instead of camelCase which is used for fields
        public int Balls
        {
            get { return balls; }
            set
            {
                if (value > 0)
                {
                    balls = value;
                }
                Reload();
            }
        }

        // Uses private *backing field* 'ballsLoaded'
        public int BallsLoaded
        {
            get { return ballsLoaded; }
            set { ballsLoaded = value; }
        }

        public bool IsEmpty() { return ballsLoaded == 0; }

        
        public void Reload()
        {
            if (balls > MAGAZINE_SIZE) ballsLoaded = MAGAZINE_SIZE;
            else ballsLoaded = balls;
        }
        public bool Shoot()
        {
            if (ballsLoaded == 0) return false;
            ballsLoaded--;
            balls--;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaintballGun gun = new PaintballGun();

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
                else if (key == '+') gun.Balls += PaintballGun.MAGAZINE_SIZE;
                else if (key == 'q') return;
            }
        }
    }
}
