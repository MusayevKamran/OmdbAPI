using System.Threading.Tasks;

namespace OmdbApi.Interfaces
{
    public interface IOmdbApi
    {
        Task<string> GetDataAsync(string query);
    }
}
