using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Application.DTO.Admins 
{
    public class ViewProfileInformationResult
    {
        public string Name {  get; set; }
        public string Email {  get; set; }
        public string imageUrl { get; set; }
        public bool IsAdmin { get; set; }
        public string Message { get; set; }
    }
}
