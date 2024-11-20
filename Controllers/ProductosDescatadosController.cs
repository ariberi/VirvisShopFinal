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
    public class ProductosDescatadosController : Controller
    {
        private readonly VirvisDatabaseContext _context;

        public ProductosDescatadosController(VirvisDatabaseContext context)
        {
            _context = context;
        }

        // GET: ProductosDescatados
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductosDescatados.ToListAsync());
        }

        // GET: ProductosDescatados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosDescatados = await _context.ProductosDescatados
                .FirstOrDefaultAsync(m => m.codDestacado == id);
            if (productosDescatados == null)
            {
                return NotFound();
            }

            return View(productosDescatados);
        }

        // GET: ProductosDescatados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductosDescatados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codDestacado")] ProductosDescatados productosDescatados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosDescatados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productosDescatados);
        }

        // GET: ProductosDescatados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosDescatados = await _context.ProductosDescatados.FindAsync(id);
            if (productosDescatados == null)
            {
                return NotFound();
            }
            return View(productosDescatados);
        }

        // POST: ProductosDescatados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codDestacado")] ProductosDescatados productosDescatados)
        {
            if (id != productosDescatados.codDestacado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosDescatados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosDescatadosExists(productosDescatados.codDestacado))
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
            return View(productosDescatados);
        }

        // GET: ProductosDescatados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosDescatados = await _context.ProductosDescatados
                .FirstOrDefaultAsync(m => m.codDestacado == id);
            if (productosDescatados == null)
            {
                return NotFound();
            }

            return View(productosDescatados);
        }

        // POST: ProductosDescatados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productosDescatados = await _context.ProductosDescatados.FindAsync(id);
            if (productosDescatados != null)
            {
                _context.ProductosDescatados.Remove(productosDescatados);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosDescatadosExists(int id)
        {
            return _context.ProductosDescatados.Any(e => e.codDestacado == id);
        }
    }
}
