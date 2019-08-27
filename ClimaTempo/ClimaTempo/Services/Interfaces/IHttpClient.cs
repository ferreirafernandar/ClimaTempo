using System.Threading.Tasks;

namespace ClimaTempo.Services.Interfaces
{
    public interface IHttpClient
    {
        Task<T> ObterBaseHttpClient<T>(string url, string endpoint, params string[] parameters);

        Task<T> AdicionarBaseHttpClient<T>(string url, string endpoint, object model, string key);
    }
}