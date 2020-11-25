using ConsoleTables;
using Serilog;
using Something.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Something.UI
{
    public class SomethingService : ISomethingService
    {
        private readonly ISomethingElseCreateInteractor createInteractor;
        private readonly ISomethingElseReadInteractor readInteractor;
        private readonly ISomethingElseUpdateInteractor updateInteractor;
        private readonly ISomethingElseDeleteInteractor deleteInteractor;
        private readonly ILogger logger;

        public SomethingService(ISomethingElseCreateInteractor createInteractor, ISomethingElseReadInteractor readInteractor, ISomethingElseUpdateInteractor updateInteractor, ISomethingElseDeleteInteractor deleteInteractor, ILogger logger)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.updateInteractor = updateInteractor;
            this.deleteInteractor = deleteInteractor;
            this.logger = logger;
        }

        public async Task Run()
        {
            await CreateData();
            List<Domain.Models.SomethingElse> somethingList = await readInteractor.GetSomethingElseIncludingSomethingsListAsync();
            foreach (var item in somethingList)
            {
                Console.WriteLine(@"# {0}", item.Name);
                Console.WriteLine("");
                ConsoleTable
                    .From<Domain.Models.Something>(item.Somethings)
                    .Configure(o => o.NumberAlignment = Alignment.Right)
                    .Write(Format.MarkDown);
            }

            Console.ReadKey();
        }

        private async Task CreateData()
        {
            await createInteractor.CreateSomethingElseAsync("name", new[] { "othername" });
            await createInteractor.CreateSomethingElseAsync("another name", new[] { "this othername", "that othername" });
        }
    }
}
