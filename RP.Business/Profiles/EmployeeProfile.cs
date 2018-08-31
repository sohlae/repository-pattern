using AutoMapper;
using RP.Business.Dto;
using RP.Domain;

namespace RP.Business.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dto => dto.Id, from => from.MapFrom(ent => ent.Id))
                .ForMember(dto => dto.FirstName, from => from.MapFrom(ent => ent.FirstName))
                .ForMember(dto => dto.LastName, from => from.MapFrom(ent => ent.LastName))
                .ForMember(dto => dto.BirthDate, from => from.MapFrom(ent => ent.BirthDate));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(ent => ent.Id, from => from.MapFrom(dto => dto.Id))
                .ForMember(ent => ent.FirstName, from => from.MapFrom(dto => dto.FirstName))
                .ForMember(ent => ent.LastName, from => from.MapFrom(dto => dto.LastName))
                .ForMember(ent => ent.BirthDate, from => from.MapFrom(dto => dto.BirthDate));
        }
    }
}
