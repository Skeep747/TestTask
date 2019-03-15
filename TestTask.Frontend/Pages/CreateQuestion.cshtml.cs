using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestTask.Data;

namespace TestTask.Frontend.Pages
{
    public class CreateQuestionModel : PageModel
    {
        private readonly HttpClient _client;

        public CreateQuestionModel(IConfiguration configuration)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value);
        }

        [BindProperty]
        public Question Question { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = new Question();
            Question.SurveyId = (int)id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _client.PostAsJsonAsync("api/Questions", Question);
            if (response.IsSuccessStatusCode)
            {
                var newQuestion = await response.Content.ReadAsAsync<Question>();

                return RedirectToPage("Survey", new { id = newQuestion.SurveyId });
            }

            return RedirectToPage("Index");
        }
    }
}