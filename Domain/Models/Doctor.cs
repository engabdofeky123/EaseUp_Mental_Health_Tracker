using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public int YearsOfExperience { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public List<Student>? Students { get; set; }

    }
}
