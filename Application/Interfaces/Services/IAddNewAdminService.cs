using Application.DTO.Admins;
using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAddNewAdminService
    {
        public Task<AuthModel> AddNewAdmin(AddNewAdminDto dto);
    }
}