using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webIDZ.Models.ViewModels
{
    public class CatchVM
    {
        [Key]
        public int ID_Catch { get; set; }

        [Required]
        [DisplayName("Дата отлова")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public System.DateTime DateCatch { get; set; }

        [Required]
        [DisplayName("Дата отлова")]
        [StringLength(10, MinimumLength = 10)]
        public string datacatch { get { return DateCatch.ToShortDateString(); } set { DateCatch = Convert.ToDateTime(value); } }

        [Required]
        [DisplayName("Зона отлова")]
        public int Zone_ID { get; set; }

        [Required]
        [DisplayName("Стация")]
        public int Station_ID { get; set; }

        [DisplayName("Биотоп")]
        public string Biotope { get; set; }

        [Required]
        [DisplayName("Район")]
        [StringLength(50, MinimumLength = 2)]
        public string Distrtict { get; set; }


        [Required]
        [DisplayName("Место")]
        [StringLength(50, MinimumLength = 2)]
        public string Place { get; set; }

        [Required]
        [DisplayName("Координаты по X")]
        [DisplayFormat(DataFormatString ="{0:0.######}", ApplyFormatInEditMode = true)]
        public decimal Coords_X { get; set; }

        [Required]
        [DisplayName("Координаты по Y")]
        [DisplayFormat(DataFormatString = "{0:0.######}", ApplyFormatInEditMode = true)]
        public decimal Coords_Y { get; set; }

        [Required]
        [DisplayName("Кол-во ловушек")]
        public int Traps_Amount { get; set; }

        [Required]
        [DisplayName("Кол-во пойманных")]
        public int Catched_Amount { get; set; }

        [DisplayName("Комментарии")]
        public string Comments { get; set; }
    }
}