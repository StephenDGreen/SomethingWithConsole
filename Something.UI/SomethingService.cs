using Serilog;
using Something.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Something.UI
{
    public class SomethingService : ISomethingService
    {
        private readonly ISomethingCreateInteractor createInteractor;
        private readonly ISomethingReadInteractor readInteractor;
        private readonly ILogger logger;

        public SomethingService(ISomethingCreateInteractor createInteractor, ISomethingReadInteractor readInteractor, ILogger logger)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.logger = logger;
        }

        public async Task Run()
        {
            await createInteractor.CreateSomethingAsync("test");
            List<Domain.Models.Something> somethingList = await readInteractor.GetSomethingListAsync();
            logger.Information(somethingList.Single().Name);
        }
    }
}
