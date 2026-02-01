using Blackjack.Models;

Console.WriteLine("=== Welcome to Blackjack! ===\n");

// Create a new game
Game game = new Game();

// Deal initial cards
game.DealInitialCards();

// Player's turn
game.PlayerTurn();

// Check if player busted
if (game.GetPlayerTotal() > 21)
{
    Console.WriteLine("\nYou busted!");
    game.DetermineWinner();
}
else
{
    // Dealer's turn if player didn't bust
    game.DealerTurn();
    
    // Determine winner
    game.DetermineWinner();
}

Console.WriteLine("\nThanks for playing!");
