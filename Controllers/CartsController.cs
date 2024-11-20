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
    public class CartsController : Controller
    {
        private readonly VirvisDatabaseContext _context;

        public CartsController(VirvisDatabaseContext context)
        {
            _context = context;
        }

        // GET: Carts
        //public async Task<IActionResult> Index()
        //{
        //    var virvisDatabaseContext = _context.Carts.Include(c => c.User);
        //    return View(await virvisDatabaseContext.ToListAsync());
        //}

        public async Task<IActionResult> Index()
        {
            var userId = 3; // Obtén el UserId del usuario autenticado

            // Obtén el carrito del usuario (solo uno, no todos)
            var cart = await _context.Carts
                .Include(c => c.Items) // Incluye los items del carrito
                .ThenInclude(ci => ci.Product) // Incluye los productos de cada item
                .FirstOrDefaultAsync(c => c.userId == userId); // Filtra por el UserId

            if (cart == null)
            {
                // Si no existe un carrito para el usuario, crea uno nuevo con una lista vacía de items
                cart = new Cart
                {
                    userId = userId,
                    Items = new List<CartItem>()
                };
            }

            return View(cart); // Pasa un solo carrito al modelo de la vista
        }

        private int GetUserId()
        {
            return Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            try
            {
                var userId = 3; // Obtén el UserId del usuario autenticado

                // Verificar si el carrito ya existe para el usuario
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.userId == userId);

                // Si no existe, crear uno nuevo
                if (cart == null)
                {
                    cart = new Cart
                    {
                        userId = userId,
                        Items = new List<CartItem>()
                    };
                    _context.Carts.Add(cart);
                }

                // Buscar si el producto ya está en el carrito
                var existingItem = cart.Items.FirstOrDefault(ci => ci.productId == productId);
                if (existingItem != null)
                {
                    // Si el producto ya está, puedes aumentar la cantidad
                    existingItem.quantity += 1; // Incrementa la cantidad del producto en el carrito
                }
                else
                {
                    // Si no está en el carrito, agregarlo
                    cart.Items.Add(new CartItem
                    {
                        productId = productId,
                        quantity = 1
                    });
                }

                await _context.SaveChangesAsync(); // Guardar cambios en la base de datos

                return Json(new { success = true, message = "Producto agregado al carrito correctamente" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Hubo un problema al agregar el producto al carrito" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
        {
            try
            {
                // Verificar si el carrito existe
                var cartItem = await _context.CartItems
                    .Include(ci => ci.Cart)
                    .FirstOrDefaultAsync(ci => ci.id == cartItemId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "El artículo no se encuentra en el carrito." });
                }

                // Verificar si la cantidad es válida
                if (quantity < 1)
                {
                    return Json(new { success = false, message = "La cantidad debe ser al menos 1." });
                }

                // Actualizar la cantidad
                cartItem.quantity = quantity;

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cantidad actualizada correctamente." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Hubo un problema al actualizar el carrito." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                
                // Verificar si el carrito existe
                var cartItem = await _context.CartItems
                    .Include(ci => ci.Cart)
                    .FirstOrDefaultAsync(ci => ci.id == cartItemId); ;

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "El artículo no se encontró en el carrito." });
                }

                // Eliminar el artículo del carrito
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Producto eliminado del carrito correctamente." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Hubo un problema al eliminar el producto del carrito." });
            }
        }





        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Users, "id", "id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,userId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", cart.userId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", cart.userId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,userId")] Cart cart)
        {
            if (id != cart.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.id))
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
            ViewData["userId"] = new SelectList(_context.Users, "id", "id", cart.userId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.id == id);
        }
    }
}
