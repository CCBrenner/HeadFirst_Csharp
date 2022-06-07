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

        public string Job { get; set; }
        public virtual float CostPerShift { get; protected set; }

        public void WorkTheNextShift()
        {
            // This doesn't quite make snes but it's what the book wants; no subtraction of honey from vault as if actually eating, just whether there is enough in there or not per 1 bee (makes this logic pretty useless bc it will always return true if CostPerShift is less than 3 for each bee which is the starting amount and the amount only ever goes up)
            if (HoneyVault.ConsumeHoney(CostPerShift)) DoJob();
        }
        protected virtual void DoJob() { /* to be overrided */ }
    }

    class QueenBee : Bee
    {
        public QueenBee() : base("Queen")
        {
            CostPerShift = 2.15F;
        }

        public float EGGS_PER_SHIFT = 0.45f;

        private Bee[] workers;
        private float eggs;
        private float unassignedWorkers = 3;

        private void AddWorker(object newWorker) {
            Array.Resize(ref workers, workers.Length + 1);
            workers[workers.Length - 1] = (Bee)newWorker;
        }
        private void AssignBee(string job)
        {
            switch (job)
            {
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturerBee());
                    break;
                case "Nectar Collector":
                    AddWorker(new NectarCollectorBee());
                    break;
                case "Egg Care":
                    AddWorker(new EggCareBee());
                    break;
            }
        }
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach(Bee bee in workers)
            {
                bee.WorkTheNextShift();  // stopped here, stopped at (in book ) ch06.html#the_queen_class_how_she_manages_the_work
            }

        }
    }

    class HoneyManufacturerBee : Bee
    {
        public HoneyManufacturerBee() : base("Honey Manufacturer")
        {
            CostPerShift = 1.7F;
        }
    }

    class NectarCollectorBee : Bee
    {
        public NectarCollectorBee() : base("Nectar Collector")
        {
            CostPerShift = 1.95F;
        }
    }

    class EggCareBee : Bee
    {
        public EggCareBee() : base("Egg Care")
        {
            CostPerShift = 1.53F;
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
