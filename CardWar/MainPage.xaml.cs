using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using CardWar.GameLibrary;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CardWar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Deck deck;
        public Scoreboard scoreboard;
        public int m_shuffles;
        public List<Button> showcase;
        public Player m_player;
        public Player m_comp;

        public MainPage()
        {
            this.InitializeComponent();

            showcase = new List<Button>();

            for (int i = 0; i < 3; ++i) {
                Button btn = (Button)FindName("Card"+i);
                showcase.Add(btn);
            }

            NewGame();
        }

        private void NewGame() {
            p_Card.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cards/card_back.png"));
            c_Card.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cards/card_back.png"));
            scoreboard = new Scoreboard();
            deck = Deck.GetInstance();
            deck.Reset();
            deck.Shuffle();
            List<Card> userHand = new List<Card>();
            List<Card> compHand = new List<Card>();
            for (int i = 0; i < 3; ++i) {
                showcase[i].IsEnabled = true;
                userHand.Add(deck.DrawCard());
                compHand.Add(deck.DrawCard());
            }
            m_player = new Player(userHand);
            m_comp = new Player(compHand);
            RenderCards();
        }

        private void RenderCards() {
            Card[] p_hand = m_player.ShowHand();
            for (int i = 0; i < showcase.Count; ++i) {
                Image img = (Image)FindName("Img"+i);
                try
                {
                    img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Cards/{p_hand[i].ToString()}.png"));
                }
                catch (IndexOutOfRangeException e)
                {
                    showcase[i].IsEnabled = false;
                    img.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cards/card_blank.png"));
                }
            }
        }

        private void Turn(Card card) {
            Card compCard = m_comp.PlayCard();
            p_Card.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Cards/{card.ToString()}.png"));
            c_Card.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Cards/{compCard.ToString()}.png"));

            scoreboard.Update(card, compCard);

            uint turnScore = scoreboard.Score(card, compCard);

            if (card.CompareTo(compCard)) {
                p_Score.Text = $"Turn score: {turnScore}";
                c_Score.Text = "Turn score: 0";
            } else {
                p_Score.Text = "Turn score: 0";
                c_Score.Text = $"Turn score: {turnScore}";
            }

            p_Overall.Text = $"Overall score: {scoreboard.PlayerScore}";
            c_Overall.Text = $"Overall score: {scoreboard.ComputerScore}";

            if (deck.Count() > 0)
            {
                m_player.DrawCard(deck.DrawCard());
                m_comp.DrawCard(deck.DrawCard());
            }
            RenderCards();
            if (m_player.ShowHand().Length == 0) {
                EndGame();
            }
        }

        private void EndGame() {
            GameOverScreen.Visibility = Visibility.Visible;
            if (scoreboard.PlayerScore > scoreboard.ComputerScore) {
                p_Condition.Text = "You Win!";
            } else if (scoreboard.ComputerScore > scoreboard.PlayerScore) {
                p_Condition.Text = "You Lose!";
            } else {
                p_Condition.Text = "Tied!";
            }
            c_OverallScore.Text = $"Overall score: {scoreboard.ComputerScore}";
            p_OverallScore.Text = $"Overall score: {scoreboard.PlayerScore}";
        }

        private void CardClick(Object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;
            BtnShuffle.IsEnabled = false;
            switch(btn.Name) {
                case "Card0":
                    Turn(m_player.PlayCard(0));
                    break;
                case "Card1":
                    Turn(m_player.PlayCard(1));
                    break;
                case "Card2":
                    Turn(m_player.PlayCard(2));
                    break;
                default:
                    Debug.WriteLine("Default Test");
                    break;
            }
        }

        private void Shuffle(Object sender, RoutedEventArgs e) {
            if (m_shuffles < 2)
            {
                m_shuffles++;
                NewGame();
            }
            if (m_shuffles == 2)
            {
                BtnShuffle.IsEnabled = false;
            }
        }

        private void Restart(Object sender, RoutedEventArgs e) {
            deck.Reset();
            scoreboard.Reset();
            m_shuffles = 0;
            GameOverScreen.Visibility = Visibility.Collapsed;
            p_Score.Text = "Turn score: 0";
            c_Score.Text = "Turn score: 0";
            p_Overall.Text = "Overall score: 0";
            c_Overall.Text = "Overall score: 0";
            BtnShuffle.IsEnabled = true;
            NewGame();
        }
    }
}
