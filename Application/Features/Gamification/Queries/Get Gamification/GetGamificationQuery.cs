using Application.DTO.Gamification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Gamification.Queries.Get_Gamification
{
    public record GetGamificationQuer(int userId): IRequest<GamificationPageDto>;
}
