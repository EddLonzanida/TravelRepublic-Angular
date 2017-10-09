using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace TravelRepublic.ApiHost.Helpers
{
    public static class ErrorHandlersExtention
    {
        public static List<string> GetModelStateErrors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors).ToList();
            var aErrors = errors.ConvertAll(r => r.ErrorMessage);
            return aErrors;
        }

        public static List<string> GetValidationErrors(this DbEntityValidationException dbEx)
        {
            var aErrors = new List<string>();
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                aErrors.AddRange(validationErrors
                                .ValidationErrors
                                .Select(validationError => $"Property: {validationError.PropertyName} Message: {validationError.ErrorMessage}"));
            }
            return aErrors;
        }
    }
}