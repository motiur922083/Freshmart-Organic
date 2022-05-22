using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organic_Food_01_EXM.Areas.Admin.Models
{
    public class RoleUserVM
    {
        [Required,Display(Name ="User")]
        public string UserId { get; set; }
        [Required, Display(Name = "User")]
        public string RoleId { get; set; }
    }
}
