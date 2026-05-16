using Application.DTO.AiModelDto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
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
    public class AiSurveyRepository : IAiSurveyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IGetStudentByUserIdService getStudentByUserIdService;

        public AiSurveyRepository(ApplicationDbContext context, IGetStudentByUserIdService service)
        {
            _context = context;
            getStudentByUserIdService = service;
        }

        public async Task<AiSurveyResultDto> SaveResultOfSurvey(int userId ,AiSurveyInputDto input)
        {
            var student = await getStudentByUserIdService.GetStudentAsyncByUserID(userId, new CancellationToken());
            if (student == null)
                return new AiSurveyResultDto { IsSaved = false , Message = "Invalid UserID .. maybe UnAuthorized"};

            var surveyResult = new SurveyResponse
            {
                StudentId = student.Id,
                PredictionLabel = input.prediction,
                PredictionValue = input.prediction switch
                {
                    "No Depression" => PredictionValues.NoDepression,
                    "Mild Depression" => PredictionValues.MildDepression,
                    "Severe Depression" => PredictionValues.SevereDepression,
                    _ => 0
                },
                QuestionResponses = input.answers.Select(a => new QuestionResponse
                {
                    QuestionTitle = a.Key,
                    QuestionAnswer = a.Value,
                    SubmittedAt = DateTime.UtcNow
                }).ToList()

            };

            await _context.SurveyResponses.AddAsync(surveyResult);
            await _context.SaveChangesAsync();

            return new AiSurveyResultDto
            {
                studentId = student.Id,
                DepressionLabel = input.prediction,
                DepressionValue = surveyResult.PredictionValue.ToString(),
                IsSaved = true,
                Message = "Survey saved successfully"
            };
        }

        public async Task<AiSurveyResultDto> GetStudentDashboardData(int userId)
        {
            var student = await getStudentByUserIdService.GetStudentAsyncByUserID(userId, new CancellationToken());
            if (student == null)
                return new AiSurveyResultDto { IsSaved = false, Message = "Invalid UserID .. maybe UnAuthorized" };

            var surveyResponses = _context.SurveyResponses.Include(sr => sr.QuestionResponses)
                .Where(sr => sr.StudentId == student.Id).ToList();

            return null;
        }
    }
}