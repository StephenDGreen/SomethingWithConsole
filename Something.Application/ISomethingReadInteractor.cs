using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingReadInteractor
    {
        List<Domain.Models.Something> GetSomethingList();
        Task<List<Domain.Models.Something>> GetSomethingListAsync();
    }
}