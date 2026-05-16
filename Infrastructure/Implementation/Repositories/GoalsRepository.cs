using Application.DTO.Goals;
using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class GoalsRepository : IGoalsRepository
    {
        private readonly ApplicationDbContext _context;
        public GoalsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetActiveGoalsResult> GetActiveGoals(int studentId, CancellationToken cancellationToken)
        {
            return new GetActiveGoalsResult
            {
                ActiveGoals = await _context.Goals.Include(g=> g.Items)
                    .Where(g => g.StudentId == studentId)
                    .Select(g => new Goal
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Deadline = g.Deadline,
                        GoalPriority = g.GoalPriority,
                        IsCompleted = g.IsCompleted,
                        Progress = g.Progress,
                        Items = g.Items
                    }).ToListAsync()
            };
        }

        public async Task<AddNewGoalResult> AddNewGoal(AddNewGoalInputDto dto, int studentId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.Title))
                return new AddNewGoalResult { Message = "Goal Title cannot be empty", isSuccess = false };

            var result = new GoalCreationResult
            {
                Title = dto.Title,
                Priority = dto.Priority,
                Deadline = dto.DeadLine,
                IsCompleted = false,
                Items = dto.Items.Select(g => new GoalItemDto
                {
                    Title = g,
                    IsCompleted = false
                }).ToList(),
                StudentId = studentId
            };

            var createdGoal = new Goal
            {
                Title = dto.Title,
                GoalPriority = dto.Priority,
                Deadline = dto.DeadLine,
                IsCompleted = false,
                StudentId = studentId,
                Items = dto.Items.Select(i => new ChecklistItem { Title = i, IsCompleted = false }).ToList()
            };
            await _context.Goals.AddAsync(createdGoal);
            await _context.SaveChangesAsync();
            return new AddNewGoalResult { isSuccess = true, Message = "Added Successfully"};
        }

        public async Task<Goal> GetGoalByIdAsync(int goalId)
        {
            return await _context.Goals.Where(g => g.Id == goalId)
                .Select(g => new Goal
                {
                    Id = g.Id,
                    StudentId = g.StudentId,
                    Title = g.Title,
                    GoalPriority = g.GoalPriority,
                    Deadline = g.Deadline,
                    IsCompleted = g.IsCompleted,
                    Items = g.Items.Select(i => new ChecklistItem
                    {
                        Id = i.Id,
                        Title = i.Title,
                        IsCompleted = i.IsCompleted
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task ToggleItemAsync(int itemId, int studentId, int goalId)
        {
            var goal = await _context.Goals.Include(g => g.Items)
                .Where(g => g.StudentId == studentId).FirstOrDefaultAsync(i => i.Id == goalId);

            if (goal == null)
                throw new Exception("Goal not found");

            var item = goal.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new Exception("Item not found");

            item.IsCompleted = !item.IsCompleted;

            goal.IsCompleted = goal.Items.All(c => c.IsCompleted);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateGoalAsync(int id, UpdateGoalDto goal)
        {

            var existingGoal = await _context.Goals.Include(g => g.Items).FirstOrDefaultAsync(g => g.Id == id);
            if (existingGoal == null)
                throw new Exception("Goal not found");

            existingGoal.Title = goal.Title;
            existingGoal.GoalPriority = goal.Priority;
            existingGoal.Deadline =  goal.Deadline;

            existingGoal.Items.Clear();
            existingGoal.Items.AddRange(goal.Items.Select(i => new ChecklistItem { Title = i.Title, IsCompleted = false }));
            await _context.SaveChangesAsync();

        }

        public async Task DeleteGoalAsync(int goalId)
        {
            var goal = await _context.Goals.FirstOrDefaultAsync(x => x.Id == goalId);

            if (goal == null)
                throw new Exception("Goal not found");

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }

        public  async Task<List<Goal>> GetAllGoalsByStudentId(int studentId)
        {
            return await _context.Goals.Where(g=> g.StudentId == studentId).ToListAsync();
        }
    }
}
