using Microsoft.EntityFrameworkCore;

namespace _1_TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options): base(options)
        {

        }

        public DbSet<TodoPerson> TodoPeople { get; set; }
    }
}