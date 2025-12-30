using Microsoft.AspNetCore.Mvc;
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

            public GiftController(IGiftService giftService)
            {
            _giftService = giftService;
            }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gift>>> GetAllGift()
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
            }
            ;
        }

        // מתנה חדשה
        [HttpPost]
        public async Task<ActionResult<Gift>> NewGift([FromBody] CreateGiftDto dto)
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //מחיקת תורם
        [HttpDelete("{id}")]
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
        public async Task<ActionResult<Gift>> GetGiftById(int id)
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
        public async Task<ActionResult<Gift>> UpdateGift([FromBody] UpdateGiftDto dto)
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
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
    }

}
