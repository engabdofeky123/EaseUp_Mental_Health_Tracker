using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Goals
{
    public class GetActiveGoalsResult
    {
        public string Message { get; set; }
        public List<Goal> ActiveGoals { get; set; } = new List<Goal>();
        public bool IsSuccess { get; set; } = false;
    }
}
