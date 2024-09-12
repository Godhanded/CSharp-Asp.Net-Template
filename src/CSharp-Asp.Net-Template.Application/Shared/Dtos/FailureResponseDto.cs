using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class FailureResponseDto<T>(T? data = default, string message = "Request Failed", int statusCode = StatusCodes.Status404NotFound, bool success = false) : IResponseDto<T>
    {
        [JsonIgnore]
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public bool Success { get; set; } = success;
        public T? Data { get; set; } = data;
    }
}
