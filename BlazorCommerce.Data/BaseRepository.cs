using Microsoft.Extensions.Options;

namespace BlazorCommerce.Data
{
    public class BaseRepository
    {
        private readonly DatabaseOptions _options;

        public BaseRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _options = databaseOptions.Value;
        }

        public string ConnectionString
        {
            get 
            {
                return _options.ConnectionString;
            }
        }
    }
}