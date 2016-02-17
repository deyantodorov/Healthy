namespace HealthySystem.Web.Infrastructure.CustomValidators
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;

        public FileSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var httpPostedFileBase = value as HttpPostedFileBase;

            return httpPostedFileBase != null && httpPostedFileBase.ContentLength <= this.maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", this.maxSize);
        }
    }
}