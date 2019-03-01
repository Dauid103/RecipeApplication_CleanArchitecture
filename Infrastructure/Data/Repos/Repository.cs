using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repos
{
    public class Repository : IRepository 
    {
        protected readonly RecipeContext _context;

        public Repository(RecipeContext context)
        {
            _context = context;
        }
        
        public IEnumerable<T> ListAll<T>() where T : class
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void SaveAll()
        {
            _context.SaveChanges();
        }

    }
}
