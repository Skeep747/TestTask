using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Data
{
    public class Survey
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public int ViewsCount { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
}