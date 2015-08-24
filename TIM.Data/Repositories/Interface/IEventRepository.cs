using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIM.Data.ModelClasses.Dto;

namespace TIM.Data.Repositories.Interface
{
    public interface IEventRepository
    {
        IEnumerable<EventDTO> GetAll();

        EventDTO GetById(int id);

        bool Add(EventDTO ev);

        bool Delete(decimal id);

        bool Update(EventDTO ev);
    }
}
