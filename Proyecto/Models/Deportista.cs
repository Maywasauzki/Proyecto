using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Proyecto.Models
{
    public class Deportista
    {
        [Key]
        public int DeportistaId { get; set; }

        [Display(Name = "Numero Documento")]
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexIdentificacion", IsUnique = true)]
        public string Documento { get; set; }


        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string RH { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string EPS { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Genero { get; set; }


        //Relacion con equipo
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int equipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        //Relacion con equipo
        //**public virtual ICollection<Equipo> Equipos { get; set; }




    }
}