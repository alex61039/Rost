using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rost.Common;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Account;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        
        private const string BaseUrl = "http://rost.notissimus.com";
        
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, IEmailService emailService, IUserService userService)  
        {  
            _userManager = userManager;  
            _roleManager = roleManager;  
            _configuration = configuration;
            _emailService = emailService;
            _userService = userService;
        }
        
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            var user = await _userManager.FindByNameAsync(model.Username);
            
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ResponseModel { Status = "Error", Message = $@"Пользователь с email {model.Username} не зарегистрирован" });
            }
            
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            
            if (!isEmailConfirmed)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ResponseModel { Status = "Error", Message = "Подтвердите адрес электронной почты" });
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await _userManager.GetRolesAsync(user);
                var userClaims = await _userManager.GetClaimsAsync(user);
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, user.UserName),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }

                foreach (var userClaim in userClaims)
                {
                    authClaims.Add(userClaim);
                }
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));  
                
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo,
                    userName = user.UserName,
                    isParentRole = userRoles.Contains(Constants.RoleParent),
                    isEmployeeRole = userRoles.Contains(Constants.RoleEmployee),
                    isSelfEmployeeRole = userRoles.Contains(Constants.RoleSelfEmployee),
                    isSuperAdminRole = userRoles.Contains(Constants.RoleSuperAdmin),
                    employeeRole = user.EmployeeRole
                });  
            }  
            
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseModel { Status = "Error", Message = "Не верные имя пользователя или пароль" });
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = "BadRequest", Message = "Не все поля заполнены" });
            }
                
            var userExists = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });  
            }  
            
            var user = new ApplicationUser
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username,
                CityId = model.CityId,
                DistrictId = model.DistrictId,
                MunicipalUnionId = model.MunicipalUnionId,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.Phone,
                IsDisplayHint = true,
                IsFirstLogin = true,
                PersonalNumber = await _userService.GetNextPersonalNumber()
            };  
            
            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            
            var tokenGenerated = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var tokenGeneratedBytes = Encoding.UTF8.GetBytes(tokenGenerated);
            var code = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            var roleSelectPageUrl = Url.Content($"{BaseUrl}/auth/confirm?id={user.Id}&code={code}");
            
            await _emailService.SendEmailAsync(model.Email, "Подтверждение регистрации на сайте рост.дети",
                $"Подтвердите регистрацию, перейдя <a href='{roleSelectPageUrl}' target='_blank'> по ссылке</a>");
 
            return Ok();  
        }  
  
        [HttpPost]  
        [Route("register-admin")]  
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)  
        {  
            var userExists = await _userManager.FindByNameAsync(model.Username);  
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });
            }

            var user = new ApplicationUser  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (await _roleManager.RoleExistsAsync("Admin"))  
            {  
                await _userManager.AddToRoleAsync(user, "Admin");  
            }  
  
            return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });  
        }

        /// <summary>
        /// Метод обновления ролей пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("role/update")]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] RoleSelectionModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            await InitializeRoles();

            var userRoles = await _userManager.GetRolesAsync(user);
            
            if (model.IsEmployeeRole)
            {
                if (!userRoles.Contains(Constants.RoleEmployee))
                {
                    await _userManager.AddToRoleAsync(user, Constants.RoleEmployee);  
                }
            }
            else
            {
                if (userRoles.Contains(Constants.RoleEmployee))
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.RoleEmployee);  
                }
            }
            
            if (model.IsParentRole)
            {
                if (!userRoles.Contains(Constants.RoleParent))
                {
                    await _userManager.AddToRoleAsync(user, Constants.RoleParent);  
                }
            }
            else
            {
                if (userRoles.Contains(Constants.RoleParent))
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.RoleParent);  
                }
            }
            
            if (model.IsSelfEmployeeRole)
            {
                if (!userRoles.Contains(Constants.RoleSelfEmployee))
                {
                    await _userManager.AddToRoleAsync(user, Constants.RoleSelfEmployee);  
                }
            }
            else
            {
                if (userRoles.Contains(Constants.RoleSelfEmployee))
                {
                    await _userManager.RemoveFromRoleAsync(user, Constants.RoleSelfEmployee);  
                }
            }

            return Ok();
        }

        /// <summary>
        /// Метод получения списка ролей пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("role/list")]
        [Authorize]
        public async Task<IActionResult> ListRoles()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            
            var model = new RoleSelectionModel
            {
                IsEmployeeRole = userRoles.Contains(Constants.RoleEmployee),
                IsParentRole = userRoles.Contains(Constants.RoleParent),
                IsSelfEmployeeRole = userRoles.Contains(Constants.RoleSelfEmployee)
            };

            return Ok(model);
        }
        
        /// <summary>
        /// Метод подтверждения электронной почты
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
        {
            if (string.IsNullOrEmpty(model.UserId))
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = "Не указан UserId" });
            }
            
            if (string.IsNullOrEmpty(model.Code))
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = "Не указан code" });
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = $"Пользователь с Id = {model.UserId} не найден" });
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Во время выполнения запроса произошла ошибка" });
            }

            return Ok(new ResponseModel { Status = "Success", Message = "Email подтвержден" });
        }

        /// <summary>
        /// Метод для отправки кода восстановления пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Password/Forgot")]
        public async Task<IActionResult> RecoverPassword([FromBody] PasswordRecoveryModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = $"Пользователь с {model.Email} не зарегистрирован" });
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var passwordResetUrl = Url.Content($"{BaseUrl}/auth/recover?id={user.Id}&code={code}");
            
            await _emailService.SendEmailAsync(model.Email, "Восстановление пароля на сайте рост.дети",
                $"Для сброса пароля перейдите <a href='{passwordResetUrl}' target='_blank'> по ссылке</a>");
 
            return Ok(new ResponseModel { Status = "Success", Message = "Recovery mail sent successfully!" });  
        }

        /// <summary>
        /// Метод для сброса пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Password/Reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return BadRequest(new ResponseModel { Status = ResponseErrors.Error, Message = ValidationErrorMessages.UserIdIsNotExists(model.UserId) });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code.Replace(" ", "+"), model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ResponseModel { Status = ResponseErrors.Error, Message = ResponseErrors.PasswordNotChanged });
            }
            
            return Ok(new ResponseModel { Status = ResponseErrors.Success, Message = ResponseSuccessMessages.PasswordChanged });  
        }

        /// <summary>
        /// Метод изменения пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Password/Change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!string.IsNullOrEmpty(model.UserId))
            {
                user = await GetUserAsync(model.UserId);
            }
            
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.UserIdIsNotExists(model.UserId) });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(new ResponseModel { Status = ResponseErrors.Error, Message = ResponseErrors.PasswordNotChanged });
            }
            
            return Ok(new ResponseModel { Status = ResponseErrors.Success, Message = ResponseSuccessMessages.PasswordChanged });  
        }
        
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Email/Send")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var roleSelectPageUrl = Url.Content($"{BaseUrl}/auth/confirm?id={user.Id}&code={code}");
            
            try
            {
                await _emailService.SendEmailAsync(email, "Подтверждение регистрации на сайте рост.дети",
                    $"Подтвердите регистрацию, перейдя <a href='{roleSelectPageUrl}' target='_blank'> по ссылке</a>");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Message sent");
        }
        
        private async Task<ApplicationUser> GetUserAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _userManager.FindByIdAsync(userId);
            }

            return await _userManager.FindByNameAsync(User.Identity.Name);
        }

        private async Task InitializeRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Constants.RoleParent))
            {
                await _roleManager.CreateAsync(new IdentityRole(Constants.RoleParent));
            }
            
            if (!await _roleManager.RoleExistsAsync(Constants.RoleEmployee))
            {
                await _roleManager.CreateAsync(new IdentityRole(Constants.RoleEmployee));
            }
            
            if (!await _roleManager.RoleExistsAsync(Constants.RoleSelfEmployee))
            {
                await _roleManager.CreateAsync(new IdentityRole(Constants.RoleSelfEmployee));
            }
        }
    }
}