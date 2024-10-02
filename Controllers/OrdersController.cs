using DetalleMaestroAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DetalleMaestroAPI.Controllers;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controlador para gestionar las órdenes.
/// </summary>
[Route("[controller]")]
[ApiController]
public class OrdersController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context) {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las órdenes.
    /// </summary>
    /// <returns>Lista de órdenes.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders() {
        return await _context.Orders.ToListAsync();
    }

    /// <summary>
    /// Obtiene una orden específica por ID.
    /// </summary>
    /// <param name="id">ID de la orden.</param>
    /// <returns>Orden correspondiente al ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id) {
        var order = await _context.Orders.FindAsync(id);

        if (order == null) {
            return NotFound();
        }

        return order;
    }

    /// <summary>
    /// Crea una nueva orden.
    /// </summary>
    /// <param name="order">Orden a crear.</param>
    /// <returns>Resultado de la creación de la orden.</returns>
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order) {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
    }

    /// <summary>
    /// Modifica una orden existente.
    /// </summary>
    /// <param name="order">Orden con los nuevos datos.</param>
    /// <returns>Resultado de la modificación de la orden.</returns>
    [HttpPut]
    public async Task<IActionResult> PutOrder(Order order) {
        if (order.OrderID <= 0) {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!OrderExists(order.OrderID)) {
                return NotFound();
            } else {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Elimina una orden por ID.
    /// </summary>
    /// <param name="id">ID de la orden a eliminar.</param>
    /// <returns>Resultado de la eliminación de la orden.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id) {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(int id) {
        return _context.Orders.Any(e => e.OrderID == id);
    }
}