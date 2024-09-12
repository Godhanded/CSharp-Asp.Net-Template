using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class ModelStateErrorResponseDto
    {
        public int StatusCode { get; init; } = (int)HttpStatusCode.BadRequest;
        public List<ModelStateErrorDto> Errors { get; init; } = [];
    }
}
