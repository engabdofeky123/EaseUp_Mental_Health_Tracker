using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class GetStudentByUserIdService : IGetStudentByUserIdService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public GetStudentByUserIdService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<Student> GetStudentAsyncByUserID(int userId, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;

            return await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
        }
    }
}
