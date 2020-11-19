using Something.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Application
{
    public class SomethingReadInteractor : ISomethingReadInteractor
    {
        private readonly ISomethingPersistence persistence;

        public SomethingReadInteractor(ISomethingPersistence persistence)
        {
            this.persistence = persistence;
        }

        public List<Domain.Models.Something> GetSomethingList()
        {
            return persistence.GetSomethingList();
        }

        public async Task<List<Domain.Models.Something>> GetSomethingListAsync()
        {
            return await persistence.GetSomethingListAsync();
        }
    }
}