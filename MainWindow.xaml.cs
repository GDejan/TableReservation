using System.Windows;
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginPage LoginPage = new LoginPage();
            this.Content = LoginPage;
        }
    }
}

