//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webIDZ.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProtocolsView
    {
        public System.DateTime Дата_исследования { get; set; }
        public string Материал_исследования { get; set; }
        public string Описание_материала { get; set; }
        public int Кол_во_образцов { get; set; }
        public string Метод_исследования { get; set; }
        public string Маркер_заболевания { get; set; }
        public int Результат { get; set; }
        public string Описание { get; set; }
    }
}