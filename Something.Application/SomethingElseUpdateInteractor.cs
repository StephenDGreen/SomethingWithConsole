using Something.Domain;
using Something.Persistence;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Something.Application
{
    public class SomethingElseUpdateInteractor : ISomethingElseUpdateInteractor
    {
        private readonly ISomethingFactory somethingFactory;
        private ISomethingElseFactory somethingElseFactory;
        private ISomethingElsePersistence persistence;

        public SomethingElseUpdateInteractor(ISomethingFactory somethingFactory, ISomethingElseFactory somethingElseFactory, ISomethingElsePersistence persistence)
        {
            this.somethingFactory = somethingFactory;
            this.somethingElseFactory = somethingElseFactory;
            this.persistence = persistence;
        }

        public Domain.Models.SomethingElse UpdateSomethingElseAddSomething(int id, string name)
        {
            var something = somethingFactory.Create(name);
            return persistence.UpdateSomethingElseByIdAddSomething(id, something);
        }

        public Domain.Models.SomethingElse UpdateSomethingElseDeleteSomething(int else_id, int something_id)
        {
            return persistence.UpdateSomethingElseByIdDeleteSomethingById(else_id, something_id);
        }

        public async Task<Domain.Models.SomethingElse> UpdateSomethingElseAddSomethingAsync(int id, string name)
        {
            var something = somethingFactory.Create(name);
            return await persistence.UpdateSomethingElseByIdAddSomethingAsync(id, something);
        }

        public async Task<Domain.Models.SomethingElse> UpdateSomethingElseDeleteSomethingAsync(int else_id, int something_id)
        {
            return await persistence.UpdateSomethingElseByIdDeleteSomethingByIdAsync(else_id, something_id);
        }

        public async Task<Domain.Models.SomethingElse> UpdateSomethingElseChangeTagAsync(int else_id, string tag)
        {
            var pattern = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            if (!pattern.IsMatch(tag)) throw new ArgumentException("The tag must be alphanumeric");
            return await persistence.UpdateSomethingElseByIdChangeTagAsync(else_id, tag);
        }

        public Domain.Models.SomethingElse UpdateSomethingElseChangeTag(int else_id, string tag)
        {
            var pattern = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            if (!pattern.IsMatch(tag)) throw new ArgumentException("The tag must be alphanumeric");
            return persistence.UpdateSomethingElseByIdChangeTag(else_id, tag);
        }
    }
}
