﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Foody.DataAccess.Repository.IRepository
{
 
        public interface IRepository<T> where T: class
        {
            IEnumerable<T> GetAll(string? includeProperties = null);
            T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
            void Add(T entity);
        
            void RemoveRange(IEnumerable<T> entity);
            public void Remove(T entity);
     
    }

}
