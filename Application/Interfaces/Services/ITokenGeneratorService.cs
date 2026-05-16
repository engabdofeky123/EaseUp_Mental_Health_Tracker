using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITokenGeneratorService
    {
        Task<(string Token, DateTime ExpiresOn)> GenerateToken(int userId, string email, IEnumerable<string> roles);
    }
}