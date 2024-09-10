using CSharp_Asp.Net_Template.Application.Shared.Interfaces;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class SuccessResponseDto<T> : IResponseDto<T>
    {
        public string Message { get; set; } = "Request Successfull";
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
    }
}
