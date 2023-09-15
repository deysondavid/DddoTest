using AutoMapper;
using DddoTest.DTOs;
using DddoTest.Models;
using System.Globalization;
namespace DddoTest.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Moneda
            CreateMap<MonedaDdo, MonedaDTO>().ReverseMap();
            #endregion

            #region Sucursal;
            CreateMap<SucursalesDdo, SucursalDTO>()
             .ForMember(destino => destino.NombreMoneda, opt => opt.MapFrom(origen => origen.IdMonedaNavigation.Descripcion))
             .ForMember(destino => destino.FechaCreacion, opt => opt.MapFrom(origen => origen.FechaCreacion))
             .ForMember(destino => destino.Simbolo, opt => opt.MapFrom(origen => origen.IdMonedaNavigation.Simbolo));


            CreateMap<SucursalDTO, SucursalesDdo>()
                .ForMember(destino =>
                destino.IdMonedaNavigation,
                opt => opt.Ignore()
                );
            #endregion
        }
    }
}
