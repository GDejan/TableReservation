using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableReservation.Classes.Users;

namespace TableReservation.Helpers
{
    internal class Checks
    {
        public Checks()
        {
        }
        public bool InputCheck(string Input) //check user input for Empty and allowed characters
        {
            if (!string.IsNullOrEmpty(Input))
            {
                foreach (char c in Input)
                {
                    if (!(((((int)c >= 65) && (int)c <= 90) || (((int)c >= 97) && (int)c <= 122)) || (((int)c >= 48) && (int)c <= 57)))
                    {
                        return false;
                        break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
