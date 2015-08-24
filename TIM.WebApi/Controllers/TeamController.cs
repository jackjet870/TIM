using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TIM.Data;
using TIM.WebApi.Helpers;

namespace TIM.WebApi.Controllers
{
    [RoutePrefix("api/team")]
    public class TeamController : TimAbstractController
    {
        public IHttpActionResult GetAll()
        {
            return new TIMJsonResult(_db.Team, Request);
        }

        [HttpGet]
        [Route("{id}", Name = "GetTeamById")]
        public IHttpActionResult GetById(int id)
        {
            Team team =_db.Team.Where(t => t.TeamId == id).FirstOrDefault();
            if (team != null)
                return new TIMJsonResult(team, Request);

            return NotFound();
        }

        //[Route("{name}")]
        //public IHttpActionResult GetByName(string name)
        //{
        //    Team team = _db.Team.Where(t => t.Name == name).FirstOrDefault();

        //    if (team != null)
        //        return new TIMJsonResult(team, Request);

        //    return NotFound();
        //}

        [HttpPost]
        public IHttpActionResult Add(Team team)
        {
            try
            {
                _db.Team.Add(team);
                _db.SaveChanges();

                return
                    new ItemCreatedHttpResult<Team>(team, Request, team.TeamId, "GetTeamById");
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
                Team team = _db.Team.Find(id);

                if (team == null)
                    return NotFound();

                _db.Entry(team).State = EntityState.Deleted;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(Team team)
        {
            try
            {
                Team teamToUpdate = _db.Team.Where(t => t.TeamId == team.TeamId).First();

                teamToUpdate.Name = team.Name;
                teamToUpdate.Sport = team.Sport;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
