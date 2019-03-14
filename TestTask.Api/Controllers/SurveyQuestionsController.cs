﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.Data;
using TestTask.Data;

namespace TestTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SurveyQuestionsController(MyDbContext context)
        {
            _context = context;
        }

        // Add qustion to list of questions: POST: api/survey/{id}
        [HttpPost("{id}")]
        public async Task PutQuestionAsync(int id, Question question)
        {
            await _context.AddQuestionToServeyAsync(id, question);
        }

        //Get all qustions for a survey: GET: api/SurveyQuestions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsForSurveyAsync(int id)
        {
            var survey = await _context.GetSurveyAsync(id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey.Questions;
        }

        // Remove all questions from questions list in survey: DELETE: api/SurveyQuestions/{id}
        [HttpDelete("{id}")]
        public async Task DeleteQuestionAsync(int id)
        {
            await _context.DeleteAllQuestionsAsync(id);
        }
    }
}