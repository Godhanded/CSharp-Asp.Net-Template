using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.Payments.Dtos;
using CSharp_Asp.Net_Template.Domain.Entities;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Mappers
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<InvoiceDto, Invoice>()
                .ReverseMap();

            CreateMap<Stripe.Invoice, Invoice>()
                .ForMember(i => i.PeriodEnd, opt => opt.MapFrom(si => si.Lines.First().Period.End))
                .ForMember(i => i.PeriodStart, opt => opt.MapFrom(si => si.Lines.First().Period.Start))
                .ForMember(i => i.ProductId, opt => opt.MapFrom(si => si.Lines.First().Plan.ProductId))
                .ForMember(i => i.WebhooksDeliveredAt, opt => opt.MapFrom(si => si.WebhooksDeliveredAt))
                .ReverseMap();
        }
    }
}
