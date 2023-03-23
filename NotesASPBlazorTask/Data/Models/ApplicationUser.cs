using Microsoft.AspNetCore.Identity;

namespace NotesASPBlazorTask.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Note> Notes { get; set; }
    }
}
