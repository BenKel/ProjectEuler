using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ProjectEuler.Models;
using ProjectEuler.Models.Poker;

namespace ProjectEuler.Problems
{
    public class Problem54 : ProblemBase
    {
        public override string Title => "Poker hands";

        public override string Description => @"
In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

    High Card: Highest value card.
    One Pair: Two cards of the same value.
    Two Pairs: Two different pairs.
    Three of a Kind: Three cards of the same value.
    Straight: All cards are consecutive values.
    Flush: All cards of the same suit.
    Full House: Three of a kind and a pair.
    Four of a Kind: Four cards of the same value.
    Straight Flush: All cards are consecutive values of same suit.
    Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.

The cards are valued in the order:
2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.

If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.

Consider the following five hands dealt to two players:
Hand	 	Player 1	 	Player 2	 	Winner
1	 	5H 5C 6S 7S KD
Pair of Fives
	 	2C 3S 8S 8D TD
Pair of Eights
	 	Player 2
2	 	5D 8C 9S JS AC
Highest card Ace
	 	2C 5C 7D 8S QH
Highest card Queen
	 	Player 1
3	 	2D 9C AS AH AC
Three Aces
	 	3D 6D 7D TD QD
Flush with Diamonds
	 	Player 2
4	 	4D 6S 9H QH QC
Pair of Queens
Highest card Nine
	 	3D 6D 7H QD QS
Pair of Queens
Highest card Seven
	 	Player 1
5	 	2H 2D 4C 4D 4S
Full House
With Three Fours
	 	3C 3D 3S 9S 9D
Full House
with Three Threes
	 	Player 1

The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1's cards and the last five are Player 2's cards. You can assume that all hands are valid (no invalid characters or repeated cards), each player's hand is in no specific order, and in each hand there is a clear winner.

How many hands does Player 1 win?
            ";

        public override string GetAnswer()
        {
            int playerOneWinCount = 0;

            foreach (PokerDuel duel in ReadHandsFromFile())
            {
                if (duel.HandOneWins())
                {
                    ++playerOneWinCount;
                }
            }

            return playerOneWinCount.ToString();
        }

        private static IEnumerable<PokerDuel> ReadHandsFromFile()
        {
            const string filePath = "Data/Problem54Data.txt";
            string[] rawText = File.ReadAllLines(filePath);

            // Convert each line into a poker hand.
            // Expecting 10 cards per line, separated by spaces in the format 'VS' where V is the value, S is the suit.
            foreach (string line in rawText)
            {
                var cardsInLine = new List<Card>();

                for (int i = 0; i < line.Length; i += 3)
                {
                    Card.FaceValue value = GetValue(line[i]);
                    Card.CardSuit suit = GetSuit(line[i + 1]);
                    cardsInLine.Add(new Card(value, suit));
                }

                var handOne = new PokerHand(cardsInLine[0], cardsInLine[1], cardsInLine[2], cardsInLine[3], cardsInLine[4]);
                var handTwo = new PokerHand(cardsInLine[5], cardsInLine[6], cardsInLine[7], cardsInLine[8], cardsInLine[9]);

                yield return new PokerDuel(handOne, handTwo);
            }
        }

        private static Card.CardSuit GetSuit(char c)
        {
            switch (c)
            {
                case 'S':
                    return Card.CardSuit.Spades;

                case 'C':
                    return Card.CardSuit.Clubs;

                case 'H':
                    return Card.CardSuit.Hearts;

                case 'D':
                    return Card.CardSuit.Diamonds;

                default:
                    Debug.WriteLine("Suit not read correctly from file. Defaulting to Spades.");
                    return Card.CardSuit.Spades;
            }
        }

        private static Card.FaceValue GetValue(char c)
        {
            switch (c)
            {
                case 'A':
                    return Card.FaceValue.Ace;

                case '2':
                    return Card.FaceValue.Two;

                case '3':
                    return Card.FaceValue.Three;

                case '4':
                    return Card.FaceValue.Four;

                case '5':
                    return Card.FaceValue.Five;

                case '6':
                    return Card.FaceValue.Six;

                case '7':
                    return Card.FaceValue.Seven;

                case '8':
                    return Card.FaceValue.Eight;

                case '9':
                    return Card.FaceValue.Nine;

                case 'T':
                    return Card.FaceValue.Ten;

                case 'J':
                    return Card.FaceValue.Jack;

                case 'Q':
                    return Card.FaceValue.Queen;

                case 'K':
                    return Card.FaceValue.King;

                default:
                    Debug.WriteLine("Card Value not read correctly from file. Defaulting to Ace.");
                    return Card.FaceValue.Ace;
            }
        }
    }
}