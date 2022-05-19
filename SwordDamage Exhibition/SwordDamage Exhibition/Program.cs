using System;

namespace SwordDamage
{
    public class SwordDamage
    {
        public const int BASE_DAMAGE = 3;
        public const int FLAME_DAMAGE = 2;

        public int Roll;
        // *Made private for purposes of improving encapsulation
        private decimal magicMultiplier = 1M;
        private int flamingDamage = 0;
        public int Damage;

        private void CalculateDamage()
        {
            Damage = BASE_DAMAGE + (int)(Roll * magicMultiplier) + flamingDamage;
            Debug.WriteLine($"CalculateDamage finished: {Damage} (roll: {Roll})");
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
            CalculateDamage();
            Debug.WriteLine($"SetFlaming finished: {Damage} (roll: {Roll})");
        }

        public void SetFlaming(bool flaming)
        {
            CalculateDamage();
            if (flaming)
            {
                Damage += FLAME_DAMAGE;
            }
            Debug.WriteLine($"SetFlaming finished: {Damage} (roll: {Roll})");
        }
    }
}
