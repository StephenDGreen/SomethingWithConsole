using Something.Domain.Models;
using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingElseUpdateInteractor
    {
        SomethingElse UpdateSomethingElseAddSomething(int id, string name);
        Task<SomethingElse> UpdateSomethingElseAddSomethingAsync(int id, string name);
        SomethingElse UpdateSomethingElseChangeTag(int else_id, string tag);
        Task<SomethingElse> UpdateSomethingElseChangeTagAsync(int else_id, string tag);
        SomethingElse UpdateSomethingElseDeleteSomething(int else_id, int something_id);
        Task<SomethingElse> UpdateSomethingElseDeleteSomethingAsync(int else_id, int something_id);
    }
}