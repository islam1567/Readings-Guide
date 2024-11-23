using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;

namespace Readings_Guide.Cores.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public string? NoteContent { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
