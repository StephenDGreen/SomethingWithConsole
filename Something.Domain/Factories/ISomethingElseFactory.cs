using Something.Domain.Models;

namespace Something.Domain
{
    public interface ISomethingElseFactory
    {
        SomethingElse Create(string name);
    }
}