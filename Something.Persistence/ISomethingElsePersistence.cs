using Something.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Persistence
{
    public interface ISomethingElsePersistence
    {
        void DeleteSomethingElseById(int id);
        Task DeleteSomethingElseByIdAsync(int id);
        List<SomethingElse> GetSomethingElseIncludingSomethingList();
        Task<List<SomethingElse>> GetSomethingElseIncludingSomethingListAsync();
        List<SomethingElse> GetSomethingElseList();
        Task<List<SomethingElse>> GetSomethingElseListAsync();
        void SaveSomethingElse(SomethingElse somethingElse);
        Task SaveSomethingElseAsync(SomethingElse somethingElse);
        SomethingElse UpdateSomethingElseByIdAddSomething(int id, Domain.Models.Something something);
        Task<SomethingElse> UpdateSomethingElseByIdAddSomethingAsync(int id, Domain.Models.Something something);
        SomethingElse UpdateSomethingElseByIdChangeTag(int id, string tag);
        Task<SomethingElse> UpdateSomethingElseByIdChangeTagAsync(int id, string tag);
        SomethingElse UpdateSomethingElseByIdDeleteSomethingById(int else_id, int something_id);
        Task<SomethingElse> UpdateSomethingElseByIdDeleteSomethingByIdAsync(int else_id, int something_id);
    }
}