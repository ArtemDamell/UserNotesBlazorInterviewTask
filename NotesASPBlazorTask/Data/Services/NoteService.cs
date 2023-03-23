using Microsoft.Data.Sqlite;
using NotesASPBlazorTask.Data.Database;
using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services
{
    public class NoteService : INoteService
    {
        private readonly ISqlDbContext _sqlDbContext;

        public NoteService(ISqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<Message> AddNote(Note newNote)
        {
            
        }
    }
}
