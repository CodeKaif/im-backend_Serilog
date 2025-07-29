using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IM.Content.Controllers.V1.Admin
{
    [Authorize]
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
