namespace Blackjack.Models
{
    public class Game
    {
        private Deck deck;
        private Hand playerHand;
        private Hand dealerHand;
        private int playerBalance;
        private int currentBet;
        private int insuranceBet;

        public Game()
        {
            deck = new Deck();
            deck.Shuffle();
            playerHand = new Hand();
            dealerHand = new Hand();
            playerBalance = 1000;
        }

        public int GetBalance()
        {
            return playerBalance;
        }

        public bool PlaceBet(int amount)
        {
            if (amount <= 0 || amount > playerBalance)
            {
                return false;
            }

            currentBet = amount;
            return true;
        }

        public int GetCurrentBet()
        {
            return currentBet;
        }

        public bool PlaceInsurance()
        {
            int insuranceCost = currentBet / 2;
            int availableBalance = playerBalance - currentBet;

            if (availableBalance < insuranceCost)
            {
                return false;
            }

            insuranceBet = insuranceCost;
            return true;
        }

        public bool DealerHasBlackjack()
        {
            return dealerHand.GetTotalValue() == 21;
        }

        public void ResolveInsurance()
        {
            if (insuranceBet > 0)
            {
                if (DealerHasBlackjack())
                {
                    int payout = insuranceBet * 2;
                    playerBalance += payout;
                    Console.WriteLine($"Dealer had blackjack. Your insurance bet wins you {payout} chips!");
                }

                else 
                {
                    playerBalance -= insuranceBet;
                    Console.WriteLine($"Dealer didn't have blackjack. Your insurance bet fails. You lose {insuranceBet} chips.");
                }

                insuranceBet = 0;
            }
        }

        public void DealInitialCards()
        {
            playerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());

            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
        }

        public Card PlayerHit()
        {
            Card newCard = deck.DealCard();
            playerHand.AddCard(newCard);
            return newCard;
        }

        public void DealerTurn()
        {
            while (dealerHand.GetTotalValue() < 17)
            {
                Card newCard = deck.DealCard();
                dealerHand.AddCard(newCard);
            }
        }

        public string DetermineWinner()
        {
            int playerTotal = playerHand.GetTotalValue();
            int dealerTotal = dealerHand.GetTotalValue();
            string result;

            if (playerTotal > 21)
            {
                result = "You busted. Dealer wins.";
                playerBalance -= currentBet;
            }
            else if (dealerTotal > 21)
            {
                result = "Dealer busted. You win!";
                playerBalance += currentBet;
            }
            else if (playerTotal > dealerTotal)
            {
                result = "You win!";
                playerBalance += currentBet;
            }
            else if (dealerTotal > playerTotal)
            {
                result = "Dealer wins.";
                playerBalance -= currentBet;
            }
            else
            {
                result = "It's a tie!";
            }

            return result;
        }

        public int GetPlayerTotal()
        {
            return playerHand.GetTotalValue();
        }

        public Card GetDealerUpCard()
        {
            return dealerHand.GetFirstCard();
        }

        public Hand GetPlayerHand()
        {
            return playerHand;
        }

        public Hand GetDealerHand()
        {
            return dealerHand;
        }

        public int GetDealerTotal()
        {
            return dealerHand.GetTotalValue();
        }

        public bool IsPlayerBusted()
        {
            return playerHand.GetTotalValue() > 21;
        }

        public bool IsDealerBusted()
        {
            return dealerHand.GetTotalValue() > 21;
        }

        public void StartNewRound()
        {
            playerHand = new Hand();
            dealerHand = new Hand();
            currentBet = 0;
            insuranceBet = 0;
            
            // Reshuffle deck if getting low on cards (fewer than 10 remaining)
            if (deck.GetRemainingCards() < 10)
            {
                deck = new Deck();
                deck.Shuffle();
            }
        }
    }
}