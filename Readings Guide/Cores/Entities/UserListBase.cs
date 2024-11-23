using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;

namespace Readings_Guide.Cores.Entities
{
    public class UserListBase
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public ApplicationUser AppUser { get; set; }
        public List<Book> Books { get; set; }
    }
}
