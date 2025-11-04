using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IspitTodo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
        public virtual IEnumerable<Todolist> Todolists { get; set; }
    }
}
