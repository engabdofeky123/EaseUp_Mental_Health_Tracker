using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Profile
{
    public class GetSupervisorProfileData
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string University { get; set; }
        public bool IsActive { get; set; }
        public int TotalStudents { get; set; }
        public DateTime CreatedAt { get; set; }

        // ✅ أضيفي هذه الخصائص إذا كانت مطلوبة للمشرف
        public string Email { get; set; }
        public string Department { get; set; }
        public int Age { get; set; }
    }
}
