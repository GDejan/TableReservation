namespace TableReservation.Classes
{
    public class Building
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Building()
        {
        }
        public Building(string name)
        {
            this.Name = name;
        }
        public Building(int id, string name)
            :this(name)
        {
            this.Id = id;
        }
    }
}
