using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AiModelDto
{
    public class AiSurveyResultDto  // To save in DB 
    {
        public int studentId {  get; set; }
        public string DepressionLabel { get; set; }
        public string DepressionValue { get; set; }

        public List<string> Errors { get; set; }
        public bool IsSaved { get; set; } = false;
        public string Message { get; set; }
    }
}
