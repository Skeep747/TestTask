using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestTask.Data;

namespace TestTask.Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;

        public IndexModel(IConfiguration configuration)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value)
            };
        }

        public List<Survey> Surveys { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _client.GetAsync("api/Surveys");
            if (response.IsSuccessStatusCode)
            {
                Surveys = await response.Content.ReadAsAsync<List<Survey>>();
            }
        }
    }
}
