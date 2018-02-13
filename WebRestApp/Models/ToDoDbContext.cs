using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebRestApp.Models;

namespace WebRestApp.Models
{
    public class ToDoDbContext : DbContext
     {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {}
       public DbSet<ToDoItems> todoItemes { get; set; }
    }
}
