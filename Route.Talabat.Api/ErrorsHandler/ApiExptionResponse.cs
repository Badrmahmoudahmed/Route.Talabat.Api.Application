namespace Route.Talabat.Api.ErrorsHandler
{
	public class ApiExptionResponse : ApiResponse
	{
        public string? Details { get; set; }
        public ApiExptionResponse(int status , string msg = null , string details = null):base(status ,msg)
        {

            Details = details ;
        }
    }
}
