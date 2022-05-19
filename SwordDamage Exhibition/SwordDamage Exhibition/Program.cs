using System;

class SwordDamage
{
    public const int BASE_DAMAGE = 3;
    public const int FLAME_DAMAGE = 2;

    public int Roll;
    public decimal MagicMultiplier = 1M;
    // The book has this variable, but what is it for? It's always 0 and where
    // it would potentially be used, FLAME_DAMAGE is used instead.
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
        while (true)
        {
            SwordDamage swordDamage = new SwordDamage();
            Random random = new Random();
            Console.Write("0 for no magic/flaming, 1 for magic, 2 for flaming, 3 for both, any other key to quit: ");
            char choice = Console.ReadKey(false).KeyChar;
            if (choice != '0' && choice != '1' && choice != '2' && choice != '3') return;
            swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
            swordDamage.CalculateDamage();
            swordDamage.SetMagic(choice == '1' || choice == '3'); 
            swordDamage.SetFlaming(choice == '2' || choice == '3');
            Console.WriteLine("\nRolled " + swordDamage.Roll + " for " + swordDamage.Damage + " HP.\n");
        }
    }
}
