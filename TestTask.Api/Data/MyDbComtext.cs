﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Data;

namespace TestTask.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }


        public void Initialize()
        {
            Surveys.AddRange(GetSeedingSurveys());
            SaveChanges();
        }

        public static List<Survey> GetSeedingSurveys()
        {
            var lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var questionText = "Had denoting properly jointure you occasion directly raillery. In said to of poor full be post face snug. Introduced imprudence see say unpleasing devonshire acceptance son. Exeter longer wisdom gay nor design age. Am weather to entered norland no in showing service. Nor repeated speaking shy appetite. Excited it hastily an pasture it observe. Snug hand how dare here too. ";

            var questions = new List<Question>
            {
                new Question{ Title = "Question 1", Text = questionText, Oprion1 = "Yes", Oprion2 = "No", Oprion3 = "Don't know" },
                new Question{ Title = "Question 2", Text = questionText, Oprion1 = "Yes", Oprion2 = "No", Oprion3 = "Don't know", Comment = "*Small comment*" }
            };

            return new List<Survey>()
            {
                new Survey(){ Title = "Survey 1", Date = DateTime.Now, Description = lorem, CreatorName = "Marcus Tullius Cicero", Questions = questions }
            };
        }

        //Create survey
        public async Task<Survey> AddSurveyAsync(Survey survey)
        {
            await Surveys.AddAsync(survey);
            await SaveChangesAsync();

            return survey;
        }

        //Get all surveys
        public async Task<List<Survey>> GetSurveysAsync()
        {
            return await Surveys
                .OrderBy(s => s.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        //Get one survey
        public async Task<Survey> GetSurveyAsync(int id)
        {
            return await Surveys.FindAsync(id);
        }

        //Edit survey
        public async Task EditSurveyAsync(int id, Survey survey)
        {
            Entry(survey).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        //Delete survey
        public async Task DeleteSurveyAsync(int id)
        {
            var survey = await Surveys.FindAsync(id);

            if (survey != null)
            {
                Surveys.Remove(survey);
                await SaveChangesAsync();
            }
        }

        //Create question
        public async Task<Question> AddQuestionAsync(Question question)
        {
            await Questions.AddAsync(question);
            await SaveChangesAsync();
            return question;
        }

        //Get all questions
        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await Questions
                .OrderBy(q => q.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        //Get one question
        public async Task<Question> GetQuestionAsync(int id)
        {
            return await Questions.FindAsync(id);
        }

        //Edit question
        public async Task EditQuestionAsync(int id, Question question)
        {
            Entry(question).State = EntityState.Modified;

            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        //Delete question
        public async Task DeleteQuestionAsync(int id)
        {
            var question = await Questions.FindAsync(id);

            if (question != null)
            {
                Questions.Remove(question);
                await SaveChangesAsync();
            }
        }

        //Add question to list of questions
        public async Task AddQuestionToServeyAsync(int id, Question question)
        {
            var survey = await Surveys.FindAsync(id);

            if (survey != null)
            {
                survey.Questions.Add(question);
                await SaveChangesAsync();
            }
        }

        //Delete all qustions from qustions list in survey
        public async Task DeleteAllQuestionsAsync(int id)
        {
            var survey = await Surveys.FindAsync(id);

            if (survey != null)
            {
                survey.Questions.Clear();
                await SaveChangesAsync();
            }
        }
    }
}
