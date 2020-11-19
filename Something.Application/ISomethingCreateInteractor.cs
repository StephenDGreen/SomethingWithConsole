using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingCreateInteractor
    {
        void CreateSomething();
        void CreateSomething(string name);
        Task CreateSomethingAsync(string name);
        Task CreateSomethingAsync();
    }
}