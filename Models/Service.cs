using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa un servicio ofrecido por la OTIC. En la nueva
    ///     estructura el servicio se asocia a una categoría, puede
    ///     registrar un acuerdo de nivel de servicio (SLA), un estado
    ///     (“ACTIVO”, “RETIRADO” o “PLANIFICADO”) y el usuario que lo creó.
    /// </summary>
    public class Service
    {
        /// <summary>
        ///     Identificador único del servicio.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Nombre legible del servicio.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Descripción detallada del servicio.
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Identificador de la categoría a la que pertenece este servicio.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///     Categoría asociada (propiedad de navegación).
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        ///     Acuerdo de nivel de servicio (por ejemplo, “1 hora”).
        /// </summary>
        public string? Sla { get; set; }

        /// <summary>
        ///     Estado del servicio (ACTIVO, RETIRADO, PLANIFICADO).
        /// </summary>
        public ServiceStatus Status { get; set; } = ServiceStatus.ACTIVO;

        /// <summary>
        ///     Identificador del usuario que creó este servicio.
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        ///     Usuario que creó el servicio (propiedad de navegación).
        /// </summary>
        public User? CreatedByUser { get; set; }

        /// <summary>
        ///     Fecha de creación del servicio.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Colección de solicitudes asociadas a este servicio.
        /// </summary>
        public List<Request> Requests { get; set; } = new();

        /// <summary>
        ///     Historial de cambios de este servicio.
        /// </summary>
        public List<ServiceHistory> Histories { get; set; } = new();
    }
}