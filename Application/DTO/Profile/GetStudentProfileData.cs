using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Profile
{
    public class GetStudentProfileData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }

        // ✅ أضيفي هذه الحقول (مهمة جداً لشاشة Edit Profile)
        public int Age { get; set; }
        public string University { get; set; } 
        public string Department { get; set; }
        public string Title { get; set; }
        public string JobTitle { get; set; }
    }
}