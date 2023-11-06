using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaApp.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrorIfNotEmpty(this ModelStateDictionary modelState, string key, string errorMessage)
        {
            if (!errorMessage.IsNullOrEmpty())
            {
                modelState.AddModelError(key, errorMessage);
            }
        }
    }
}
