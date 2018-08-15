using System.Collections.Generic;

namespace CardWar.GameLibrary
{
    class Player
    {
        private List<Card> _hand;
        private int _emptySlot;

        public Player()
        {
            _hand = new List<Card>();
        }

        public void DrawCard(Card c)
        {
            _hand.Insert(_emptySlot, c);
        }

        public Card PlayCard(int index)
        {
            Card c = _hand[index];

            _hand.RemoveAt(index);
            _emptySlot = index;

            return c;
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
