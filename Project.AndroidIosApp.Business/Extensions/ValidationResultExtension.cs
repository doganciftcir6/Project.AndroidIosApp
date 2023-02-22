using FluentValidation.Results;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationErrors> ConverToCustomValidationError(this FluentValidation.Results.ValidationResult validationResult)
        {
            List<CustomValidationErrors> customValidationErrors= new List<CustomValidationErrors>();
            foreach (var item in validationResult.Errors)
            {
                customValidationErrors.Add(new CustomValidationErrors()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName,
                });
            }
            return customValidationErrors;
        }
    }
}
