using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleApi.Controllers;
using SaleApi.Models;
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

    // שליפת כל ההזמנות
    [HttpGet]
    //[Authorize(Roles = "Admin")]

    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
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

    // שליפת היסטוריה לפי משתמש
    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        try
        {
            var history = await _orderService.GetUserHistoryAsync(userId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // 1. צפייה ברכישות עבור מתנה ספציפית
    [HttpGet("by-gift/{giftId}")]
    public async Task<IActionResult> GetOrdersByGiftId(int giftId)
    {
        try
        {
            var orders = await _orderService.GetOrdersByGiftId(giftId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // 2. מיון לפי המתנה הנרכשת ביותר (פופולריות)
    [HttpGet("sort/popularity")]
    public async Task<ActionResult<IEnumerable<GetGiftDto>>> GetOrdersSortedByPopularity()
    {
        try
        {
            // הקריאה לשירות שעכשיו מחזיר רשימת מתנות ייחודית
            var popularGifts = await _orderService.GetOrdersSortedByPopularity();
            return Ok(popularGifts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // 3. מיון לפי המתנה היקרה ביותר
    [HttpGet("sort/price")]
    public async Task<ActionResult<IEnumerable<GetGiftDto>>> GetOrdersSortedByPrice()
    {
        try
        {
            var gifts = await _orderService.GetOrdersSortedByPrice();
            return Ok(gifts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
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
