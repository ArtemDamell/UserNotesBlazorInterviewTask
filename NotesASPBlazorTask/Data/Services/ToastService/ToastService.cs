using NotesASPBlazorTask.Data.Models.Toast;

namespace NotesASPBlazorTask.Data.Services.ToastService
{
    public class ToastService : IToastService
    {
        // This event is used to notify subscribers when a ToastMessage is shown. The Action<ToastMessage> parameter allows
        // subscribers to receive the ToastMessage that is being shown.
        public event Action<ToastMessage> OnShow;

        /// <summary>
        /// Invokes the OnShow event with a ToastMessage object containing the specified title, message, type and duration.
        /// </summary>
        /// <param name="title">The title of the ToastMessage.</param>
        /// <param name="message">The message of the ToastMessage.</param>
        /// <param name="type">The type of the ToastMessage.</param>
        /// <param name="duration">The duration of the ToastMessage.</param>
        public void ShowToast(string title, string message, ToastMessageType type = ToastMessageType.Info, int duration = 5000)
        {
                 OnShow?.Invoke(new ToastMessage { Title = title, Message = message, Type = type, Duration = duration });
        }
    }
}
