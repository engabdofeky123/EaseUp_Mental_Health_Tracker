using Application.DTO.Admins;
using Application.DTO.Authentication;
using MediatR;

namespace Application.Features.Admin.AddNewAdmin
{
    public record AddNewAdminCommand(AddNewAdminDto dto): IRequest<AuthModel>;
}
