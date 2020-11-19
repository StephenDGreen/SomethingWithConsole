using Something.Domain;
using Something.Persistence;
using System;
using System.Threading.Tasks;

namespace Something.Application
{
    public class SomethingCreateInteractor : ISomethingCreateInteractor
    {
        private readonly ISomethingFactory factory;
        private readonly ISomethingPersistence persistence;

        public SomethingCreateInteractor(ISomethingFactory factory, ISomethingPersistence persistence)
        {
            this.factory = factory;
            this.persistence = persistence;
        }

        public void CreateSomething()
        {
            var something = factory.Create();
            persistence.SaveSomething(something);
        }

        public void CreateSomething(string name)
        {
            var something = factory.Create(name);
            persistence.SaveSomething(something);
        }

        public async Task CreateSomethingAsync()
        {
            var something = factory.Create();
            await persistence.SaveSomethingAsync(something);
        }

        public async Task CreateSomethingAsync(string name)
        {
            var something = factory.Create(name);
            await persistence.SaveSomethingAsync(something);
        }
    }
}