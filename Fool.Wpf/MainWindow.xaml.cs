using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        string[] arr = {"6 пик", "7 пик", "8 пик", "9 пик", "10 пик", "Валет пик", "Дама пик", "Король пик", "Туз пик",
                "6 бубей", "7 бубей", "8 бубей", "9 бубей", "10 бубей", "Валет бубей", "Дама бубей", "Король бубей", "Туз бубей",
                "6 треф", "7 треф", "8 треф", "9 треф", "10 треф", "Валет треф", "Дама треф", "Король треф", "Туз треф",
                "6 чирв", "7 чирв", "8 чирв", "9 чирв", "10 чирв", "Валет чирв", "Дама чирв", "Король чирв", "Туз чирв"};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var but = new Button() { Content = "Дама пик", Height = 20, Width = 80 };
            but.Click += But_Click;
            _grid.Children.Add(but);
        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            var but = new Button() { Content = "Дама треф", Height = 30, Width = 70 };
            but.Click += But_Click;
            _grid.Children.Add(but);
        }

        private void OneCard(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            var but = new Button() { Content = arr[rnd.Next(37)], Height = 40, Width = 70 };
            _handArea.Children.Add(but);
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            var but = new Button() { Content = "Дама пик", Height = 40, Width = 70 };
            _handArea.Children.Add(but);
            var but1 = new Button() { Content = "Дама треф", Height = 40, Width = 70 };
            _handArea.Children.Add(but1);
            var but2 = new Button() { Content = "Дама пик", Height = 40, Width = 70 };
            _handArea.Children.Add(but2);
            var but3 = new Button() { Content = "Дама пик", Height = 40, Width = 70 };
            _handArea.Children.Add(but3);
        }
    }
}
