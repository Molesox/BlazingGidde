using System;
using BlazingGidde.Client.Pages.PersonMain;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.Identity;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity;

namespace BlazingGidde.Server.Services;

    /// <summary>
    /// Provides account-related services such as user registration.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<FlowUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager to handle identity operations.</param>
        public AccountService(UserManager<FlowUser> userManager)
        {
            _userManager = userManager;
        }

        /// <inheritdoc />
        public async Task<RegisterResult> RegisterAsync(RegisterModel model)
        {
            var newUser = new FlowUser 
            { 
                UserName = model.Email, 
                Email = model.Email, 
                PhoneNumber = model.PhoneNumber,
                Person = new Person() // Ensure Person is initialized
            };

            newUser.Person.FirstName = model.FirstName;
            newUser.Person.LastName = model.LastName;
            newUser.Person.Emails.Add(new Email 
            { 
                EmailAddress = model.Email, 
                IsDefault = true 
            });

            var result = await _userManager.CreateAsync(newUser, model.Password!);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return new RegisterResult { Successful = false, Errors = errors };
            }

            return new RegisterResult { Successful = true };
        }
    }