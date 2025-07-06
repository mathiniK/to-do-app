using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Models.DTOs;

namespace TodoApi.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetUncompletedTasksAsync();
        Task<TaskItem> CreateTaskAsync(TaskItemDto dto);
        Task<bool> MarkTaskDoneAsync(int id);
    }
}
