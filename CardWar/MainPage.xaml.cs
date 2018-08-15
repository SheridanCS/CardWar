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
            scoreboard = new Scoreboard();
            deck = Deck.GetInstance();
            deck.Shuffle();
            List<Card> userHand = new List<Card>();
            List<Card> compHand = new List<Card>();
            for (int i = 0; i < 3; ++i) {
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
                if (p_hand[i] == null) {
                    img.Source = new BitmapImage(new Uri("ms-appx:///CardWar/Assets/Cards/card_blank.png"));
                } else {
                    img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Cards/{p_hand[i].ToString()}.png"));
                }
            }
        }

        private void Turn(Card card) {
            Image p_card = (Image)FindName("p_Card");
            Image c_card = (Image)FindName("c_Card");
            p_card.Source = new BitmapImage(new Uri($"ms-appx:///CardWar/Assets/Cards/{card.ToString()}.png"));
            Card compCard = m_comp.PlayCard();
            c_card.Source = new BitmapImage(new Uri($"ms-appx:///CardWar/Assets/Cards/{compCard.ToString()}.png"));

            scoreboard.Update(card, compCard);

            uint turnScore = scoreboard.Score(card, compCard);

            TextBlock p_Score = (TextBlock)FindName("p_Score");
            TextBlock c_Score = (TextBlock)FindName("c_Score");

            if (card.CompareTo(compCard)) {
                p_Score.Text = $"Turn score: {turnScore}";
                c_Score.Text = "Turn score: 0";
            } else {
                p_Score.Text = "Turn score: 0";
                c_Score.Text = $"Turn score: {turnScore}";
            }

            TextBlock p_Overall = (TextBlock)FindName("p_Overall");
            TextBlock c_Overall = (TextBlock)FindName("c_Overall");
            p_Overall.Text = $"Overall score: {scoreboard.PlayerScore}";
            c_Overall.Text = $"Overall score: {scoreboard.ComputerScore}";
        }

        private void CardClick(Object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;
            switch(btn.Name) {
                case "Card1":
                    Turn(m_player.PlayCard(0));
                    break;
                case "Card2":
                    Turn(m_player.PlayCard(1));
                    break;
                case "Card3":
                    Turn(m_player.PlayCard(2));
                    break;
                default:
                    Debug.WriteLine("Default Test");
                    break;
            }
        }

        private void Shuffle(Object sender, RoutedEventArgs e) {
            NewGame();
        }

        private void Restart(Object sender, RoutedEventArgs e) {
            deck.Reset();
            scoreboard.Reset();
            m_shuffles = 0;
            Grid r_grid = (Grid)((Button)sender).Parent;
            r_grid.Visibility = Visibility.Collapsed;
            NewGame();
        }
    }
}
