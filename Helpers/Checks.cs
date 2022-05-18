using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TableReservation.Classes.Users;

namespace TableReservation.Helpers
{
    internal class Checks
    {
        private Msgs Msgs = new Msgs();
        public Checks()
        {
        }
        public bool InputCheck(string Input) //check user input for Empty and allowed characters
        {
            if (!string.IsNullOrEmpty(Input))
            {
                if (Input.Length < 50)
                {
                    //if (!Regex.Match(Input, "^[a-zA-Z0-9][a-zA-Z0-9]*$").Success)
                    if (!Regex.Match(Input, "^[0-9a-zA-Z][a-zA-Z0-9]*$").Success)
                    {
                        MessageBox.Show(Msgs.WrongInput + " -> " + Input.ToString(), Msgs.Error, MessageBoxButton.OK);
                        return false;
                    }
                    return true;
                }
                else 
                {
                    MessageBox.Show(Msgs.LengthInput + " -> " + Input.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(Msgs.EmptyInput + Input.ToString(), Msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }
    }
}
