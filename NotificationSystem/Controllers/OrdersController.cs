using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Enums;
using NotificationSystem.Services;

namespace NotificationSystem.Controllers;

public class OrdersController(
    OrderService orderService,
    ILogger logger) : Controller
{
    [Produces("application/json")]
    [Consumes("application/json")]
    //[ProducesResponseType(typeof(ApiError), 500)]
    //[ProducesResponseType(typeof(ApiError), 401)]
    //[ProducesResponseType(typeof(ApiError), 403)]
    //[ProducesResponseType(typeof(), 200)]
    [HttpPost("/api/orders")]
    public async Task<IActionResult> SendOrder(
    //[Required][FromBody] string[] goods,
    )
    {
        try
        {
            await orderService.CreateOrderAsync("Milk", new Models.UserPreferences { NotificationChannelTypes = [NotificationChannelTypeEnum.None] });
            return Ok();

            //return BadRequest(result.AsT1);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return Json(ex);
        }
    }
}
