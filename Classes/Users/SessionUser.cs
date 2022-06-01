namespace TableReservation.Users
{
    public class SessionUser
    {
        public User User { get; set; }
        public bool IsActiv { get; set; }
        public SessionUser()
        {

        }
        public SessionUser(User user)
        {
            this.User = user;
            this.IsActiv = true;
        }
    }
}
