using System.Windows;

namespace Fool.Wpf
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void GetError(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
