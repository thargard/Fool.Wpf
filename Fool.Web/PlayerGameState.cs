namespace Fool.Web
{
    public class PlayerGameState
    {
        public string CardOnTheTable { get; set; } = null!;
        public List<string> Hand { get; set; } = null!;
        public TimeSpan TimeToMove { get; set; }
    }
}