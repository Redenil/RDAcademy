using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace RDAcademy.Models
{
    [KnownType(typeof(Individual))]
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        public string CodeContract { get; set; }

        [DataType(DataType.Date)]
        public DateTime EffectDate { get; set; }

        public int IndividualId { get; set; }

        public Individual Individual { get; set; }
    }

}