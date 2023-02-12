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
    /// <summary>
    /// Interaction logic for SuitChoose.xaml
    /// </summary>
    public partial class SuitChoose : Window
    {
        private int port = 5001;

        public SuitChoose()
        {
            InitializeComponent();
        }

        private async void MakeSpades(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var str = $"https://localhost:{port}/GameState/TSPRoute?suit=Пики";
            var str1 = await httpClient.PostAsJsonAsync(str, 0);
            this.DialogResult = true;
        }

        private async void MakeDiamonds(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var str = $"https://localhost:{port}/GameState/TSPRoute?suit=Буби";
            var str1 = await httpClient.PostAsJsonAsync(str, 0);
            this.DialogResult = true;
        }

        private async void MakeHearts(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var str = $"https://localhost:{port}/GameState/TSPRoute?suit=Черви";
            var str1 = await httpClient.PostAsJsonAsync(str, 0);
            this.DialogResult = true;
        }

        private async void MakeClubs(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient();
            var str = $"https://localhost:{port}/GameState/TSPRoute?suit=Трефы";
            var str1 = await httpClient.PostAsJsonAsync(str, 0);
            this.DialogResult = true;
        }
    }
}
