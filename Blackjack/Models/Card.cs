namespace Blackjack.Models
{
    public class Card
    {
        public Suit CardSuit { get; }
        public Rank CardRank { get; }

        public Card(Suit suit, Rank rank)
        {
            CardSuit = suit;
            CardRank = rank;
        }

        public int GetValue()
        {
            return CardRank switch
            {
                Rank.Ace => 11,
                Rank.King => 10,
                Rank.Queen => 10,
                Rank.Jack => 10,
                _ => (int)CardRank
            };
        }

        public override string ToString()
        {
            return $"{CardRank} of {CardSuit}";
        }
    }
}