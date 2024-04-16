using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Infrastructure.Data;

namespace Route.Talabat.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		private readonly StoreContext _dbcontext;

		public BuggyController(StoreContext dbcontext)
        {
			_dbcontext = dbcontext;
		}

		[HttpGet("NotFound")]
		public ActionResult GetNotFoundError()
		{
			var product = _dbcontext.Products.Find(100);
			if (product is null)
				return NotFound(new ApiResponse(404));

			return Ok(product);
		}

		[HttpGet("ServerError")]
		public ActionResult GetServerError()
		{
			var product = _dbcontext.Products.Find(100);
			return GetServerError();
		}

		[HttpGet("BadRequest")]
		public ActionResult GetBadRequestError() 
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("Unuthorized")]
		public ActionResult GetUnAuthorizedError()
		{
			return Unauthorized(new ApiResponse(401));
		}
    }
}
