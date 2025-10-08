using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa una solicitud realizada para un servicio. Corresponde a la
    ///     tabla <c>solicitudes</c> del esquema de base de datos.
    /// </summary>
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Identificador del servicio al que está asociada esta solicitud.
        /// </summary>
        [Required]
        public int ServicioId { get; set; }

        /// <summary>
        ///     Servicio asociado. Propiedad de navegación para EF Core.
        /// </summary>
        public Service Servicio { get; set; } = null!;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Estado de la solicitud (ej. pendiente, en-progreso, cerrada).
        /// </summary>
        public string Estado { get; set; } = "pendiente";
    }
}