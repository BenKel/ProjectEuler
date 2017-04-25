namespace ProjectEuler.DataTypes
{
    internal class Card
    {
        public enum CardSuit
        {
            Spades,
            Hearts,
            Clubs,
            Diamonds
        }

        public enum FaceValue
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public FaceValue Value { get; }
        public CardSuit Suit { get; }

        public Card(FaceValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }
        
        public static bool operator == (Card a, Card b)
        {
            return a.Suit == b.Suit && a.Value == b.Value;
        }

        public static bool operator != (Card a, Card b)
        {
            return a.Suit != b.Suit || a.Value != b.Value;
        }

        public static bool operator > (Card a, Card b)
        {
            return a.Value > b.Value;
        }

        public static bool operator < (Card a, Card b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >= (Card a, Card b)
        {
            return !(a < b);
        }

        public static bool operator <= (Card a, Card b)
        {
            return !(a > b);
        }

        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != GetType())
            {
                return false;
            }

            return this == o as Card;
        }

        public override int GetHashCode()
        {
            return (int)Value ^ (int)Suit;
        }
    }
}
