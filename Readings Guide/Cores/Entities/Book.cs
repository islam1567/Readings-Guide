namespace Readings_Guide.Cores.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Descreption { get; set; }
        public string? Auther { get; set; }
        public DateTime? PublicTime { get; set; }
        public string? ImageUrl { get; set; }
        public int? CatagoryId { get; set; }
        public Catagory catagory { get; set; }
        public List<FavouriteBookList> FavouriteBookLists { get; set; }
        public List<Notes> Notes { get; set; }
        public List<CurrentluBookList> CurrentluBookLists { get; set; }
        public List<ReadingList> ReadingLists { get; set; }
        public List<ToReadingList> ToReadingLists { get; set; }
    }
}
