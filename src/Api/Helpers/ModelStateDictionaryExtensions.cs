using Core.Functional;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static ModelStateDictionary AddErrors(this ModelStateDictionary modelState, Errors errors)
        {
            foreach (var error in errors)
                modelState.AddModelError("", error.Key);

            return modelState;
        }

        public static ModelStateDictionary AddErrors(this ModelStateDictionary modelState, Result result)
        {
            return modelState.AddErrors(result.Errors);
        }
    }
}
