﻿using Microsoft.EntityFrameworkCore;
using Repository.Absract;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }


        public  IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }


        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await  _dbSet.FindAsync(id);
        }



        public async void CreateAsync(TEntity item)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(new DbContextOptions <ApplicationDbContext>() )) // всеравно не работает
            {
                context.Set<TEntity>().Add(item);
                await context.SaveChangesAsync();
            }
        }


        public async void UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await  _context.SaveChangesAsync();
        }


        public async void RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
