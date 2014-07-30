using RDAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDAcademy.DAL
{
    public class IndividualInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<IndividualContext>
    {
        protected override void Seed(IndividualContext context)
        {
            var i1 = new Individual {Id = 1, FirstName = "Reda", LastName = "Ouafi", BirthDate = DateTime.Now.Date};
            var i2 = new Individual{Id = 2, FirstName = "Guillaume", LastName = "Trohel", BirthDate = DateTime.Now.Date};

            var students = new List<Individual>();
            students.Add(i1);
            students.Add(i2);

            var c1 = new Contract {Id = 1, CodeContract = "COReda01", EffectDate = DateTime.Now.Date};
            var c2 = new Contract {Id = 2, CodeContract = "COReda02", EffectDate = DateTime.Now.Date};
            var c3 = new Contract {Id = 3, CodeContract = "COGuillaume01", EffectDate = DateTime.Now.Date};
            var c4 = new Contract {Id = 4, CodeContract = "COGuillaume02", EffectDate = DateTime.Now.Date};

            var contracts = new List<Contract>();
            contracts.Add(c1);
            contracts.Add(c2);
            contracts.Add(c3);
            contracts.Add(c4);

            i1.Contracts.Add(c1);
            i1.Contracts.Add(c2);
            i2.Contracts.Add(c3);
            i2.Contracts.Add(c4);

            students.ForEach(s => context.Individuals.Add(s));
            contracts.ForEach(s=>context.Contracts.Add(s));
            context.SaveChanges();
        }

    }
}