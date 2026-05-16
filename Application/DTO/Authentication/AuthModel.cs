using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Authentication
{
    public class AuthModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; } 
        public int StudentId { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public string Token { get; set; }
        public string Message { get; set; }
        public DateTime ExpiresON { get; set; }
        public List<string> Roles { get; set; }

    }
}
