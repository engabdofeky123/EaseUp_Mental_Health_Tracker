using Application.DTO.AiModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAiSurveyRepository
    {
        public Task<AiSurveyResultDto> SaveResultOfSurvey(int userId ,AiSurveyInputDto input);
        public Task<AiSurveyResultDto> GetStudentDashboardData(int userId);
    }
}
