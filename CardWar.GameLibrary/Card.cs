using System;

namespace CardWar.GameLibrary
{
    public class Card
    {
        private readonly Faces _face;
        private readonly Suits _suit;
        public Faces Face { get => _face; }
        public Suits Suit { get => _suit; }

        public enum Faces { Two=2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        public enum Suits { Diamonds, Clubs, Hearts, Spades };
        

        public Card(Faces face, Suits suit)
        {
            _face = face;
            _suit = suit;
        }

        public override string ToString()
        {
            string face = Enum.GetName(typeof(Card.Faces), Face);
            string suit = Enum.GetName(typeof(Card.Suits), Suit);

            return $"{face} of {suit}";
        }

        public uint PointValue()
        {
            uint value = 0;

            switch (Face)
            {
                case Faces.Two:
                case Faces.Three:
                case Faces.Four:
                case Faces.Five:
                case Faces.Six:
                case Faces.Seven:
                case Faces.Eight:
                case Faces.Nine:
                case Faces.Ten:
                    value = (uint)Face;
                    break;
                case Faces.Jack:
                case Faces.Queen:
                case Faces.King:
                    value = 10;
                    break;
                case Faces.Ace:
                    value = 11;
                    break;
                default:
                    break;
            }

            return value;
        }

        public bool CompareTo(Card c)
        {
            bool isHigherValue = false;

            if (this.Face > c.Face)
            {
                isHigherValue = true;
            }
            else if (this.Face == c.Face)
            {
                if (this.Suit > c.Suit)
                {
                    isHigherValue = true;
                }
            }

            return isHigherValue;
        }
    }
}
