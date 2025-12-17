using Microsoft.Extensions.Hosting;
using TempMailX.Data;

namespace TempMailX.Background
{
    public class EmailExpiryService : BackgroundService
    {
        private readonly TempEmailDAL _dal;

        // DI through constructor
        public EmailExpiryService(TempEmailDAL dal)
        {
            _dal = dal;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _dal.ExpireOldEmails();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
