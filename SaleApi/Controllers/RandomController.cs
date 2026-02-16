using Microsoft.AspNetCore.Mvc;
using SaleApi.Services;

namespace SaleApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RandomController: ControllerBase
    {
        private readonly IRandomService _randomService;

        public RandomController(IRandomService randomService)
        {
           _randomService = randomService;
        }


        [HttpPost("{giftId}")]
        public async Task<IActionResult> RunDraw(int giftId)
        {
            int? winnerId = await _randomService.PickWinner(giftId);

            if (winnerId == null)
            {
                return NotFound("לא נמצאו כרטיסים זכאים להגרלה עבור מוצר זה.");
            }
            return Ok(new { WinnerId = winnerId });
        }
    }
}
