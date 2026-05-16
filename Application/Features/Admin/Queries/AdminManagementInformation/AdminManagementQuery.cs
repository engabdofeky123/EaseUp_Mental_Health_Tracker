using Application.DTO.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.AdminManagementInformation
{
    public record AdminManagementQuery(): IRequest<List<AdminManagementItem>>;
}
