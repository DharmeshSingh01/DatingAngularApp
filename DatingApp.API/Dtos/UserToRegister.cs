using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserToRegister
    {
        [Required (ErrorMessage ="User name must be Requried")]
        public string  Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
