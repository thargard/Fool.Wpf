using CommonClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
        private DispatcherTimer dt;

        private int port = 5001;

        private int decrement;

        private bool _isId1Checked;
        public bool IsId1Checked { get => _isId1Checked; set => _isId1Checked = value; }


        public MainWindow()
        {
            IsId1Checked = true;
            InitializeComponent();

            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.5);
            dt.Tick += OnceASecond;
            dt.Start();
        }

        private async void OneCard(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            int id  = IsId1Checked? 1 : 2;
            var str = $"https://localhost:{port}/TakeCard?playerId={id}";
            var str1 = await httpClient.PostAsJsonAsync(str, 0 /*об этом пока не думать*/);
        }

        private async void Pass(object sender, RoutedEventArgs e)
        {
            
        }

        private async void MakeMove(object sender, RoutedEventArgs e)
        {
            // Post запрос

            var httpClient = new HttpClient();
            int id = IsId1Checked ? 1 : 2;

            var cardd = (Button)sender;
            var card = HttpUtility.UrlEncode(cardd.Content.ToString());

            var str = $"https://localhost:{port}/Move?playerId={id}&card={card}";
            var str1 = await httpClient.PostAsJsonAsync(str, 0 /*об этом пока не думать*/);
            // str1.EnsureSuccessStatusCode();
        }

        private async Task GetPlayerState()
        {
            var httpClient = new HttpClient();
            int id = IsId1Checked ? 1 : 2;
            var gs = await httpClient.GetFromJsonAsync<PlayerGameState>(
                $"https://localhost:{port}/GameState?playerId={id}");

            UpdateUI(gs);
        }

        private void UpdateUI(PlayerGameState gs)
        {
            if (gs == null) return;
            _handArea.Children.Clear();

            var name = IsId1Checked ? "Boris ходит" : "Gleb ходит";
            
            _turnLabel.Content = name;
            var txt = new TextBlock() { Text = gs.CardOnTheTable, Height = 80, Width = 100, Background = Brushes.White };
            _grid.Children.Add(txt);

            foreach (var card in gs.Hand)
            {
                var but = new Button() { Content = card, Height = 40, Width = 70 };
                but.Click += MakeMove;
                _handArea.Children.Add(but);
            }
            decrement = (int)gs.TimeToMove.TotalSeconds;
            decrement--;
            _timerLable.Content = decrement.ToString();

        }

        private void OnceASecond(object? sender, EventArgs e)
        {
            
           GetPlayerState();
        }

        private async void StartGame(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Id1 is {IsId1Checked}");
            var httpClient = new HttpClient();
            int id = IsId1Checked ? 1 : 2;
            var str = $"https://localhost:{port}/GameState?playerId={id}";
            var str1 = await httpClient.PostAsJsonAsync(str, 0);
        }

    }
}
