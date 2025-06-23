using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _repository.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _repository.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            var createdOrder = await _repository.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("update-order/{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest();

            var updatedOrder = await _repository.UpdateOrder(order);
            return Ok(updatedOrder);
        }

        [HttpDelete("delete-order{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _repository.DeleteOrder(id);
            if (!result)
                return NotFound();
            return NoContent();
        }


        [HttpGet("by-product/{id}")]
        public async Task<ActionResult<Order>> GetOrderByProduct(int id)
        {
            var orders = await _repository.GetOrderByProductId(id);

            if(orders == null)
                return NotFound();

            return Ok(orders);
        }
    }
}
