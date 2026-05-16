using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admins
{
    public class AdminsOverviewResult
    {
        public int Users { get; set; }
        public int ActiveUsers { get; set; }
        public int CrisisUsers { get; set; }
    }
}