using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IspitTodo.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }    
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        public required bool Status { get; set; }
        public DateTime TimeFinished { get; set; }
        [Required]
        public required int TodolistId { get; set; }
        [NotMapped]
        public Todolist Todolist { get; set; }
    }
}
