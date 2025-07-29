using Localization.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ResponseWrapper.V1.Model;
using System.Net;

namespace ResponseWrapper.V1
{
    public class ResponseHelper
    {
        private readonly JsonLocalizationService _localizationService;

        public ResponseHelper(IServiceProvider serviceProvider)
        {
            _localizationService = serviceProvider.GetRequiredService<JsonLocalizationService>();
        }

        public CommonResponse CreateResponse(string responseKey, object responseData, int statusCode, string lang, bool isError = false)
        {
            string localizedMessage = responseKey;
            try
            {
                localizedMessage = _localizationService.GetLocalizedString(responseKey, lang);
            }
            catch
            {
                localizedMessage = responseKey;
            }

            return new CommonResponse
            {
                Data = responseData,
                Message = localizedMessage,
                IsError = isError,
                Status = ResponseStatusCode(statusCode)
            };
        }

        public HttpStatusCode ResponseStatusCode(int statusCode)
        => statusCode switch
        {
            201 => HttpStatusCode.Created,
            200 => HttpStatusCode.OK,
            202 => HttpStatusCode.Accepted,
            204 => HttpStatusCode.NoContent,
            401 => HttpStatusCode.Unauthorized,
            403 => HttpStatusCode.Forbidden,
            404 => HttpStatusCode.NotFound,
            409 => HttpStatusCode.Conflict,
            500 => HttpStatusCode.InternalServerError,
            400 => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.BadRequest
        };

        public IActionResult ResponseWrapper(CommonResponse response)
        {
            return response.Status switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.Accepted => new AcceptedResult(),
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.Unauthorized => new UnauthorizedResult(),
                HttpStatusCode.Forbidden => new ForbidResult(),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.Conflict => new ConflictObjectResult(response),
                HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };
        }
    }
}
