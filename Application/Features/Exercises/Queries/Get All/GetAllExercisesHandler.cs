using Application.DTO.Exercises;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exercises.Queries.Get_All
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public GetAllExercisesHandler(IExerciseRepository repo)
        {
            _exerciseRepository = repo;
        }
        public async Task<List<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.GetExercises();
        }
    }
}
