using AutoMapper;
using WebApiSmartClinic.Dto.Configuracoes;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.MappingProfiles;

public class ConfiguracoesProfile : Profile
{
    public ConfiguracoesProfile()
    {
        CreateMap<ConfiguracoesEdicaoDto, EmpresaModel>()
            .ForMember(d => d.CNPJEmpresaMatriz, o => o.MapFrom(s => s.CnpjEmpresaMatriz))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<EmpresaModel, ConfiguracoesEdicaoDto>()
            .ForMember(d => d.CnpjEmpresaMatriz, o => o.MapFrom(s => s.CNPJEmpresaMatriz));
    }
}