using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Proyecto.Models
{
    public class Escenario
    {
        [Key]
        public int EscenarioId { get; set; }

        [Display(Name = "Nombre Escenario")]
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexNombre", IsUnique = true)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Range(0, 9999999999)]

        //[DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        //Relacion con Torneo
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }

        public virtual ICollection<Cancha> Canchas { get; set; }



    }
}