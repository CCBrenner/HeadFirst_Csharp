using System;
using System.Diagnostics;

namespace SwordAndArrows
{
    class WeaponDamage
    {
        public WeaponDamage(int roll, bool magic, bool flaming, int damage)
        {
            Roll = roll;
            Magic = magic;
            Flaming = flaming;
            Damage = damage;
        }

        public decimal BaseDamage { get; protected set; }
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
        public decimal MagicMultiplier { get; protected set; }

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
        public decimal FlameDamage { get; protected set; }
        public int Damage { get; protected set; }

        protected virtual void CalculateDamage()
        {
            /* The subclass overrites this. */
        }
    }
    class SwordDamage : WeaponDamage
    {

        /// <summary>
        /// The constructor calculates damage based on default Magic
        /// and Flaming values and a starting 3d6 roll.
        /// </summary>
        /// <param name="startingRoll">Starting 3d6 roll</param>
        public SwordDamage(int startingRoll) : base(startingRoll, false, false, 0)
        {
            Roll = startingRoll;
            BaseDamage = BASE_DAMAGE;
            CalculateDamage();
        }
        private const int BASE_DAMAGE = 3;
        private const decimal MAGIC_MULTIPLIER = 1.75M;
        private const int FLAME_DAMAGE = 2;

        /// <summary>
        /// Calculates the damage based on the current properties.
        /// </summary>
        protected override void CalculateDamage()
        {
            MagicMultiplier = Magic ? MAGIC_MULTIPLIER : 1M;
            FlameDamage = Flaming ? FLAME_DAMAGE : 0;
            Damage = (int)(BaseDamage + (Roll * MagicMultiplier) + FlameDamage);
        }
    }

    class ArrowDamage : WeaponDamage
    {

        /// <summary>
        /// The constructor calculates damage based on default Magic
        /// and Flaming values and a starting 3d6 roll.
        /// </summary>
        /// <param name="startingRoll">Starting 3d6 roll</param>
        public ArrowDamage(int startingRoll) : base(startingRoll, false, false, 0)
        {
            roll = startingRoll;
            CalculateDamage();
        }

        private const decimal ROLL_MULTIPLIER = 0.35M;
        private const decimal MAGIC_MULTIPLIER = 2.5M; 
        private const decimal FLAME_DAMAGE = 1.25M;

        /// <summary>
        /// Calculates the damage based on the current properties.
        /// </summary>
        private void CalculateDamage()
        {
            BaseDamage = Roll * ROLL_MULTIPLIER;
            MagicMultiplier = Magic ? MAGIC_MULTIPLIER : 1M;
            FlameDamage = Flaming ? FLAME_DAMAGE : 0M;
            Damage = (BaseDamage + FlameDamage)  // Stopped here, need to go back and find out what this formula was supposed to be because there are no more remnants of its state


            baseDamage = Magic ? baseDamage * MAGIC_MULTIPLIER : baseDamage;
            Damage = Flaming ? (int)Math.Ceiling(baseDamage + FLAME_DAMAGE) : (int)baseDamage;
        }

        /// <summary>
        /// Contains the calculated damage.
        /// </summary>
        public int Damage { get; private set; }

        private int roll;

        /// <summary>
        /// Sets or gets the 3d6 roll.
        /// </summary>
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                CalculateDamage();
            }
        }

        private bool magic;

        /// <summary>
        /// True if the sword is magic, false otherwise.
        /// </summary>
        public bool Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                CalculateDamage();
            }
        }

        private bool flaming;

        /// <summary>
        /// True if the sword is flaming, false otherwise.
        /// </summary>
        public bool Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }
    }

    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            SwordDamage swordDamage = new SwordDamage(RollDice(3));
            ArrowDamage arrowDamage = new ArrowDamage(RollDice(1));
            
            while (true)
            {
                Console.Write("0 for no magic/flaming, 1 for magic, 2 for flaming, " +
                                "3 for both, anything else to quit: ");
                char key = Console.ReadKey().KeyChar;
                if (key != '0' && key != '1' && key != '2' && key != '3') return;

                Console.Write("\nS for Sword, A for Arrow, anything else to quit: ");
                char weaponKey = char.ToUpper(Console.ReadKey().KeyChar);
                switch (weaponKey)
                {
                    case 'S':
                        swordDamage.Roll = RollDice(3);
                        swordDamage.Magic = (key == '1' || key == '3');
                        swordDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine($"\nRolled {swordDamage.Roll} for {swordDamage.Damage} HP\n");
                        break;
                    case 'A':
                        arrowDamage.Roll = RollDice(1);
                        arrowDamage.Magic = (key == '1' || key == '3');
                        arrowDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine($"\nRolled {arrowDamage.Roll} for {arrowDamage.Damage} HP\n");
                        break;
                    default:
                        return;
                }
                
            }
        }

        private static int RollDice(int numberOfRolls)
        {
            int roll = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                roll += random.Next(1, 7);
            }
            return roll;
        }
    }
}