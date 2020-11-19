using Something.Domain;
using Something.Persistence;
using System;
using System.Threading.Tasks;

namespace Something.Application
{
    public class SomethingElseCreateInteractor : ISomethingElseCreateInteractor
    {
        private readonly ISomethingFactory somethingFactory;
        private ISomethingElseFactory somethingElseFactory;
        private ISomethingElsePersistence persistence;

        public SomethingElseCreateInteractor(ISomethingFactory somethingFactory, ISomethingElseFactory somethingElseFactory, ISomethingElsePersistence persistence)
        {
            this.somethingFactory = somethingFactory;
            this.somethingElseFactory = somethingElseFactory;
            this.persistence = persistence;
        }

        public void CreateSomethingElse(string name)
        {
            var somethingElse = somethingElseFactory.Create(name);
            persistence.SaveSomethingElse(somethingElse);
        }

        public void CreateSomethingElse(string name, string[] othernames)
        {
            var somethingElse = somethingElseFactory.Create(name);
            foreach (var nm in othernames)
            {
                var something = somethingFactory.Create(nm);
                somethingElse.Somethings.Add(something);
            }
            persistence.SaveSomethingElse(somethingElse);
        }

        public async Task CreateSomethingElseAsync(string name)
        {
            var somethingElse = somethingElseFactory.Create(name);
            await persistence.SaveSomethingElseAsync(somethingElse);
        }

        public async Task CreateSomethingElseAsync(string name, string[] othernames)
        {
            var somethingElse = somethingElseFactory.Create(name);
            foreach (var nm in othernames)
            {
                var something = somethingFactory.Create(nm);
                somethingElse.Somethings.Add(something);
            }
            await persistence.SaveSomethingElseAsync(somethingElse);
        }
    }
}
