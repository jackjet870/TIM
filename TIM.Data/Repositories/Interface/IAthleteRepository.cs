using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
//using System.ServiceModel;

namespace TIM.Data.Repositories.Interface
{    
    public interface IAthleteRepository
    {
        Athlete GetById(int id);

        IEnumerable<Athlete> GetAll();

        Athlete GetByName(string FirstName, string LastName);

        bool Add(Athlete athlete);

        bool Delete(decimal id);

        bool Update(Athlete athlete);
    }
}
