using Microsoft.EntityFrameworkCore;
using Repository.Absract;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class BanEmailRepository : IBanEmailRepository
    {
        ApplicationDbContext _context;
        DbSet<BanEmail> _dbSet;

        public BanEmailRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<BanEmail>();
        }


        public async Task<IEnumerable<BanEmail>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }


        public IEnumerable<BanEmail> Get(Func<BanEmail, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }


        public async Task<BanEmail> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }



        public async void CreateAsync(BanEmail item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }


        public async void UpdateAsync(BanEmail item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async void RemoveAsync(BanEmail item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

