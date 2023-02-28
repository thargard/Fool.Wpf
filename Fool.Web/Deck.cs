using CommonClasses;

namespace Fool.Web
{
    public class Deck
    {
        public string[] AllCrads = {"6 пик", "7 пик", "8 пик", "9 пик", "10 пик", "Валет пик", "Дама пик", "Король пик", "Туз пик",
                "6 бубей", "7 бубей", "8 бубей", "9 бубей", "10 бубей", "Валет бубей", "Дама бубей", "Король бубей", "Туз бубей",
                "6 треф", "7 треф", "8 треф", "9 треф", "10 треф", "Валет треф", "Дама треф", "Король треф", "Туз треф",
                "6 червей", "7 червей", "8 червей", "9 червей", "10 червей", "Валет червей", "Дама червей", "Король червей", "Туз червей"};

        public List<Card> AllCards2 = new List<Card>();

        public void FillDeck()
        {
            string[] suits = { "пик", "червей", "бубей", "треф" };
            int[] values = { 6, 7, 8, 9, 10, 11, 12, 13, 14 };
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
