using Microsoft.AspNetCore.Mvc;
using SaleApi.Controllers;
using SaleApi.Services;
using static SaleApi.Dto.GiftDto;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService; 

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetGiftDto>>> GetAllGift()
    {
        try
        {
            var ord = await _orderService.GetAllOrders();
            return Ok(ord);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }





    //[HttpPost("checkout/{userId}")]
    //public async Task<IActionResult> Checkout(int userId)
    //{
    //    var result = await _orderService.CloseBagToOrder(userId);
    //    if (result)
    //    {
    //        return Ok(new { message = "הרכישה בוצעה בהצלחה, הסל התרוקן!" });
    //    }
    //    return BadRequest(new { message = "הסל ריק או שקרתה שגיאה" });
    //}
}