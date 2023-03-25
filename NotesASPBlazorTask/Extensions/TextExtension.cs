namespace NotesASPBlazorTask.Extensions
{
    public static class TextExtension
    {
        public static string GetShortDescription(this string text, int lenght = 10, bool dotsAdding = true)
        {
            var textResult = text.Substring(0, Math.Min(lenght, text.Length));
            if (dotsAdding)
                return textResult + "...";
            else 
                return textResult;
        }
    }
}
