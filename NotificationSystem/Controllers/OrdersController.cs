using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Commands;

namespace NotificationSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    ICommandHandler<CreateOrderCommand, CreateOrderResult> commandHandler,
    ILogger<OrdersController> logger) : Controller
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken ct = default)
    {
        try
        {
            var result = await commandHandler.HandleAsync(command, ct);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating order");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    //[HttpGet("{id}")]
    //public IActionResult GetOrder(int id)
    //{
    //    var order = orderRepository.GetById(id);
    //    if (order == null)
    //        return NotFound();
    //    return Ok(order);
    //}
}

