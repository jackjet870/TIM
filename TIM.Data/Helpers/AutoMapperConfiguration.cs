using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TIM.Data.ModelClasses.Dto;
using TIM.Data.ModelClasses;

namespace TIM.Data.Helpers
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<EventViewModel, EventDTO>();
            Mapper.CreateMap<EventDTO, Event>();
            //Mapper.CreateMap<EventDTO, EventViewModel>();
            Mapper.CreateMap<EventViewModel, EventDTO>();
            Mapper.CreateMap<Event, EventDTO>();
        }
    }
}