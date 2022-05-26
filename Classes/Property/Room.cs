namespace TableReservation.Classes
{
    public class Room
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Room()
        {
        }
        public Room(string name)
        {
            this.Name = name;
        }
        public Room(int id, string name)
            : this(name)
        {
            this.Id = id;
        }
    }
}
