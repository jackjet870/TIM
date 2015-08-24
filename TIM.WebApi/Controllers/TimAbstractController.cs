using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TIM.Data;

namespace TIM.WebApi.Controllers
{
    public abstract class TimAbstractController : ApiController
    {
        protected TimDbContext _db;

        public TimAbstractController()
        {
            _db = new TimDbContext();
            _db.Configuration.ProxyCreationEnabled = false;
        }
    }
}
