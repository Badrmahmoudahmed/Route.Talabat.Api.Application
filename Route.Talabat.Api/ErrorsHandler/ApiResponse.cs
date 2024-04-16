using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Route.Talabat.Api.ErrorsHandler
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statuscode , string? msg = null)
        {
            StatusCode = statuscode;
            Message = msg ?? GetDefaultMessage(statuscode);
        }

        private string GetDefaultMessage(int statuscode)
        {
            return statuscode switch
            {
                400 => "A Bad Request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career thange",
                _ => null!

            };

		}

        
    }
}
