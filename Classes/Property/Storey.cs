namespace TableReservation.Property
{
    public class Storey
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Storey()
        {
        }
        public Storey(string name)
        {
            this.Name = name;
        }
        public Storey(int id, string name)
            : this(name)
        {
            this.Id = id;
        }
    }
}
