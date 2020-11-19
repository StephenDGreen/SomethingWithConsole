using System.Threading.Tasks;

namespace Something.Application
{
    public interface ISomethingElseCreateInteractor
    {
        void CreateSomethingElse(string name);
        void CreateSomethingElse(string name, string[] othernames);
        Task CreateSomethingElseAsync(string name);
        Task CreateSomethingElseAsync(string name, string[] othernames);
    }
}