using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TIM.Data.Repositories.Interface;

namespace TIM.Data.Repositories.Implementation
{
    public class TeamRepository : TimAbstractRepository, ITeamRepository
    {
        IEnumerable<Team> ITeamRepository.GetAll()
        {
            Task<IEnumerable<Team>> team = GetAll();
            return team.Result;
        }

        private async Task<IEnumerable<Team>> GetAll()
        {
            try
            {
                var response = await _client.GetAsync("/api/team/")
                                   .ConfigureAwait(false);

                // never hits line below
                return await response.Content.ReadAsAsync<Team[]>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Team ITeamRepository.GetById(int id)
        {
            Task<Team> team = GetById(id);
            return team.Result;
        }

        private async Task<Team> GetById(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("api/team/{0}", id))
                                                            .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Team team = await response.Content.ReadAsAsync<Team>();
                return team;
            }

            return null;
        }
                        
        bool ITeamRepository.Add(Team team)
        {
            Task<bool> addedSuccessfully = Add(team);
            return addedSuccessfully.Result;
        }

        private async Task<bool> Add(Team team)
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync<Team>(string.Format("/api/team/"), team)
                            .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
                //return await response.Content.ReadAsAsync<bool>();
            }
            else
                return false;
        }

        bool ITeamRepository.Delete(decimal id)
        {
            Task<bool> isDeleted = Delete(id);
            return isDeleted.Result;
        }

        private async Task<bool> Delete(decimal id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(string.Format("api/team/delete/{0}", id))
                                            .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        bool ITeamRepository.Update(Team team)
        {
            Task<bool> isModified = Update(team);
            return isModified.Result;
        }

        private async Task<bool> Update(Team team)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/team/", team)
                    .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }
    }
}