using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Interfaces;

namespace Readings_Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly IRepository<CatagoryDto> service;

        public CatagoryController(IRepository<CatagoryDto> service)
        {
            this.service = service;
        }

        [HttpGet("get-all-catagory")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("get-catagory-by-id", Name = "CatagoryRoute")]
        public IActionResult GetById(int id)
        {
            var catagory = service.GetById(id);
            if (catagory == null)
            {
                return NotFound("Catagory not fount");
            }
            return Ok(service.GetById(id));
        }

        [HttpPost("add-catagory")]
        public IActionResult Add(CatagoryDto dto)
        {
            if (ModelState.IsValid)
            {
                service.Add(dto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            var url = Url.Link("CatagoryRout", new { id = dto.Id });
            return Created(url, dto);
        }

        [HttpPut("update-catagory")]
        public IActionResult Update(int id, CatagoryDto dto)
        {
            var book = service.GetById(id);
            if (book == null)
            {
                return NotFound("Catagory not fount");
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

        [HttpDelete("delete-catagory")]
        public IActionResult Delete(int id)
        {
            var book = service.GetById(id);
            if (book == null)
            {
                return NotFound("Catagory not fount");
            }
            service.Delete(id);
            return Ok();
        }
    }
}
