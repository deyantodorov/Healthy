using Ganss.XSS;

using HealthySystem.Services.Web.Contracts;

namespace HealthySystem.Services.Web
{
    public class HtmlSecuritySanitizer : IHtmlSecuritySanitizer
    {
        private readonly HtmlSanitizer sanitizer;

        public HtmlSecuritySanitizer()
        {
            this.sanitizer = new HtmlSanitizer();
        }

        public string Clean(string text)
        {
            return this.sanitizer.Sanitize(text);
        }
    }
}