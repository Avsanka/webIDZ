using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webIDZ.Models.ViewModels
{
    public class UserVM
    {
        [Required]
        [DisplayName("Логин")]
        [Key]
        public string Login { get; set; }
        
        [Required]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}

