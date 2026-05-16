using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionAnswer { get; set; }
        public DateTime SubmittedAt { get; set; } 

        // Navigation 
        public SurveyResponse? SurveyResponse { get; set; }
    }
}