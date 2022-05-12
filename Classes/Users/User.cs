using System;
using System.Collections.Generic;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation.Classes
{
    public class User
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        List<Desk> Tables = new List<Desk>();
        public User()
        {
        }
        public User(string name, string surname, string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Name = name;   
            this.Surname = surname;
            this.IsAdmin = false;
        }
    }
}
