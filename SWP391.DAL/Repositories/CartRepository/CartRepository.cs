﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391.DAL.Entities;
using SWP391.DAL.Swp391DbContext;

namespace SWP391.DAL.Repositories
{
    public class CartRepository
    {
        private readonly Swp391Context _context;

        public CartRepository(Swp391Context context)
        {
            _context = context;
        }

        public async Task<OrderDetail> GetOrderDetailAsync(int? userId, int productId)
        {
            return await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.UserId == userId && od.ProductId == productId && od.OrderId == null);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetCartDetailsAsync(int? userId)
        {
            return await _context.OrderDetails
                .Where(od => od.UserId == userId && od.OrderId == null)
                .Include(od => od.Product)
                .ToListAsync();
        }
    }
}
