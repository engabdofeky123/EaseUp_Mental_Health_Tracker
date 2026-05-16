using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChecklistItem
    {
        public int Id { get; set; }

        public int GoalId { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        // Navigation
        public Goal Goal { get; set; }
    }
}
