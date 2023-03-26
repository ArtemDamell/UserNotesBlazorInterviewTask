using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotesAsync(string userId);
        Task<Message> CreateNoteAsync(Note newNote);
        Task<Message> UpdateNoteAsync(Note editedNote);
        Task<Message> DeleteNoteAsync(Note deletingNote);
        Task<IEnumerable<Note>> GetNotesByCondition(string noteCondition, string userId);
        IEnumerable<Note> GetDemoNotieces();
    }
}
