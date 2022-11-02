using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Houses.Web.ModelBinders
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        private readonly string _customDateFormat;

        public DateTimeModelBinderProvider(string customDateFormat)
        {
            _customDateFormat = customDateFormat;
        }

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new DateTimeModelBinder(_customDateFormat);
            }

            return null;
        }
    }
}
