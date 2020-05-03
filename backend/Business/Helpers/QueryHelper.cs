using System;
using System.Linq;
using System.Linq.Expressions;
using backend.Data;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Helpers
{
    public class QueryHelper<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public QueryHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public  IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();

            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }
    }
}