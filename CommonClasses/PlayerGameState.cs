namespace CommonClasses
{
    public class PlayerGameState
    {
        public Card CardOnTheTable { get; set; } = null!;
        public List<Card> Hand { get; set; } = null!;
        public TimeSpan TimeToMove { get; set; }
        public bool IsMineTurn { get; set; }
        public CardSuit TopCardSuit { get; set; }
    }
}