# Healthy 
**Teleric Academy ASP.NET MVC Final Project**

# Task
1. Create fully functional seo optimized responsive website with area for administrator, editor, registered users and not registered users.
    - Administrator - should be able to manage everything and have access to system tools like:
        - Glimpse
        - Elmah
        - Google Analytics API, Facebook API, Google plus API (will be implemented in next version)
        - Manage deleted items (will be implemented in next version)
    - Editor - should be able to manage site content
    - Not registered users should be able to browse and search
    - Registered users should be able to browse, search, comment and manage their password

2. Extra tools and options:
    - Google Analytics
    - Google AdSense
    - Google Webmaster Tools
    - Query Facebook and Google for articles likes and shares
    - Create Google Sitemap 

# Result:
- **0** StyleCop errors
- **44** Unit Tests passed
- **3298** URLS crawled (30 parallel threads) by XENU with **0** client or server errors for just 00:01:02 minutes
- controllers > 15
- actions > 40
- display/editor templates >= 10
- 2 areas
- partial views > 10

# Used technologies
- ASP.NET MVC and Visual Studio 2015 (Removed Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Microsoft.Net.Compilers. Why? You could read here: http://stackoverflow.com/questions/32282880/publish-website-without-roslyn)
- MS SQL Server + Entity Framework 6 + Service Layer
- Bootstrap, JQuery, TinyMCE
- Autofac, Automapper etc.
    
**This file is missing, cause contains sensitive information, which is not for public usage:**
	
- Web.Release.config
