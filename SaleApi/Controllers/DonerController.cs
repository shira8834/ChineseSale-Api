using Microsoft.AspNetCore.Mvc;
using SaleApi.Models;
using SaleApi.Services;
using static SaleApi.Dto.DonerDto;

namespace SaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonerController : ControllerBase
    {
        private readonly IDonerService  _donerService;

        public DonerController(IDonerService donerService)
        {
            _donerService = donerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doner>>> GetAllDoner()
        {
            var doners = await _donerService.GetAllDoner();
            return Ok(doners);
        }

        [HttpPost]
        public async Task<ActionResult<Doner>> NewDoner([FromBody] CreateDonerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _donerService.NewDoner(dto);
                if (created == null)
                    return BadRequest("Failed to create doner.");

                return Ok(created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //מחיקת תורם
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoner(int id)
        {
            try
            {
                await _donerService.DeleteDoner(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Doner>> GetDonerById(int id)
        {
            try
            {
                var doner = await _donerService.GetDonerById(id);
                if (doner == null)
                    return NotFound();
                return Ok(doner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        //עידכון תורם 
        [HttpPut]
        public async Task<ActionResult<Doner>> UpdateDoner([FromBody] UpdateDonerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updated = await _donerService.UpdateDoner(dto);
                if (updated == null)
                    return BadRequest("Failed to update doner.");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
