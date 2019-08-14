using System.Threading.Tasks;

namespace ClimaTempo.Services.Interfaces
{
    public interface IHttpClient
    {
        Task<T> ObterBaseHttpClient<T>(string url, string endpoint, params string[] parameters);
    }
}