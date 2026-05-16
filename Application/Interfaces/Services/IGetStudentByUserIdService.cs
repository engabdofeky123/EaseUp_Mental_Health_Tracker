using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IGetStudentByUserIdService 
    {
        Task<Student> GetStudentAsyncByUserID(int userId, CancellationToken cancellationToken);
    }
}
