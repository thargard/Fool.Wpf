namespace CommonClasses
{
    public enum CardValue
    {
        Six = 6, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
    public sealed class Card
    {
        public CardValue Value { get; }
        public string Suit { get; }

        public Card(CardValue value, string suit)
        {
            if (value < CardValue.Six || value > CardValue.Ace)
                throw new ArgumentOutOfRangeException(nameof(value), "Should be >= 6 and <= 14");
            Value = value;
            Suit = suit;
        }

        public string Name =>
            Value switch
            {
                < CardValue.Jack => $"{(int)Value} {Suit}",
                CardValue.Jack => $"Валет {Suit}",
                CardValue.Queen => $"Дама {Suit}",
                CardValue.King => $"Король {Suit}",
                CardValue.Ace => $"Туз {Suit}",
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
