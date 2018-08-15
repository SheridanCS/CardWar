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

        public MainPage()
        {
            this.InitializeComponent();

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
            RenderCards();
        }

        private void RenderCards() {
            foreach(Button i in showcase) {

            }
        }

        private void CardClick(Object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;
            switch(btn.Name) {
                case "Card1":
                    Debug.WriteLine("Test");
                    break;
                case "Card2":
                    Debug.WriteLine("Test1");
                    break;
                case "Card3":
                    Debug.WriteLine("Test2");
                    break;
                default:
                    Debug.WriteLine("Test3");
                    break;
            }
        }

        private void Shuffle(Object sender, RoutedEventArgs e) {
            if (m_shuffles < 2) {
                // Shuffle deck
                ++m_shuffles;
                if (m_shuffles >= 3) {
                    Button shufBtn = (Button)sender;
                    shufBtn.IsEnabled = false;
                }
            }
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
