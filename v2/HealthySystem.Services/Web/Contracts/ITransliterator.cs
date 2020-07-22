namespace HealthySystem.Services.Web.Contracts
{
    public interface ITransliterator
    {
        string GetTextInEnglish(string text);

        string GetLetterInEnglish(char letter);
    }
}