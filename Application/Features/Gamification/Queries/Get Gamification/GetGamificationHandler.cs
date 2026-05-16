using Application.DTO.Acheivements;
using Application.DTO.Gamification;
using Application.DTO.Goals;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System.Linq;
using System.Linq.Expressions;


namespace Application.Features.Gamification.Queries.Get_Gamification
{
    public class GetGamificationHandler : IRequestHandler<GetGamificationQuer, GamificationPageDto>
    {
        private readonly IGetStudentByUserIdService getStudentByUserIdService;
        private readonly IGoalsRepository _goalsRepository;
        private readonly IAcheivementsService _acheivementsService;

        public GetGamificationHandler(IGetStudentByUserIdService service, IGoalsRepository goalsRepository, IAcheivementsService acheivementsService)
        {
            getStudentByUserIdService = service;
            _goalsRepository = goalsRepository;
            _acheivementsService = acheivementsService;
        }

        public async Task<GamificationPageDto> Handle(GetGamificationQuer request, CancellationToken cancellationToken)
        {
            var student = await getStudentByUserIdService.GetStudentAsyncByUserID(request.userId, cancellationToken);
            if (student == null)
                return null;

            var studentGoals = await _goalsRepository.GetActiveGoals(student.Id,cancellationToken);

            var result = new GamificationPageDto
            {
                TotalPoints = student.Total_Exercise_Score,
                CurrentGoals = studentGoals.ActiveGoals.Where(g => g.IsCompleted == false).Select(g=> new GoalDtoForGamification
                {
                    Title = g.Title,
                    IsCompleted = g.IsCompleted,
                    Deadline = g.Deadline,
                    Description = g.Description,
                    DoneAt = g.DoneAt,
                    GoalPriority = g.GoalPriority,
                    StudentId = student.Id,
                    Items = g.Items.Select(i => new GoalItemDto
                    {
                        GoalId = i.Id,
                        Id = i.Id,
                        IsCompleted= i.IsCompleted,
                        Title = i.Title
                    }).ToList(),
                    Id = g.Id
                }).ToList(),
                Acheivements = await _acheivementsService.GetAchievementsByStudentIdAsync(student.Id)
            };
            return result;  
        }
    }
}