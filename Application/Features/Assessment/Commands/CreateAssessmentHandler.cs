using Application.DTO.Mentals;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Assessment.Commands
{
    public class CreateAssessmentHandler : IRequestHandler<CreateAssessmentCommand, MentalHealthDto>
    {
        private readonly IAssessmentRepository _repository;
        private readonly IGetStudentByUserIdService _getStdByUserId;  // عشان تجيب الـ StudentId من الـ Token

        public CreateAssessmentHandler(IAssessmentRepository repository, IGetStudentByUserIdService userContext)
        {
            _repository = repository;
            _getStdByUserId = userContext;
        }

        public async Task<MentalHealthDto> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
        {

            var student = await _getStdByUserId.GetStudentAsyncByUserID(request.userId,cancellationToken);
            
            var assessment = new MentalHealth
            {
                AnxietyScore = request.AnxietyScore,
                CreatedAt = DateTime.UtcNow,
                DepressionScore = request.DepressionScore,
                Diagnosis = request.Diagnosis,
                StressScore = request.StressScore,
                // StudentId = student.Id
            };

            await _repository.AddAsync(assessment);
            return new MentalHealthDto 
            {
                AnxietyScore= assessment.AnxietyScore,
                CreatedAt= assessment.CreatedAt,
                DepressionScore=assessment.DepressionScore,
                Diagnosis=assessment.Diagnosis,
                StressScore=assessment.StressScore,
            };
        }
    }
}
