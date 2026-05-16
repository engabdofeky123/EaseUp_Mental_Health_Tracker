using Application.DTO.Goals;
using Domain.Models;


namespace Application.Interfaces.Repositories
{
    public interface IGoalsRepository
    {
        public Task<GetActiveGoalsResult> GetActiveGoals(int studentId, CancellationToken cancellationToken);
        public Task<AddNewGoalResult> AddNewGoal(AddNewGoalInputDto dto, int studentId, CancellationToken cancellationToken);
#nullable enable
        Task<Goal?> GetGoalByIdAsync(int goalId);
        Task UpdateGoalAsync(int id, UpdateGoalDto goal);
        Task DeleteGoalAsync(int goalId);

        Task ToggleItemAsync(int itemId, int studentId, int goalId);
        Task<List<Goal>> GetAllGoalsByStudentId(int studentId);
    }
}