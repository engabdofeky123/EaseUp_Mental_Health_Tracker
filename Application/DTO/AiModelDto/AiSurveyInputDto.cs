using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AiModelDto
{
    public class AiSurveyInputDto  // come from AI Model
    {
        // Label
        public string prediction {  get; set; }
        public Dictionary<string, string> answers { get; set; }  
    }
}