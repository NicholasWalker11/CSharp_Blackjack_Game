namespace Blackjack.Models
{
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        
        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                total += card.GetValue();
                if (card.CardRank == Rank.Ace)
                {
                    aceCount++;
                }
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount --;
            }

            return total;
        }

        public string GetHandDisplay()
        {
            string display = "";
            foreach (Card card in cards)
            {
                display += $"{card}, ";
            }

            if (display.Length > 0)
            {
                display = display.Substring(0, display.Length - 2);
            }
    
            return display;
        }

        public Card GetFirstCard()
        {
            return cards[0];
        }
    }
}