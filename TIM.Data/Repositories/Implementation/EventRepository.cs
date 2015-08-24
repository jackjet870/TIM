using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIM.Data.Repositories.Interface;
using System.Threading.Tasks;
using System.Net.Http;
using TIM.Data.ModelClasses.Dto;

namespace TIM.Data.Repositories.Implementation
{
    public class EventRepository : TimAbstractRepository, IEventRepository
    {
        IEnumerable<EventDTO> IEventRepository.GetAll()
        {
            Task<IEnumerable<EventDTO>> events = GetAll();
            return events.Result;
        }

        public async Task<IEnumerable<EventDTO>> GetAll()
        {
            try
            {
                var response = await _client.GetAsync("api/event/")
                                   .ConfigureAwait(false);

                // never hits line below                
                return await response.Content.ReadAsAsync<EventDTO[]>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        EventDTO IEventRepository.GetById(int id)
        {
            Task<EventDTO> ev = GetById(id);
            return ev.Result;
        }

        public async Task<EventDTO> GetById(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("api/event/{0}", id))
                                                            .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                EventDTO events = await response.Content.ReadAsAsync<EventDTO>();
                return events;
            }
            else
                return null;
        }
             

        bool IEventRepository.Add(EventDTO ev)
        {
            Task<bool> addedSuccessfully = Add(ev);
            return addedSuccessfully.Result;            
        }

        private async Task<bool> Add(EventDTO ev)
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync<EventDTO>(string.Format("api/event/"), ev)
                                        .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        bool IEventRepository.Delete(decimal id)
        {
            Task<bool> isDeleted = Delete(id);
            return isDeleted.Result;
        }

        private async Task<bool> Delete(decimal id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(string.Format("api/event/delete/{0}", id))
                                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        bool IEventRepository.Update(EventDTO ev)
        {
            Task<bool> isModified = Update(ev);
            return isModified.Result;
        }

        private async Task<bool> Update(EventDTO ev)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/event/", ev)
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