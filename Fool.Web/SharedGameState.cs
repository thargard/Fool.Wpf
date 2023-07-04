using CommonClasses;

namespace Fool.Web;


public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Card> Hand { get; set; } = null!;

}

public class SharedGameState
{
    // false if the current game is not started or finished
    public bool GameIsGoing { get; set; }
    // Колода 
    public Deck Deck { get; set; } = new Deck();

    // последняя карта на столе
    public Card CardOnTheTable { get; set; }

    public DateTime LastTurnTime { get; set; }

    public List<Player> Players { get; set; } = new List<Player>();

    public int CurrentMovePlayerId { get; set; }

    public CardSuit TopCardSuit { get; set; }

    public Card GetOneCard()
    {
        if (Deck.Used.Count == 36)
        {
            Deck.Used2.Clear();
            foreach (var player in Players)
                Deck.Used2.AddRange(player.Hand);
            
            Deck.Used2.Add(CardOnTheTable);
            return GetOneCard();

        }
        Random rnd = new Random();
        int card = rnd.Next(36);
        if (Deck.Used2.Contains(Deck.AllCards2[card]))
        {
            return GetOneCard();
        }
        
        else
        {
            Deck.Used2.Add(Deck.AllCards2[card]);
            return Deck.AllCards2[card];
        }
    }
}
