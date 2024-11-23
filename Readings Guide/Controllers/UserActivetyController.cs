using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Entities;

namespace Readings_Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivetyController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserActivetyController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("add-to-currently-reading")]
        public IActionResult AddCurrentlyBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(clr => clr.CurrentlyReadingList)
                .ThenInclude(e => e.Books)
                .FirstOrDefault(x => x.Id == userid);

            var book = context.Books.FirstOrDefault(x => x.Id == bookid);

            if (book == null)  return NotFound("Book not found");
            if (user == null)  return NotFound("user not found");
            if (user.CurrentlyReadingList.Books.Contains(book))
                return BadRequest("This book already exsists");

            user.CurrentlyReadingList.Books.Add(book);
            context.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPost("add-to-favourite-reading")]
        public IActionResult AddFavouriteBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(f => f.FavouriteList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(x => x.Id == userid);

            var book = context.Books.FirstOrDefault(x => x.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");
            if (user.FavouriteList.Books.Contains(book)) 
                return BadRequest("user not found");

            user.FavouriteList.Books.Add(book);
            context.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPost("add-to-book-reading")]
        public IActionResult AddToReadBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(f => f.ReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(x => x.Id == userid);

            var book = context.Books.FirstOrDefault(x => x.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");
            if (user.ReadList.Books.Contains(book))
                return BadRequest("user not found");

            user.ReadList.Books.Add(book);
            context.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPost("add-to-to read-reading")]
        public IActionResult AddToToReadBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(f => f.ToReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(x => x.Id == userid);

            var book = context.Books.FirstOrDefault(x => x.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");
            if (user.ToReadList.Books.Contains(book))
                return BadRequest("user not found");

            user.ToReadList.Books.Add(book);
            context.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPost("add-note-to-book")]
        public IActionResult AddNote(string userid, Notes note)
        {
            var user = context.Users.Include(n => n.Notes).FirstOrDefault(x => x.Id == userid);
            var book = context.Books.Include(n => n.Notes).FirstOrDefault(x => x.Id == note.BookId);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("User not found");
            if (note == null) return NotFound("Note not found");
            
            user.Notes.Add(note);
            book.Notes.Add(note);
            context.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPost("add-user-to-list")]
        public IActionResult AddUserToList(ApplicationUser user)
        {            
            var newuser = context.Users
                .Include(f => f.FavouriteList)
                .Include(c => c.CurrentlyReadingList)
                .Include(rd => rd.ReadList)
                .Include(trd => trd.ToReadList)
                .FirstOrDefault(x => x.Id == user.Id);

            newuser.FavouriteList = new FavouriteBookList() { AppUserId = user.Id };
            newuser.CurrentlyReadingList = new CurrentluBookList() { AppUserId = user.Id };
            newuser.ReadList = new ReadingList() { AppUserId = user.Id };
            newuser.ToReadList = new ToReadingList() { AppUserId = user.Id };
            context.SaveChangesAsync();
            return Ok("Added Successfully");
        }

        [HttpGet("get-all-notes")]
        public IActionResult GetAllNotes(string userid, int bookid)
        {
            var note = context.Notes.Where(e => e.UserId == userid && e.BookId == bookid)
            .Select(e => new NotesDto
                {
                    Id = e.Id,
                    NoteContent = e.NoteContent,
                    UserId = e.UserId
                 }).ToList();
            return Ok(note);
        }

        [HttpGet("get-favourite-book")]
        public IActionResult GetFavouriteBook(string userid)
        {
            var favourite = context.FavouriteBookLists
                .Include(b => b.Books)
                .FirstOrDefault(e => e.AppUserId == userid);
            return Ok(favourite);
        }

        [HttpGet("get-Currently-book")]
        public IActionResult GetCurrentlyBook(string userid)
        {
            var favourite = context.CurrentluBookLists
                .Include(b => b.Books)
                .FirstOrDefault(e => e.AppUserId == userid);
            return Ok(favourite);
        }

        [HttpGet("get-read-book")]
        public IActionResult GetReadingBook(string userid)
        {
            var favourite = context.ReadingLists
                .Include(b => b.Books)
                .FirstOrDefault(e => e.AppUserId == userid);
            return Ok(favourite);
        }

        [HttpGet("get-to-read-book")]
        public IActionResult GetToReadBook(string userid)
        {
            var favourite = context.ToReadingLists
                .Include(b => b.Books)
                .FirstOrDefault(e => e.AppUserId == userid);
            return Ok(favourite);
        }

        [HttpDelete("remove-currently-book")]
        public IActionResult RemoveCurrentlyBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(c => c.CurrentlyReadingList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(e => e.Id == userid);

            var book = context.Books.FirstOrDefault(b => b.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");

            var IsBookExists = user.CurrentlyReadingList.Books.Any(b => b.Id == bookid);
            if (!IsBookExists) return NotFound("Book does not even exsists");

            user.CurrentlyReadingList.Books.Remove(book);
            context.SaveChanges();
            return Ok("Remove successfully");
        }

        [HttpDelete("remove-favourite-book")]
        public IActionResult RemovefavouriteBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(c => c.FavouriteList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(e => e.Id == userid);

            var book = context.Books.FirstOrDefault(b => b.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");

            var IsBookExists = user.FavouriteList.Books.Any(b => b.Id == bookid);
            if (!IsBookExists) return NotFound("Book does not even exsists");

            user.FavouriteList.Books.Remove(book);
            context.SaveChanges();
            return Ok("Remove successfully");
        }

        [HttpDelete("remove-reading-book")]
        public IActionResult RemoveReadingBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(c => c.ReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(e => e.Id == userid);

            var book = context.Books.FirstOrDefault(b => b.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");

            var IsBookExists = user.ReadList.Books.Any(b => b.Id == bookid);
            if (!IsBookExists) return NotFound("Book does not even exsists");

            user.ReadList.Books.Remove(book);
            context.SaveChanges();
            return Ok("Remove successfully");
        }

        [HttpDelete("remove-to-read-book")]
        public IActionResult RemoveToReadBook(string userid, int bookid)
        {
            var user = context.Users
                .Include(c => c.ToReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefault(e => e.Id == userid);

            var book = context.Books.FirstOrDefault(b => b.Id == bookid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("user not found");

            var IsBookExists = user.ToReadList.Books.Any(b => b.Id == bookid);
            if (!IsBookExists) return NotFound("Book does not even exsists");

            user.ToReadList.Books.Remove(book);
            context.SaveChanges();
            return Ok("Remove successfully");
        }

        [HttpPut("remove-note-book")]
        public IActionResult RemoveNoteBook(string userid,int bookid, int noteid)
        {
            var user = context.Users
                .Include(n => n.Notes)
                .FirstOrDefault (e => e.Id == userid);

            var book = context.Books.Include(n => n.Notes).FirstOrDefault (b => b.Id == bookid);
            var note = context.Notes.FirstOrDefault(n => n.Id == noteid);

            if (book == null) return NotFound("Book not found");
            if (user == null) return NotFound("User not found");
            if (note == null) return NotFound("Note not found");

            book.Notes.Remove(note);
            user.Notes.Remove(note);
            context.SaveChanges();
            return Ok("Remove Successfully");
        }
    }
}
