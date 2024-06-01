using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TasksManager.application.UseCase.TasksManager.Create;
using TasksManager.application.UseCase.TasksManager.Delete;
using TasksManager.application.UseCase.TasksManager.GetAll;
using TasksManager.application.UseCase.TasksManager.GetAllTask;
using TasksManager.application.UseCase.TasksManager.Update;
using TasksManager.comunication.Request;
using TasksManager.comunication.Response;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksManager : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCreateTaskJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseCreateTaskJson), StatusCodes.Status400BadRequest)]
        public IActionResult CreateTask(RequestCreateTaskJson request)
        {
            var useCase = new CreateTask();

            useCase.Execute(request);

            if(request == null)
            {
                return BadRequest();
            }
            return Created(string.Empty, request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseGetTaskJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGetTaskJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAllTasks()
        {
            var useCase = new GetAllTask().Execute();

            if(useCase == null)
            {
                return NotFound();
            }

            return Ok(useCase);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseGetTaskJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGetTaskJson), StatusCodes.Status404NotFound)]
        public IActionResult GetTaskId(Guid id)
        {
            var useCase = new GetTask().Execute(id);

            if(useCase == null)
            {
                return NotFound();
            }
            return Ok(useCase);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTask(Guid id, RequestUpdateTaskJson request)
        {
            var useCase = new UpdateTask().Execute(id, request);

            if (useCase == null)
            {
                return NotFound();
            }
            return Created();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask(Guid id)
        {
            var useCase = new DeleteTask();

            useCase.Execute(id);

            return Created();
        }
    }
}
