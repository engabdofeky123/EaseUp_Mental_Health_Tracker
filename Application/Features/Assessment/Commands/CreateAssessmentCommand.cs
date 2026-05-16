using Application.DTO.Mentals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Assessment.Commands
{
    public record CreateAssessmentCommand(int userId ,double DepressionScore,double AnxietyScore,double StressScore,string Diagnosis) : IRequest<MentalHealthDto>;
}
// ("depression_score",  "anxiety_score",  "stress_score”,  "diagnosis") 
