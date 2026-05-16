using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Student
{
    public class MoodEntryDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MoodResult { get; set; }  // From Mode Analysis Model

    }
}
