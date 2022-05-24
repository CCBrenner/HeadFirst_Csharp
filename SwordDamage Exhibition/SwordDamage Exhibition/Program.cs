using System;

namespace SwordDamage
{
    public class SwordDamage
    {
        public const int BASE_DAMAGE = 3;
        public const int FLAME_DAMAGE = 2;

        public int Roll { get; set; }
        // *Made private for purposes of improving encapsulation
        private decimal magicMultiplier = 1M;
        private int flamingDamage = 0;
        public int Damage;

        private void UpdateDamage()
        {
            Damage = BASE_DAMAGE + (int)(Roll * magicMultiplier) + flamingDamage;
            // Debug.WriteLine($"CalculateDamage finished: {Damage} (roll: {Roll})");
        }

        public void SetMagic(bool magical)
        {
            if (magical)
            {
                magicMultiplier = 1.75M;
            }
            else
            {
                magicMultiplier = 1M;
            }
            UpdateDamage();
            // Debug.WriteLine($"SetFlaming finished: {Damage} (roll: {Roll})");
        }

        public void SetFlaming(bool flaming)
        {
            UpdateDamage();
            if (flaming)
            {
                Damage += FLAME_DAMAGE;
            }
            // Debug.WriteLine($"SetFlaming finished: {Damage} (roll: {Roll})");
        }
    }

    class Program
    {
        static Random random = new Random();
        SwordDamage swordDamage = new SwordDamage();
        public void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("0 for no magic/flaming, 1 for magic, 2 for flaming, 3 for both, any other key to quit: ");
                char userChoice = Console.ReadKey().KeyChar;
                if (userChoice != '0' && userChoice != '1' && userChoice != '2' && userChoice != '3') return;
                if (userChoice == '1' || userChoice == '3')
                {
                    swordDamage.SetMagic(true);
                }
                if (userChoice == '2' || userChoice == '3')
                {
                    swordDamage.SetFlaming(true);
                }
                DisplayDamage();
            }
        }
        public void RollDice()
        { 
        swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
        }
        public void DisplayDamage()
        {
            Console.WriteLine($"Roll 3d6 is { swordDamage.Roll } for { swordDamage.Damage } HP.");
        }
    }
}
