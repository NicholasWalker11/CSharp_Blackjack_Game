namespace Blackjack.Models
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        cards.Add(new Card(suit, rank));
                    }
                }
        }

        public void Shuffle()
        {
            Random random = new Random();
                int n = cards.Count;

                for (int i = n - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    Card temp = cards[i];
                    cards[i] = cards[j];
                    cards[j] = temp;
                }
        }

        public Card DealCard()
        {
            if (cards.Count == 0)
                {
                    throw new InvalidOperationException("No cards left in the deck!");
                }

                Card card = cards[0];
                cards.RemoveAt(0);
                return card;
        }

        public int GetRemainingCards()
        {
            return cards.Count;
        }
    }
}