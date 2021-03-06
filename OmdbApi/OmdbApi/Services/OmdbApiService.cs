﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OmdbApi.Configurations;
using OmdbApi.Interfaces;

namespace OmdbApi.Services
{
    public class OmdbApiService : IOmdbApi
    {
        private readonly IOptions<OmdbApiConfiguration> _omdbApiConfiguration;

        public OmdbApiService(IOptions<OmdbApiConfiguration> omdbApiConfiguration)
        {
            _omdbApiConfiguration = omdbApiConfiguration;
        }
        public async Task<string> GetDataAsync(string query)
        {
            var apiKey = _omdbApiConfiguration.Value.Key;
            var baseUri = _omdbApiConfiguration.Value.Url + apiKey;

            var request = WebRequest.Create(baseUri + query);
            request.Timeout = 10000;
            request.Method = "GET";
            request.ContentType = "application/json";

            var result = string.Empty;
            try
            {
                using var response = await request.GetResponseAsync();
                await using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream!, Encoding.UTF8);
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }
    }
}
