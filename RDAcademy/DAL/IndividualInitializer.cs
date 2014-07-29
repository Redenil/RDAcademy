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
            var students = new List<Individual>
            {
            new Individual{Id=1, FirstName="Reda",LastName="Ouafi",BirthDate= DateTime.Now.Date},
            new Individual{Id=2, FirstName="Guillaume",LastName="Trohel",BirthDate= DateTime.Now.Date}
            };

            students.ForEach(s => context.Individuals.Add(s));
            context.SaveChanges();
        }

    }
}