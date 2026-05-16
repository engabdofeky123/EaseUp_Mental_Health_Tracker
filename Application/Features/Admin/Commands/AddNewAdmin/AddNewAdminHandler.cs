using Application.DTO.Authentication;
using Application.Features.Admin.AddNewAdmin;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Features.Admin.Commands.AddNewAdmin
{
    public class AddNewAdminHandler : IRequestHandler<AddNewAdminCommand, AuthModel>
    {
        private readonly IAddNewAdminService _addNewAdminService;

        public AddNewAdminHandler(IAddNewAdminService adminService)
        {
            _addNewAdminService = adminService;
        }
        public async Task<AuthModel> Handle(AddNewAdminCommand request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            if(request.dto == null)
                return new AuthModel {Message = "Where Inputs !!!" };
            return await _addNewAdminService.AddNewAdmin(request.dto);
        }
    }
}