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

        void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) DoJob();
        }
        protected virtual void DoJob()
        {
            // to be overwritten by subclasses
        }
    }

    class QueenBee : Bee
    {
        public QueenBee() : base("Queen")
        {
            CostPerShift = 2.15F;
        }

        private Bee[] workers;
        private float eggs;
        private float unassignedWorkers = 3;

        private void AddWorker(new) // left 
        private void AssignBee(string job)
        {
            switch (job)
            {
                case "Honey Manufacturer":
                    Array.Resize(ref workers, workers.Length + 1);
                    workers[workers.Length - 1] = new HoneyManufacturerBee();
                    break;
                case "Nectar Collector":
                    Array.Resize(ref workers, workers.Length + 1);
                    workers[workers.Length - 1] = new NectarCollectorBee();
                    break;
                case "Egg Care":
                    Array.Resize(ref workers, workers.Length + 1);
                    workers[workers.Length - 1] = new EggCareBee();
                    break;
            }
        }
        protected override void DoJob()
        {
            base.DoJob();
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
