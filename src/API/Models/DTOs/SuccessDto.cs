namespace API.Models.DTOs
{
    public class SuccessDto
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode = 200; 
        public SuccessDto(string message)
        {
            Message = message;
        }
        public SuccessDto(object obj, string? message)
        {
            Data = obj;
            Message = message;
        }
    }
}
