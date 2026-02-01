using Blackjack.Models;

Console.WriteLine("=== Testing Deck ===\n");

// Create a new deck
Deck deck = new Deck();
Console.WriteLine("Created a new deck of 52 cards.\n");

// Deal first 5 cards before shuffle
Console.WriteLine("First 5 cards (before shuffle):");
for (int i = 0; i < 5; i++)
{
    Card card = deck.DealCard();
    Console.WriteLine($"{i + 1}. {card} (Value: {card.GetValue()})");
}

// Create a new deck and shuffle it
Console.WriteLine("\n=== Creating and shuffling a new deck ===\n");
Deck shuffledDeck = new Deck();
shuffledDeck.Shuffle();

// Deal 10 cards from shuffled deck
Console.WriteLine("First 10 cards from shuffled deck:");
for (int i = 0; i < 10; i++)
{
    Card card = shuffledDeck.DealCard();
    Console.WriteLine($"{i + 1}. {card} (Value: {card.GetValue()})");
}

Console.WriteLine("\nDeck test complete!");