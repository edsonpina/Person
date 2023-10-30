using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Person.API.Model
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }
    }
}
