using ApplicationLayer.Dtos;
using AutoMapper;
using DomainLayer.Entities;
using Infrastucture.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using QmsBackend.ViewModels;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public RegistrationController(IUnitOfWork unitOfWork,IMapper mapper,IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _unitofwork = unitOfWork;
            _mapper= mapper;
        }

        //<summary>
        //registration process.
        //</summay>

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users=await _unitofwork.registerUsers.GetAllAsync();
            var decryptData =users.Select(u =>new
            {
                u.Id,
                u.userName,
                u.email,
                u.role,
                password = DomainLayer.Common.EncryptionDecryption.decrypt(u.password),
                confirmpassword = DomainLayer.Common.EncryptionDecryption.decrypt(u.confirmPassword)
            });
            return Ok(decryptData);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegisteredUserDto registeredUserDto)
        {
            var AllUsers = await _unitofwork.registerUsers.GetAllAsync();
            var User = _mapper.Map<RegisteredUserDto, RegisteredUsers>(registeredUserDto);
            var epassword = DomainLayer.Common.EncryptionDecryption.encrypt(User.password);
            var econfirmPassword = DomainLayer.Common.EncryptionDecryption.encrypt(User.confirmPassword);

            if (AllUsers.Any(u => u.userName == User.userName))
            {
                return BadRequest("user in use");

            }
            if (User != null )
            {
                if (AllUsers.Any(u => u.role == "Admin"))
                {
                    User.role = "Trainee";
                }
                else
                {
                    User.approved = true;
                    User.role = "Admin";
                }
                var userInDb = new RegisteredUsers
                {
                    userName = User.userName,
                    email = User.email,
                    role = User.role,
                    password = epassword,
                    confirmPassword = econfirmPassword,
                };

                //sending email

                var emailHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(userInDb.email));
                var code = Convert.ToBase64String(emailHash);
                var confirmationLink = Url.Action("ConfirmEmail", "Registration",
                    new {userId = userInDb.Id, codeis = code }, protocol:Request.Scheme);
                await _emailSender.SendEmailAsync(userInDb.email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                if (userInDb.emailConfirm == true)
                {
                    await _unitofwork.registerUsers.AddAsync(userInDb);
                    await _unitofwork.registerUsers.SaveChangesAsync();
                    return Ok(userInDb);
                }
                else
                {
                    return BadRequest("confirm your mail please");
                }
            }
            return BadRequest();
        }
        //confirmation action
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var allusers=await _unitofwork.registerUsers.GetAllAsync();
            //if(allusers.Any(u=>u.Id==userId))
            //{
            //    return Ok(userId);
            //}
            return BadRequest("no user found");
        }
        //<summary>
        //login process.
        //</summay>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginVm loginVm)
        {
            var user = _unitofwork.loginUser.Login(loginVm.Email,loginVm.Password);
            if (user != null)
            {
                if (user.email == loginVm.Email)
                {
                    if (user.password == loginVm.Password)
                    {
                        return Ok(user);

                    }
                    else
                    {
                        return BadRequest("password is incorrect");
                    }
                }
                else
                {
                    return BadRequest("user does't exist");
                }
            }
            return BadRequest();
        }
        //[HttpPost("Approve")]
        //public async Task<IActionResult> ApproveUser([FromBody] ApproveVm approveVm)
        //{

        //}
    }
}
