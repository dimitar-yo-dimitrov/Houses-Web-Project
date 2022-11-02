using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Houses.Web.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly string _customDateFormat;

        public DateTimeModelBinder(string customDateFormat)
        {
            _customDateFormat = customDateFormat;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime actualValue = DateTime.MinValue;
                bool success = false;
                string dateValue = valueResult.FirstValue;

                try
                {
                    actualValue = DateTime.ParseExact(dateValue, _customDateFormat, CultureInfo.InvariantCulture);
                    success = true;
                }
                catch (FormatException)
                {
                    try
                    {
                        actualValue = DateTime.Parse(dateValue, new CultureInfo("bg-bg"));
                    }
                    catch (Exception e)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    }

                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
