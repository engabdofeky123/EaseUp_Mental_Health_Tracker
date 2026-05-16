using Application.DTO.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IHomeRepository
    {
        public Task<HomeDataDto> GetHomeData(int userId);
    }
}
