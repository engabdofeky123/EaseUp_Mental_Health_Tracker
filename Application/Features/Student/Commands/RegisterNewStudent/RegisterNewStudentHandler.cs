using Application.DTO.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.RegisterNewStudent
{
    internal class RegisterNewStudentHandler: IRequestHandler<RegisterNewStudentCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        private readonly IStudentRepository _studentRepository;

        public RegisterNewStudentHandler(IAuthService authService, IStudentRepository studentRepo)
        {
            _authService = authService;
            _studentRepository = studentRepo;
        }

        public async Task<AuthModel> Handle(RegisterNewStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.dto is null)
                throw new ArgumentNullException(nameof(request));
            var result = await _authService.RegisterAsync(request.dto);

            if (!result.IsAuthenticated)
                return result;
      
            var student = new Domain.Models.Student
            {
               Name = $"{request.dto.FirstName} {request.dto.LastName}",
               UserId = result.UserId,
               IsActive = true,
               HasScolarship = false,
               Gender = "Not Specified",
               Department = "Undeclared",
                University = "Unknown",
                AcademicYear = 3
            };

            await _studentRepository.AddStudentAsync(student);

            result.UserId = student.UserId;
            result.StudentId = student.Id;

            return result;
        }
    }
}