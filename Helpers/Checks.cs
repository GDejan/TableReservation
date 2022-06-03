using System;
using System.Text.RegularExpressions;
using System.Windows;

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
                    if (!Regex.Match(Input, "^[0-9a-zćčžđšA-ZĆČŽĐŠ][a-zćčžđšA-ZĆČŽĐŠ0-9]*$").Success)
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
        /// <summary>
        /// Helper class for checking user entry data (if its number)
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>true if check performed ok, false if not</returns>
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
        /// <summary>
        /// Helper class for checking user entry data (password)
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>true if check performed ok, false if not</returns>
        public bool InputCheckPass(string Input) 
        {
            if (string.IsNullOrEmpty(Input))
            {
                MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                return false;
               
            }
            else
            {
                return true;
            }
        }
    }
}
