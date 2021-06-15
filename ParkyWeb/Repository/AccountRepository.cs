﻿using Newtonsoft.Json;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory _ClientFactory;
        public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<User> LoginAsync(string url, User ObjToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (ObjToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(ObjToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }
            var client = _ClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> RegisterAsync(string url, User ObjToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (ObjToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(ObjToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var client = _ClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
