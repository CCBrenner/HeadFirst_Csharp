using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace swordDamageUI
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random random = new Random();
        SwordDamage swordDamage;
        public MainWindow()
        {
            InitializeComponent();
            swordDamage = new SwordDamage(random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7));
            DisplayDamage();
        }

        public void RollDice()
        {
            swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
        }
        void DisplayDamage()
        {
            Damage.Text = $"Rolled { swordDamage.Roll } for { swordDamage.Damage } HP.";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
            DisplayDamage();
        }
        private void flaming_Checked(object sender, RoutedEventArgs e)
        {
            swordDamage.Flaming = 2;
            DisplayDamage();
        }
        private void flaming_Unchecked(object sender, RoutedEventArgs e)
        {
            swordDamage.Flaming = 0;
            DisplayDamage();
        }
        private void magical_Checked(object sender, RoutedEventArgs e)
        {
            swordDamage.Magic = 1.75M;
            DisplayDamage();
        }
        private void magical_Unchecked(object sender, RoutedEventArgs e)
        {
            swordDamage.Magic = 1;
            DisplayDamage();
        }
    }
}
