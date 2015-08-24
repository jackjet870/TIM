using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIM.Data.Repositories.Interface
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetAll();

        Team GetById(int id);

        bool Add(Team team);

        bool Delete(decimal id);

        bool Update(Team team);
    }
}
