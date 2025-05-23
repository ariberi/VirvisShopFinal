﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using VirvisShopFinal.Context;
using VirvisShopFinal.Models;

namespace VirvisShopFinal.Controllers
{
    public class UsersController : BaseController
    {
        private readonly VirvisDatabaseContext _context;

        public UsersController(VirvisDatabaseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,lastname,email,password,role")] User user)
        {
            if (user.name != null && user.lastname != null && user.email != null && user.password != null && user.role != null)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: Users/Register
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( Register register)
        {
            if (ModelState.IsValid)
            {

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == register.email);
                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "El email ya está en uso.";
                    return View();
                }
                var newUser = new User
                {
                    id = 0,
                    name = register.name,
                    lastname = register.lastname,
                    email = register.email,
                    password = register.password,
                    role = RoleType.User
                };
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(register);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,lastname,email,password,role")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (user.name != null && user.lastname != null && user.email != null && user.password != null && user.role != null)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.email == user.email && u.password == user.password);
            if (userInDb != null)
            {
                HttpContext.Session.SetString("Username", userInDb.name);
                HttpContext.Session.SetString("Role", userInDb.role.ToString());
                HttpContext.Session.SetString("UserId", userInDb.id.ToString());

                if (userInDb.role == RoleType.Admin)
                {
                    return RedirectToAction("Index", "Products");
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }





    }
}