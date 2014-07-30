using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace RDAcademy.Models
{
    [KnownType(typeof(Contract))]
    public class Individual
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public List<Contract> Contracts { get; set; }

        public Individual()
        {
            Contracts = new List<Contract>();
        }
    }
}