using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int SurveyId { get; set; }  // for future use, if we want to link question to specific survey

        // Navigation Properties
        public Survey? Survey { get; set; }
        public List<Choice>? Choices { get; set; }
    }
}
