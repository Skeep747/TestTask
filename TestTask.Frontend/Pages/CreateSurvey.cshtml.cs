using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestTask.Data;

namespace TestTask.Frontend.Pages
{
    public class CreateSurveyModel : PageModel
    {
        private readonly HttpClient _client;

        public CreateSurveyModel(IConfiguration configuration)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value)
            };
        }

        [BindProperty]
        public Survey Survey { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _client.PostAsJsonAsync("api/Surveys", Survey);
            if (response.IsSuccessStatusCode)
            {
                var newSurvey = await response.Content.ReadAsAsync<Survey>();

                return RedirectToPage("Survey", new { id = newSurvey.Id });
            }

            return RedirectToPage("Index");
        }
    }
}