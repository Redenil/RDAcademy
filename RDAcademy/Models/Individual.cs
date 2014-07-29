﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RDAcademy.Models
{
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
    }
}