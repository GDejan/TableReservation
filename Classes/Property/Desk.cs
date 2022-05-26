namespace TableReservation.Classes
{
    public class Desk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Desk()
        {
        }
        public Desk(string name)
        {
            this.Name = name;
        }
        public Desk(int id, string name)
            :this(name)
        {
            this.Id = id;
        }
    }
}
