namespace TableReservation.Users
{
    public class User
    {
        public int Id { get; set; }
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

        public User(string name, string surname, string username, string password, bool isadmin, bool istemp)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.IsAdmin = isadmin;
            this.Password=password;
            this.IsTemp = istemp;
        }

        public string FullName() 
        {
            return string.Format(this.Name + " " + this.Surname);
        }
    }
}
