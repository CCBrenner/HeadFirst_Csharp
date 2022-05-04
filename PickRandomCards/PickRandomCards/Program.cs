// something to note: the book says I should use a Main() method but VS told me that it was a problem
// the code runs fine so I'll take it
// this project can be improved by making non-repeating cards liek in a real deck of cards

using PickRandomCards;

// Use Console.Write to ask the user for the number of cards to pick.
Console.Write("How many cards would you  like to pick?");

// Use Console.ReadLine to read a line of input into a string variable called line.
string line = Console.ReadLine();

// Use int.TryParse to try to convert it to an int variable called numberOfCards.
if (int.TryParse(line, out int numberOfCards))
{
    // If the user input could be converted to an int value, use your CardPicker class to pick the number of cards that the user specified: CardPicker.PickSomeCards(numberOfCards). Use a string[] variable to save the results, then use a foreach loop to call Console.WriteLine on each card in the array. Flip back to Chapter 1 to see an example of a foreach loop—you’ll use it to loop through every element of the array.Here’s the first line of the loop:foreach (string card in CardPicker.PickSomeCards(numberOfCards))
    string[] someCards = CardPicker.PickSomeCards(numberOfCards);
    foreach (string card in someCards)
    {
        Console.WriteLine(card);
    }
}
else
{
    // If the user input could not be converted, use Console.WriteLine to write a message to the user indicating that the number was not valid.
    Console.WriteLine("Sorry, that input is not a valid input.");
}
