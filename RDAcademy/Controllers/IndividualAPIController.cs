namespace RDAcademy.Controllers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using RDAcademy.ActionFilter;
    using RDAcademy.DAL;
    using RDAcademy.Models;
    using AttributeRouting.Web.Http;

    public class IndividualAPIController : ApiController
    {
        //static readonly IIndividualRepository _individualRepository = new IndividualRepository();
        private IndividualContext db = new IndividualContext();

        // GET api/Individual
        [BasicAuthorized]
        public IEnumerable<Individual> Get()
        {
            return db.Individuals.AsEnumerable();
        }

        // GET api/Individual/5
        //[HttpRoute("api/IndividualAPI/{id:int}")]
        public Individual Get(int id)
        {
            Individual individual = db.Individuals.Find(id);
            if (individual == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return individual;
        }

        // GET api/Individual/Guy
        //[HttpRoute("api/IndividualAPI/{name}")]
        public Individual Get(string name)
        {
            Individual individual = db.Individuals.SingleOrDefault(p => p.LastName == name);
            if (individual == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return individual;
        }

        //[HttpRoute("api/IndividualAPI/{id}/Contracts")]
        //public IEnumerable<Contract> GetContractByIndividual(int id)
        //{
        //    var contracts = db.Contracts.Where(c => c.Individual.Id == id).AsEnumerable();
        //    return contracts;
        //}

        // PUT api/Individual/5
        /// <summary>
        /// Puts the individual.
        /// </summary>
        /// <param name="individual">The individual.</param>
        /// <returns></returns>
        [CultureActionFilter("en-GB")]
        [BasicAuthorized]
        public HttpResponseMessage Put(Individual individual)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Entry(individual).State = EntityState.Modified;

            try
            {
                db.Individuals.Add(individual);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Individual
        /*        public HttpResponseMessage PostIndividual(Individual individual)
                {
                    if (ModelState.IsValid)
                    {
                        db.Individuals.Add(individual);
                        db.SaveChanges();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, individual);
                        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = individual.Id }));
                        return response;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }*/

        // DELETE api/Individual/5
        /*        public HttpResponseMessage DeleteIndividual(int id)
                {
                    Individual individual = db.Individuals.Find(id);
                    if (individual == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    db.Individuals.Remove(individual);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, individual);
                }*/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}