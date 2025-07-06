using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Models.DTOs;

namespace TodoApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TodoContext _context;

        public TaskService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetUncompletedTasksAsync()
        {
            return await _context.Tasks
                .Where(t => !t.IsCompleted)
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToListAsync();
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItemDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description, 
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> MarkTaskDoneAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            task.IsCompleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
