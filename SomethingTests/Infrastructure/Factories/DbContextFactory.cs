using Microsoft.EntityFrameworkCore;
using Something.Persistence;
using System.Diagnostics.CodeAnalysis;

namespace SomethingTests.Infrastructure.Factories
{
    [ExcludeFromCodeCoverage]
    public class DbContextFactory
    {
        public AppDbContext CreateAppDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName).Options;

            return new AppDbContext(options);
        }

    }
}
