using Application.DTO.AiModelDto;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AiSurvey.Commands
{
    public class SaveSurveyResponsesInDbHandler : IRequestHandler<SaveSurveyResponsesInDbCommand, AiSurveyResultDto>
    {

        private readonly IAiSurveyRepository _surveyRepository;

        public SaveSurveyResponsesInDbHandler(IAiSurveyRepository service) 
        {
            _surveyRepository = service;
        }

        public async Task<AiSurveyResultDto> Handle(SaveSurveyResponsesInDbCommand request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
                return null;
            return await _surveyRepository.SaveResultOfSurvey(request.userId, request.inputDto);
        }
    }
}
