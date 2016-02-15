namespace HealthySystem.Web
{
    using System.Web.Mvc;

    public class ViewEngineConfig
    {
        public static void Initialize()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
