using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admins
{
    public class UpdateProfileInformationData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
#nullable enable
        public IFormFile? ImageFile { get; set; }
    }
}