using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using TableReservation.Helpers;
using TableReservation.Views;
using System.Configuration;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings settings = new Settings();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        public object ClientScript { get; private set; }

        private void MailTo(object sender, RequestNavigateEventArgs e)
        {
            string emailAdd = ConfigurationManager.AppSettings["E-Mail"];
            Process process = new Process();
            process.StartInfo.FileName = emailAdd;
            process.StartInfo.UseShellExecute = true;            
            process.Start();
        }
    }
}

