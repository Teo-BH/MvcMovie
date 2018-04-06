using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers {
    
    [Route("api/[controller]")]
    public class HelloWorldController: Controller {

    	private readonly IDistributedCache cache;
 
        public HelloWorldController(IDistributedCache distributedCache)
        {
            this.cache = distributedCache;
        }

        // 
        // GET: /HelloWorld/
        
        [HttpGet]
        public string Index() {
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        [HttpGet("welcome/{id?}")]
        public string Welcome(string name, int ID = 1) {
            if (String.IsNullOrEmpty(name)) {
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                int index = GetValueCache();
                name = $"Index {index}";
            }

            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }

        private int GetValueCache() {

            const string cacheKey = "MvcMovieHelloWorldWelcomeIndex";
            string data = cache.GetString(cacheKey);
            int result = 1;
            if (Int32.TryParse(data, out result)) {
                result += 1;
            }

            cache.SetString(cacheKey, result.ToString());

            return result;
        }
    }        
}