using TableReservation.Helpers;

namespace TableReservation.Classes
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTemp { get; set; }
        public User()
        {
        }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public User(string name, string surname, string username, string password)
            : this(username, password)
        {
            this.Name = name;
            this.Surname = surname;
            this.IsAdmin = false;
        }
        public User(string name, string surname, string username, bool isadmin)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.IsAdmin = isadmin;
        }

        public User(int id, string name, string surname, string username, bool isadmin)
            :this(name, surname, username, isadmin)
        { 
            this.Id = id;
        }
        public User(string name, string surname, string username, string password, bool isadmin, bool istemp)
            : this(name, surname, username, isadmin)
        {
            this.Password=password;
            this.IsTemp = istemp;
        }
        public User(int id,string name, string surname, string username, string password, bool isadmin, bool istemp)
            : this(name, surname, username, password, istemp,istemp)
        {
            this.Id=id;
        }
    }
}
