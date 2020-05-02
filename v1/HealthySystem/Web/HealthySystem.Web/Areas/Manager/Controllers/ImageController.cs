namespace HealthySystem.Web.Areas.Manager.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Areas.Manager.ViewModels;
    using HealthySystem.Web.Areas.Manager.ViewModels.Image;
    using HealthySystem.Web.Infrastructure.Images;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ImageController : AdministratorController
    {
        private readonly IImageService imageService;
        private readonly IArticleService articleService;

        public ImageController(IImageService imageService, IArticleService articleService)
        {
            this.imageService = imageService;
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page = 0)
        {
            var all = this.imageService.GetAll();

            if (this.TempData.ContainsKey(ModelConstants.Error))
            {
                this.ViewBag.Error = this.TempData[ModelConstants.Error];
            }

            this.ViewBag.CurrentSort = sortOrder;
            this.ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "IdAsc" : string.Empty;
            this.ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "DescriptionDesc" : string.Empty;
            this.ViewBag.RubricSortParam = string.IsNullOrEmpty(sortOrder) ? "PathsDesc" : string.Empty;

            if (searchString != null)
            {
                page = 0;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                all = all.Where(t => t.ImageDescription.Contains(searchString.Trim()));
            }

            switch (sortOrder)
            {
                case "IdAsc":
                    all = all.OrderBy(s => s.Id);
                    break;
                case "DescriptionDesc":
                    all = all.OrderByDescending(s => s.ImageDescription);
                    break;
                case "PathsDesc":
                    all = all.OrderByDescending(s => s.ImagePath);
                    break;
                default:
                    all = all.OrderByDescending(x => x.Id);
                    break;
            }

            int totalPages = (int)Math.Ceiling(all.Count() / (decimal)WebConstants.AdminPageSize);
            int pageNumber = page ?? 0;

            if (pageNumber > totalPages)
            {
                return this.NotFound("Page can't be found");
            }

            this.ViewBag.TotalPages = totalPages;
            this.ViewBag.CurrentPage = pageNumber;

            var allAsViewModel = all
                .Skip(pageNumber * WebConstants.AdminPageSize)
                .Take(WebConstants.AdminPageSize)
                .To<ImagePreviewViewModel>()
                .ToList();

            // allAsViewModel.ForEach(x => x.ImagePath = Images.GetImageFromCache(x.ImagePath, WebConstants.ImageWidth, WebConstants.ImageMaxHeight));
            return this.View(allAsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ImageViewModel model)
        {
            if (model == null || !this.ModelState.IsValid || model.ImagePath.ContentLength <= 0)
            {
                this.TempData.Add(ModelConstants.Error, ModelConstants.ModelError);
                return this.RedirectToAction("Index", "Image");
            }

            string serverPath = this.HttpContext.Server.MapPath("~");
            string currentFolder = DateTime.Now.Year + "_" + DateTime.Now.Month;
            string hddPath = serverPath + WebConstants.DirectoryUpload + "\\" + currentFolder;

            if (!Directory.Exists(hddPath))
            {
                Directory.CreateDirectory(hddPath);
            }

            string fileName = Path.GetFileName(model.ImagePath.FileName);

            if (fileName == null)
            {
                this.TempData.Add(ModelConstants.Error, ModelConstants.FileNotFound);
                return this.RedirectToAction("Index", "Image");
            }

            string fileNameSaveAs = Math.Abs(fileName.GetHashCode() ^ fileName.Length) + "-" + fileName;
            model.ImagePath.SaveAs(Path.Combine(hddPath, fileNameSaveAs));

            string imageDbPath = "/" + WebConstants.DirectoryUpload + "/" + currentFolder + "/" + fileNameSaveAs;

            var image = new Image()
            {
                ImagePath = imageDbPath,
                ImageDescription = model.ImageDescription
            };

            this.imageService.Add(image);

            return this.RedirectToAction("Index", "Image");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var image = this.imageService.GetById(id);

            if (image == null)
            {
                return this.NotFound("Image not found");
            }

            if (this.articleService.HasImageId(id))
            {
                return new HttpStatusCodeResult(400, ModelConstants.Undeletable);
            }

            this.imageService.Delete(image);

            return this.RedirectToAction("Index", "Image");
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            var image = this.imageService.GetById(id);

            if (image == null)
            {
                return this.NotFound("Image not found");
            }

            var itemViewModel = this.Mapper.Map<ImagePreviewViewModel>(image);

            return this.PartialView("_EditPartial", itemViewModel);
        }

        [HttpPost]
        public ActionResult Update(ImagePreviewViewModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.ModelError });
            }

            var image = this.imageService.GetById(model.Id);

            if (image == null)
            {
                return this.PartialView("_Error", new ErrorViewModel() { Message = ModelConstants.FileNotFound });
            }

            var imageToUpdate = this.Mapper.Map<Image>(model);
            this.imageService.Update(imageToUpdate);

            var updatedImage = this.Mapper.Map<ImagePreviewViewModel>(imageToUpdate);
            return this.PartialView("_ItemPartial", updatedImage);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var image = this.imageService.GetById(id);
            var imageViewModel = this.Mapper.Map<ImagePreviewViewModel>(image);
            return this.PartialView("_ItemPartial", imageViewModel);
        }
    }
}