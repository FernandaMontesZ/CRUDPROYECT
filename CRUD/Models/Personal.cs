
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    
    public partial class Personal
    {
        public int ID_personal { get; set; }

        [StringLength(50, ErrorMessage = "El Nombre no puede contener mas de 50 caracteres.")]
        [Required]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El Apellido Paterno no puede contener mas de 50 caracteres.")]
        [Display(Name = "Apellido Paterno")]
        [Required]
        public string ApePaterno { get; set; }

        [StringLength(50, ErrorMessage = "El Apellido Materno no puede contener mas de 50 caracteres.")]
        [Display(Name = "Apellido Materno")]
        [Required]
        public string ApeMaterno { get; set; }

        [Required]
        [Range(1,100, ErrorMessage ="Debe ser una Edad valida ")]
        public Nullable<int> Edad { get; set; }

        [Required]
        public Nullable<bool> IsActive { get; set; }
    }
}
