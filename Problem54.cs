using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using ProjectEuler.DataTypes;

namespace ProjectEuler
{
    internal class Problem54
    {
        public int GetAnswer()
        {
            int playerOneWinCount = 0;

            foreach (PokerDuel duel in ReadHandsFromFile())
            {
                if (duel.HandOneWins())
                {
                    ++playerOneWinCount;
                }
            }

            return playerOneWinCount;
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
                    Card.CardSuit suit = GetSuit(line[i+1]);
                    cardsInLine.Add(new Card(value, suit));
                }

                Debug.Assert(cardsInLine.Count == 10);

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

    internal class PokerDuel
    {
        public PokerDuel(PokerHand handOne, PokerHand handTwo)
        {
            HandOne = handOne;
            HandTwo = handTwo;
        }

        public PokerHand HandOne { get; }
        public PokerHand HandTwo { get; }

        public bool HandOneWins()
        {
            return HandOne.WillBeatHand(HandTwo);
        }
    }

    internal class PokerHand
    {
        public List<Card> Cards { get; }

        public PokerHand(Card cardOne, Card cardTwo, Card cardThree, Card cardFour, Card cardFive)
        {
            // Sort and add to list.
            Cards = new List<Card>
            {
                cardOne,
                cardTwo,
                cardThree,
                cardFour,
                cardFive
            }
            .OrderBy(card => card.Value).ToList();
        }

        // Returns the number of distinct pairs in the hand.
        public int PairCount()
        {
            int count = 0;
            var hash = new HashSet<int> {(int)Cards[0].Value};

            if (!hash.Add((int)Cards[1].Value))
            {
                ++count;
                hash.Remove((int)Cards[1].Value);
            }
            if (!hash.Add((int)Cards[2].Value))
            {
                ++count;
                hash.Remove((int)Cards[2].Value);
            }
            if (!hash.Add((int)Cards[3].Value))
            {
                ++count;
                hash.Remove((int)Cards[3].Value);
            }
            if (!hash.Add((int)Cards[4].Value))
            {
                ++count;
                hash.Remove((int)Cards[4].Value);
            }

            return count;
        }

        public bool HasThreeOfAKind()
        {
            List<int> values = Cards.Select(card => (int)card.Value).ToList();

            List<int> selection = values.Where(card => card == values[0]).ToList();
            if (selection.Count >= 3)
            {
                return true;
            }

            selection = values.Where(card => card == values[1]).ToList();
            if (selection.Count >= 3)
            {
                return true;
            }

            selection = values.Where(card => card == values[2]).ToList();
            return selection.Count >= 3;
        }

        public bool HasFourOfAKind()
        {
            List<int> values = Cards.Select(card => (int)card.Value).ToList();

            List<int> selection = values.Where(card => card == values[0]).ToList();
            if (selection.Count >= 4)
            {
                return true;
            }

            selection = values.Where(card => card == values[1]).ToList();
            return selection.Count >= 4;
        }

        public bool HasFullHouse()
        {
            return HasThreeOfAKind() && PairCount() >= 2;
        }

        public bool HasStraight()
        {
            // Look at special case of A2345 straight - Ace is sorted to be highest
            if (Cards[0].Value == Card.FaceValue.Two && Cards[4].Value != Card.FaceValue.Six)
            {
                if (Cards[4].Value != Card.FaceValue.Ace)
                {
                    return false;
                }

                for (int i = 0; i < 3; ++i)
                {
                    if (Cards[i].Value + 1 != Cards[i + 1].Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            for (int i = 0; i < 4; ++i)
            {
                if (Cards[i].Value + 1 != Cards[i + 1].Value)
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasFlush()
        {
            return Cards.All(card => card.Suit == Cards[0].Suit);
        }

        public bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
        }

        public bool HasRoyalFlush()
        {
            return HasStraightFlush() && Cards.Any(card => card.Value == Card.FaceValue.Ten)
                && Cards.Any(card => card.Value == Card.FaceValue.Ace);
        }

        public bool WillBeatHand(PokerHand opposingHand)
        {
            // Royal Flush
            if (HasRoyalFlush())
            {
                return !opposingHand.HasRoyalFlush();
            }
            if (opposingHand.HasRoyalFlush())
            {
                return false;
            }

            // Straight flush
            if (HasStraightFlush())
            {
                if (!opposingHand.HasStraightFlush())
                {
                    return true;
                }

                return Cards[4] > opposingHand.Cards[4];
            }
            if (opposingHand.HasStraightFlush())
            {
                return false;
            }

            // Four of a Kind
            if (HasFourOfAKind())
            {
                if (!opposingHand.HasFourOfAKind())
                {
                    return true;
                }

                // The middle card will be part of the 4 - the hand is sorted.
                return Cards[2] > opposingHand.Cards[2];
            }
            if (opposingHand.HasFourOfAKind())
            {
                return false;
            }

            // Full House
            if (HasFullHouse())
            {
                if (!opposingHand.HasFullHouse())
                {
                    return true;
                }

                // The middle card will be part of the 3 - the hand is sorted.
                return Cards[2] > opposingHand.Cards[2];
            }
            if (opposingHand.HasFullHouse())
            {
                return false;
            }

            // Flush
            if (HasFlush())
            {
                if (!opposingHand.HasFlush())
                {
                    return true;
                }
                
                return Cards[4] > opposingHand.Cards[4];
            }
            if (opposingHand.HasFlush())
            {
                return false;
            }

            // Straight
            if (HasStraight())
            {
                if (!opposingHand.HasStraight())
                {
                    return true;
                }

                return Cards[4] > opposingHand.Cards[4];
            }
            if (opposingHand.HasStraight())
            {
                return false;
            }

            // Three of a Kind
            if (HasThreeOfAKind())
            {
                if (!opposingHand.HasThreeOfAKind())
                {
                    return true;
                }

                // The middle card will be part of the 3 - the hand is sorted.
                return Cards[2] > opposingHand.Cards[2];
            }
            if (opposingHand.HasThreeOfAKind())
            {
                return false;
            }

            // Two Pairs
            if (PairCount() == 2)
            {
                if (opposingHand.PairCount() < 2)
                {
                    return true;
                }

                // If the final card is part of the higher pair, compare it to the high pair in the other hand.
                if (Cards[4].Value == Cards[3].Value)
                {
                    if (opposingHand.Cards[4].Value == opposingHand.Cards[3].Value)
                    {
                        return Cards[4] > opposingHand.Cards[4];
                    }
                    
                    return Cards[4] > opposingHand.Cards[3];
                }

                if (opposingHand.Cards[4].Value == opposingHand.Cards[3].Value)
                {
                    return Cards[3] > opposingHand.Cards[4];
                }

                return Cards[3] > opposingHand.Cards[3];

                //TODO: account for KKQQ7 vs KKJJ9
            }
            if (opposingHand.PairCount() == 2)
            {
                return false;
            }

            // Pair
            if (PairCount() == 1)
            {
                if (opposingHand.PairCount() == 0)
                {
                    return true;
                }

                Card.FaceValue pairCard = GetHighestPairValue();
                Card.FaceValue oppPairCard = opposingHand.GetHighestPairValue();

                // If the final card is part of the higher pair, compare it to the high pair in the other hand.
                if (pairCard > oppPairCard)
                {
                    return true;
                }
                if (pairCard < oppPairCard)
                {
                    return false;
                }

                Card.FaceValue highestSingle = GetHighestSingleValue();
                Card.FaceValue oppHighestSingle = opposingHand.GetHighestSingleValue();

                if (highestSingle > oppHighestSingle)
                {
                    return true;
                }

                if (highestSingle < oppHighestSingle)
                {
                    return false;
                }

                // TODO: check lower 2 cards, if the highest is identical
            }
            if (opposingHand.PairCount() == 1)
            {
                return false;
            }

            // Highest Card
            return GetHighestSingleValue() > opposingHand.GetHighestSingleValue();
        }

        private Card.FaceValue GetHighestPairValue()
        {
            if (Cards[4].Value == Cards[3].Value)
            {
                return Cards[4].Value;
            }
            if (Cards[3].Value == Cards[2].Value)
            {
                return Cards[3].Value;
            }
            if (Cards[2].Value == Cards[1].Value)
            {
                return Cards[2].Value;
            }
            if (Cards[1].Value == Cards[0].Value)
            {
                return Cards[1].Value;
            }

            Debug.WriteLine("No pair found. Can't return highest pair. Returning highest card.");
            return Cards[4].Value;
        }

        private Card.FaceValue GetHighestSingleValue()
        {
            if (Cards[4].Value != Cards[3].Value)
            {
                return Cards[4].Value;
            }
            if (Cards[3].Value != Cards[2].Value)
            {
                return Cards[3].Value;
            }
            if (Cards[2].Value != Cards[1].Value)
            {
                return Cards[2].Value;
            }
            if (Cards[1].Value != Cards[0].Value)
            {
                return Cards[1].Value;
            }
            return Cards[0].Value;
        }
    }
}
