using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NotesASPBlazorTask.Data.Models;
using SQLitePCL;

namespace NotesASPBlazorTask.Data.Database
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext()
        {
            
        }
    }
}
