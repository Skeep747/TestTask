using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestTask.Data;

namespace TestTask.Frontend.Pages
{
    public class SurveyModel : PageModel
    {
        private readonly HttpClient _client;

        public SurveyModel(IConfiguration configuration)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value)
            };
        }

        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"api/Surveys/{id}");
            if (response.IsSuccessStatusCode)
            {
                Survey = await response.Content.ReadAsAsync<Survey>();
            }

            return Page();
        }
    }
}