﻿using System;

class SwordDamage
{
    public const int BASE_DAMAGE = 3;
    public const int FLAME_DAMAGE = 2;

    public int Roll;
    public decimal MagicMultiplier = 1M;
    public int FlamingDamage = 0;
    public int Damage;

    public void CalculateDamage()
    {
        Damage = BASE_DAMAGE + (int)(Roll * MagicMultiplier) + FlamingDamage;
    }

    public void SetMagic(bool magical)
    {
        if (magical)
        {
            MagicMultiplier = 1.75M;
        }
        else
        {
            MagicMultiplier = 1M;
        }
        CalculateDamage();
    }

    public void SetFlaming(bool flaming)
    {
        CalculateDamage();
        if (flaming)
        {
            Damage += FLAME_DAMAGE;
        }
    }

    public static void Main()
    {
        bool rolling = true;
        while (rolling)
        {
            // Create a new instance of the SwordDamage class, and also a new instance of Random.
            SwordDamage swordDamage = new SwordDamage();
            Random random = new Random();

            // Write the prompt to the console and read the key. Call Console.ReadKey(false) so the key that the user typed is printed to the console. If the key isn’t 0, 1, 2, or 3, return to end the program.
            Console.Write("0 for no magic/flaming, 1 for magic, 2 for flaming, 3 for both, any other key to quit: ");
            char choice = Console.ReadKey(false).KeyChar;
            if (choice == '0' || choice == '1' || choice == '2' || choice == '3')
            {
                // Roll 3d6 by calling random.Next(1, 7) three times and adding the results together, and set the Roll field.
                swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
                swordDamage.CalculateDamage();

                // If the user pressed 1 or 3 call SetMagic(true): otherwise call SetMagic(false). You don’t need an if statement to do this: key == ’1’ returns true, so you can use || to check the key directly inside the argument.
                if (choice == '1' || choice == '3')
                {
                    swordDamage.SetMagic(true);
                }

                // If the user pressed 2 or 3, call SetFlaming(true); otherwise call SetFlaming(false). Again, you can do this in a single statement using == and ||.
                if (choice == '2' || choice == '3')
                {
                    swordDamage.SetFlaming(true);
                }

                // Write the results to the console. Look carefully at the output and use \n to insert line breaks where needed
                Console.WriteLine("\nRolled " + swordDamage.Roll + " for " + swordDamage.Damage + " HP.\n");
            }
            else
            {
                return;
            }
        }
    }
}
