namespace NotesASPBlazorTask.Extensions
{
    public static class TextExtension
    {
        /// <summary>
        /// Gets a short description of a string with a given length and option to add dots at the end.
        /// </summary>
        /// <param name="text">The string to get a short description from.</param>
        /// <param name="lenght">The length of the short description.</param>
        /// <param name="dotsAdding">Whether to add dots at the end of the short description.</param>
        /// <returns>A short description of the given string.</returns>
        public static string GetShortDescription(this string text, int lenght = 10, bool dotsAdding = true)
        {
            var textResult = text.Substring(0, Math.Min(lenght, text.Length));
            if (dotsAdding)
                return textResult + " ...";
            else
                return textResult;
        }
    }
}
