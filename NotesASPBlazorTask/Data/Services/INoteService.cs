using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services
{
    public interface INoteService
    {
        Task<Message> AddNote(Note newNote);
    }
}
