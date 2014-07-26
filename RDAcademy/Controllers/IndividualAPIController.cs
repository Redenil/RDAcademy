﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RDAcademy.Models;
using RDAcademy.DAL;

namespace RDAcademy.Controllers
{
    public class IndividualAPIController : ApiController
    {
        //static readonly IIndividualRepository _individualRepository = new IndividualRepository();
        private IndividualContext db = new IndividualContext();

        // GET api/Individual
        public IEnumerable<Individual> GetIndividuals()
        {
            return db.Individuals.AsEnumerable();
        }

        // GET api/Individual/5
        public Individual GetIndividual(int id)
        {
            Individual individual = db.Individuals.Find(id);
            if (individual == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return individual;
        }

        // PUT api/Individual/5
        public HttpResponseMessage PutIndividual(int id, Individual individual)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != individual.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(individual).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Individual
        public HttpResponseMessage PostIndividual(Individual individual)
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
        }

        // DELETE api/Individual/5
        public HttpResponseMessage DeleteIndividual(int id)
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
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}