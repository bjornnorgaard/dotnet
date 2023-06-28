using Microsoft.AspNetCore.Identity;

namespace Todos.Database.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
