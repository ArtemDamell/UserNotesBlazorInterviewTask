namespace NotesASPBlazorTask.Data.Models.Toast
{
    public class ToastMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastMessageType Type { get; set; } = ToastMessageType.Info;
        public int Duration { get; set; } = 5000;
    }
}
