using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TIM.Data;
using TIM.WebApi.Helpers;

namespace TIM.WebApi.Controllers
{
    [RoutePrefix("api/athlete")]
    public class AthleteController : TimAbstractController
    {
        [HttpGet]
        public IHttpActionResult GetAllAthletes()
        {
            var athleteWithTeamName = _db.Athlete
                .Join(_db.Team, a => a.TeamId, t => t.TeamId, (a, t) => new { a, teamName = t.Name });

            if (athleteWithTeamName != null)
            {
                ICollection<Athlete> athletes = new List<Athlete>();

                foreach (var ath in athleteWithTeamName)                    
                    { 
                        ath.a.TeamName = ath.teamName;
                        athletes.Add(ath.a);
                    }

                return new TIMJsonResult(athletes, Request);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id}", Name = "GetAthleteById")]
        public IHttpActionResult GetById(int id)
        {
            var athleteWithTeamName =  _db.Athlete
                .Join(_db.Team, a => a.TeamId, t => t.TeamId, (a, t) => new { a, teamName = t.Name })
                .Where(a => a.a.AthleteId == id)
                .FirstOrDefault();

            if (athleteWithTeamName != null)
            {
                athleteWithTeamName.a.TeamName = athleteWithTeamName.teamName;
                return Json<Athlete>(athleteWithTeamName.a);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{firstName}/{lastName}")]
        public IHttpActionResult GetByName(string firstName, string lastName)
        {
            var athleteWithTeamName = _db.Athlete
                .Where(a => a.FirstName == firstName && a.LastName == lastName)
                .Join(_db.Team, a => a.TeamId, t => t.TeamId, (a, t) => new { a, teamName = t.Name })                
                .FirstOrDefault();

            if (athleteWithTeamName != null)
            {

                athleteWithTeamName.a.TeamName = athleteWithTeamName.teamName;

                return new TIMJsonResult(athleteWithTeamName.a, Request);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getbyteamid/{id}")]
        public IHttpActionResult GetByTeamId(int id)
        {
            var athleteWithTeamName = _db.Athlete
                .Join(_db.Team, a => a.TeamId, t => t.TeamId, (a, t) => new { a, teamName = t.Name, teamId = t.TeamId })
                .Where(a => a.teamId == id);

            if (athleteWithTeamName != null)
            {
                ICollection<Athlete> athletes = new List<Athlete>();

                foreach (var ath in athleteWithTeamName)
                {
                    ath.a.TeamName = ath.teamName;
                    athletes.Add(ath.a);
                }

                return new TIMJsonResult(athletes, Request);
            }

            return NotFound();
        }

        [HttpPost]
        //[Route("add/")]
        public IHttpActionResult Add(Athlete athlete)
        {
            try
            {
                _db.Athlete.Add(athlete);
                _db.SaveChanges();

                return
                    new ItemCreatedHttpResult<Athlete>(athlete, Request, athlete.AthleteId, "GetAthleteById");
            }
            catch (Exception ex)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Conflict);
                responseMsg.Content = new StringContent(/*ex.Message.ToString()*/"Either duplicate key or duplicate name",
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
                Athlete athlete = _db.Athlete.Find(id);

                if (athlete == null)
                    return NotFound();

                _db.Entry(athlete).State = EntityState.Deleted;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(Athlete athlete)
        {
            try
            {
                Athlete athleteToUpdate = _db.Athlete.Where(a => a.AthleteId == athlete.AthleteId).First();

                athleteToUpdate.FirstName = athlete.FirstName;
                athleteToUpdate.LastName = athlete.LastName;
                athleteToUpdate.Sport = athlete.Sport;
                athleteToUpdate.TeamId = athlete.TeamId;

                
                _db.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
