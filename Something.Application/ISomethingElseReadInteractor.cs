using Something.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingElseReadInteractor
    {
        List<SomethingElse> GetSomethingElseIncludingSomethingsList();
        Task<List<SomethingElse>> GetSomethingElseIncludingSomethingsListAsync();
        List<SomethingElse> GetSomethingElseList();
        Task<List<SomethingElse>> GetSomethingElseListAsync();
    }
}