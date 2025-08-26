using AutoMapper;
using InvoiceApp.DTOs;
using InvoiceApp.Models;

namespace InvoiceApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
        }
    }
}
