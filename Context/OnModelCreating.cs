using Microsoft.EntityFrameworkCore;
using VirvisShopFinal.Models;

namespace VirvisShopFinal.Context;
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Relación User ↔ Orders (Restrict Delete)
    modelBuilder.Entity<Order>()
        .HasOne(o => o.User)
        .WithMany(u => u.Orders)
        .HasForeignKey(o => o.UserId)
        .OnDelete(DeleteBehavior.Restrict); // Evitar cascada en la eliminación de usuarios

    // Relación Cart ↔ Orders (Cascade Delete)
    modelBuilder.Entity<Order>()
        .HasOne(o => o.Cart)
        .WithMany(c => c.Orders)
        .HasForeignKey(o => o.CartId)
        .OnDelete(DeleteBehavior.Cascade); // Permitir cascada en la eliminación de carritos

    // Relación User ↔ Carts (Restrict Delete)
    modelBuilder.Entity<Cart>()
        .HasOne(c => c.User)
        .WithMany(u => u.Carts)
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Restrict); // Evitar cascada en la eliminación de usuarios
}
