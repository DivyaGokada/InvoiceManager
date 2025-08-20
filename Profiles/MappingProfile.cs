using AutoMapper;
using InvoiceApp.Dtos.Invoice;
using InvoiceApp.Models;

namespace InvoiceApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }
    }
}
