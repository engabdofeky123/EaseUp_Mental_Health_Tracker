using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.HomePage
{
    public class HomeDataDto
    {
        public int surveysCount {  get; set; }
        public int JournalsCount { get; set; }

        public DateTime LastSurvey {  get; set; }
        public DateTime LastJournal { get; set; }

        public string DepressionLabel { get; set; } 

        public bool IsSuccess { get; set; }
        public string Messege { get; set; }   
        public List<string> Errors { get; set; }    
    }
}