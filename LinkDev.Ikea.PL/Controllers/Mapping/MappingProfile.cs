using AutoMapper;
using LinkDev.Ikea.BLL.Models.Departments;
using LinkDev.Ikea.BLL.Models.Employees;
using LinkDev.Ikea.PL.ViewModels.Departments;
using LinkDev.Ikea.PL.ViewModels.Employees;

namespace LinkDev.Ikea.PL.Controllers.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            #region Employee
            CreateMap<EmployeeDetailsDto, EmployeeViewModel>();// Making Mapping
                                                                   //.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name));


            CreateMap<EmployeeViewModel, UpdatedEmployeeDto>();

            CreateMap<EmployeeViewModel, CreatedEmployeeDto>();

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
