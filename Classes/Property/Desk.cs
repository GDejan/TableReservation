namespace TableReservation.Property
{
    public class Desk
    {
        public int Id { get; private set; }
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
