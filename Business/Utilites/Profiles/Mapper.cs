
using AutoMapper;
using Core.Entities.Concrete;
using Entities.Concrete.Models;
using Entities.Dtos.Holiday;
using Entities.Dtos.ResHistory;
using Entities.Dtos.Time;

public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Home, HomeGetDto>();
            CreateMap<HomePostDto, Home>();
            CreateMap<About, AboutGetDto>();
            CreateMap<AboutPostDto, About>();
            CreateMap<DoctorPostDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>().ReverseMap();
            CreateMap<Service, ServiceGetDto>();
            CreateMap<Position, PositionGetDto>();
            CreateMap<PositionPostDto, Position>();
            CreateMap<ServicePostDto, Service>();
            CreateMap<SettingPostDto, Setting>();
            CreateMap<Setting, SettingGetDto>();
            CreateMap<Message, MessageGetDto>();
            CreateMap<ResHistory, ResGetDto>();
            CreateMap<ResPostDto, ResHistory>();
            CreateMap<Holiday, HolidayGetDto>();
            CreateMap<MessagePostDto, Message>();
            CreateMap<Time, TimeGetDto>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }

