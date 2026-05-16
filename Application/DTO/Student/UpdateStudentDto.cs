using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Student
{
    public class UpdateStudentDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string College { get; set; }
        public string Department { get; set; }  

        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }


        // Additional Data 

        public int Age { get; set; }
        public string University { get; set; }
        public string Title { get; set; }
        public string JobTitle { get; set; }

    }
}