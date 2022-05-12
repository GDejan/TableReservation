using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.Helpers
{
    static class EnumMsgs
    {
        static public string WrongUser = "Wrong username or password!";
        static public string UserExist = "Username exist!";
        static public string UserDontExist = "Username doesent exist!";
        static public string WrongInput = "Wrong input data!";
        static public string UserCreated = "User Created";
        static public string UserChanged = "User Changed";
        static public string UserRemoved = "User Removed";
        static public string NotImplemented = "Not Implemented";
        static public string Error = "Error!";
        static public string Ok = "Ok";
        static public string CheckOldValues = "Check old User and confirm!";
        static public string CheckNewValues = "Check new User and confirm!";

        enum Messages 
        {
            
        }

    }
}
