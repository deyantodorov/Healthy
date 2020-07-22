namespace HealthySystem.Services.Web.Contracts
{
    public interface IHtmlSecuritySanitizer
    {
        string Clean(string text);
    }
}
