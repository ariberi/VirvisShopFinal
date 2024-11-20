using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirvisShopFinal.Context;
using VirvisShopFinal.Models;

namespace VirvisShopFinal.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly VirvisDatabaseContext _context;

        public OrdersController(VirvisDatabaseContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        }

        public IActionResult CardPayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CardPayment([Bind("cardHolderName,cardNumber,expriryDate,cvv,paymentMethod")] Payment model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();

            // Obtener el carrito del usuario
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.userId == userId);

            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Tu carrito está vacío. Agrega productos antes de proceder al pago.";
                return RedirectToAction("Index", "Carts");
            }

            // Crear el pedido
            var order = new Order
            {
                userId = userId,
                OrderDate = DateTime.Now,
                status = OrderStatus.Paid,
                total = cart.Items.Sum(ci => ci.quantity * ci.Product.price),
                Items = cart.Items.Select(ci => new OrderItem
                {
                    productId = ci.productId,
                    quantity = ci.quantity,
                    price = ci.Product.price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Limpiar el carrito
            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Pedido completado exitosamente. ¡Gracias por tu compra!";
            return RedirectToAction("OrderSummary", "Orders", new { orderId = order.id });
        }

        // GET: OrderSummary
        [HttpGet]
        public async Task<IActionResult> OrderSummary(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.id == orderId);

            if (order == null)
            {
                TempData["ErrorMessage"] = "Orden no encontrada.";
                return RedirectToAction("Index", "Carts");
            }

            return View(order);
        }


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var virvisDatabaseContext = _context.Orders.Include(o => o.User);
            return View(await virvisDatabaseContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Users, "id", "id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,userId,total,status,OrderDate,PaymentMethod,PaymentStatus,PaymentDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", order.userId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", order.userId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,userId,total,status,OrderDate,PaymentMethod,PaymentStatus,PaymentDate")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", order.userId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.id == id);
        }
    }
}
