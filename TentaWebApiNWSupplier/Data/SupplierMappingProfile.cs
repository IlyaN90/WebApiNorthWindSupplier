using AutoMapper;
using TentaWebApiNWSupplier.Models;
using TentaWebApiNWSupplier.ViewModels;

namespace TentaWebApiNWSupplier.Data
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<Supplier, SupplierMapperModel>().ReverseMap(); 
        }
    }
}