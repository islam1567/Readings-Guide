using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Interfaces;

namespace Readings_Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<BookDto> service;

        public BookController(IRepository<BookDto> service)
        {
            this.service = service;
        }

        [HttpGet("get-all-book")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("get-book-by-id", Name ="BookRoute")]
        public IActionResult GetById(int id)
        {
            var book = service.GetById(id);
            if (book == null)
            {
                return NotFound("Book not fount");
            }
            return Ok(service.GetById(id));
        }

        [HttpPost("add-book")]
        public IActionResult Add(BookDto dto)
        {
            if(ModelState.IsValid)
            {
                service.Add(dto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            var url = Url.Link("BookRoute", new { id = dto.Id });
            return Created(url, dto);
        }

        [HttpPut("update-book")]
        public IActionResult Update(int id, BookDto dto)
        {
            var book = service.GetById(id);
            if (book == null)
            {
                return NotFound("Book not fount");
            }
            if (ModelState.IsValid)
            {
                service.Update(id, dto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(dto);
        }

        [HttpDelete("delete-book")]
        public IActionResult Delete(int id)
        {
            var book = service.GetById(id);
            if (book == null)
            {
                return NotFound("Book not fount");
            }
            service.Delete(id);
            return Ok();
        }
    }
}
