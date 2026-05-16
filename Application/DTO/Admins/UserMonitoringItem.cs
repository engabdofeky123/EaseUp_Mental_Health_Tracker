using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admins
{
    public class UserMonitoringItem
    {
        public int Id { get; set; }
        public string imageUrl { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool LastActive { get; set; }
        public string Status { get; set; }
    }
}
