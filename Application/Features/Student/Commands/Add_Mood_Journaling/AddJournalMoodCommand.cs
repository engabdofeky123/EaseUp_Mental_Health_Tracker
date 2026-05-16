using Application.DTO.Student;
using Application.DTO.Student.Mood_Journal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.Add_Mood_Journaling
{
    public record AddJournalMoodCommand(int userId ,AddJournalMoodInputDto moodInput) : IRequest<AddMoodJournalResult>;
}
