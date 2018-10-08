using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RefinanceCore.DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }
    }
}
