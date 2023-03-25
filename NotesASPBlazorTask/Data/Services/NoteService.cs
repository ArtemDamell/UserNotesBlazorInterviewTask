using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NotesASPBlazorTask.Data.Database;
using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services
{
    public class NoteService : INoteService
    {
        private readonly SqlDbContext _sqlDbContext;

        public NoteService(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync(string userId)
        {
            var allUserNotes = await _sqlDbContext.Notes.Where(x => x.UserId == userId).ToListAsync();
            return allUserNotes;
        }

        public async Task<Message> CreateNoteAsync(Note newNote)
        {
            if (newNote is null)
            {
                var message = new Message { State = MessageState.Failed, MessageText = "Created note cannot be null" };
                return await Task.FromResult(message);
            }

            try
            {
                await _sqlDbContext.Notes.AddAsync(newNote);
                await _sqlDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Message { State = MessageState.Failed, MessageText = ex.Message});
            }
            return await Task.FromResult(new Message { State = MessageState.Success, MessageText = $"Note '{newNote.Title}' has been created"});
        }
    }
}
