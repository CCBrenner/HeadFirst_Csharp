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
using System.Windows.Threading;
using System.ComponentModel;

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
                statusReport = $"\nVault report:\n{honey} units of honey\n{nectar} units of nectar";
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

    interface IWorker
    {
        string Job { get; set; }
        void WorkTheNextShift();
    }

    abstract class Bee : IWorker
    {
        public Bee(string title)
        {
            Job = title;
        }

        public string Job { get; set; }
        public abstract float CostPerShift { get; protected set; }

        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) 
                DoJob();
        }
        protected abstract void DoJob();
    }

    class QueenBee : Bee, INotifyPropertyChanged
    {
        public QueenBee() : base("Queen")
        {
            AssignBee("Honey Manufacturer");
            AssignBee("Nectar Collector");
            AssignBee("Egg Care");
            CostPerShift = 2.15F;
            UpdateStatusReport();
        }

        public float EGGS_PER_SHIFT = 0.45F;
        public float HONEY_PER_UNASSIGNED_WORKER = 0.5F;

        private IWorker[] workers = {};  // another option: private IWorker[] workers = new Worker[0];
        private float eggs;
        private float unassignedWorkers = 3;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StatusReport {  get; private set; }
        public override float CostPerShift { get; protected set; }

        private void AddWorker(IWorker newWorker) {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = newWorker;
            }
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
            UpdateStatusReport();
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (IWorker worker in workers) if (worker.Job == job) count += 1;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {job} bee{s}";
        }
        private void UpdateStatusReport()
        { 
            StatusReport = $"{HoneyVault.StatusReport}" +
                $"\n\nQueen's report:\nEgg count: {eggs}\nUnassigned workers: {unassignedWorkers}" +
                $"\n{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
                $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
            OnPropertyChanged("StatusReport");  // This is saying "invoke OnPropertyChanged()" and "the reference
                                                // is the tag that has "StatusReport" as its Binding"
        }
        private void OnPropertyChanged(string name)
        {
            // This says, "if any properties have changed, then update the object in the WPF Window.Resources
            // to reflect the current properties listed here (event handling)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        protected override void DoJob()
        {
            // Lay egg portion
            eggs += EGGS_PER_SHIFT;

            // Have bees do their jobs
            foreach(IWorker worker in workers)
            {
                worker.WorkTheNextShift();
            }

            // Feed unassigned workers
            HoneyVault.ConsumeHoney((float)Math.Floor(unassignedWorkers) * HONEY_PER_UNASSIGNED_WORKER);

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

        public override float CostPerShift { get; protected set; }

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

        public override float CostPerShift { get; protected set; }

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

        public override float CostPerShift { get; protected set; }

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
            // statusReport.Text = theQueen.StatusReport;
            theQueen = Resources["queen"] as QueenBee;
            /*
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Start();
            */
        }
        /*
        private void Timer_Tick(object sender, EventArgs e)
        {
            workNextShift_Click(this, new RoutedEventArgs());
        }
        */
        private readonly QueenBee theQueen;
        // private DispatcherTimer timer = new DispatcherTimer();

        private void assignBee_Click(object sender, RoutedEventArgs e)
        {
            theQueen.AssignBee(jobSelector.Text);
            // statusReport.Text = theQueen.StatusReport;
        }

        private void workNextShift_Click(object sender, RoutedEventArgs e)
        {
            theQueen.WorkTheNextShift();
            // statusReport.Text = theQueen.StatusReport;
        }
    }
}
