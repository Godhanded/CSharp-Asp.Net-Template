using System.Net;

namespace CSharp_Asp.Net_Template.Application.Shared.Dtos
{
    public class ModelStateErrorResponseDto
    {
        public int StatusCode { get; init; } = (int)HttpStatusCode.BadRequest;
        public List<ModelStateErrorDto> Errors { get; init; } = [];
    }
}
