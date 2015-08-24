using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIM.Data.Repositories.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TIM.Data.Repositories.Implementation
{
    public class AthleteRepository : TimAbstractRepository, IAthleteRepository
    {
        Athlete IAthleteRepository.GetById(int id)
        {
            Task<Athlete> a = GetById(id);
            a.Wait();
            return a.Result;
        }

        IEnumerable<Athlete> IAthleteRepository.GetAll()
        {
            Task<IEnumerable<Athlete>> athletesTask = GetAll();
            return athletesTask.Result;
        }

        Athlete IAthleteRepository.GetByName(string FirstName, string LastName)
        {
            Task<Athlete> athlete = GetByName(FirstName, LastName);
            athlete.Wait();
            return athlete.Result;
        }

        private async Task<Athlete> GetById(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("api/athlete/{0}", id))
                                                    .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Athlete athlete = await response.Content.ReadAsAsync<Athlete>();
                return athlete;
            }
            else
                return null;
        }

        private async Task<IEnumerable<Athlete>> GetAll()
        {
            try
            {
                var response = await _client.GetAsync("/api/athlete/")
                                   .ConfigureAwait(false);

                // never hits line below
                return await response.Content.ReadAsAsync<Athlete[]>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Athlete> GetByName(string FirstName, string LastName)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("api/athlete/{0}/{1}", FirstName, LastName))
                                                                           .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Athlete athlete = await response.Content.ReadAsAsync<Athlete>();                
                return athlete;
            }
            else
                return null;
        }

        bool IAthleteRepository.Add(Athlete athlete)
        {
            Task<bool> addedSuccessfully = Add(athlete);
            return addedSuccessfully.Result;
        }

        private async Task<bool> Add(Athlete athlete)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<Athlete>("api/athlete/", athlete)
                                                    .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
                //return await response.Content.ReadAsAsync<bool>();
            }
            else
                return false;
        }

        bool IAthleteRepository.Delete(decimal id)
        {
            Task<bool> isDeleted = Delete(id);
            return isDeleted.Result;
        }

        private async Task<bool> Delete(decimal id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(string.Format("api/athlete/delete/{0}", id))
                                            .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        bool IAthleteRepository.Update(Athlete athlete)
        {
            Task<bool> isModified = Update(athlete);
            return isModified.Result;
        }

        private async Task<bool> Update(Athlete athlete)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/athlete/", athlete)
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