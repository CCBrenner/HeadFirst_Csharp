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

namespace PickACardUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create array of picked cards locally using method
            string[] pickedCards = CardPicker.PickSomeCards((int)numberOfCards.Value);

            // prepare ListBox (listOfCards) by clearing it if there were any previous items in it
            listOfCards.Items.Clear();

            // add the items to the ListBox (listOfCards)
            foreach (string card in pickedCards)
            {
                listOfCards.Items.Add(card);
            }
        }
    }
}
