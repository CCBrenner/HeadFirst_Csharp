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
                statusReport = $"Vaultreport:\n{honey} units of honey\n{nectar} units of nectar";
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
        public static bool ConsumeHoney(float consumptionAmount) 
        { 
            if (consumptionAmount < honey)
            {
                honey -= consumptionAmount;
                return true;
            }
            return false; 
        }
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
        protected virtual void DoJob() { /* to be overridden */ }
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

        private void AddWorker(Bee newWorker) {
            Array.Resize(ref workers, workers.Length + 1);
            workers[workers.Length - 1] = newWorker;
        }
        public void AssignBee(string job)
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
                    AddWorker(new EggCareBee(this));
                    break;
            }
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }
        private void UpdateStatusReport()
        {
            string status = HoneyVault.StatusReport;

            int honeyManufacturerCount = 0;
            int nectarCollectorCount = 0;
            int eggCareCount = 0;
            foreach (Bee bee in workers)
            {
                if (bee.GetType().ToString() == "HoneyManufacturerBee") honeyManufacturerCount += 1;
                else if (bee.GetType().ToString() == "NectarCollectorBee") nectarCollectorCount += 1;
                else if (bee.GetType().ToString() == "EggCareBee") eggCareCount += 1;
            }
            string pluralH = honeyManufacturerCount == 1 ? "" : "s";
            string pluralN = nectarCollectorCount == 1 ? "" : "s";
            string pluralE = eggCareCount == 1 ? "" : "s";

            status += $"Egg count: {eggs}\nUnassigned workers: {unassignedWorkers}\n{nectarCollectorCount} Nector Collector bee{pluralN}\n{honeyManufacturerCount} Honey Manufacturer bee{pluralH}\n{eggCareCount} Egg Care bee{pluralE}\nTOTAL WORKERS: {workers.Length}";
        }
        protected override void DoJob()
        {
            // Lay egg portion
            eggs += EGGS_PER_SHIFT;

            // Have bees do their jobs
            foreach(Bee bee in workers)
            {
                bee.WorkTheNextShift();
            }

            // Give status report
            UpdateStatusReport();
        }
    }

    class NectarCollectorBee : Bee
    {
        public NectarCollectorBee() : base("Nectar Collector")
        {
            CostPerShift = 1.95F;
        }

        public const float NECTAR_COLLECTED_PER_SHIFT = 33.25F;

        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }
    }

    class HoneyManufacturerBee : Bee
    {
        public HoneyManufacturerBee() : base("Honey Manufacturer")
        {
            CostPerShift = 1.7F;
        }

        public const float NECTAR_PROCESSED_PER_SHIFT = 33.15F;

        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }
    }

    class EggCareBee : Bee
    {
        public EggCareBee(QueenBee queen) : base("Egg Care")
        {
            CostPerShift = 1.53F;
            this.queen = queen;
        }

        public const float CARE_PROGRESS_PER_SHIFT = 0.15F;

        private QueenBee queen;

        protected override void DoJob()
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT);
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
            theQueen = new QueenBee();
        }

        private QueenBee theQueen;

        private void assignBee_Click(object sender, RoutedEventArgs e)
        {
            theQueen.AssignBee(jobSelector.Text);
        }

        private void workNextShift_Click(object sender, RoutedEventArgs e)
        {
            theQueen.WorkTheNextShift();
        }
    }
}
