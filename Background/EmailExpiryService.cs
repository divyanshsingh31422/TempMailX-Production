using Microsoft.Extensions.Hosting;
using TempMailX.Data;

namespace TempMailX.Background
{
    public class EmailExpiryService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailExpiryService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dal = new TempEmailDAL();
                dal.ExpireOldEmails();

                // run every 1 minute
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
