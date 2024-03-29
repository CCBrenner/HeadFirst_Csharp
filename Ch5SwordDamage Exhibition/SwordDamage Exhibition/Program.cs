﻿using System;
using System.Diagnostics;

namespace SwordDamage
{
    public class SwordDamage
    {
        public SwordDamage()
        {
            magicMultiplier = 1M;
            FlamingDamage = 0;
        }

        public const int BASE_DAMAGE = 3;
        public const int FLAME_DAMAGE = 2;
        public const decimal MAGIC_MULTIPLIER = 1.75M;

        public int Roll { get; set; }

        private decimal magicMultiplier;
        public decimal MagicMultiplier
        {
            get { return magicMultiplier; }
            // to set magicMultiplier you must to use this kind of set accessor declaration:
            private set { magicMultiplier = value; }
        }
        private int flamingDamage;
        public int FlamingDamage
        {
            get { return flamingDamage; }
            // to set flamingDamage you must to use this kind of set accessor declaration:
            private set { flamingDamage = value; }
        }

        public int Damage { get; private set; }

        public void CalculateDamage(bool isMagical, bool isFlaming)
        {
            MagicMultiplier = isMagical ? 1.75M : 1M;
            FlamingDamage = isFlaming ? FLAME_DAMAGE : 0;
            Damage = BASE_DAMAGE + (int)(Roll * magicMultiplier) + flamingDamage;
            // Debug.WriteLine($"\nCalculateDamage complete:\nmagicMultiplier = {magicMultiplier}\nFlamingDamage = {FlamingDamage}\nDamage = {Damage}\nRoll = {Roll}");
        }
    }

    class Program
    {
        static Random random = new Random();
        static SwordDamage swordDamage = new SwordDamage();
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("0 for no magic/flaming, 1 for magic, 2 for flaming, 3 for both, any other key to quit: ");
                char userChoice = Console.ReadKey().KeyChar;
                RollDice();
                if (userChoice != '0' && userChoice != '1' && userChoice != '2' && userChoice != '3') return;
                bool isMagical = (userChoice == '1' || userChoice == '3');
                bool isFlaming = (userChoice == '2' || userChoice == '3');
                swordDamage.CalculateDamage(isMagical, isFlaming);
                DisplayDamage();
            }
        }
        public static void RollDice()
        { 
        swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
        }
        public static void DisplayDamage()
        {
            Console.WriteLine($"\nRoll 3d6 is { swordDamage.Roll } for { swordDamage.Damage } HP.\n");
        }
    }
}
