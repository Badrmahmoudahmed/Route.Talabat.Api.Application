using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.ErrorsHandler;

namespace Route.Talabat.Api.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings (IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{

		public ActionResult Error(int code)
		{
			if (code == 400)
				return BadRequest(new ApiResponse(400));
			else if (code == 404)
				return NotFound(new ApiResponse(404));
			else 
				return StatusCode(code);
		}
	}
}
