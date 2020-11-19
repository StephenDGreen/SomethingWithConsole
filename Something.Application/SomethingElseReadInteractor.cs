using Something.Domain.Models;
using Something.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Something.Application
{
    public class SomethingElseReadInteractor : ISomethingElseReadInteractor
    {
        private readonly ISomethingElsePersistence persistence;

        public SomethingElseReadInteractor(ISomethingElsePersistence persistence)
        {
            this.persistence = persistence;
        }

        public List<SomethingElse> GetSomethingElseList()
        {
            return persistence.GetSomethingElseList();
        }

        public List<SomethingElse> GetSomethingElseIncludingSomethingsList()
        {
            return persistence.GetSomethingElseIncludingSomethingList();
        }

        public async Task<List<SomethingElse>> GetSomethingElseListAsync()
        {
            return await persistence.GetSomethingElseListAsync();
        }

        public async Task<List<SomethingElse>> GetSomethingElseIncludingSomethingsListAsync()
        {
            return await persistence.GetSomethingElseIncludingSomethingListAsync();
        }
    }
}
