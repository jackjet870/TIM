using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TIM.Data;
using TIM.Data.ModelClasses.Dto;
using TIM.WebApi.Helpers;
using TIM.Data.Helpers;

namespace TIM.WebApi.Controllers
{
    [RoutePrefix("api/event")]
    public class EventController : TimAbstractController
    {
        public IHttpActionResult GetAll()
        {
            IEnumerable<Event> events = _db.Event;

            foreach(var ev in events)
            {
                _db.Entry(ev).Collection(e => e.Athlete).Load();
                _db.Entry(ev).Collection(e => e.Team).Load();
                _db.Entry(ev).Collection(e => e.User).Load();
            }

            return new TIMJsonResult(EventDTO.CreateArray(events), Request);
        }

        [HttpGet]
        [Route("{id}", Name = "GetEventById")]
        public IHttpActionResult GetById(int id)
        {
            Event ev = _db.Event.Where(e => e.EventId == id).FirstOrDefault();

            _db.Entry(ev).Collection(e => e.Athlete).Load();
            _db.Entry(ev).Collection(e => e.Team).Load();
            _db.Entry(ev).Collection(e => e.User).Load();

            if (ev != null)
                return new TIMJsonResult(new EventDTO(ev), Request);

            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult Add(EventDTO ev)
        {
            try
            {
                Event eventEntity = Mapper.Map<EventDTO, Event>(ev);
                _db.Event.Add(eventEntity);
                _db.SaveChanges();

                InsertIntoTable(ev.AthleteIds, eventEntity.EventId, "AthletesEvents(EventId, AthleteId)");
                InsertIntoTable(ev.TeamIds, eventEntity.EventId, "TeamsEvents(EventId, TeamId)");
                InsertIntoTable(ev.UserIds, eventEntity.EventId, "UsersFavEvents(EventId, AthleteId)");

                //if (ev.TeamIds != null && ev.TeamIds.Count > 0)
                //{
                //    StringBuilder teamsQuery = new StringBuilder("INSERT INTO TeamsEvents(EventId, TeamId)");
                //    teamsQuery.Append(" VALUES");

                //    for (int i = 0; i < ev.AthleteIds.Count; i++)
                //    {
                //        if (i != 0)
                //            teamsQuery.Append(',');

                //        teamsQuery.Append("(" + eventEntity.EventId.ToStringIgnoreFraction() + ", " + ev.TeamIds.ElementAt(i).ToStringIgnoreFraction() + ")");
                //    }

                //    _db.Database.ExecuteSqlCommand(teamsQuery.ToString());
                //}

                //if (ev.UserIds != null && ev.UserIds.Count > 0)
                //{
                //    StringBuilder usersQuery = new StringBuilder("INSERT INTO UsersFavEvents(UserId, EventId)");
                //    usersQuery.Append(" VALUES");

                //    for (int i = 0; i < ev.AthleteIds.Count; i++)
                //    {
                //        if (i != 0)
                //            usersQuery.Append(',');

                //        usersQuery.Append("(" + eventEntity.EventId.ToStringIgnoreFraction() + ", " + ev.UserIds.ElementAt(i).ToStringIgnoreFraction() + ")");
                //    }

                //    _db.Database.ExecuteSqlCommand(usersQuery.ToString());
                //}
                
                _db.SaveChanges();
                
                return
                    new ItemCreatedHttpResult<Event>(eventEntity, Request, eventEntity.EventId, "GetEventById");
            }
            catch (Exception ex)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Conflict);
                responseMsg.Content = new StringContent(/*ex.Message.ToString()*/"Something went really fucking wrong.",
                                                            Encoding.UTF8, "text/plain");
                IHttpActionResult response = ResponseMessage(responseMsg);

                return response;
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Event ev = _db.Event.Find(id);

                if (ev == null)
                    return NotFound();

                _db.Entry(ev).State = EntityState.Deleted;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(EventDTO ev)
        {
            try
            {
                string query = "DELETE FROM TeamsEvents WHERE EventId=" + ev.EventId.ToStringIgnoreFraction();
                _db.Database.ExecuteSqlCommand(query);
                InsertIntoTable(ev.TeamIds, ev.EventId, "TeamsEvents(EventId, TeamId)");

                query = "DELETE FROM AthletesEvents WHERE EventId=" + ev.EventId.ToStringIgnoreFraction();
                _db.Database.ExecuteSqlCommand(query);
                InsertIntoTable(ev.AthleteIds, ev.EventId, "AthletesEvents(EventId, AthleteId)");

                query = "DELETE FROM UsersFavEvents WHERE EventId=" + ev.EventId.ToStringIgnoreFraction();
                _db.Database.ExecuteSqlCommand(query);
                InsertIntoTable(ev.UserIds, ev.EventId, "UsersFavEvents(EventId, UserId)");

                Event evToUpdate = _db.Event.Where(e => e.EventId == ev.EventId).First();

                evToUpdate.Name = ev.Name;
                evToUpdate.Sport = ev.Sport;
                evToUpdate.Individual = ev.Individual;
                evToUpdate.Latitude = ev.Latitude;
                evToUpdate.Longitude = ev.Longitude;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        
        private void InsertIntoTable(IEnumerable<decimal> ids, decimal eventId, string tableNameAndFields)
        {
            if (ids != null && ids.Count() > 0)
            {
                StringBuilder athletesQuery = new StringBuilder("INSERT INTO " + tableNameAndFields);
                athletesQuery.Append(" VALUES");

                for (int i = 0; i < ids.Count(); i++)
                {
                    if (i != 0)
                        athletesQuery.Append(',');

                    athletesQuery.Append("(" + eventId.ToStringIgnoreFraction() + ", " + ids.ElementAt(i).ToStringIgnoreFraction() + ")");
                }

                _db.Database.ExecuteSqlCommand(athletesQuery.ToString());
            }
        }
    }
}
