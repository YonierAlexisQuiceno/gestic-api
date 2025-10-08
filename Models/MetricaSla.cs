using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesticApi.Models
{
    /// <summary>
    ///     Métrica de cumplimiento de SLA para un servicio específico. Corresponde
    ///     a la tabla <c>metricas_sla</c> en la base de datos.
    /// </summary>
    public class MetricaSla
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServicioId { get; set; }

        /// <summary>
        ///     Servicio asociado a esta métrica.
        /// </summary>
        public Service Servicio { get; set; } = null!;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Tiempo que tomó atender la solicitud en minutos.
        /// </summary>
        public int? AtendidoEnMin { get; set; }

        /// <summary>
        ///     Indica si el tiempo de atención cumplió el SLA establecido.
        /// </summary>
        public bool? Cumplimiento { get; set; }
    }
}