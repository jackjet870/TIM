using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIM.Data.ModelClasses.Dto;
using TIM.Data.Repositories.Interface;
using TIM.Data.Helpers;

namespace TIM.Data.ModelClasses
{
    public class EventViewModel
    {    
        public EventViewModel()
        { }

        public EventViewModel(EventDTO evDto)
        {
            this.EventId = evDto.EventId;
            this.Name = evDto.Name;
            this.Sport = evDto.Sport;
            this.Longitude = evDto.Longitude;
            this.Latitude = evDto.Latitude;
            this.Individual = evDto.Individual;
            this.StartDate = evDto.StartDate;
            this.EndDate = evDto.EndDate;
            this.AthleteIds = evDto.AthleteIds;
            this.TeamIds = evDto.TeamIds;
            this.UserIds = evDto.UserIds;
        } 

        public EventViewModel(EventDTO evDto, ITeamRepository _teamRepo, IAthleteRepository _athRepo)
        {
            this.EventId = evDto.EventId;
            this.Name = evDto.Name;
            this.Sport = evDto.Sport;
            this.Longitude = evDto.Longitude;
            this.Latitude = evDto.Latitude;
            this.Individual = evDto.Individual;
            this.StartDate = evDto.StartDate;
            this.EndDate = evDto.EndDate;
            this.AthleteIds = evDto.AthleteIds;
            this.TeamIds = evDto.TeamIds;
            this.UserIds = evDto.UserIds;

            this.allTeams = _teamRepo.GetAll();
            this.allAthletes = _athRepo.GetAll();
            this.allSports = ItemListCreator.SportsList();
        }        

        public MultiSelectList AthletesSelectList
        {
            get
            {
                return new MultiSelectList(
                    this.allAthletes.Select(a => new { a.AthleteId, a.FullName })
                    , "AthleteId", "FullName", this.AthleteIds);
            }
        }

        public MultiSelectList TeamsSelectList
        {
            get
            {
                return new MultiSelectList(
                    this.allTeams.Select(a => new { a.TeamId, a.Name })
                    , "TeamId", "Name", this.TeamIds);
            }
        }

        public MultiSelectList SportSelectList
        {
            get
            {
                return new MultiSelectList(this.allSports, this.Sport);
            }
        }

        public static IEnumerable<EventViewModel> CreateViewModels(IEnumerable<EventDTO> eventDtos)
        {
            if (eventDtos != null && eventDtos.Count() > 0)
            {
                int count = eventDtos.Count();
                EventViewModel[] events = new EventViewModel[count];

                for (int i = 0; i < count; i++)
                    events[i] = (new EventViewModel(eventDtos.ElementAt(i)));

                return events;
            }

            return null;
        }

        public decimal EventId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Longitude")]
        [DataType("numeric")]
        public double? Longitude { get; set; }

        [DisplayName("Latitude")]
        public double? Latitude { get; set; }

        [Required]
        [DisplayName("Sport")]
        public string Sport { get; set; }

        [DisplayName("Is individual")]
        public bool Individual { get; set; }

        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime EndDate { get; set; }

        public String StartDateShort
        {
            get
            {
                return this.StartDate.ToShortDateString();
            }
        }

        public String EndDateShort
        {
            get
            {
                return this.EndDate.ToShortDateString();
            }
        }

        public IEnumerable<Team> allTeams { get; set; }

        public IEnumerable<Athlete> allAthletes { get; set; }

        public IEnumerable<string> allSports { get; set; }

        [DisplayName("Teams")]
        public ICollection<decimal> TeamIds { get; set; }

        [DisplayName("Athletes")]
        public ICollection<decimal> AthleteIds { get; set; }

        [DisplayName("Users")]
        public ICollection<decimal> UserIds { get; set; }       
    }
}