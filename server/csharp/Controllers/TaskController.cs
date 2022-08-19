using Microsoft.AspNetCore.Mvc;
using todolist.Repositories;
using todolist.Filters;
using todolist.Helpers;
using todolist.DTOs;
using AutoMapper;

namespace todolist.Controllers;

[ApiController]
[Route("task")]
[ServiceFilter(typeof(CheckAuthFilter))]
public class TaskController : ControllerBase
{
    protected readonly TaskRepository _repository;
    protected readonly IMapper _mapper;

    public TaskController(TaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create(TaskCreateDTO request)
    {
        var user = (UserEntity)HttpContext.Items["user"]!;

        _repository.Create(_mapper.Map<TaskEntity>(request), user);
        ResponseHelper<TaskCreateDTO> response = new($"Task {MessageHelper.Success.Created}", request);
        return Ok(response);
    }

    [HttpGet("list")]
    public IActionResult List()
    {
        var user = (UserEntity)HttpContext.Items["user"]!;

        var taskEntities = _repository.GetByOwner(user);
        var taskList = taskEntities.Select(_mapper.Map<TaskInfoDTO>).ToArray();

        ResponseHelper<TaskInfoDTO[]> response = new($"{taskList.Count()} task found", taskList);
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id, TaskUpdateDTO item)
    {
        var user = (UserEntity)HttpContext.Items["user"]!;
        var originalTask = _repository.GetById(id);

        if (user.Id != originalTask?.OwnerId)
            return BadRequest(new ResponseHelper($"Task {MessageHelper.Error.NotFound}", true));

        var editedTask = _mapper.Map<TaskEntity>(item);

        _repository.Update(originalTask, editedTask);

        ResponseHelper response = new($"Task {MessageHelper.Success.Edited}");
        return Ok(response);
    }

    [HttpPatch("{id}/set_done")]
    public IActionResult SetDone(int id)
    {
        var user = (UserEntity)HttpContext.Items["user"]!;
        var originalTask = _repository.GetById(id);

        if (user.Id != originalTask?.OwnerId)
            return BadRequest(new ResponseHelper($"Task {MessageHelper.Error.NotFound}", true));

        _repository.SetDone(originalTask);

        ResponseHelper response = new($"Task {MessageHelper.Success.Edited}");
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = (UserEntity)HttpContext.Items["user"]!;

        if (!_repository.Exist(id, user))
            return BadRequest(new ResponseHelper($"Task {MessageHelper.Error.NotFound}", true));

        _repository.Delete(id);

        ResponseHelper response = new($"Task {MessageHelper.Success.Deleted}");
        return Ok(response);
    }
}
