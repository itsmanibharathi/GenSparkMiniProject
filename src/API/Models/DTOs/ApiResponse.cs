using System.Diagnostics;

namespace API.Models.DTOs
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        

        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
