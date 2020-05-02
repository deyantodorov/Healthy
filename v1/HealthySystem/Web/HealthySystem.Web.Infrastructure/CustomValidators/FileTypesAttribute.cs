namespace HealthySystem.Web.Infrastructure.CustomValidators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> types;

        public FileTypesAttribute(string types)
        {
            this.types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var fileExt = Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return this.types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", string.Join(", ", this.types));
        }
    }
}