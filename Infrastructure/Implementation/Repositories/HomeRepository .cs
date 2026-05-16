using Application.DTO.HomePage;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IGetStudentByUserIdService getStudentByUserIdService;
        public HomeRepository(IGetStudentByUserIdService service, ApplicationDbContext context)
        {
            getStudentByUserIdService = service;
            _context = context;
        }

        public async  Task<HomeDataDto> GetHomeData(int userId)
        {
            var student = await getStudentByUserIdService.GetStudentAsyncByUserID(userId, new CancellationToken());
            if (student == null) 
                return new HomeDataDto
                {
                    Messege= "Invalid UserID... maybe UnAuthorized",
                    IsSuccess = false,
                };

            var surveys = await _context.SurveyResponses.Where(c => c.StudentId == student.Id).ToListAsync();
            var surveyCounts = surveys.Count();
            var lastSurveyResponse = surveys.OrderByDescending(x=>x.SubmittedAt).First();
            var lastSurveyDate = lastSurveyResponse.SubmittedAt;
            var lastPrediction = lastSurveyResponse.PredictionLabel;

            var studentJournals = await _context.MoodEntries.Where(x=> x.StudentId == student.Id).ToListAsync();
            var MoodCount = studentJournals.Count();
            var LastMood = surveys.OrderByDescending(x => x.SubmittedAt).First();
            var lastMoodDate = LastMood.SubmittedAt;

            return new HomeDataDto
            {
                surveysCount = surveyCounts,
                DepressionLabel = lastPrediction,
                JournalsCount = MoodCount,
                LastJournal = lastMoodDate,
                LastSurvey = lastSurveyDate,
                IsSuccess = true,
            };

            throw new NotImplementedException();
        }
    }
}
