using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RDAcademy.Controllers
{
    using System.Web.Http.OData;

    using RDAcademy.DAL;
    using RDAcademy.Models;

    public class IndividualsController : ODataController
    {
        private IndividualContext db = new IndividualContext();

        // GET api/<controller>
        [Queryable]
        public IQueryable<Individual> GetIndividuals()
        {
            return db.Individuals;
        }
    }
}