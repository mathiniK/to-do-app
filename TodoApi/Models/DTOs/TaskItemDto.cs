using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(1, ErrorMessage = "Title cannot be empty.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
