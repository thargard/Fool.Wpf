using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fool.Wpf
{
    public partial class VictoryWindow : Window
    {
        private const int Port = 5001;
        private readonly HttpClient _httpClient = new();

        public VictoryWindow()
        {
            InitializeComponent();
            CurrentId();
        }

        private async void CurrentId()
        {
            var id = await _httpClient.GetFromJsonAsync<int>($"https://localhost:{Port}/GameState/PlayerToMove?");
            var text = id == 1 ? "Boris победил!" : "Gleb победил!";
            _winnerLabel.Content = text;
            return;
        }
    }
}
