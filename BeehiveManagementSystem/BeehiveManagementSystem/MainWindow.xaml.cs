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
    class HoneyVault
    {
        public HoneyVault()
        {

        }

        public const float NECTAR_CONVERTION_RATIO = 0.19F;
        public const float LOW_LEVEL_WARNING = 10F;


        private float honey = 25F;
        private float nectar = 100F;
        private string statusReport;
        public string StatusReport
        {
            get
            {
                statusReport = $"Amount of Honey in Vault: {honey}\nAmount of Nectar in Vault: {nectar}";
                if (honey < LOW_LEVEL_WARNING) statusReport += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) statusReport += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                return statusReport;
            }
        }

        public void CollectNectar(float nectarToStore) { nectar += nectarToStore > 0F ? nectarToStore : 0F; }
        public void ConvertNectarToHoney(float amountToConvert)
        {
            amountToConvert = amountToConvert > nectar ? nectar : amountToConvert;
            nectar -= amountToConvert;
            honey += amountToConvert * NECTAR_CONVERTION_RATIO;
        }
        public bool ConsumeHoney(float consumptionAmount) { return consumptionAmount > honey; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
