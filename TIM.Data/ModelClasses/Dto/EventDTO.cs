using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIM.Data.ModelClasses.Dto
{
    public class EventDTO
    {
        public static IEnumerable<EventDTO> CreateArray(IEnumerable<Event> events)
        {
            int count = events.Count();

            if (count > 0)
            {
                EventDTO[] eventDtos = new EventDTO[count];

                for (int i = 0; i < count; i++)
                    eventDtos[i] = new EventDTO(events.ElementAt(i));              

                return eventDtos;
            }
            else
                return null;
        }

        public EventDTO() { }

        public EventDTO(EventViewModel ev)
        {
            if (ev != null)
            {
                this.EventId = ev.EventId;
                this.Name = ev.Name;
                this.Latitude = ev.Latitude;
                this.Longitude = ev.Longitude;
                this.Sport = ev.Sport;
                this.Individual = ev.Individual;
                this.StartDate = ev.StartDate;
                this.EndDate = ev.EndDate;

                if (ev.AthleteIds != null && ev.AthleteIds.Count > 0)
                {
                    this.AthleteIds = new List<decimal>();

                    foreach (var ath in ev.AthleteIds)
                        this.AthleteIds.Add(ath);
                }

                if (ev.TeamIds != null && ev.TeamIds.Count > 0)
                {
                    this.TeamIds = new List<decimal>();

                    foreach (var team in ev.TeamIds)
                        this.TeamIds.Add(team);
                }

                if (ev.UserIds != null && ev.UserIds.Count > 0)
                {
                    this.UserIds = new List<decimal>();

                    foreach (var user in ev.UserIds)
                        this.UserIds.Add(user);
                }
            }
        }

        public EventDTO(Event ev)
        {
            if (ev != null)
            {
                this.EventId = ev.EventId;
                this.Name = ev.Name;
                this.Latitude = ev.Latitude;
                this.Longitude = ev.Longitude;
                this.Sport = ev.Sport;
                this.Individual = ev.Individual;
                this.StartDate = ev.StartDate;
                this.EndDate = ev.EndDate;

                if (ev.Athlete != null && ev.Athlete.Count > 0)
                {
                    this.AthleteIds = new List<decimal>();

                    foreach (var ath in ev.Athlete)
                        this.AthleteIds.Add(ath.AthleteId);
                }

                if (ev.Team != null && ev.Team.Count > 0)
                {
                    this.TeamIds = new List<decimal>();

                    foreach (var team in ev.Team)
                        this.TeamIds.Add(team.TeamId);
                }

                if (ev.User != null && ev.User.Count > 0)
                {
                    this.UserIds = new List<decimal>();

                    foreach (var user in ev.User)
                        this.UserIds.Add(user.User_ID);
                }
            }
        }

        public decimal EventId { get; set; }

        public string Name { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public string Sport { get; set; }

        public bool Individual { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<decimal> TeamIds { get; set; }

        public ICollection<decimal> AthleteIds { get; set; }

        public ICollection<decimal> UserIds { get; set; }
    }
}