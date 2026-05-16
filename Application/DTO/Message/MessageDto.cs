
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } // The Student ID
        public string UserName { get; set; } // For display
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
