using RDAcademy.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RDAcademy.Models
{ 
    public class IndividualRepository : IIndividualRepository
    {
        IndividualContext context = new IndividualContext();

        public IQueryable<Individual> All
        {
            get { return context.Individuals; }
        }

        public IQueryable<Individual> AllIncluding(params Expression<Func<Individual, object>>[] includeProperties)
        {
            IQueryable<Individual> query = context.Individuals;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Individual Find(int id)
        {
            return context.Individuals.Find(id);
        }

        public void InsertOrUpdate(Individual individual)
        {
            if (individual.Id == default(int)) {
                // New entity
                context.Individuals.Add(individual);
            } else {
                // Existing entity
                context.Entry(individual).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var individual = context.Individuals.Find(id);
            context.Individuals.Remove(individual);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IIndividualRepository : IDisposable
    {
        IQueryable<Individual> All { get; }
        IQueryable<Individual> AllIncluding(params Expression<Func<Individual, object>>[] includeProperties);
        Individual Find(int id);
        void InsertOrUpdate(Individual individual);
        void Delete(int id);
        void Save();
    }
}