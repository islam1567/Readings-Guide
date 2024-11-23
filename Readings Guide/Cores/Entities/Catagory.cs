namespace Readings_Guide.Cores.Entities
{
    public class Catagory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descreption { get; set; }
        public List<Book> Books { get; set; }
    }
}
