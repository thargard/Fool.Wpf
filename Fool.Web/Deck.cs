using CommonClasses;

namespace Fool.Web
{
    public class Deck
    {
    
        public List<Card> AllCards2 = new List<Card>();

        public void FillDeck()
        {
            string[] suits = { "пик", "червей", "бубей", "треф" };

            var values = Enum.GetValues<CardValue>();
            foreach (var suit in suits)
            {
                foreach (var value in values)
                {
                    Card card = new Card(value, suit);
                    AllCards2.Add(card);
                }
            }
        }

        public List<string> Used = new List<string>() { };

        public List<Card> Used2 = new List<Card>() { };
    }
}
