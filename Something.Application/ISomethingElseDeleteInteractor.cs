using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingElseDeleteInteractor
    {
        void DeleteSomethingElse(int id);
        Task DeleteSomethingElseAsync(int id);
    }
}