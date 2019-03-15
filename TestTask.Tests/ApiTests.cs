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

            var surveys = await surveysController.GetSurveysAsync();

            var actualSurveys = Assert.IsAssignableFrom<ActionResult<IEnumerable<Survey>>>(surveys).Value;
            Assert.Equal(
                expectedSurveys.OrderBy(s => s.Id).Select(s => s.Title),
                actualSurveys.OrderBy(s => s.Id).Select(s => s.Title));
        }
    }
}
