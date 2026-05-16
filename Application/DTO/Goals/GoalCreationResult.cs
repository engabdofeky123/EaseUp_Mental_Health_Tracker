using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Goals
{
    public class GoalCreationResult
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Title { get; set; }

        public Priority Priority { get; set; }

        public DateOnly? Deadline { get; set; }

        public bool IsCompleted { get; set; } = false;
        public List<GoalItemDto> Items { get; set; }
    }
}
