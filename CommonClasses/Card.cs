namespace CommonClasses
{
    public class Card
    {
        public int Value { get; }
        public string Suit { get; }

        public Card(int value, string suit)
        {
            if (value < 6 || value > 14)
                throw new ArgumentOutOfRangeException(nameof(value), "Should be >= 6 and <= 14");
            Value = value;
            Suit = suit;
        }

        public string Name =>
            Value switch
            {
                6 => $"{Value} {Suit}",
                7 => $"{Value} {Suit}",
                8 => $"{Value} {Suit}",
                9 => $"{Value} {Suit}",
                10 => $"{Value} {Suit}",
                11 => $"Валет {Suit}",
                12 => $"Дама {Suit}",
                13 => $"Король {Suit}",
                14 => $"Туз {Suit}",
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
