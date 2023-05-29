using Lec11.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Lec11.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private BookAlertConfigModel _configration;
        public MessageRepository(IOptionsMonitor<BookAlertConfigModel> configration)
        {
            _configration = configration.CurrentValue;
            configration.OnChange(config =>
            {
                _configration = config;

            });
        }
        public string GetName()
        {
            return _configration.NewBookName;
        }
    }
}
