using System;
using System.ComponentModel.DataAnnotations;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa una solicitud realizada por un usuario para un
    ///     determinado servicio. Incluye referencias al servicio y al
    ///     usuario, la fecha de la solicitud, el estado y detalles
    ///     adicionales que describen la necesidad del solicitante.
    /// </summary>
    public class Request
    {
        /// <summary>
        ///     Identificador único de la solicitud.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Identificador del usuario que realiza la solicitud.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Usuario que realiza la solicitud (propiedad de navegación).
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        ///     Identificador del servicio solicitado.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        ///     Servicio al que corresponde la solicitud (propiedad de
        ///     navegación).
        /// </summary>
        public Service? Service { get; set; }

        /// <summary>
        ///     Fecha en la que se registra la solicitud.
        /// </summary>
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Estado de la solicitud (pendiente, en proceso, completada o
        ///     cancelada).
        /// </summary>
        public RequestStatus Status { get; set; } = RequestStatus.PENDIENTE;

        /// <summary>
        ///     Información adicional proporcionada por el usuario sobre la
        ///     solicitud.
        /// </summary>
        public string? Details { get; set; }
    }
}