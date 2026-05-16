using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Goal;

namespace Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? University { get; set; }
        public string? Department { get; set; }
        public int AcademicYear { get; set; }
        public double CurrentGPA { get; set; } = 4.0;
        public bool IsActive { get; set; } = true;
        public bool HasScolarship { get; set; } = false;
        public int Total_Exercise_Score { get; set; } = 0;
        public string AdminNotes { get; set; } // showed for admins only (: 

        // ✅ إضافة خاصية صورة البروفايل
        public string? ProfilePictureUrl { get; set; }

        // Navigation

        public List<Goal>? Goals { get; set; } = new();
        public List<MoodEntries>? MoodEntries { get; set; } = new();
        public List<Notification>? Notifications { get; set; } = new();
        public List<SurveyResponse>? SurveyResponses { get; set; } = new();
        public List<Acheivements> Acheivements { get; set; } = new();

        public List<Chat>? SentChats { get; set; } = new();
        public List<Chat>? ReceivedChats { get; set; } = new();
        public List<MentalHealth> MentalHealth { get; set;} = new(); 
    }
}
