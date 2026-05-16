using Application.DTO.Dashboard;
using Application.Features.Student.Queries.GetDashBoard;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Features.Student.Queries.GetDashboard
{
    public class GetDashboardHandler : IRequestHandler<GetDashboardQuery, DashboardDto?>
    {
        private readonly IGetStudentByUserIdService _getStudentByUserIdService;
        private readonly IStudentRepository _studentRepository;

        public GetDashboardHandler(
            IGetStudentByUserIdService getStudentByUserIdService,
            IStudentRepository studentRepository)
        {
            _getStudentByUserIdService = getStudentByUserIdService;
            _studentRepository = studentRepository;
        }

        public async Task<DashboardDto?> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var student = await _getStudentByUserIdService.GetStudentAsyncByUserID(request.UserId, cancellationToken);
            if (student is null)
                return null;

            return await _studentRepository.GetDashboardDataAsync(student.Id, cancellationToken);
        }
    }
}
