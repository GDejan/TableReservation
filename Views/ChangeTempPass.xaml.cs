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
using System.Windows.Shapes;
using TableReservation.Classes;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for ChangeTempPass.xaml
    /// </summary>
    public partial class ChangeTempPass : Window
    {
        public SessionUser SessionUser = new SessionUser();

        private Msgs msgs = new Msgs();
        private UserMng userMng = new UserMng();
        private User user = new User();

        public ChangeTempPass(SessionUser SessionUser)
        {
            InitializeComponent();
            this.SessionUser = SessionUser;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrEmpty(Password.Password)) && (!string.IsNullOrEmpty(ConfirmPass.Password)))
            {
                if (Password.Password == ConfirmPass.Password)
                {
                    PassHash passHash = new PassHash(Password.Password);
                    if (userMng.ChangeUser(new User(SessionUser.User.Id, SessionUser.User.Name, SessionUser.User.Surname, SessionUser.User.Username, passHash.HashedPassword, false, false), SessionUser.User) == true)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(msgs.NoMatch, msgs.Error, MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
