using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Goals
{
    public class GoalItemDto
    {
        public int Id { get; set; }

        public int GoalId { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

    }
}
