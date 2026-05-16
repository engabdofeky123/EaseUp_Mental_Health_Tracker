using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admins
{
    public class StudentProfileInformationResutl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string PictureUrl { get; set; }

        // Academic Summary 
        public string Department { get; set; }
        public int AcademicYear { get; set; }
        public double CurrentGPA { get; set; }
        public bool HasScolarship { get; set; }

        // Recent Activity
        public int Total_Exercise_Score { get; set; } = 0;
        public int GoalsCount { get; set; }
        public int GoalsAchieved { get ; set ; }
        public List<GoalsAchievedDto> GoalsAchieveds { get; set; }
    }
    public class GoalsAchievedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; } 
        public DateOnly? DeadLine { get; set; }
        public  DateOnly? DoneAt { get; set; }
    }
}
