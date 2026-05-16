using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Goals
{
    public class GoalDtoForGamification
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority GoalPriority { get; set; }

        public DateOnly? Deadline { get; set; }

        public bool IsCompleted { get; set; } = false;
        public DateOnly? DoneAt { get; set; }

        public List<GoalItemDto> Items { get; set; }
    }
}
