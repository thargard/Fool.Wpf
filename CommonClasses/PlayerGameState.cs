namespace CommonClasses
{
    public class PlayerGameState
    {
        public string CardOnTheTable { get; set; } = null!;
        public List<string> Hand { get; set; } = null!;
        public TimeSpan TimeToMove { get; set; }
        public bool IsMineTurn { get; set; }
        public string TopCardSuit { get; set; }
    }
}