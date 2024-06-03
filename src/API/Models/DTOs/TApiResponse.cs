namespace API.Models.DTOs
{
    public class ApiResponse<T> 
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public ApiResponse(int statusCode, T data) 
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}
