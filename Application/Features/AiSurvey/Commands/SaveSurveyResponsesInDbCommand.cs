using Application.DTO.AiModelDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AiSurvey.Commands
{
    public record SaveSurveyResponsesInDbCommand(int userId ,AiSurveyInputDto inputDto): IRequest<AiSurveyResultDto>;

}
