using System;
using System.Collections.Generic;

namespace TestTask.Data
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int ViewsCount { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
}