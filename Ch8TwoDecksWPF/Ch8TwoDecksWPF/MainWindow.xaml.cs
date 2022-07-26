using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Ch8TwoDecksWPF
{
    // ObservableCollection<T>, from System.Collections.ObjectModel, is a collection class that automatically notifies your WPF app any time its items have changed
    class Deck : ObservableCollection<Card>  //, INotifyPropertyChanged
    {
        public Deck()
        {
            int index = 0;
            // for CardNumbers
            for (int number = 0; number < 13; number++)
            {
                // for CardSuites
                for (int suit = 0; suit < 4; suit++)
                {
                    Cards[index++] = new Card((CardSuit)suit, (CardNumber)number);
                }
            }
        }
        public Card[] Cards = new Card[52];

        public string PrintDeckOrder()
        {
            string deckOrder = "";
            for (int i = 0; i < Cards.Length; i++)
            {
                deckOrder += ($"\n#{i + 1}: {Cards[i]}");
            }
            return deckOrder;
        }
        public void ShuffleCards(int shuffles)
        {
            for (int j = shuffles; j > 0; j--)
            {
                Random random = new Random();
                List<Card> cards = Cards.ToList();
                List<Card> tempCards = new List<Card>();
                for (int i = cards.Count; i > 0; i--)
                {
                    int movingCard = random.Next(i);
                    tempCards.Add(cards[movingCard]);
                    cards.RemoveAt(movingCard);
                }
                Cards = tempCards.ToArray();
            }
        }
        public void SortCards()
        {
            List<Card> tempCards = Cards.ToList();
            tempCards.Sort();
            Cards = tempCards.ToArray();
        }
    }
    enum CardSuit
    {
        Spades = 0,
        Clubs = 1,
        Hearts = 2,
        Diamonds = 3,
    }
    enum CardNumber
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
    class Card : IComparable<Card>
    {
        public Card(CardSuit suit, CardNumber number)
        {
            this.Suit = suit;
            this.Number = number;
        }
        public CardSuit Suit { get; private set; }
        public CardNumber Number { get; private set; }
        public override string ToString()
        {
            return $"{Number} of {Suit}";
        }

        public int CompareTo(Card comparissonCard)
        {
            if (this.Suit > comparissonCard.Suit)
                return 1;
            else if (this.Suit < comparissonCard.Suit)
                return -1;
            else
            {
                if (this.Number > comparissonCard.Number)
                    return 1;
                else if (this.Number < comparissonCard.Number)
                    return -1;
                else
                    return 0;
            }
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

            if (Resources["rightDeck"] is Deck rightDeck)
            {
                rightDeck.Clear();
            }
        }

        private void MoveCard(bool leftToRight)
        {
            if ((Resources["rightDeck"] is Deck rightDeck)
                && (Resources["leftDeck"] is Deck leftDeck))
            {
                if (leftToRight)
                {
                    if (leftDeckListBox.SelectedItem is Card card)
                    {
                        leftDeck.Remove(card);
                        rightDeck.Add(card);
                    }
                }
                else
                {
                    if (rightDeckListBox.SelectedItem is Card card)
                    {
                        rightDeck.Remove(card);
                        leftDeck.Add(card);
                    }
                }
            }
        }
    }
}


