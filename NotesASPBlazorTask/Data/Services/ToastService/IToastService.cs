using NotesASPBlazorTask.Data.Models.Toast;

namespace NotesASPBlazorTask.Data.Services.ToastService
{
    public interface IToastService
    {
        event Action<ToastMessage> OnShow;
        void ShowToast(string title, string message, ToastMessageType type = ToastMessageType.Info, int duration = 5000);
    }
}
