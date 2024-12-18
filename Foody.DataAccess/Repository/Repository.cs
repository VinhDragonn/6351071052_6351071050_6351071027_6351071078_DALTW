﻿
using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository( ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //dbSet.categories == dbset 
            _db.Products.Include(u => u.Category).Include(u => u.Id);

        }
        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }
  
        
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T,bool>>? filter= null, string?  includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }   
            return query.ToList();

        }

        public T Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {

            IQueryable<T> query;
            if (tracked) {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void RemoveRange(IEnumerable<T> filter)
        {
            dbSet.RemoveRange(filter);  
        }

     
    }
}
