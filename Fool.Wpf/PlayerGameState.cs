using System;

namespace Fool.Wpf
{
    public class PlayerGameState
    {
        public string CardOnTheTable { get; set; } = null!;
        public string[] Hand { get; set; } = null!;
        public TimeSpan TimeToMove { get; set; }
    }
}