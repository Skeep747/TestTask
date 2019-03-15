using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using TestTask.Data;

namespace TestTask.Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;

        public IndexModel(IConfiguration configuration)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value);
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
