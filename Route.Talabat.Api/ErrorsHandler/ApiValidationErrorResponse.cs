namespace Route.Talabat.Api.ErrorsHandler
{
	public class ApiValidationErrorResponse : ApiResponse
	{
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
