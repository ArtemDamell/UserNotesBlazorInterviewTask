using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NotesASPBlazorTask.Data.Models;
using SQLitePCL;

namespace NotesASPBlazorTask.Data.Database
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
