using System;
using System.Linq;
using AutoMapper;
using Rost.Common.Enums;
using Rost.Repository.Domain;
using Rost.WebApi.Models.Account;
using Rost.WebApi.Models.Career;
using Rost.WebApi.Models.CareerDirection;
using Rost.WebApi.Models.Child;
using Rost.WebApi.Models.City;
using Rost.WebApi.Models.Community;
using Rost.WebApi.Models.District;
using Rost.WebApi.Models.Education;
using Rost.WebApi.Models.Institution;
using Rost.WebApi.Models.Invitation;
using Rost.WebApi.Models.MunicipalUnion;
using Rost.WebApi.Models.Profile;
using Rost.WebApi.Models.Structure;

namespace Rost.WebApi.Mappings
{
    /// <summary>
    /// Mapping profile
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Career, CareerModel>().ForMember(dest => dest.IsSelected, opt => opt.Ignore());
            CreateMap<CareerModel, Career>();
            CreateMap<CareerDirection, CareerDirectionModel>();
            CreateMap<CareerDirectionModel, CareerDirection>();
            CreateMap<City, CityModel>().ReverseMap();
            CreateMap<MunicipalUnion, MunicipalUnionModel>().ReverseMap();
            CreateMap<MunicipalUnion, MunicipalUnionCreateModel>().ReverseMap();
            CreateMap<District, DistrictModel>().ReverseMap();
            CreateMap<DistrictCreateModel, District>().ReverseMap();
            CreateMap<ApplicationUser, ProfileDetailModel>()
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(
                        src => src.City != null ? src.City.Name : string.Empty))
                .ForMember(dest => dest.MainPhoto,
                    opt => opt.MapFrom(
                        dest => dest.Photos != null ? dest.Photos.Where(t => t.IsMain).Select(t => t.Url).FirstOrDefault() : String.Empty))
                .ForMember(opt => opt.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumberDisplay))
                .ReverseMap();
            CreateMap<ApplicationUser, ProfileEditModel>()
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumberDisplay))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
            

            CreateMap<Child, ChildrenListModel>()
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumberDisplay))
                .ForMember(dest => dest.Careers, opt => opt.MapFrom(src => string.Join(", ", src.Careers.Select(r => r.Name)).TrimEnd(new char[]
                    { ' ', ',' })))
                ;

            CreateMap<Child, ChildEditModel>();
            
            CreateMap<ChildEditModel, Child>()
                .ForMember(dest => dest.PersonalNumber, opt => opt.Ignore());
            
            CreateMap<ChildAddModel, Child>()
                .ForMember(dest => dest.PersonalNumber, opt => opt.Ignore());

            CreateMap<ParentSimpleStructure, ParentStructure>().ReverseMap();
            CreateMap<ChildSimpleStructure, SubStucture>().ReverseMap();
            CreateMap<StructureCreateModel, Structure>().ReverseMap();
            CreateMap<EducationModel, Education>().ReverseMap();
            CreateMap<ApplicationUserModel, ApplicationUser>().ReverseMap();
            
            /* NOTE: ReverseMap вызывает какой-то side-effect с полями, имеющими пользовательский тип и значение null.
             Вместо того, чтобы после маппинга в destination-свойстве было значение null, он создает объект этого типа с пустыми полями.
             Если вместо ReverseMap() обратный мэппинг задать явно, то всё работает, как положено.
             */
            CreateMap<InstitutionAdminModel, Institution>();
            CreateMap<Institution, InstitutionAdminModel>();

            CreateMap<Institution, InstitutionEditModel>();
            CreateMap<InstitutionEditModel, Institution>();
            CreateMap<Institution, InstitutionListModel>();
            CreateMap<InstitutionListModel, Institution>();
            CreateMap<Institution, InstitutionDetailsModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != null ? src.City.Name : string.Empty))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District != null ? src.District.Name : string.Empty))
                .ForMember(dest => dest.MunicipalUnion, opt => opt.MapFrom(src => src.MunicipalUnion != null ? src.MunicipalUnion.Name : string.Empty))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education != null ? src.Education.Name : string.Empty))
                .ForMember(dest => dest.AdminUser, opt => opt.MapFrom(src => src.Employees.Where(t => t.EmployeeRole == EmployeeRole.GlobalAdmin).Select(t => $"{t.Name} {t.Surname}").FirstOrDefault()))
                .ForMember(dest => dest.MainPhoto, opt => opt.MapFrom(src => src.Employees.Where(t => t.EmployeeRole == EmployeeRole.GlobalAdmin).Select(t => t.Photos.Where(p => p.IsMain).Select(p => p.Url).FirstOrDefault()).FirstOrDefault()))
                ;
            CreateMap<ApplicationUser, EmployeeListModel>()
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumberDisplay))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeRole))
                .ForMember(dest => dest.MainPhoto,
                    opt => opt.MapFrom(
                        dest => dest.Photos != null ? dest.Photos.Where(t => t.IsMain).Select(t => t.Url).FirstOrDefault() : String.Empty))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Position) ? src.Position : src.EmployeeRole == EmployeeRole.Admin ? "Администратор учреждения" : src.EmployeeRole == EmployeeRole.GlobalAdmin ? "Главный администратор учреждения" : "Сотрудник учреждения"))
                ;

            CreateMap<Community, CommunityListModel>()
                .ForMember(dest => dest.Career, opt => opt.MapFrom(src => src.Career.Name));

            CreateMap<Subscription, CommunityListModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CommunityId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Community.Name))
                .ForMember(dest => dest.Career, opt => opt.MapFrom(src => src.Community.Career.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Community.Image));

            CreateMap<Invitation, InvitationListModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Community.Id, opt => opt.MapFrom(src => src.CommunityId))
                .ForPath(dest => dest.Community.Name, opt => opt.MapFrom(src => src.Community.Name))
                .ForPath(dest => dest.Community.Career, opt => opt.MapFrom(src => src.Community.Career.Name))
                .ForPath(dest => dest.Community.Image, opt => opt.MapFrom(src => src.Community.Image));
        }
    }
}