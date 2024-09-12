using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class SuccessResponseDto<T>(
        T? data,
        string message = "Request Successfull",
        int statusCode = StatusCodes.Status200OK,
        bool success = true) : IResponseDto<T>
    {
        [JsonIgnore]
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public bool Success { get; set; } = success;
        public T? Data { get; set; } = data;
    }
}
