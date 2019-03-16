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
    public class QuestionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public QuestionsController(MyDbContext context)
        {
            _context = context;
        }

        // Create: POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestionAsync(Question question)
        {
            var newQuestion = await _context.AddQuestionAsync(question);

            return newQuestion;
        }

        //Get all: GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsAsync()
        {
            return await _context.GetQuestionsAsync();
        }

        // Get one: GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionAsync(int id)
        {
            var question = await _context.GetQuestionAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // Edit: PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAsync(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.EditQuestionAsync(id, question);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // Delete: DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestionAsync(int id)
        {
            await _context.DeleteQuestionAsync(id);

            return Ok();
        }
    }
}