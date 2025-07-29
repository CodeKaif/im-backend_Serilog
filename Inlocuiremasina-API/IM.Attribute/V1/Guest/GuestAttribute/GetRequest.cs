using IM.Attribute.V1.Guest.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IM.Attribute.V1.Guest.GuestAttribute
{
    public class GetRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            var metadata = new GuestRequestMetadata
            {
                lang_code = request.Headers["Accept-Language"].FirstOrDefault() ?? "en"
            };

            // Store metadata in HttpContext.Items so the controller can access it
            httpContext.Items["GuestRequestMetadata"] = metadata;

            base.OnActionExecuting(context);
        }
    }
}
