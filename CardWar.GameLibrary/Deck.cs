using System;
using System.Collections.Generic;

namespace CardWar.GameLibrary
{
    public class Deck
    {
        private static Random _rng = new Random();
        private static Stack<Card> _cards = new Stack<Card>();
        private static Deck _deck;

        private Deck()
        {
            Reset();
        }

        public static Deck GetInstance()
        {
            if (_deck == null)
            {
                _deck = new Deck();
            }

            return _deck;
        }

        public void Reset()
        {
            _cards.Clear();
            foreach (Card.Faces face in Enum.GetValues(typeof(Card.Faces)))
            {
                foreach (Card.Suits suit in Enum.GetValues(typeof(Card.Suits)))
                {
                    _cards.Push(new Card(face, suit));
                }
            }
        }

        public void Shuffle()
        {
            int n = _cards.Count;
            Card[] tmpDeck = _cards.ToArray();
            _cards.Clear();

            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                Card value = tmpDeck[k];
                tmpDeck[k] = tmpDeck[n];
                tmpDeck[n] = value;
            }

            _cards = new Stack<Card>(tmpDeck);
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0)
            {
                throw new Exception("deck is empty");
            }

            return _cards.Pop();
        }


    }
}
