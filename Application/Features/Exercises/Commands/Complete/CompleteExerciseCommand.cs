using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exercises.Commands.Complete
{
    public record CompleteExerciseCommand(int userId): IRequest<bool>;
}
