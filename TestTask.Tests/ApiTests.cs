using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Api.Controllers;
using TestTask.Api.Data;
using TestTask.Data;
using Xunit;

namespace TestTask.Test
{
    public class ApiTests
    {
        private readonly Mock<MyDbContext> _mockMyDbContext;

        public ApiTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>().UseInMemoryDatabase("InMemoryDb");
            _mockMyDbContext = new Mock<MyDbContext>(optionsBuilder.Options);
        }

        [Fact]
        public async Task PostSurveyAsync_AddNewSurvey()
        {
            var survey = new Survey();
            var returnSurvey = new Survey { Id = survey.Id++ };

            _mockMyDbContext.Setup(
                db => db.AddSurveyAsync(survey)).Returns(Task.FromResult(returnSurvey));

            var surveysController = new SurveysController(_mockMyDbContext.Object);

            var response = await surveysController.PostSurveyAsync(survey);

            Assert.NotEqual(survey.Id, response.Value.Id);
        }

        [Fact]
        public async Task GetSurveysAsync_ReturnAListOfSurves()
        {
            var expectedSurveys = MyDbContext.GetSeedingSurveys();

            _mockMyDbContext.Setup(
                db => db.GetSurveysAsync()).Returns(Task.FromResult(expectedSurveys));

            var surveysController = new SurveysController(_mockMyDbContext.Object);

            var result = await surveysController.GetSurveysAsync();

            var actualSurveys = Assert.IsAssignableFrom<ActionResult<IEnumerable<Survey>>>(result).Value;
            Assert.Equal(
                expectedSurveys.OrderBy(s => s.Id).Select(s => s.Title),
                actualSurveys.OrderBy(s => s.Id).Select(s => s.Title));
        }

        [Fact]
        public async Task GetSurveyAsync_ReturnOneSurve()
        {
            var id = 5;
            var survey = new Survey { Id = id };

            _mockMyDbContext.Setup(
                db => db.GetSurveyAsync(id)).Returns(Task.FromResult(survey));

            var surveysController = new SurveysController(_mockMyDbContext.Object);

            var returnedSurvey = (await surveysController.GetSurveyAsync(id)).Value;

            Assert.True(returnedSurvey.Id == id);
        }

        [Fact]
        public async Task EditSurveyAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.EditSurveyAsync(id, new Survey())).Returns(Task.CompletedTask);

            var surveysController = new SurveysController(_mockMyDbContext.Object);

            var result = await surveysController.PutSurveyAsync(id, new Survey { Id = id });

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task DeleteSurveyAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.DeleteSurveyAsync(id)).Returns(Task.CompletedTask);

            var surveysController = new SurveysController(_mockMyDbContext.Object);

            var result = await surveysController.DeleteSurveyAsync(id);

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task PostQuestionAsync_AddNewQuestion()
        {
            var question = new Question();
            var returnQuestion = new Question { Id = question.Id++ };

            _mockMyDbContext.Setup(
                db => db.AddQuestionAsync(question)).Returns(Task.FromResult(returnQuestion));

            var questionsController = new QuestionsController(_mockMyDbContext.Object);

            var response = await questionsController.PostQuestionAsync(question);

            Assert.NotEqual(question.Id, response.Value.Id);
        }

        [Fact]
        public async Task GetQuestionsAsync_ReturnAListOfQuestions()
        {
            var expectedQuestions = MyDbContext.GetSeedingQuestions();

            _mockMyDbContext.Setup(
                db => db.GetQuestionsAsync()).Returns(Task.FromResult(expectedQuestions));

            var questionsController = new QuestionsController(_mockMyDbContext.Object);

            var result = await questionsController.GetQuestionsAsync();

            var actualSurveys = Assert.IsAssignableFrom<ActionResult<IEnumerable<Question>>>(result).Value;
            Assert.Equal(
                expectedQuestions.OrderBy(s => s.Id).Select(s => s.Title),
                actualSurveys.OrderBy(s => s.Id).Select(s => s.Title));
        }

        [Fact]
        public async Task GetQuestionAsync_ReturnOneQuestion()
        {
            var id = 5;
            var question = new Question { Id = id };

            _mockMyDbContext.Setup(
                db => db.GetQuestionAsync(id)).Returns(Task.FromResult(question));

            var questionsController = new QuestionsController(_mockMyDbContext.Object);

            var returnedQuestion = (await questionsController.GetQuestionAsync(id)).Value;

            Assert.True(returnedQuestion.Id == id);
        }

        [Fact]
        public async Task EditQuestionAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.EditQuestionAsync(id, new Question())).Returns(Task.CompletedTask);

            var questionsController = new QuestionsController(_mockMyDbContext.Object);

            var result = await questionsController.PutQuestionAsync(id, new Question { Id = id });

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task DeleteQuestionAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.DeleteQuestionAsync(id)).Returns(Task.CompletedTask);

            var questionsController = new QuestionsController(_mockMyDbContext.Object);

            var result = await questionsController.DeleteQuestionAsync(id);

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task PutQuestionAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.AddQuestionToServeyAsync(id, new Question())).Returns(Task.CompletedTask);

            var surveyQuestionsController = new SurveyQuestionsController(_mockMyDbContext.Object);

            var result = await surveyQuestionsController.AddQuestionAsync(id, new Question());

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task GetQuestionsForSurveyAsync_ReturnListOfQuestions()
        {
            var id = 0;
            var expectedSurvey = MyDbContext.GetSeedingSurveys().FirstOrDefault(s => s.Id == id);

            _mockMyDbContext.Setup(
                db => db.GetSurveyAsync(id)).Returns(Task.FromResult(expectedSurvey));

            var surveyQuestionsController = new SurveyQuestionsController(_mockMyDbContext.Object);

            var result = await surveyQuestionsController.GetQuestionsForSurveyAsync(id);

            Assert.Equal(id, result.Value.FirstOrDefault().SurveyId);
        }

        [Fact]
        public async Task DeleteAllQuestionsAsync_ReturnOkResult()
        {
            var id = 5;

            _mockMyDbContext.Setup(
                db => db.DeleteAllQuestionsAsync(id)).Returns(Task.CompletedTask);

            var surveyQuestionsController = new SurveyQuestionsController(_mockMyDbContext.Object);

            var result = await surveyQuestionsController.DeleteQuestionsAsync(id);

            Assert.True(result is OkResult);
        }
    }
}
