﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ClimaTempo.Models.AppCenter;
using ClimaTempo.Services.Interfaces;
using Newtonsoft.Json;

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


        //public HttpRequestMessage AdicionarBaseHttpClient<T>(string url, string endpoint, object model, string key)
        //{
        //    var request = new HttpRequestMessage
        //    {
        //        RequestUri = new Uri($"{url}{endpoint}"),
        //        Method = HttpMethod.Post,
        //    };
        //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    request.Headers.Add("X-API-Token", $"{key}");

        //    if (model != null)
        //        request.Content = new ObjectContent(model.GetType(), model, new JsonMediaTypeFormatter());
        //    return request;
        //}

        public async Task<Uri> AdicionarBaseHttpClient<T>(string url, string endpoint, object model, string key)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = await client.PostAsJsonAsync(
                    $"{url}{endpoint}", model);
                response.EnsureSuccessStatusCode();

                response.Headers.Add("X-API-Token", $"{key}");


                // return URI of the created resource.
                return response.Headers.Location;
            }
        }
    }
}