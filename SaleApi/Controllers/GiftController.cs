using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleApi.Models;
using SaleApi.Services;
using static SaleApi.Dto.DonerDto;
using static SaleApi.Dto.GiftDto;

namespace SaleApi.Controllers


{    [Route("api/[controller]")]
        [ApiController]
    public class GiftController: ControllerBase
    {
            private readonly IGiftService _giftService;
            private readonly ILogger<GiftController> _logger;

        public GiftController(IGiftService giftService, ILogger<GiftController>logger)
            {
            _giftService = giftService;
            _logger = logger;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetGiftDto>>> GetAllGift()
        {
            try
            {
                var gifts = await _giftService.GetAllGift();
                return Ok(gifts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            };
        }

        // מתנה חדשה
        [HttpPost]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<Gift>> NewGift([FromForm] CreateGiftDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var created = await _giftService.NewGift(dto);
                if (created == null)
                    return BadRequest("Failed to create gift.");

                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //מחיקת מתנה
        [HttpDelete("{id}")]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> DeletGift(int id)
        {
            try
            {
                await _giftService.DeletGift(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<GetGiftDto>> GetGiftById(int id)
        {
            try
            {
                var gift = await _giftService.GetGiftById(id);
                if (gift == null)
                    return NotFound();
                return Ok(gift);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        //עידכון מתנה
        [HttpPut]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<GiftResponseDto>> UpdateGift([FromForm] UpdateGiftDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updated = await _giftService.UpdateGift(dto);
                if (updated == null)
                    return BadRequest("Failed to update gift.");
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //מי התורם של המתנה
        [HttpGet("{id}/doner")]
        public async Task<ActionResult<GiftDonerDto>> GetDoner(int id)
        {
            var doner = await _giftService.GetGiftDoner(id);
            if (doner == null)
            {
                return NotFound($"Doner for gift ID {id} not found.");
            }
            return Ok(doner);
        }



        // get by doner name

        [HttpGet("doner/")]
        public async Task<ActionResult<IEnumerable<GetGiftDto>>> GetGiftByDoner([FromQuery] string name)
        {
            try
            {
                var gift = await _giftService.GetGiftByDoner(name);
                //if (gift == null)
                //    return NotFound();
                return Ok(gift);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // get by name

        [HttpGet("name/")]
        public async Task<ActionResult<IEnumerable<UpdateGiftDto>>> GetGiftByName([FromQuery] string name)
        {
            try
            {
                var gift = await _giftService.GetGiftByName(name);
                //if (gift == null)
                //    return NotFound();
                return Ok(gift);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
