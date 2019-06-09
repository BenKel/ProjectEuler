namespace ProjectEuler.Models.Poker
{
    public class PokerDuel
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
}