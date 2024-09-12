namespace CSharp_Asp.Net_Template.Application.Shared.Interfaces
{
    public interface IResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
