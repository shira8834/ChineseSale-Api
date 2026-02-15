using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SaleApi.Models;
using SaleApi.Services;
using static SaleApi.Dto.DonerDto;
using static SaleApi.Dto.GiftDto;

namespace SaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonerController : ControllerBase
    {
        private readonly IDonerService  _donerService;
        private readonly ILogger<DonerController> _logger;

        public DonerController(IDonerService donerService, ILogger<DonerController> logger)
        {
            _donerService = donerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UpdateDonerDto>>> GetAllDoner()
        {
            var doners = await _donerService.GetAllDoner();
            return Ok(doners);
        }

        [HttpPost]
        //[Authorize(Roles = "manager")]
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
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //מחיקת תורם
        [HttpDelete("{id}")]
        //[Authorize(Roles = "manager")]
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
        //[Authorize(Roles = "manager")]
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
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //כל התורמים ורשימת המתנות שלהם
        [HttpGet ("withGifts")]
        public async Task<ActionResult<IEnumerable<GetDonerDtoWithGift>>> GetAllDonerWithGift()
        {
            var doners = await _donerService.GetAllDonerWithGift();
            return Ok(doners);
        }


        //get by id with gift
        [HttpGet("withGifts/{id}")]
        public async Task<ActionResult<GetDonerDtoWithGift>> GetDonerByIdWithGigt(int id)
        {
            try
            {
                var doner = await _donerService.GetDonerByIdWithGift(id);
                if (doner == null)
                    return NotFound();
                return Ok(doner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //get by name
        [HttpGet("doner/name")]

        public async Task<ActionResult<Doner>> GetDonerByName([FromQuery] string name)
        {
            try
            {
                var doner = await _donerService.GetDonerByName(name);
                if (doner == null)
                    return NotFound();
                return Ok(doner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //get by name
        [HttpGet("doner/email")]

        public async Task<ActionResult<Doner>> GetDonerByMail([FromQuery] string email)
        {
            try
            {
                var doner = await _donerService.GetDonerByMail(email);
                if (doner == null)
                    return NotFound();
                return Ok(doner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //get by gift name
        [HttpGet("doner/gift")]

        public async Task<ActionResult<Doner>> GetDonerByGift([FromQuery] string giftName)
        {
            try
            {
                var doner = await _donerService.GetDonerByGift(giftName);
                if (doner == null)
                    return NotFound();
                return Ok(doner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
