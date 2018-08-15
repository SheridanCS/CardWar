namespace CardWar.GameLibrary
{
    public class Scoreboard
    {
        private uint _playerScore;
        private uint _computerScore;

        public uint PlayerScore { get => _playerScore; }
        public uint ComputerScore { get => _computerScore; }

        public void Update(Card c1, Card c2)
        {
            uint score = (c1.PointValue() + c2.PointValue()) * 10;

            if (c1.CompareTo(c2))
            {
                _playerScore += score;
            }
            else
            {
                _computerScore += score;
            }
        }

        public void Reset()
        {
            _playerScore = 0;
            _computerScore = 0;
        }
    }
}
