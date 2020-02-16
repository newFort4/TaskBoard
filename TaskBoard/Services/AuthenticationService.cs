using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskBoard.Models;
using TaskBoard.ViewModels;

namespace TaskBoard.Services
{
    public class AuthenticationService
    {
        readonly TaskBoardDbContext db;
        readonly SignInManager<IdentityUser> signInManager;
        readonly UserManager<IdentityUser> userManager;

        public AuthenticationService(TaskBoardDbContext db,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task RegisterUserAsync(RegistrationModel registrationModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber
            };

            var result = await userManager
                .CreateAsync(identityUser, registrationModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Registration is not succeded.");
            }
        }
    }
}
