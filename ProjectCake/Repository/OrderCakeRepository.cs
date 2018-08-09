using Microsoft.EntityFrameworkCore;
using ProjectCake.Data;
using ProjectCake.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectCake.Repository
{
    public class OrderCakeRepository
    {
        private readonly DbSet<OrderCake> _dbSet;
        private readonly ApplicationDbContext _context;

        public OrderCakeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<OrderCake>();
        }

        public List<OrderCake> GetAll()
        {
            var allOrderCakes = _dbSet.ToList();
            return allOrderCakes;
        }

        public void Add(OrderCake order)
        {
            _dbSet.Add(order);
            _context.SaveChanges();
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}