# Healthy

This files are missing:
	
	- Web.Release.config
	- WebConstants.cs

*Contains sensitive information, which is not for public usage*

# Architecture

1. HealthySystem.Web
    - Areas
        -   Identity
            - AccountController
            - ManageController
        -   Manager
            -   AdministratorController
            -   ArticleController
            -   HomeController
            -   ImageController
            -   RubricController
            -   TagController
    - Controllers
        - BaseController
        - SiteController
        - ArticleController
        - HomeController
        - RubricController
        - SearchController
        - SitemapController
        - TagController
2. Users
    - Administrator
    - Editor
    - User