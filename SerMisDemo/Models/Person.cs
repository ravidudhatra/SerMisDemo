using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerMisDemo.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string City { get; set; }

        public int? Age { get; set; }

        public int Flag { get; set; }

        public DateTime DateUpdated { get; set; }
    }

}