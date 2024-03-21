// UserRepository
using AutoMapper;
using Ecars.Model;
using Ecars.Model.Dto_s;
using Ecars.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApiResponseHelper _apiResponseHelper;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApiResponseHelper apiResponseHelper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _apiResponseHelper = apiResponseHelper;
        }

        public async Task<ApiResponse> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByNameAsync(loginRequestDTO.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiSettings:Secret"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var response = new LoginResponseDTO
                {
                    //User = new UserDTO
                    //{
                    //    ID = user.Id,
                    //    Name = user.UserName,
                    //    UserName = user.UserName
                    //},
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                return _apiResponseHelper.OkResponse(response);
            }

            return _apiResponseHelper.HandleException("User name or password is incorrect");
        }

        public async Task<ApiResponse> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            var userExist = await userManager.FindByNameAsync(registerationRequestDTO.UserName);
            if (userExist == null)
            {
                var user = new ApplicationUser
                {
                    UserName = registerationRequestDTO.UserName,
                    Email = registerationRequestDTO.UserName,
                    NormalizedEmail = registerationRequestDTO.UserName.ToUpper()
                };

                try
                {
                    var result = await userManager.CreateAsync(user, registerationRequestDTO.Password);
                    if (result.Succeeded)
                    {
                        if (!await roleManager.RoleExistsAsync("admin"))
                        {
                            await roleManager.CreateAsync(new IdentityRole("admin"));
                            await roleManager.CreateAsync(new IdentityRole("customer"));
                        }
                        if (registerationRequestDTO.Role == "admin")
                        {
                            await userManager.AddToRoleAsync(user, "admin");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "customer");
                        }
                        return _apiResponseHelper.OkResponse("User registered successfully");
                    }
                    else
                    {
                        List<string> errors = new List<string>();
                        foreach(var error in result.Errors)
                        {
                            errors.Add(error.Description);
                        }

                        return _apiResponseHelper.HandleException(errors);
                    }

                }
                catch (Exception ex)
                {
                    return _apiResponseHelper.HandleException(ex);
                }

            }
            else
            {
                return _apiResponseHelper.HandleException("Username already in use");
            }
        }
    }
}