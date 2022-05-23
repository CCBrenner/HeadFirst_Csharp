using System;

namespace SwordAndArrows
{
    public class SwordDamage
    {
        public SwordDamage(int startingRoll)
        {
            Roll = startingRoll;
            Magic = 1;
            CalculateDamage();
        }

        public const int BASE_DAMAGE = 3;
        public const int FLAME_DAMAGE = 2;

        private int roll;
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                CalculateDamage();
            }
        }
        private int flaming;
        public int Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }
        private decimal magic;
        public decimal Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                CalculateDamage();
            }
        }
        public int Damage { get; private set; }

        private void CalculateDamage()
        {
            Damage = BASE_DAMAGE + (int)(Roll * Magic) + Flaming;
            // Debug.WriteLine($"CalculateDamage finished: {Damage} (roll: {Roll})");
        }
    }

    public class ArrowDamage
    {
        public ArrowDamage(int startingRoll)
        {
            Roll = startingRoll;
            Magic = 1;
            CalculateDamage();
        }

        public const decimal BASE_MULTIPLIER = 0.35M;
        public const decimal MAGIC_MULTIPLIER = 2.5M;
        public const decimal FLAME_DAMAGE = 1.25M;

        private int roll;
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                CalculateDamage();
            }
        }
        private bool flaming;
        public bool Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }
        private bool magic;
        public bool Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                CalculateDamage();
            }
        }
        public int Damage { get; private set; }

        private void CalculateDamage()
        {
            decimal baseDamage = Roll + BASE_MULTIPLIER;
            if (Magic) baseDamage *= MAGIC_MULTIPLIER;
            if (Flaming) Damage = (int)Math.Ceiling(baseDamage + FLAME_DAMAGE);
            else Damage = (int)Math.Ceiling(baseDamage);
        }
    }
}