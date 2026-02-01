namespace Blackjack.Models
{
    public class Game
    {
        private Deck deck;
        private Hand playerHand;
        private Hand dealerHand;

        public Game()
        {
            deck = new Deck();
            deck.Shuffle();
            playerHand = new Hand();
            dealerHand = new Hand();
        }

        public void DealInitialCards()
        {
            playerHand.AddCard(deck.DealCard());
            playerHand.AddCard(deck.DealCard());

            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());

            Console.WriteLine("Your hand is: ");
            Console.Write(playerHand.GetHandDisplay());
            Console.WriteLine($"Total: {playerHand.GetTotalValue()}\n");

            Console.WriteLine("Dealer's Hand: ");
            Console.WriteLine($"  - [Hidden Card]");
            Console.WriteLine($"  - {dealerHand.GetFirstCard()}");

            Card dealerUpCard = dealerHand.GetFirstCard();
            if (dealerUpCard.CardRank == Rank.Ace){
                Console.WriteLine("\nDealer shows an Ace!");
            }
        }

        public void PlayerTurn(){
            while (playerHand.GetTotalValue() < 21){
                Console.Write("Do you want to (H)it or (S)tand? ");
                string choice = Console.ReadLine()?.ToUpper() ?? "";
                
                if (choice == "H"){
                    Card newCard = deck.DealCard();
                    playerHand.AddCard(newCard);
                    Console.WriteLine($"\nYou drew: {newCard}");
                    Console.Write($"Your current hand is: \n{playerHand.GetHandDisplay()}");
                    Console.WriteLine($"Total: {playerHand.GetTotalValue()}");
                }
                else if (choice == "S"){
                    Console.WriteLine("\nYou Stand.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please enter H or S.");
                }
            }
        }

        public void DealerTurn()
        {
            Console.WriteLine("\n=== Dealer's Turn ===");
            Console.WriteLine("Dealer reveals hidden card");
            Console.Write(dealerHand.GetHandDisplay());
            Console.WriteLine($"Dealer's total: {dealerHand.GetTotalValue()}");

            while (dealerHand.GetTotalValue() < 17)
            {
                Console.WriteLine("\nDealer hits...");
                Card newCard = deck.DealCard();
                dealerHand.AddCard(newCard);
                Console.WriteLine($"Dealer drew: {newCard}");
                Console.WriteLine($"Dealer's total is: {dealerHand.GetTotalValue()}");
                
                if (dealerHand.GetTotalValue() > 21)
                {
                    Console.WriteLine("\nDealer busts!");
                    break;
                }
            }

            if (dealerHand.GetTotalValue() <= 21)
            {
                Console.WriteLine("\nDealer stands.");
            }
        }

        public void DetermineWinner()
        {
            Console.WriteLine("\n=== Final Results ===");

            int playerTotal = playerHand.GetTotalValue();
            int dealerTotal = dealerHand.GetTotalValue();

            Console.WriteLine($"Your total is: {playerTotal}");
            Console.WriteLine($"Dealer's total is: {dealerTotal}\n");

            if (playerTotal > 21)
            {
                Console.WriteLine("You busted. Dealer wins.");
            }

            else if (dealerTotal > 21)
            {
                Console.WriteLine("Dealer busted. You win!");
            }

            else if (playerTotal > dealerTotal)
            {
                Console.WriteLine("You win!");
            }

            else if (dealerTotal > playerTotal)
            {
                Console.WriteLine("Dealer wins.");
            }

            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
        
        public int GetPlayerTotal()
        {
            return playerHand.GetTotalValue();
        }
    }
}