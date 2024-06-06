using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webIDZ.Models.ViewModels
{
    public class MouseVM
    {
        [Key]
        public System.Guid ID_Mouse { get; set; }
        [Required]
        public int Catch_ID { get; set; }
        [Required]
        [DisplayName("Тип")]
        public int Type_ID { get; set; }
        [Required]
        [DisplayName("Беременность")]
        public int Pregnancy_ID { get; set; }
        [Required]
        [DisplayName("Пол")]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Возраст")]
        public string Age { get; set; }
        [DisplayName("Кол-во эмбрионов")]
        public int Embryos_Amount { get; set; }
    }
}