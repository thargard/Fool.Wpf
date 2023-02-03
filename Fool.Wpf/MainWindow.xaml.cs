using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fool.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] deck = {"6 пик", "7 пик", "8 пик", "9 пик", "10 пик", "Валет пик", "Дама пик", "Король пик", "Туз пик",
                "6 бубей", "7 бубей", "8 бубей", "9 бубей", "10 бубей", "Валет бубей", "Дама бубей", "Король бубей", "Туз бубей",
                "6 треф", "7 треф", "8 треф", "9 треф", "10 треф", "Валет треф", "Дама треф", "Король треф", "Туз треф",
                "6 чирв", "7 чирв", "8 чирв", "9 чирв", "10 чирв", "Валет чирв", "Дама чирв", "Король чирв", "Туз чирв"};

        List<string> trash = new List<string>() { "7 треф" };

        /*string[] trash = { "7 треф" };*/

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OneCard(object sender, RoutedEventArgs e)
        {
            if (trash.Count == 36) return;
            Random rnd = new Random();
            int card = rnd.Next(36);
            if (trash.Contains(deck[card]))
            {
                OneCard(sender, e);
            }
            else
            {
                var but = new Button() { Content = deck[card], Height = 40, Width = 70 };
                trash.Add(deck[card]);
                _handArea.Children.Add(but);
            }
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
                OneCard(sender, e);
        }

        private async void Move(object sender, RoutedEventArgs e)
        {
            // Post запрос
            var httpClient = new HttpClient();
            var id = 1;
            var card = HttpUtility.UrlEncode("дама пик");
            var str = $"https://localhost:7081/Move?playerId={id}&card={card}";
            var str1 = await httpClient.PostAsJsonAsync(str, 0 /*об этом пока не думать*/);
            str1.EnsureSuccessStatusCode();
        }

        private async void UpdateGameState_OnClick(object sender, RoutedEventArgs e)
        {
            // как делать GET запрос
            var httpClient = new HttpClient();
            var gs = await httpClient.GetFromJsonAsync<PlayerGameState>(
                "https://localhost:7081/GameState?playerId=1");
            Console.WriteLine(gs);
        }
    }   
}
