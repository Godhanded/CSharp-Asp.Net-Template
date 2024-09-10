using CSharp_Asp.Net_Template.Application.Shared.Interfaces;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class FailureResponseDto<T> : IResponseDto<T>
    {
        public string Message { get; set; } = "Request Failed";
        public bool Success { get; set; } = false;
        public T? Data { get; set; } = default;
    }
}
