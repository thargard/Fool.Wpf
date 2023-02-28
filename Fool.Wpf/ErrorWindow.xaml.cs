using System.Windows;

namespace Fool.Wpf
{
    public partial class ErrorWindow
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
