using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Choice
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        public int QuestionId { get; set; }

        // Navigation Property
#nullable enable
        public Question? Question { get; set; }
    }
}
