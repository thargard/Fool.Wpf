using CommonClasses;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Fool.Wpf
{
    public partial class MainWindow
    {
        private const int Port = 5001;
        private readonly HttpClient _httpClient = new();
        public bool IsBorisRadioButtonSelected { get; set; } = true;

        public MainWindow()
        {
            InitializeComponent();

            var dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += (_, _) => GetPlayerState();
            dt.Start();
        }

        private async void DrawOneCard(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync(
                $"https://localhost:{Port}/CardDraws?playerId={GetCurrentPlayerId()}", 0);
        }
        private async void PassOneMove(object sender, RoutedEventArgs e)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"https://localhost:{Port}/Move?playerId={GetCurrentPlayerId()}&command=skip", 0);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotAcceptable:
                case System.Net.HttpStatusCode.Conflict:
                case System.Net.HttpStatusCode.BadRequest:
                    ShowError();
                    break;
            }
        }
        private async void MakeMove(object sender, RoutedEventArgs e)
        {
            var cardButton = (Button)sender;
            var card = HttpUtility.UrlEncode(cardButton.Content.ToString());
            var response = await _httpClient.PostAsJsonAsync(
                $"https://localhost:{Port}/Move?playerId={GetCurrentPlayerId()}&command={card}",
                0);

            var currentPlayerId = await _httpClient
                .GetFromJsonAsync<int>($"https://localhost:{Port}/GameState/PlayerToMove?");
            var winner = currentPlayerId == 1 ? "Boris победил!" : "Gleb победил!";

            if (response.StatusCode == System.Net.HttpStatusCode.MultipleChoices)
                ShowSuitChoice();

            if (response.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                ShowError();

            if (response.StatusCode == System.Net.HttpStatusCode.Gone)
                ShowVictory();
        }
        private async void StartGame(object sender, RoutedEventArgs e)
        {
            await _httpClient.PostAsJsonAsync(
                $"https://localhost:{Port}/GameState?playerId={GetCurrentPlayerId()}",
                0);
        }

        private async void UpdateUi(PlayerGameState gameState)
        {
            _handArea.Children.Clear();

            var currentPlayerId = await _httpClient
                .GetFromJsonAsync<int>($"https://localhost:{Port}/GameState/PlayerToMove?");
            PlayerToMoveLabel.Content = currentPlayerId == 1 ? "Boris ходит" : "Gleb ходит";
            CardOnTheTable.Text = gameState.CardOnTheTable.Name;

            _currentSuitLabel.Content = "Масть: " + gameState.TopCardSuit;
            foreach (var card in gameState.Hand)
            {
                var cardInHand = new Button
                {
                    Content = card.Name,
                    Height = 40,
                    Width = 70
                };
                cardInHand.Click += MakeMove;
                _handArea.Children.Add(cardInHand);
            }
            _timerLable.Content = ((int)gameState.TimeToMove.TotalSeconds).ToString();
        }

        private static void ShowVictory()
        {
            var vw = new VictoryWindow();
            vw.ShowDialog();
        }

        private static void ShowError()
        {
            var erw = new ErrorWindow();
            erw.ShowDialog();
        }

        private static void ShowSuitChoice()
        {
            var choiceWindow = new SuitChoose();
            choiceWindow.ShowDialog();
        }

        private async Task GetPlayerState()
        {
            var gs = await _httpClient.GetFromJsonAsync<PlayerGameState>(
                $"https://localhost:{Port}/GameState?playerId={GetCurrentPlayerId()}");
            if (gs != null)
                UpdateUi(gs);
        }
        private int GetCurrentPlayerId() => IsBorisRadioButtonSelected ? 1 : 2;
    }
}
