using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoLists.Models
{
    public class TodoListsContext : DbContext
    {
        public TodoListsContext (DbContextOptions<TodoListsContext> options)
            : base(options)
        {
        }

        public DbSet<TodoLists.Models.Note> Note { get; set; }
        public DbSet<TodoLists.Models.CheckList> CheckList { get; set; }
        public DbSet<TodoLists.Models.Label> Label { get; set; }
    }
}
