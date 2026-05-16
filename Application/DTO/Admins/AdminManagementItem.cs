using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admins
{
    public class AdminManagementItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
#nullable enable
        public DateOnly? LastActive { get; set; }
    }
}
