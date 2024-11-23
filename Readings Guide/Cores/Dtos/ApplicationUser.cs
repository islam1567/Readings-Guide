using Microsoft.AspNetCore.Identity;
using Readings_Guide.Cores.Entities;

namespace Readings_Guide.Cores.Dtos
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public FavouriteBookList FavouriteList { get; set; }
        public ToReadingList ToReadList { get; set; }
        public ReadingList ReadList { get; set; }
        public CurrentluBookList CurrentlyReadingList { get; set; }
        public List<Notes> Notes { get; set; }
    }
}
