using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Api.Data;
using TestTask.Data;

namespace TestTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SurveysController(MyDbContext context)
        {
            _context = context;
        }

        // Create: POST: api/Surveys
        [HttpPost]
        public async Task PostSurveyAsync(Survey survey)
        {
            await _context.AddSurveyAsync(survey);
        }

        //Get all: GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveysAsync()
        {
            return await _context.GetSurveysAsync();
        }

        // Get one: GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurveyAsync(int id)
        {
            var survey = await _context.GetSurveyAsync(id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // Edit: PUT: api/Surveys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyAsync(int id, Survey survey)
        {
            if (id != survey.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.EditSurveyAsync(id, survey);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // Delete: DELETE: api/Surveys/5
        [HttpDelete("{id}")]
        public async Task DeleteSurveyAsync(int id)
        {
            await _context.DeleteSurveyAsync(id);
        }
    }
}
