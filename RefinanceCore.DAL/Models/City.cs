using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RefinanceCore.DAL.Models
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "CityName")]
        public string Name { get; set; }

        [Display(Name = "SignificanceLevel")]
        public int SignificanceLevel { get; set; }
    }
}
