using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }
        public DateTime? ReceivedAt { get; set; }

        // Navigation Properties
        public Student Sender { get; set; }
        public Student Receiver { get; set; }
    }
}
