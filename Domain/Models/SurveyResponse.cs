using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public PredictionValues PredictionValue { get; set; }    // from 1 --> 3  (Enum)
        public string PredictionLabel { get; set; }  // come from Model 

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Student Student { get; set; }
        public List<QuestionResponse>? QuestionResponses { get; set; }
    }

    public enum PredictionValues
    {
        NoDepression = 1,
        MildDepression = 2,
        SevereDepression = 3,   
    }
}
