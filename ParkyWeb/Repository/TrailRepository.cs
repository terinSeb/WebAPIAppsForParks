using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyWeb.Repository.IRepository;
using ParkyWeb.Models;
using System.Net.Http;

namespace ParkyWeb.Repository
{    
    public class TrailRepository : Repository<Trails>, ITrailRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public TrailRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
