using System;
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

        public MainPage()
        {
            this.InitializeComponent();
            deck = Deck.GetInstance();
            deck.Shuffle();

            bool emptyDeck = false;
            do
            {
                try
                {
                    Card c1 = deck.DrawCard();
                    Card c2 = deck.DrawCard();

                    TmpMessage.Text += $"C1: {c1.HumanReadable()} (Value - {c1.PointValue()}), C2: {c2.HumanReadable()} (Value - {c2.PointValue()}). C1 Higher Value? {c1.CompareTo(c2)}\n";
                } catch (Exception)
                {
                    emptyDeck = true;
                }
            } while (!emptyDeck);
        }
    }
}
