using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace Fool.Wpf
{
    public partial class SuitChoose : Window
    {
        private int port = 5001;
        
        private readonly HttpClient _httpClient = new();

        public SuitChoose()
        {
            InitializeComponent();
        }

        private async void MakeSuit(object sender, RoutedEventArgs e)
        {
            var suitButton = (Button)sender;
            var suit = suitButton.Content.ToString();
            await _httpClient.PostAsJsonAsync($"https://localhost:{port}/GameState/CurrentSuit?suit={suit}", 0);
            this.DialogResult = true;
        }

        /*private async void MakeSpades(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:{port}/GameState/CurrentSuit?suit=Пики", 0);
            this.DialogResult = true;
        }

        private async void MakeDiamonds(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:{port}/GameState/CurrentSuit?suit=Буби", 0);
            this.DialogResult = true;
        }

        private async void MakeHearts(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:{port}/GameState/CurrentSuit?suit=Черви", 0);
            this.DialogResult = true;
        }

        private async void MakeClubs(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:{port}/GameState/CurrentSuit?suit=Трефы", 0);
            this.DialogResult = true;
        }*/
    }
}
