using ApplicationLayer.Dtos;
using AutoMapper;
using DomainLayer.Entities;
using Infrastucture.Repository.Implementations;
using Infrastucture.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _iunitofwork;
        private readonly IMapper _mapper;
        public TaskController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper= mapper;
            _iunitofwork= unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            var tasks=await _iunitofwork.tasks.GetAllAsync();
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TasksDto tasksDto)
        {
            var task = _mapper.Map<TasksDto, Tasks>(tasksDto);
            if (task != null)
            {
               await _iunitofwork.tasks.AddAsync(task);
               await _iunitofwork.tasks.SaveChangesAsync();
                return Ok(task);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task< IActionResult> UpdateTask([FromBody] TasksDto taskDto)
        {
            var task = _mapper.Map<TasksDto, Tasks>(taskDto);
            if (task != null && task.Id!=0)
            {
                 _iunitofwork.tasks.Update(task);
               await _iunitofwork.tasks.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deletesuccess(int id)
        {
            var taskFromDb = _iunitofwork.tasks.GetByIdAsync(id);
            if (taskFromDb == null)
            {
                return BadRequest(new { message = "no data found" });
            }
            _iunitofwork.tasks.Remove(taskFromDb);
           await _iunitofwork.tasks.SaveChangesAsync();
            return Ok();

        }
    }
}
