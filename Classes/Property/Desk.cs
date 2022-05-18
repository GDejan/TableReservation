namespace TableReservation.Classes
{
    internal class Desk
    {
        public int Id { get; }
        public string Name { get; set; }
        public User User { get; set; }
        public Desk()
        {
        }
        public Desk(string name)
        {
            this.Name = name;
        }
    }
}
