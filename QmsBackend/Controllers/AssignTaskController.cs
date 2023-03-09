using ApplicationLayer.Dtos;
using AutoMapper;
using DomainLayer.Entities;
using Infrastucture.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignTaskController : Controller
    {
        private readonly IUnitOfWork _iunitofwork;
        private readonly IMapper _imapper;
        private readonly IEmailSender _emailSender;
        public AssignTaskController(IUnitOfWork iunitofwork, IMapper imapper,IEmailSender emailSender)
        {
            _emailSender= emailSender;
            _iunitofwork = iunitofwork;
            _imapper = imapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAssignedTasks()
        {
            var assignedtasks= await _iunitofwork.assignTasks.GetAllAsync(IncludeProperties: "registeredUsers,tasks");
            return Ok(assignedtasks);
        }
        [HttpPost]
        public async Task<IActionResult> AddAssignTask([FromBody] AssignTasksDto assignTasksDto)
        {
            var assignedtask = _imapper.Map<AssignTasksDto, AssignTasks>(assignTasksDto);
            if(assignedtask != null)
            {
               await _iunitofwork.assignTasks.AddAsync(assignedtask);
               await _iunitofwork.assignTasks.SaveChangesAsync();
               return Ok(assignedtask);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAssignTask([FromBody] AssignTasksDto assignTasksDto)
        {
            var assignedtask=_imapper.Map<AssignTasksDto,AssignTasks>(assignTasksDto);
            if(assignedtask!=null) {
                 _iunitofwork.assignTasks.Update(assignedtask);
                await _iunitofwork.assignTasks.SaveChangesAsync();
                return Ok(assignedtask);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignTask(int id)
        {
            var assigntask=_iunitofwork.assignTasks.GetByIdAsync(id);
            if(assigntask != null)
            {
                _iunitofwork.assignTasks.Remove(assigntask);
               await _iunitofwork.assignTasks.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
