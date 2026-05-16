using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public List<Question> Questions { get; set; } = new();
        public List<Choice> Choices { get; set; } = new();

    }
}
