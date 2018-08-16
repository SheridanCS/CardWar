using System;
using System.Collections.Generic;

namespace CardWar.GameLibrary
{
    public class Player
    {
        private List<Card> _hand;
        private Random rand;

        public Player(List<Card> hand)
        {
            _hand = hand;
            rand = new Random();
        }

        public void DrawCard(Card c)
        {
            _hand.Add(c);
        }

        public Card PlayCard(int index)
        {
            Card c = _hand[index];

            _hand.RemoveAt(index);

            return c;
        }

        public Card PlayCard() {
            return PlayCard(rand.Next(_hand.Count));
        }

        public Card[] ShowHand()
        {
            return _hand.ToArray();
        }

        public void Reset()
        {
            _hand.Clear();
        }
    }
}
