using ClimaTempo.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ClimaTempo.Models.AppCenter;

namespace ClimaTempo.Services
{
    public class HttpClient : IHttpClient
    {
        public async Task<T> ObterBaseHttpClient<T>(string url, string endpoint, params string[] parameters)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));

            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"{url}/{endpoint}{(parameters != null && parameters.Length > 0 ? "?" + string.Join("&", parameters) : string.Empty)}").Result;
                var result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(result);

                throw new WebException(result);
            }
        }

        public async Task<T> AdicionarBaseHttpClient<T>(string url, string endpoint, object model, string key)
        {
            using (var response = new System.Net.Http.HttpClient())
            {
                var request = MontarRequest(url, endpoint, HttpMethod.Post, key, model);

                var ret = response.SendAsync(request, CancellationToken.None).Result;

                var result = ret.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<T>(result);
            }
        }


        //private static T ProcessarRetorno<T>(HttpResponseMessage message)
        //{
        //    var result = message.Content.ReadAsStringAsync().Result;

        //    if (message.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<T>(result);

        //    if (message.StatusCode == HttpStatusCode.InternalServerError)
        //        return //ValidationBase.CreateErrorReturn<T>("01 - Erro interno no servidor de pagamentos.");

        //    var validacao = JsonConvert.DeserializeObject<T>(result);

        //    if (validacao?.Validacao?.Erros != null && validacao.Validacao.Erros.Any())
        //        return validacao;

        //    var erros = JsonConvert.DeserializeObject<Error>(result);

        //    return erros?.ModelState?.Erros != null ? ValidationBase.CreateErrorReturn<T>(erros.ModelState.Erros) : ValidationBase.CreateErrorReturn<T>("01 - Erro interno no servidor de pagamentos.");
        //}

        public HttpRequestMessage MontarRequest(string url, string endpoint, HttpMethod method, string key, object content = null, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url + endpoint),
                Method = method
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Headers.Add("X-API-Token", key);

            if (content != null)
                request.Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter());

            return request;
        }
    }
}