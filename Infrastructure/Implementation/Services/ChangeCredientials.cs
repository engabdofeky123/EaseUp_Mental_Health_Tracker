using Application.DTO.Credentials;
using Application.Interfaces.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class ChangeCredientials : IChangeCredientials
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeCredientials(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ChangeCredentialsResult> ChangeEmailAsync(int userId, ChangeEmailRequest request)
        {
            // Check if the user exists and authorised 

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return new ChangeCredentialsResult { Message = "invalid authorization" };
            // Check if the new email is valid and not already in use

            var IsEmailUsed = await _userManager.FindByEmailAsync(request.NewEmail);

            if (IsEmailUsed != null && IsEmailUsed.Id != user.Id)
                return new ChangeCredentialsResult { Message = "Email used before" };

            // Update the email in the database
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);

            var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);
            if (!result.Succeeded)
            {
                var ErrorList = "";
                foreach (var error in result.Errors)
                    ErrorList += error.Description + ' ';
                return new ChangeCredentialsResult { Message = ErrorList };
            }
            return new ChangeCredentialsResult { IsSuccess = true, Message = "Email changed successfully" };

        }

        public async Task<ChangeCredentialsResult> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new ChangeCredentialsResult { Message = "Invalid Authorization" };
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var ErrorList = "";
                foreach (var error in result.Errors)
                    ErrorList += error.Description + ' ';
                return new ChangeCredentialsResult { Message = ErrorList };
            }
            return new ChangeCredentialsResult { IsSuccess = true, Message = "Password changed successfully" };
        }

    }
}
