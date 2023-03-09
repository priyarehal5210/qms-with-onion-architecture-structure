using ApplicationLayer.Dtos;
using AutoMapper;
using DomainLayer.Entities;
using Infrastucture.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSuccessController : Controller
    {
        private readonly IUnitOfWork _iunitofwork;
        private readonly IMapper _mapper;
        public UserSuccessController(IUnitOfWork iunitofwork, IMapper mapper)
        {
            _iunitofwork = iunitofwork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserSuccess()
        {
            var allSuccess=await _iunitofwork.userSuccess.GetAllAsync(IncludeProperties: "assignTasks.registeredUsers,assignTasks.tasks");
            return Ok(allSuccess);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserSuccess([FromBody] UserSuccessDto userSuccessDto)
        {
            var usersuccess = _mapper.Map<UserSuccessDto, UserSuccess>(userSuccessDto);

            if(usersuccess != null)
            {
               await _iunitofwork.userSuccess.AddAsync(usersuccess);
               await _iunitofwork.userSuccess.SaveChangesAsync();
               return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserSuccess([FromBody] UserSuccessDto userSuccessDto)
        {
            var usersuccess = _mapper.Map<UserSuccessDto, UserSuccess>(userSuccessDto);

            if (usersuccess != null)
            {
                 _iunitofwork.userSuccess.Update(usersuccess);
                await _iunitofwork.userSuccess.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserSuccess(int id)
        {
            var usersuccess=_iunitofwork.userSuccess.GetByIdAsync(id);
            if (usersuccess != null)
            {
                _iunitofwork.userSuccess.Remove(usersuccess);
                await _iunitofwork.userSuccess.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
