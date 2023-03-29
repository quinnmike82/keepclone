using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteShare> NoteShares { get; set; }
    }
}