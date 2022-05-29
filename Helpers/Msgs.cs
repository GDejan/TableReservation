using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TableReservation.Helpers
{
    public class Msgs
    {
        public string WrongInput = "Wrong input data!";
        public string WrongId = "Wrong input id!";
        public string Wrong = "Something went wrong";
        public string EmptyInput = "Empty input data!";
        public string NoMatch = "Password and Confirm Password doesent match!";
        public string LengthInput = "Input data to loong!";
        public string NotImplemented = "Not Implemented";
        public string Error = "Error!";
        public string Ok = "Ok";
        public string CheckOldValues = "Check old Values and confirm!";
        public string CheckNewValues = "Check new Values and confirm!";
        public string CheckValues = "Check old and new Values and confirm!";

        public string UserCreated = "User Created";
        public string UserChanged = "User Changed";
        public string UserRemoved = "User Removed";
        public string WrongUser = "Wrong username or password!";
        public string UserExist = "Username exist!";
        public string UserDontExist = "User doesent exist!";
        public string UsersDontExist = "No Users in Database!";
        public string UserItSelf = "Not possible to remove itself!";
        public string UserIsMaster = "Not possible to remove/change masteradmin!";

        public string BuildCreated = "Building Created";
        public string BuildChanged = "Building Changed";
        public string BuildRemoved = "Building Removed";
        public string BuildExist = "Building exist!";
        public string BuildDontExist = "Building doesent exist!";
        public string BuildsDontExist = "No Buildings in Database!";

        public string StoreyCreated = "Storey Created";
        public string StoreyChanged = "Storey Changed";
        public string StoreyRemoved = "Storey Removed";
        public string StoreyExist = "Storey exist!";
        public string StoreyDontExist = "Storey doesent exist!";
        public string StoreysDontExist = "No Storeys in Database";

        public string RoomCreated = "Room Created";
        public string RoomChanged = "Room Changed";
        public string RoomRemoved = "Room Removed";
        public string RoomExist = "Room exist!";
        public string RoomDontExist = "Room doesent exist!";
        public string RoomsDontExist = "No Rooms in Database";

        public string DeskCreated = "Desk Created";
        public string DeskChanged = "Desk Changed";
        public string DeskRemoved = "Desk Removed";
        public string DeskExist = "Desk exist!";
        public string DeskDontExist = "Desk doesent exist!";
        public string DesksDontExist = "No Desks in Database!";

        public string ResCreated = "Reservation Created";
        public string ResRemoved = "Reservation Removed";
        public string ResRemove = "Should reservation be removed";
        public string ResExist = "Reservation exist for selected date and table!";
        public string ResNoExist = "Reservation doesent exist!";
        public string ResNotExist = "Reservation doesent exist for selected date and table!";
        public string DoubleRes = "User has reservation for this date!";
        
        /// <summary>
        /// helper class for displaying messages to user
        /// </summary>
        public Msgs()
        {
        }
    }
}
