using System.Diagnostics;

namespace API.Models.DTOs
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Error Model
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ErrorDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        /// <summary>
        /// Debug Error Model 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>   
        public ErrorDto(int statusCode, Exception ex , string contrller)
        {
            StatusCode = statusCode;
            Message = ex.Message;
            Debug.WriteLine($"[{contrller}] ::  Error: {statusCode} - {Message}");
        }
    }
}
