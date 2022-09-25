using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Proyecto.Models
{
    public class Cancha
    {
        [Key]
        public int CanchaId { get; set; }

        [Display(Name = "Nombre Cancha")]
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexNombre", IsUnique = true)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Disciplina { get; set; }

        [Display(Name = "Cantidad Espectadores")]
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(7, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string CantidadEspec { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        
        public string Medidas { get; set; }

        //Relacion con Escenario
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int EscenarioId { get; set; }
        public virtual Escenario Escenario { get; set; }



    }
}