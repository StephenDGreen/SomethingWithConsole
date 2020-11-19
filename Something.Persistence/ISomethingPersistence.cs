using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Persistence
{
    public interface ISomethingPersistence
    {
        List<Domain.Models.Something> GetSomethingList();
        Task<List<Domain.Models.Something>> GetSomethingListAsync();
        void SaveSomething(Domain.Models.Something something);
        Task SaveSomethingAsync(Domain.Models.Something something);
    }
}