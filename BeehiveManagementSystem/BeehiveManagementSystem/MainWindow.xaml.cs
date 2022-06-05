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

namespace BeehiveManagementSystem
{
    static class HoneyVault
    {
        static HoneyVault()
        {

        }

        public const float NECTAR_CONVERTION_RATIO = 0.19F;
        public const float LOW_LEVEL_WARNING = 10F;


        private static float honey = 25F;
        private static float nectar = 100F;
        private static string statusReport;
        public static string StatusReport
        {
            get
            {
                statusReport = $"Amount of Honey in Vault: {honey}\nAmount of Nectar in Vault: {nectar}";
                if (honey < LOW_LEVEL_WARNING) statusReport += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) statusReport += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                return statusReport;
            }
        }

        public static void CollectNectar(float nectarToStore) { nectar += nectarToStore > 0F ? nectarToStore : 0F; }
        public static void ConvertNectarToHoney(float amountToConvert)
        {
            amountToConvert = amountToConvert > nectar ? nectar : amountToConvert;
            nectar -= amountToConvert;
            honey += amountToConvert * NECTAR_CONVERTION_RATIO;
        }
        public static bool ConsumeHoney(float consumptionAmount) { return consumptionAmount > honey; }
    }

    class Bee
    {
        public Bee(string title)
        {
            Job = title;
        }

        public string Job { get; private set; }
        public virtual float CostPerShift { get; set; }

        void WorkTheNextShift()
        {

        }
        protected virtual void DoJob()
        {

        }
    }

    class QueenBee : Bee
    {
        public QueenBee() : base("Queen")
        {

        }
    }

    class HoneyManufacturerBee : Bee
    {
        public HoneyManufacturerBee() : base("Honey Manufacturer")
        {

        }
    }

    class NectarCollectorBee : Bee
    {
        public NectarCollectorBee() : base("Nectar Collector")
        {

        }
    }

    class EggCareBee : Bee
    {
        public EggCareBee() : base("Egg Care")
        {

        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QueenBee theQueen = new QueenBee();
        }

        private void assignBee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void workNextShift_Click(object sender, RoutedEventArgs e)
        {
            // theQueen.WorkNextShift();
        }
    }
}
