using System.ComponentModel.DataAnnotations;

namespace TestTask.Data
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string Oprion1 { get; set; }
        public string Oprion2 { get; set; }
        public string Oprion3 { get; set; }
        public string Comment { get; set; }

        [Required]
        public int SurveyId { get; set; }
    }
}