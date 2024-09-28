using AutoMapper;
using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.PL.ViewModels.Departments;

namespace LinkDev.Ikea.PL.Controllers.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            #region Employee

            #endregion

            #region Department
            CreateMap<DepartmentDetailsDto, DepartmentViewModel>();// Making Mapping
                //.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name));


            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();



            #endregion
        }
    }
}
