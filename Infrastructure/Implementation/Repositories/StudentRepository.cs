using Application.DTO.Dashboard;
using Application.DTO.Goals;
using Application.DTO.Student;
using Application.DTO.Student.Mood_Journal;
using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student.Id;
        }

        public Task DeleteStudentAsync(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<AddMoodJournalResult> AddMoodJournal(AddJournalMoodInputDto dto, int studentId, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(dto.Mood))
                return new AddMoodJournalResult { Message = "Mood cannot be empty", isSuccess = false };

            var moodJournal = new MoodEntries
            {
                StudentId = studentId,
                Content = dto.Mood,
                CreatedAt = DateTime.UtcNow
            };

            await _context.AddAsync(moodJournal);
            await _context.SaveChangesAsync();
            return new AddMoodJournalResult { Message = "Added successfully", isSuccess = true };
        }

        public async Task<DashboardDto?> GetDashboardDataAsync(int studentId, CancellationToken cancellationToken)
        {
            // ── 1. Latest mental-health record ────────────────────────────────
            var latestRecord = await _context.MentalHealthRecords
                .Where(m => m.StudentId == studentId)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            var latestScores = latestRecord is null
                ? new LatestMentalHealthScoresDto()
                : new LatestMentalHealthScoresDto
                {
                    DepressionScore = latestRecord.DepressionScore,
                    AnxietyScore = latestRecord.AnxietyScore,
                    StressScore = latestRecord.StressScore,
                    Diagnosis = latestRecord.Diagnosis ?? string.Empty,
                    RecordedAt = latestRecord.CreatedAt
                };

            // ── 2. Weekly mental-health trends (last 7 weeks) ─────────────────
            var sevenWeeksAgo = DateTime.UtcNow.AddDays(-49);

            var mentalRecords = await _context.MentalHealthRecords
                .Where(m => m.StudentId == studentId && m.CreatedAt >= sevenWeeksAgo)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync(cancellationToken);

            // Group by ISO week number so Mon-Sun stays together
            var weeklyMentalGroups = mentalRecords
                .GroupBy(m => ISOWeek.GetWeekOfYear(m.CreatedAt))
                .OrderBy(g => g.Key)
                .ToList();

            var weeklyTrends = weeklyMentalGroups
                .Select((g, idx) => new WeeklyMentalHealthDto
                {
                    WeekLabel = $"Week {idx + 1}",
                    WeekStart = g.Min(m => m.CreatedAt).Date,
                    AvgDepressionScore = Math.Round(g.Average(m => m.DepressionScore), 2),
                    AvgAnxietyScore = Math.Round(g.Average(m => m.AnxietyScore), 2),
                    AvgStressScore = Math.Round(g.Average(m => m.StressScore), 2)
                })
                .ToList();

            // ── 3. Sentiment heatmap (last 7 weeks, one mood per day) ─────────
            var moodEntries = await _context.MoodEntries
                .Where(e => e.StudentId == studentId && e.CreatedAt >= sevenWeeksAgo)
                .OrderBy(e => e.CreatedAt)
                .ToListAsync(cancellationToken);

            // Day-abbreviation order that matches the dashboard rows (S M T W T F S)
            var dayKeys = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            var weeklyMoodGroups = moodEntries
                .GroupBy(e => ISOWeek.GetWeekOfYear(e.CreatedAt))
                .OrderBy(g => g.Key)
                .ToList();

            var sentimentOverTime = weeklyMoodGroups
                .Select((g, idx) =>
                {
                    // Pick the last mood entry per day-of-week inside this week
                    var byDay = g
                        .GroupBy(e => e.CreatedAt.DayOfWeek)
                        .ToDictionary(
                            d => dayKeys[(int)d.Key],
                            d => d.OrderByDescending(e => e.CreatedAt)
                                  .FirstOrDefault()?.MoodResult
                        );

                    // Ensure all 7 day keys are present (null = no entry)
                    var dailyMoods = dayKeys.ToDictionary(
                        k => k,
                        k => byDay.TryGetValue(k, out var v) ? v : null
                    );

                    return new WeeklySentimentDto
                    {
                        WeekLabel = $"Week {idx + 1}",
                        WeekStart = g.Min(e => e.CreatedAt).Date,
                        DailyMoods = dailyMoods
                    };
                })
                .ToList();

            return new DashboardDto
            {
                LatestScores = latestScores,
                WeeklyTrends = weeklyTrends,
                SentimentOverTime = sentimentOverTime
            };
        }

    }
}