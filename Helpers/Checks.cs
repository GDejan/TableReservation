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
        private Msgs msgs = new Msgs();
        /// <summary>
        /// Helper class for checking user entry data
        /// </summary>
        public Checks()
        {
        }

        /// <summary>
        /// Helper class for checking user entry data (string like)
        /// </summary>
        /// <param name="Input">user entry data</param>
        /// <returns>true if check performed ok, false if not</returns>
        public bool InputCheck(string Input) //check input data for Empty and allowed characters
        {
            if (!string.IsNullOrEmpty(Input))
            {
                if (Input.Length < 50)
                {
                    if (!Regex.Match(Input, "^[0-9a-zA-Z][a-zA-Z0-9]*$").Success)
                    {
                        MessageBox.Show(msgs.WrongInput + " -> " + Input.ToString(), msgs.Error, MessageBoxButton.OK);
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show(msgs.LengthInput + " -> " + Input.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Helper class for checking user entry data (int like)
        /// </summary>
        /// <param name="Input">user entry data</param>
        /// <returns>true if check performed ok, false if not</returns>
        public bool InputCheckStringIntId(string Input) 
        {
            if (!string.IsNullOrEmpty(Input))
            {
                try
                {
                    int ID = Int32.Parse(Input);
                    if (ID > 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(msgs.WrongId + "->" + Input.ToString(), msgs.Error, MessageBoxButton.OK);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show(msgs.WrongInput + "->" + Input.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                return false;
            }
        } 
        
        public bool InputCheckStringInt(string Input) 
        {
            if (!string.IsNullOrEmpty(Input))
            {
                try
                {
                    int input = Int32.Parse(Input);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.WrongInput + "->" + Input.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }
    }
}
