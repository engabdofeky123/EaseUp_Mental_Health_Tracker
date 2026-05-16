using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Goals
{
    public class ToggleItemsDto
    {
        public int itemId { get; set; }
        public int studentId { get; set; }
        public int goalId { get; set; }
    }
}