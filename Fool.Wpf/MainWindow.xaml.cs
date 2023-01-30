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
            var but = new Button() { Content = "Дама пик", Height = 40, Width = 70 };
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
