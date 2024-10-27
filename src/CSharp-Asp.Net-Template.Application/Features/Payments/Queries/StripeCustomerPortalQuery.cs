using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Queries
{
    public class StripeCustomerPortalQuery : IRequest<IResponseDto<string?>>
    {
    }
}
