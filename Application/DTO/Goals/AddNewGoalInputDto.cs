using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Goal;

namespace Application.DTO.Goals
{
    public class AddNewGoalInputDto
    {
        public string Title { get; set; }
        public string GoalDescription { get; set; }
        public DateOnly DeadLine { get; set; }
        public Priority Priority { get; set; } = Priority.High;
        public List<string> Items { get; set; }
    }
}
