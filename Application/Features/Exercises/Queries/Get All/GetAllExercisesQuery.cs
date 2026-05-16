using Application.DTO.Exercises;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exercises.Queries.Get_All
{
    public record GetAllExercisesQuery():IRequest<List<ExerciseDto>>;
}
