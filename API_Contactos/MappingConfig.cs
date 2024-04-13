using API_Contactos.Models;
using API_Contactos.Models.DTOs.Contact;
using API_Contactos.Models.DTOs.PhoneNumberss;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Contactos
{
    public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			        CreateMap<Contact, ContactDTO>()
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));
    

			CreateMap<Contact, ContactCreateDTO>().ReverseMap();
			CreateMap<Contact, ContactUpdateDTO>().ReverseMap();


			CreateMap<PhoneNumbers, PhoneNumbersDTO>().ReverseMap();
			CreateMap<PhoneNumbers, PhoneNumbersCreateDTO>().ReverseMap();
			CreateMap<PhoneNumbers, PhoneNumbersUpdateDTO>().ReverseMap();
			CreateMap<PhoneNumbers, MainPhoneNumberCreateDTO>().ReverseMap();


		}
	}
}
