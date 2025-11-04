using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IspitTodo.Models
{
    public class Todolist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        [NotMapped]
        public virtual ApplicationUser User { get; set; }
        [NotMapped]
        public IEnumerable<Task> Tasks { get; set; }
    }
}
