using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Something.Persistence
{
    public class SomethingPersistence : ISomethingPersistence
    {
        private AppDbContext ctx;

        public SomethingPersistence(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void SaveSomething(Domain.Models.Something something)
        {
            ctx.Somethings.Add(something);
            ctx.SaveChanges();
        }

        public List<Domain.Models.Something> GetSomethingList()
        {
            return ctx.Somethings.ToList();
        }

        public async Task SaveSomethingAsync(Domain.Models.Something something)
        {
            ctx.Somethings.Add(something);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Domain.Models.Something>> GetSomethingListAsync()
        {
            return await ctx.Somethings.ToListAsync();
        }
    }
}