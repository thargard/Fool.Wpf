namespace Fool.Web;


public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Hand { get; set; } = null!;

}




public class SharedGameState
{
    // Колода 
    public Deck Deck { get; set; } = new Deck();

    // последняя карта на столе
    public string CardOnTheTable { get; set; }


    public List<Player> Players { get; set; } = new List<Player>();

    public int CurrentMovePlayerId { get; set; }

    public string GetOneCard()
    {
        if (Deck.Used.Count == 36)
        {
            Deck.Used.Clear();
            foreach (var player in Players)
                Deck.Used.AddRange(player.Hand);
            
            Deck.Used.Add(CardOnTheTable);
            return GetOneCard();

        }
        Random rnd = new Random();
        int card = rnd.Next(36);
        if (Deck.Used.Contains(Deck.AllCrads[card]))
        {
            return GetOneCard();
        }
        
        else
        {
            Deck.Used.Add(Deck.AllCrads[card]);
            return Deck.AllCrads[card];
        }
    }
}
