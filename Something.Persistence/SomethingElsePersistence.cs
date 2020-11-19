using Microsoft.EntityFrameworkCore;
using Something.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Something.Persistence
{
    public class SomethingElsePersistence : ISomethingElsePersistence
    {
        private AppDbContext ctx;

        public SomethingElsePersistence(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void SaveSomethingElse(SomethingElse somethingElse)
        {
            ctx.SomethingElses.Add(somethingElse);
            ctx.SaveChanges();
        }

        public List<SomethingElse> GetSomethingElseList()
        {
            return ctx.SomethingElses.ToList();
        }

        public List<SomethingElse> GetSomethingElseIncludingSomethingList()
        {
            return ctx.SomethingElses.Include(s => s.Somethings).ToList();
        }

        public SomethingElse UpdateSomethingElseByIdAddSomething(int id, Domain.Models.Something something)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("SomethingElse does not exist");
            somethingElse.Somethings.Add(something);
            ctx.Update(somethingElse);
            ctx.SaveChanges();
            return somethingElse;
        }

        public SomethingElse UpdateSomethingElseByIdDeleteSomethingById(int else_id, int something_id)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == else_id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            var something = somethingElse.Somethings.Where(s => s.Id == something_id).FirstOrDefault();
            if (something == null) throw new InvalidOperationException("This Something does not exist");
            ctx.Somethings.Remove(something);
            ctx.SaveChanges();
            return somethingElse;
        }

        public void DeleteSomethingElseById(int id)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            ctx.Remove(somethingElse);
            ctx.SaveChanges();
        }

        public async Task SaveSomethingElseAsync(SomethingElse somethingElse)
        {
            ctx.SomethingElses.Add(somethingElse);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<SomethingElse>> GetSomethingElseListAsync()
        {
            return await ctx.SomethingElses.ToListAsync();
        }

        public async Task<List<SomethingElse>> GetSomethingElseIncludingSomethingListAsync()
        {
            return await ctx.SomethingElses.Include(s => s.Somethings).ToListAsync();
        }

        public async Task<SomethingElse> UpdateSomethingElseByIdAddSomethingAsync(int id, Domain.Models.Something something)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("SomethingElse does not exist");
            somethingElse.Somethings.Add(something);
            ctx.Update(somethingElse);
            await ctx.SaveChangesAsync();
            return somethingElse;
        }

        public async Task<SomethingElse> UpdateSomethingElseByIdDeleteSomethingByIdAsync(int else_id, int something_id)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == else_id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            var something = somethingElse.Somethings.Where(s => s.Id == something_id).FirstOrDefault();
            if (something == null) throw new InvalidOperationException("This Something does not exist");
            ctx.Somethings.Remove(something);
            await ctx.SaveChangesAsync();
            return somethingElse;
        }

        public async Task DeleteSomethingElseByIdAsync(int id)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            ctx.Remove(somethingElse);
            await ctx.SaveChangesAsync();
        }

        public SomethingElse UpdateSomethingElseByIdChangeTag(int id, string tag)
        {
            var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            somethingElse.Tag = tag;
            ctx.SaveChanges();
            return somethingElse;
        }

        public async Task<SomethingElse> UpdateSomethingElseByIdChangeTagAsync(int id, string tag)
        {
            SomethingElse somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
            if (somethingElse == null) throw new InvalidOperationException("This SomethingElse does not exist");
            somethingElse.Tag = tag;
            await ctx.SaveChangesAsync();
            return somethingElse;
        }
    }
}
