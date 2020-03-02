using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Prime.Services;

namespace Prime.Infrastructure
{
    public class EnrolleeModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string routeParameter = "enrolleeId";

            var valueProviderResult = bindingContext.ValueProvider.GetValue(routeParameter);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return;
            }

            string value = valueProviderResult.FirstValue;
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (!int.TryParse(value, out var enrolleeId))
            {
                bindingContext.ModelState.TryAddModelError(routeParameter, "Enrollee Id must be an integer.");
                return;
            }

            EnrolleeService enrolleeService = (EnrolleeService)bindingContext.HttpContext.RequestServices.GetService(typeof(IEnrolleeService));
            var enrollee = await enrolleeService.GetEnrolleeAsync(enrolleeId);
            bindingContext.Result = ModelBindingResult.Success(enrollee);
        }
    }
}
